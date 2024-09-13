using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class DraggedObject : MonoBehaviour, IDragDropped
{
    [SerializeField] private float _lerpSpeed;
    [SerializeField] private string _playerLayerMask;

    [Space(10)] [SerializeField] private LayerMask _checkPlayerMask;
    [SerializeField] private float _checkRadius;
    [SerializeField] private float _checkRate;

    private Rigidbody _rigidbody;

    private CompositeDisposable _disposable = new CompositeDisposable();

    public bool Pickuped { get; set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Pickup(Transform pointToLerp)
    {
        StopAllCoroutines();
        _disposable.Clear();

        Pickuped = true;

        _rigidbody.useGravity = false;

        gameObject.layer = LayerMask.NameToLayer(_playerLayerMask);

        Observable.EveryUpdate().Subscribe(_ =>
        {
            transform.position =
                Vector3.Lerp(transform.position, pointToLerp.position, _lerpSpeed * Time.deltaTime);
        }).AddTo(_disposable);
    }


    public void Drop()
    {
        Pickuped = false;
        _rigidbody.useGravity = true;

        StartCoroutine(CheckingForPlayerToChangeLayer());
        _disposable.Clear();
    }

    public void Throw(Vector3 forceVector)
    {
        Drop();
        _rigidbody.AddForce(forceVector, ForceMode.Impulse);
    }

    private IEnumerator CheckingForPlayerToChangeLayer()
    {
        while (true)
        {
            yield return new WaitForSeconds(_checkRate);
            Collider[] player = new Collider[1];

            int detectedPlayers =
                Physics.OverlapSphereNonAlloc(transform.position, _checkRadius, player, _checkPlayerMask);

            Debug.Log(detectedPlayers);

            if (detectedPlayers > 0)
            {
                gameObject.layer = LayerMask.NameToLayer("Default");
                yield break;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _checkRadius);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        _disposable.Clear();
    }
}