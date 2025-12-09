using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private Player player;
    private float SoundTimer;
    private float SoundTimerMax = .1f;

    private void Start()
    {
        player = GetComponent<Player>();
    }
    void Update()
    {
        SoundTimer -= Time.deltaTime;
        if (SoundTimer < 0)
        {
            SoundTimer = SoundTimerMax;
            if (player.IsWalking)
            {
                SoundManager.Instance.PlayFootStep(player.transform.position);
            }
        }
    }
}
