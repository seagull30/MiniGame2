using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SocreText : MonoBehaviour
{
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        GameManager.Instance.OnScoreChange += UpdateText;
    }

    public void UpdateText(int score)
    {
        _text.text = $"현재스코어\n{score:N1}";
    }

    private void OnDisable()
    {
        GameManager.Instance.OnScoreChange -= UpdateText;
    }
}
