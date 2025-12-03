using UnityEngine;
using System;

public class PlatesCounter : BaseCounter
{
    public event EventHandler OnPlateSpawn;
    public event EventHandler OnPlateRemoved;
    [SerializeField] private KitchenObjectsSO PlateObjectSO;
    private float SpawnPlateTimer;
    private float SpawnPlateTimerMax = 4f;
    private int PlateSpawnAmount;
    private int PlateSpawnAmountMax = 4;
    private void Update()
    {
        SpawnPlateTimer += Time.deltaTime;
        if (SpawnPlateTimer >= SpawnPlateTimerMax)
        {
            SpawnPlateTimer = 0;
            if (PlateSpawnAmount < PlateSpawnAmountMax)
            {
                PlateSpawnAmount++;
                OnPlateSpawn?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player Player)
    {
        if (!Player.HasKitchenObject())
        {
            if (PlateSpawnAmount > 0)
            {
                // There is at least one plate
                KitchenObject.SpawnKitchenObject(PlateObjectSO, Player);
                PlateSpawnAmount--;
                OnPlateRemoved?.Invoke(this, EventArgs.Empty);

            }
        }
    }
}
