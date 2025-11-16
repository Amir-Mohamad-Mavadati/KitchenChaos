using System;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectsSO KitchenObjectsSO;
   
    public event EventHandler OnPlayerGrabbedObject;
    public override void Interact(Player Player)
    {
        if (!Player.HasKitchenObject())
        {
            // player is empty handed
            KitchenObject.SpawnKitchenObject(KitchenObjectsSO, Player);
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }
}
