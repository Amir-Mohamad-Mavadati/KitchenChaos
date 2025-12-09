using UnityEngine;

[CreateAssetMenu()]
public class SoundEffectSO : ScriptableObject
{
    public AudioClip[] Chop;
    public AudioClip[] DeliveryFail;
    public AudioClip[] DeliverySuccess;
    public AudioClip FootSteps;
    public AudioClip[] ObjectDrop;
    public AudioClip[] ObjectPickup;
    public AudioClip[] Trash;
    public AudioClip[] Warning;
}
