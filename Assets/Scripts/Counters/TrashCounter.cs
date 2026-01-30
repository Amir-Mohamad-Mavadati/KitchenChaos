using System;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public static EventHandler OnItemTrashed;

    new static public void ResetStaticData()
    {
        OnItemTrashed = null;
    }
    public override void Interact(Player Player)
    {
        // Interaction logic for TrashCounter
        if (Player.HasKitchenObject())
        {
            // If the player is carrying something, destroy it
            Player.GetKitchenObject().DestroySelf();
            OnItemTrashed?.Invoke(this, EventArgs.Empty);
        }
    }
}
