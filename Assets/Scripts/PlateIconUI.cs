using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlateIconUI : MonoBehaviour
{
    [SerializeField] private PlateKichenObject plateKichenObject;
    [SerializeField] private Transform IconTemplate;

    private void Awake()
    {
        IconTemplate.gameObject.SetActive(false);
    }
    private void Start()
    {
        plateKichenObject.OnIngredientAdded += PlateKichenObject_OnIngredientAdded;
    }

    private void PlateKichenObject_OnIngredientAdded (object Sender, PlateKichenObject.OnIngredientAddedEventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform Child in transform)
        {
            if (Child == IconTemplate)
            {
                continue;
            }

            Destroy(Child.gameObject);
        }

        foreach (KitchenObjectsSO kitchenObjectsSO in plateKichenObject.GetKitchenObjectsSOList())
        {
            Transform IconTransform = Instantiate(IconTemplate, transform);
            IconTransform.gameObject.SetActive(true);
            IconTransform.GetComponent<PlateIconSingleUI>().SetKitchenObjectIcon(kitchenObjectsSO);
            
        }
    }
}
