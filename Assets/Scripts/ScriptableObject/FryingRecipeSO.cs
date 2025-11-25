using UnityEngine;

[CreateAssetMenu()]
public class FryingRecipeSO : ScriptableObject
{
    public KitchenObjectsSO Input;
    public KitchenObjectsSO Output;
    public float FryingTimerMax;
}
