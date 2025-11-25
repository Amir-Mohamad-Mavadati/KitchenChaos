using UnityEngine;

[CreateAssetMenu()]
public class BurnedRecipeSO : ScriptableObject
{
    public KitchenObjectsSO Input;
    public KitchenObjectsSO Output;
    public float BurnedTimerMax;
}
