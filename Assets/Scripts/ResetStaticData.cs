using UnityEngine;

public class ResetStaticData : MonoBehaviour
{
    private void Awake()
    {
        CuttingCounter.ResetStaticData();
        TrashCounter.ResetStaticData();
        BaseCounter.ResetStaticData();
    }
}
