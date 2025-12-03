using Unity.VisualScripting;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    public override void Interact(Player Player)
    {
        if (!HasKitchenObject())
        {
            // There is no kichen object here
            if (Player.HasKitchenObject())
            {
                // Player has somthing
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
                if (Player.GetKitchenObject().TryGetPlate(out PlateKichenObject plateKichenObject))
                {
                    // Player is holdig plate
                    if(plateKichenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
                else
                {
                    // Player is not carrying plate but somthing else
                    if(GetKitchenObject().TryGetPlate(out plateKichenObject))
                    {
                        // There is a plate on counter
                        if (plateKichenObject.TryAddIngredient(Player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            Player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
            else
            {
                // Player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(Player);
            }
        }

    }
    
}
