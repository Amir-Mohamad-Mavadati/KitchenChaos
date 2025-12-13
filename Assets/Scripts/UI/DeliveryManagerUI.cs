using TMPro;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform Container;
    [SerializeField] private Transform RecipeTemplate;

    private void Awake()
    {
        RecipeTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSpawned += DeliveryManager_OnRecipeSpawned;
        DeliveryManager.Instance.OnRecipeCompleted += DeliveryManager_OnRecipeCompleted;
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
    }

    private void GameManager_OnStateChanged(object Sender, System.EventArgs e)
    {
        if ( GameManager.Instance.IsGameOver())
        {
            gameObject.SetActive(false);
        }
    }

    private void DeliveryManager_OnRecipeSpawned(object Sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void DeliveryManager_OnRecipeCompleted(object Sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform Child in Container)
        {
            if (Child == RecipeTemplate)
            {
                continue;
            }

            Destroy(Child.gameObject);
        }

        foreach (RecipeSO CustomerOrder in DeliveryManager.Instance.GetCustomerOrderList())
        {
            Transform RecipeTemplateAdded = Instantiate(RecipeTemplate, Container);
            RecipeTemplateAdded.gameObject.SetActive(true);
            RecipeTemplateAdded.GetComponent<DeliveryManagerSingleUI>().SetRecipeSO(CustomerOrder);
        }
    }
}
