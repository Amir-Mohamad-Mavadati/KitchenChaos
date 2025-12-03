using System.Collections.Generic;
using UnityEngine;
using System;

public class PlateKichenObject : KitchenObject
{
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObjectsSO KitchenObjectsSO;
    }
    private List<KitchenObjectsSO> KitchenObjectsSOList;
    [SerializeField] private List<KitchenObjectsSO> ValidKitchenObjectSO;

    private void Awake()
    {
        KitchenObjectsSOList = new List<KitchenObjectsSO>();
    }

    public bool TryAddIngredient(KitchenObjectsSO kitchenObjectsSO)
    {
        if(!ValidKitchenObjectSO.Contains(kitchenObjectsSO))
        {
            // Not a valid ingredient
            return false;
        }
        if (KitchenObjectsSOList.Contains(kitchenObjectsSO))
        {
            // Already has this type
            return false;
        }
        else
        {
            KitchenObjectsSOList.Add(kitchenObjectsSO);
            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs
            {
                KitchenObjectsSO = kitchenObjectsSO
            });
            return true;
        }
    }

    public List<KitchenObjectsSO> GetKitchenObjectsSOList()
    {
        return KitchenObjectsSOList;
    }
}
