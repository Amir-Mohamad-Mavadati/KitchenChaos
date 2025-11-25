using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    private const string OPEN_CLOSE = "OpenClose";
    private Animator Animator;
    [SerializeField] private ContainerCounter ContainerCounter;

    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    private void Start()
    {
        ContainerCounter.OnPlayerGrabbedObject += ContainerCounter_OnPlayerGrabbedObject;
    }

    private void ContainerCounter_OnPlayerGrabbedObject(object sender, System.EventArgs e)
    {
        Animator.SetTrigger(OPEN_CLOSE);
    }
}
