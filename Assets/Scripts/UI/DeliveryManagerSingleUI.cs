using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class DeliveryManagerSingleUI : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI RecipeName;
   [SerializeField] private Transform IconContainer;
   [SerializeField] private Transform IngredientIcon;

    private void Awake()
    {
        IngredientIcon.gameObject.SetActive(false);
    }
    public void SetRecipeSO(RecipeSO recipeSO)
    {
        RecipeName.text = recipeSO.RecipeName;

        foreach (Transform Child in IconContainer)
        {
            if (Child == IngredientIcon)
            {
                continue;
            }

            Destroy(Child);
        }

        foreach (KitchenObjectsSO kitchenObjectsSO in recipeSO.KitchenObjectsSOsList)
        {
            Transform TransformIngredientIcon = Instantiate(IngredientIcon, IconContainer);
            TransformIngredientIcon.gameObject.SetActive(true);
            TransformIngredientIcon.GetComponent<Image>().sprite = kitchenObjectsSO.Icone;
        }
    } 


    
}
