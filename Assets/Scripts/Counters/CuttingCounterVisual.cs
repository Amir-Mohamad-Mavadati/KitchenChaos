using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    private const string CUT = "Cut";
    private Animator Animator;
    [SerializeField] private CuttingCounter CuttingCounter;

    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    private void Start()
    {
        CuttingCounter.OnPlayerCutting += CuttingCounter_OnPlayerCutting;
    }

    private void CuttingCounter_OnPlayerCutting(object sender, System.EventArgs e)
    {
        Animator.SetTrigger(CUT);
    }
}
