using UnityEngine;

[CreateAssetMenu()]
public class CuttingCounterSo : ScriptableObject
{
    public KitchenObjectsSO Input;
    public KitchenObjectsSO Output;
    public int MaxCuttingProgress;
}
