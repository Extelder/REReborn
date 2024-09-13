using UnityEngine;

public interface IDragDropped
{
    public bool Pickuped { get; set; }

    public void Pickup(Transform pointToLerp);
    public void Drop();
    public void Throw(Vector3 forceVector);
}