using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
    }


    private void StoveCounter_OnStateChanged(object Sender, StoveCounter.OnStateChangedEventArgs e)
    {
        bool PLaySound = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;

        if (PLaySound)
        {
            audioSource.Play();
        }

        else
        {
            audioSource.Pause();
        }
    }

}
