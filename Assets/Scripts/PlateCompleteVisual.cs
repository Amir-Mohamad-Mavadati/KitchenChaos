using UnityEngine;
using System;
using System.Collections.Generic;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenObjectSO_GameObjects
    {
        public KitchenObjectsSO KitchenObjectsSO;
        public GameObject GameObject;
    }

    [SerializeField] private PlateKichenObject plateKichenObject;
    [SerializeField] private List<KitchenObjectSO_GameObjects> KitchenObjectSOGameObjectsList;

    private void Start()
    {
        plateKichenObject.OnIngredientAdded += PlateKichenObject_OnIngredientAdded;
        foreach (KitchenObjectSO_GameObjects KitchenObjectSOGameObjects in KitchenObjectSOGameObjectsList)
        {
            KitchenObjectSOGameObjects.GameObject.SetActive(false);
        }
    }

    private void PlateKichenObject_OnIngredientAdded (object Sender, PlateKichenObject.OnIngredientAddedEventArgs e)
    {
        foreach (KitchenObjectSO_GameObjects KitchenObjectSOGameObjects in KitchenObjectSOGameObjectsList)
        {
            if (KitchenObjectSOGameObjects.KitchenObjectsSO == e.KitchenObjectsSO)
            {
                KitchenObjectSOGameObjects.GameObject.SetActive(true);
            }
        }
    }
}
