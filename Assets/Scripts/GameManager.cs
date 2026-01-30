using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event EventHandler OnStateChanged;
    
    public event EventHandler OnPausedGame;
    public event EventHandler OnUnPausedGame;
    public static GameManager Instance {get; private set;}
    private enum State
    {
        WaitingToStart,
        CountDownToStart,
        GamePlaying,
        GameOver,
    }

    private State GameState;
    private float WaitingTimer = 1f;
    private float CountDownTimer = 3f;
    private float GamePlayingTimer;
    private float GamePlayingTimerMax = 30f;
    private bool IsGamePaused = false;

    private void Start()
    {
        PlayerInput.Instance.OnTogglePausedGame += PlayerInput_OnTogglePausedGame;
    }

    private void Awake()
    {
        Instance = this;
        GameState = State.WaitingToStart;
    }

    private void Update()
    {
        switch (GameState)
        {
            case State.WaitingToStart:
                WaitingTimer -= Time.deltaTime;
                if(WaitingTimer < 0f)
                {
                   GameState =  State.CountDownToStart;
                   OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            
            case State.CountDownToStart:
                CountDownTimer -= Time.deltaTime;
                GamePlayingTimer = GamePlayingTimerMax;
                if (CountDownTimer < 0f)
                {
                    GameState = State.GamePlaying;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            
            case State.GamePlaying:
                GamePlayingTimer -= Time.deltaTime;
                if (GamePlayingTimer < 0f)
                {
                    GameState = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            
            case State.GameOver:
                OnStateChanged?.Invoke(this, EventArgs.Empty);
                break;
        }
    }

    public void PlayerInput_OnTogglePausedGame(object Sender, System.EventArgs e)
    {
        IsGamePaused = !IsGamePaused;
        if (IsGamePaused)
        {
            Time.timeScale = 0f;
            OnPausedGame?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;
            OnUnPausedGame?.Invoke(this, EventArgs.Empty);
        }
    }

    public bool IsGamePlaying()
    {
        return GameState == State.GamePlaying;
    }

    public bool IsCountDownActive()
    {
        return GameState == State.CountDownToStart;
    }

    public float GetCountDownTimer()
    {
        return CountDownTimer;
    }

    public bool IsGameOver()
    {
        return GameState == State.GameOver;
    }

    public float GetGamePlayTimer()
    {
        return 1 - (GamePlayingTimer / GamePlayingTimerMax);
    }

}
