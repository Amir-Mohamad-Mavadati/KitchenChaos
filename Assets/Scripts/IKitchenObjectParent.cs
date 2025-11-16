using UnityEngine;

public interface IKitchenObjectParent
{
    public Transform Getkitchenobjectfollowtransform();
    public void ClearKitchenObject();
    public KitchenObject GetKitchenObject();
    public void SetKitchenObject(KitchenObject KitchenObject);
    public bool HasKitchenObject();
}
