using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource MusicSource;
    private float MusicVolume = 1f;
    private const string PLAYER_PREFS_MUSIC_VOLUME = "MusicVolume";

    private void Awake()
    {
        MusicSource = GetComponent<AudioSource>();
        MusicVolume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME, .3f);
        MusicSource.volume = MusicVolume;
    }
    public void ChangeMusicVolume()
    {
        MusicVolume += .1f;
        if (MusicVolume > 1f)
        {
            MusicVolume = 0;
        }
        MusicSource.volume = MusicVolume;
        PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME, MusicVolume);
        PlayerPrefs.Save();
    }
    public float GetMusicVolume()
    {
        return MusicVolume;
    }
}
