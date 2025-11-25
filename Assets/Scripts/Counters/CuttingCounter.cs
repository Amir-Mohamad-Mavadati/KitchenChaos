using UnityEngine;
using System;

public class CuttingCounter : BaseCounter, IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler OnPlayerCutting;
    [SerializeField] private CuttingCounterSo[] CuttingRecipeSOArray;
    private int CuttingProgress;
    public override void Interact(Player Player)
    {
        if (!HasKitchenObject())
        {
            if (Player.HasKitchenObject() && HasRecipeWithInput(Player.GetKitchenObject().GetKitchenObjectSO()))
            {
                Player.GetKitchenObject().SetKitchenObjectParent(this);
                CuttingProgress = 0;
                CuttingCounterSo CuttingResipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    ProgressNormalized = (float)CuttingProgress / CuttingResipeSO.MaxCuttingProgress
                });
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
            OnPlayerCutting?.Invoke(this, EventArgs.Empty);
            // Cutting logic would go here
            CuttingProgress++;

            CuttingCounterSo CuttingResipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
                ProgressNormalized = (float)CuttingProgress / CuttingResipeSO.MaxCuttingProgress
            });

            if (CuttingProgress >= CuttingResipeSO.MaxCuttingProgress)
            {
                KitchenObjectsSO OutputKitchenObjectSO = GetOutputForKitchenObject(GetKitchenObject().GetKitchenObjectSO());

                GetKitchenObject().DestroySelf();
                
                KitchenObject.SpawnKitchenObject(OutputKitchenObjectSO, this);
            }
        }

    }
    
    private KitchenObjectsSO GetOutputForKitchenObject(KitchenObjectsSO InputKitchenObjectSO)
    {
        CuttingCounterSo CuttingResipeSO = GetCuttingRecipeSOWithInput(InputKitchenObjectSO);
        if (CuttingResipeSO != null)
        {
            return CuttingResipeSO.Output;
        }
        return null;
    }

    private bool HasRecipeWithInput(KitchenObjectsSO InputKitchenObjectSO)
    {
        CuttingCounterSo CuttingResipeSO = GetCuttingRecipeSOWithInput(InputKitchenObjectSO);
        return CuttingResipeSO != null;
    }

    private CuttingCounterSo GetCuttingRecipeSOWithInput(KitchenObjectsSO InputKitchenObjectSO)
    {
        foreach (CuttingCounterSo CuttingResipeSO in CuttingRecipeSOArray)
        {
            if (CuttingResipeSO.Input == InputKitchenObjectSO)
            {
                return CuttingResipeSO;
            }
        }
        return null;
    }
   
}
