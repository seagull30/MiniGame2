using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : SingletonBehaviour<GameManager>
{
    public GameObject Player;
    public bool IsGameOver = false;

    private int _score;
    public int Score 
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value; 
        }
    }

    private void Awake()
    {

    }

    public void Start()
    {

    }

    public void GameOver()
    {

    }
}
