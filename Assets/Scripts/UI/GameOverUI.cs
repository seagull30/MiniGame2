using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    private GameObject[] _childs;
    private int _childCount;

    TextMeshProUGUI changeText;


    private void Awake()
    {
        _childCount = transform.childCount;
        _childs = new GameObject[_childCount];

        for (int i = 0; i < _childCount; ++i)
        {
            _childs[i] = transform.GetChild(i).gameObject;
            _childs[i].SetActive(false);
        }
    }

    private void OnEnable()
    {
        GameManager.Instance.OnGameEnd += Activate;
    }

    public void Activate(int score, int stageScore)
    {
        for (int i = 0; i < _childCount; ++i)
        {
            _childs[i].SetActive(true);
            if (_childs[i].name == "Final Score")
            {
                changeText = _childs[i].GetComponent<TextMeshProUGUI>();
                changeText.text = score + stageScore + "Á¡";
            }
            if (_childs[i].name == "Score")
            {
                changeText = _childs[i].GetComponent<TextMeshProUGUI>();
                changeText.text = score + "Á¡";
            }
            if (_childs[i].name == "Bonus")
            {
                changeText = _childs[i].GetComponent<TextMeshProUGUI>();
                changeText.text = stageScore + "Á¡";
            }
        }
    }
    private void OnDisable()
    {
        GameManager.Instance.OnGameEnd -= Activate;
    }
}
