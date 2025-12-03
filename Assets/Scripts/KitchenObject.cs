using UnityEngine;
using UnityEngine.Animations;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectsSO KitchenObjectsSO;
    private IKitchenObjectParent KitchenObjectParent;
    public KitchenObjectsSO GetKitchenObjectSO()
    {
        return KitchenObjectsSO;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent KitchenObjectParent)
    {
        {
            if (this.KitchenObjectParent != null)
            {
                this.KitchenObjectParent.ClearKitchenObject();
            }

            this.KitchenObjectParent = KitchenObjectParent;

            if (KitchenObjectParent.HasKitchenObject())
            {
                Debug.LogError("KitchenObjectParent already has a kitchen object!");
            }

            KitchenObjectParent.SetKitchenObject(this);

            transform.parent = KitchenObjectParent.Getkitchenobjectfollowtransform();

            transform.localPosition = Vector3.zero;
        }

    }

    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return KitchenObjectParent;
    }

    public void DestroySelf()
    {
        KitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }

    public static KitchenObject SpawnKitchenObject(KitchenObjectsSO KitchenObjectsSO, IKitchenObjectParent KitchenObjectParent)
    {
        Transform KitchenObjectTransform = Instantiate(KitchenObjectsSO.Prefab);
        KitchenObject KitchenObject = KitchenObjectTransform.GetComponent<KitchenObject>();
        KitchenObject.SetKitchenObjectParent(KitchenObjectParent);
        return KitchenObject;
    }

    public bool TryGetPlate(out PlateKichenObject plateKichenObject)
    {
        if (this is PlateKichenObject)
        {
            plateKichenObject = this as PlateKichenObject;
            return true;
        }
        else
        {
            plateKichenObject = null;
            return false;
        }
    }
}