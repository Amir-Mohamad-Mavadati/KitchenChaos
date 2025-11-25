using UnityEngine;

public class TrashCounter : BaseCounter
{
    public override void Interact(Player Player)
    {
        // Interaction logic for TrashCounter
        if (Player.HasKitchenObject())
        {
            // If the player is carrying something, destroy it
            Player.GetKitchenObject().DestroySelf();
        }
    }
}
