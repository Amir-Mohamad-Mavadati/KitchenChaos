using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
   [SerializeField] private GameObject StoveVisual;
   [SerializeField] private GameObject ParticalEffect;
   [SerializeField] private StoveCounter StoveCounter;

    private void Start()
    {
        StoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
    }

    private void StoveCounter_OnStateChanged(object Sender, StoveCounter.OnStateChangedEventArgs e)
    {
        bool ShowObject = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
        StoveVisual.SetActive(ShowObject);
        ParticalEffect.SetActive(ShowObject);
    }
}
