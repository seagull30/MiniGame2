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

    public UnityAction<int, int> OnGameEnd;

    public UnityAction<float> playerOnCiling;

    public int stageScore = 10000;

    private int _currentScore;

    public bool _ispause = false;

    private readonly string _GameOverSound = "GameOver";
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

    public void addScore(int points)
    {
        if (!_ispause)
            Score += points;
    }

    public void PlayerOnCiling(float distence)
    {
        playerOnCiling.Invoke(distence);
    }

    public void reset()
    {
        SceneManager.LoadScene("Main");
        Score = 0;
        stageScore = 10000;
    }

    public void goTitle()
    {
        Score = 0;
        stageScore = 10000;

        SceneManager.LoadScene("TitleScene");
    }
    public void GameOver()
    {
        SoundManager.instance.StopAllSE();
        SoundManager.instance.PlaySE(_GameOverSound);
        OnGameEnd.Invoke(Score, stageScore);
    }
}
