using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class OptionUI : MonoBehaviour
{
    private GameObject[] _childs;
    private int _childCount;

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


    public void onClickEvent()
    {
        Time.timeScale = 0f;
        Time.fixedDeltaTime = 0f;
        GameManager.Instance._ispause = true;

        for (int i = 0; i < _childCount; ++i)
        {
            _childs[i].SetActive(true);
        }
    }

    public void onClickEventReturn()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 1f;
        GameManager.Instance._ispause = false;

        for (int i = 0; i < _childCount; ++i)
        {
            _childs[i].SetActive(false);
        }
    }

    public void onClickEventReturnRobby()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 1f;
        GameManager.Instance.goTitle();
    }


    public void BgmSoundControl()
    {
        if (SoundManager.BGMActivated)
        {
            //true 상태 였다면 끄기
            SoundManager.instance.StopBGM_M();
        }
        else
        {
            SoundManager.instance.StartBGM_M();
        }
    }

    public void EffectSoundControl()
    {
        if (SoundManager.EffectActivated)
        {
            //true 상태 였다면 끄기
            SoundManager.instance.StopEffect_M();
        }
        else
        {
            SoundManager.instance.StartEffect_M();
        }
    }

}
