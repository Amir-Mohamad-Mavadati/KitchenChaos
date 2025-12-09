using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public static DeliveryCounter Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    public override void Interact(Player Player)
    {
        if (Player.HasKitchenObject() && Player.GetKitchenObject().TryGetPlate(out PlateKichenObject plateKichenObject))
        {
            // Player has a plate
            DeliveryManager.Instance.DeliveryRecipe(plateKichenObject);
            Player.GetKitchenObject().DestroySelf();
        }
    }
}
