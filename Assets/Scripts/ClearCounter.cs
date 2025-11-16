using Unity.VisualScripting;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    public override void Interact(Player Player)
    {
        if (!HasKitchenObject())
        {
            if (Player.HasKitchenObject())
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
    
}
