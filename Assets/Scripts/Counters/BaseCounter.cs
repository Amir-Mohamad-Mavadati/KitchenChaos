using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private Transform SpawnPoint;
    private KitchenObject KitchenObject;

    public virtual void Interact(Player Player)
    {
        Debug.LogError("BaseCounter.Interact() was called. This should never happen!");
    }
    
    public virtual void InteractAlternate(Player Player)
    {
        Debug.LogError("BaseCounter.InteractAlternate() was called. This should never happen!");
    }
    
    public Transform Getkitchenobjectfollowtransform()
    {
        return SpawnPoint;
    }

    public void ClearKitchenObject()
    {
        KitchenObject = null;
    }

    public KitchenObject GetKitchenObject()
    {
        return KitchenObject;
    }

    public void SetKitchenObject(KitchenObject KitchenObject)
    {
        this.KitchenObject = KitchenObject;
    }

    public bool HasKitchenObject()
    {
        return KitchenObject != null;
    }
}
