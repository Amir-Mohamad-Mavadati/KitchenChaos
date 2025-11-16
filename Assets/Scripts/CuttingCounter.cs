using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingCounterSo[] CuttingRecipeSOArray;
    public override void Interact(Player Player)
    {
        if (!HasKitchenObject())
        {
            if (Player.HasKitchenObject() && HasRecipeWithInput(Player.GetKitchenObject().GetKitchenObjectSO()))
            {
                Player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                // Player has nothing
            }
        }
        else
        {
            if (Player.HasKitchenObject())
            {
                // Both have kitchen object
            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(Player);
            }
        }
    }

    public override void InteractAlternate(Player Player)
    {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            // Cutting logic would go here
            KitchenObjectsSO OutputKitchenObjectSO = GetOutputForKitchenObject(GetKitchenObject().GetKitchenObjectSO());
            GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObject(OutputKitchenObjectSO, this);
            
        }

    }
    
    private KitchenObjectsSO GetOutputForKitchenObject(KitchenObjectsSO InputKitchenObjectSO)
    {
        foreach (CuttingCounterSo CuttingResipeSO in CuttingRecipeSOArray)
        {
            if (CuttingResipeSO.Input == InputKitchenObjectSO)
            {
                return CuttingResipeSO.Output;
            }
        }
        return null;
    }

    private bool HasRecipeWithInput(KitchenObjectsSO InputKitchenObjectSO)
    {
        foreach (CuttingCounterSo CuttingResipeSO in CuttingRecipeSOArray)
        {
            if (CuttingResipeSO.Input == InputKitchenObjectSO)
            {
                return true;
            }
        }
        return false;
    }
   
}
