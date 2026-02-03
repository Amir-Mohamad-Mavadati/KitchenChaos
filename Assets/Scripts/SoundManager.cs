using System.Linq;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set;}
    [SerializeField] private SoundEffectSO SoundRefSO;
    private const string PLAYER_PREF_SOUND_EFFECT_VOLUME = "SoundEffect";

    private float SoundEffectVolume = 1f;

    private void Awake()
    {
        Instance = this;
        SoundEffectVolume = PlayerPrefs.GetFloat(PLAYER_PREF_SOUND_EFFECT_VOLUME, .3f);
    }
    void Start()
    {
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;
        Player.Instance.OnPlayerPickup += Player_OnPlayerPickup;
        TrashCounter.OnItemTrashed += TrashCounter_OnItemTrashed;
    }

    private void TrashCounter_OnItemTrashed(object Sender, System.EventArgs e)
    {
        TrashCounter trashCounter = Sender as TrashCounter;
        Vector3 TrashCounterPosition = trashCounter.transform.position;
        PlaySound(SoundRefSO.Trash, TrashCounterPosition);
    }

    private void BaseCounter_OnAnyObjectPlacedHere(object Sender, System.EventArgs e)
    {
        BaseCounter baseCounter = Sender as BaseCounter;
        Vector3 BaseCounterPosition = baseCounter.transform.position;
        PlaySound(SoundRefSO.ObjectDrop, BaseCounterPosition);
    }

    private void Player_OnPlayerPickup(object Sender, System.EventArgs e)
    {
        Vector3 PlayerPosition = Player.Instance.transform.position;
        PlaySound(SoundRefSO.ObjectPickup, PlayerPosition);
    }

    private void CuttingCounter_OnAnyCut(object Sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = Sender as CuttingCounter;
        Vector3 CuttingCounterPosition = cuttingCounter.transform.position;
        PlaySound(SoundRefSO.Chop, CuttingCounterPosition);
    }

    private void DeliveryManager_OnRecipeFailed(object Sender, System.EventArgs e)
    {
        Vector3 DeliveryCounterPosition = DeliveryCounter.Instance.transform.position;
        PlaySound(SoundRefSO.DeliveryFail, DeliveryCounterPosition);
    }
    private void DeliveryManager_OnRecipeSuccess(object Sender, System.EventArgs e)
    {
        Vector3 DeliveryCounterPosition = DeliveryCounter.Instance.transform.position;
        PlaySound(SoundRefSO.DeliverySuccess, DeliveryCounterPosition);
    }

    private void PlaySound(AudioClip[] AudioArray, Vector3 Position, float Volume = 1f)
    {
        AudioSource.PlayClipAtPoint(AudioArray[Random.Range(0, AudioArray.Length)], Position, Volume * SoundEffectVolume);
    }
    private void PlaySound(AudioClip Audio, Vector3 Position, float Volume = 1f)
    {
        AudioSource.PlayClipAtPoint(Audio, Position, Volume);
    }

    public void PlayFootStep(Vector3 Position, float Volume = 1f)
    {
        PlaySound(SoundRefSO.FootSteps, Position, Volume * SoundEffectVolume);
    }

    public void ChangeSoundEffectsVolume()
    {
        SoundEffectVolume += .1f;
        if (SoundEffectVolume > 1f)
        {
            SoundEffectVolume = 0;
        }
        PlayerPrefs.SetFloat(PLAYER_PREF_SOUND_EFFECT_VOLUME, SoundEffectVolume);
        PlayerPrefs.Save();
    }

    public float GetSoundEffectVolume()
    {
        return SoundEffectVolume;
    }
    
}
