using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : SingletonBehaviour<GameManager>
{
    public PlayerControl player;
    public bool IsGameOver = false;

    public UnityAction<int> OnScoreChange;

    public UnityAction<float, int, int> OnGameEnd;

    public UnityAction<float> playerOnCiling;

    public int stageIndex = 1;

    private int _currentScore;
    public int Score
    {
        get
        {
            return _currentScore;
        }
        set
        {
            _currentScore = value;
            OnScoreChange.Invoke(_currentScore);
        }
    }
    public float StartTime;
    public float PlayTime;


    public void Start()
    {
        StartTime = Time.time;
        //player.playerOnCiling += PlayerOnCiling;
    }
    public void addScore(int points)
    {
        Score += points;
    }

    public void PlayerOnCiling(float distence)
    {
        playerOnCiling.Invoke(distence);
    }

    public void reset()
    {
        SceneManager.LoadScene("Main");
        //player.playerOnCiling += playerOnCiling;
        StartTime = Time.time;
        Score = 0;
    }

    public void goTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }
    public void GameOver()
    {
        PlayTime = Time.time - StartTime;
        OnGameEnd.Invoke(PlayTime, Score, stageIndex);
    }
}
