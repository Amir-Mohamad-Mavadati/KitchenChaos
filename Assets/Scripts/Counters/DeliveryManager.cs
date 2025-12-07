using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public static DeliveryManager Instance {get; private set;}
   [SerializeField] private RecipeSOList AvailableFood;
   private List<RecipeSO> CustomerOrderList;
   private float SpawnRecipeTimer;
   private float SpawnRecipeTimerMax = 4f;
   private int SpawnRecipesMax = 4;

    private void Awake()
    {
        Instance = this;
        CustomerOrderList = new List<RecipeSO>();
    }
    private void Update()
    {
        if (CustomerOrderList.Count <= SpawnRecipesMax)
        {
            SpawnRecipeTimer += Time.deltaTime;

            if (SpawnRecipeTimer >= SpawnRecipeTimerMax)
            {
                SpawnRecipeTimer = 0f;
                RecipeSO CustomerOrder = AvailableFood.WithingRecipeSOList[UnityEngine.Random.Range(0, AvailableFood.WithingRecipeSOList.Count)];
                Debug.Log(CustomerOrder.RecipeName);
                CustomerOrderList.Add(CustomerOrder);
                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }
    
    public void DeliveryRecipe(PlateKichenObject plateKichenObject)
    {
        for (int i = 0; i < CustomerOrderList.Count; i++)
        {
            RecipeSO CustomerOrder = CustomerOrderList[i];

            if (CustomerOrder.KitchenObjectsSOsList.Count == plateKichenObject.KitchenObjectsSOList.Count)
            {
                bool PlateContentMatchsRecipes = true;
                foreach (KitchenObjectsSO OrderIngredient in CustomerOrder.KitchenObjectsSOsList)
                {
                    bool IngredientFound = false;
                    foreach (KitchenObjectsSO PlateIngredient in plateKichenObject.KitchenObjectsSOList)
                    {
                        if (OrderIngredient == PlateIngredient)
                        {
                            IngredientFound = true;
                            break;
                        }
                    }

                    if (!IngredientFound)
                    {
                        // This recipe was not found on the plate
                        PlateContentMatchsRecipes = false;
                    }

                }

                if (PlateContentMatchsRecipes)
                {
                    // Player deliver the correct recipe
                    Debug.Log("Player deliver the correct recipe");
                    CustomerOrderList.RemoveAt(i);
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }
        // Player deliver the incorrect recipe
        Debug.Log("Player deliver the incorrect recipe");
    }

    public List<RecipeSO> GetCustomerOrderList()
    {
        return CustomerOrderList;
    }
}
