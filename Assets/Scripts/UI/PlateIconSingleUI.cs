using UnityEngine;
using UnityEngine.UI;

public class PlateIconSingleUI : MonoBehaviour
{
    [SerializeField] private Image Image;

    public void SetKitchenObjectIcon(KitchenObjectsSO kitchenObjectsSO)
    {
        Image.sprite = kitchenObjectsSO.Icone;
    }
}
