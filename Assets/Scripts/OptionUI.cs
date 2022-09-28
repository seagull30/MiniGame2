using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    private GameObject[] _childs;
    private int _childCount;

    [SerializeField]
    private Image _effectButtonSprite;
    [SerializeField]
    private Image _BgmButtonSprite;

    [SerializeField]
    private Sprite _onSprite;
    [SerializeField]
    private Sprite _offSprite;

    private readonly string _effectSound = "Button";

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
        //Time.fixedDeltaTime = 0f;
        Time.timeScale = 0f;
        GameManager.Instance._ispause = true;

        for (int i = 0; i < _childCount; ++i)
        {
            _childs[i].SetActive(true);
        }
        SoundManager.instance.PlaySE(_effectSound);
    }

    public void onClickEventReturn()
    {
        //Time.fixedDeltaTime = 1f;
        Time.timeScale = 1f;
        GameManager.Instance._ispause = false;

        for (int i = 0; i < _childCount; ++i)
        {
            _childs[i].SetActive(false);
        }
        SoundManager.instance.PlaySE(_effectSound);
    }

    public void onClickEventReturnRobby()
    {
        //Time.fixedDeltaTime = 1f;
        Time.timeScale = 1f;
        SoundManager.instance.PlaySE(_effectSound);
        GameManager.Instance.goTitle();
    }


    public void BgmSoundControl()
    {
        if (SoundManager.instance.BGMActivated)
        {
            //true 상태 였다면 끄기
            SoundManager.instance.StopBGM_M();
            _BgmButtonSprite.sprite = _offSprite;
        }
        else
        {
            SoundManager.instance.StartBGM_M();
            _BgmButtonSprite.sprite = _onSprite;
        }
        SoundManager.instance.PlaySE(_effectSound);
    }

    public void EffectSoundControl()
    {
        if (SoundManager.instance.EffectActivated)
        {
            //true 상태 였다면 끄기
            SoundManager.instance.StopEffect_M();
            _effectButtonSprite.sprite = _offSprite;
        }
        else
        {
            SoundManager.instance.StartEffect_M();
            _effectButtonSprite.sprite = _onSprite;
        }
        SoundManager.instance.PlaySE(_effectSound);
    }

}
