using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleMenu : MonoBehaviour
{

    [SerializeField]
    private GameObject go_ScoreBaseUI; //스코어 패널

    [SerializeField]
    private GameObject go_GameBaseUI; //게임 패널

    [SerializeField]
    private GameObject go_AchievementBaseUI; //성과 패널

    [SerializeField]
    private GameObject go_OptionBaseUI; //옵션 패널
    [SerializeField]
    private Image _effectButtonSprite;
    [SerializeField]
    private Image _BgmButtonSprite;
    private readonly string _effectSound = "Button";
    private readonly string _bgmName = "TitleBGM";

    [SerializeField]
    private Sprite _onSprite;
    [SerializeField]
    private Sprite _offSprite;

    private void Start()
    {
        SoundManager.instance.PlayBGM(_bgmName);
    }
    public void CallScoreMenu()
    {
        go_ScoreBaseUI.SetActive(true);
        go_GameBaseUI.SetActive(false);
        SoundManager.instance.PlaySE(_effectSound);
    }

    public void CloseScoreMenu()
    {
        go_ScoreBaseUI.SetActive(false);
        go_GameBaseUI.SetActive(true);
        SoundManager.instance.PlaySE(_effectSound);
    }

    public void CallAchivementMenu()
    {
        go_AchievementBaseUI.SetActive(true);
        go_GameBaseUI.SetActive(false);
        SoundManager.instance.PlaySE(_effectSound);
    }

    public void CloseAchivementMenu()
    {
        go_AchievementBaseUI.SetActive(false);
        go_GameBaseUI.SetActive(true);
        SoundManager.instance.PlaySE(_effectSound);
    }

    public void CallOptionMenu()
    {
        go_OptionBaseUI.SetActive(true);
        go_GameBaseUI.SetActive(false);
        SoundManager.instance.PlaySE(_effectSound);
    }

    public void CloseOptionMenu()
    {
        go_OptionBaseUI.SetActive(false);
        go_GameBaseUI.SetActive(true);
        SoundManager.instance.PlaySE(_effectSound);
    }

    public void BgmSoundControl()
    {
        if(SoundManager.instance.BGMActivated)
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

    public void EndGame()
    {
        Application.Quit();
    }


    public void CallGame()
    {
        SoundManager.instance.PlaySE(_effectSound);
        SceneManager.LoadScene("Main");
        Debug.Log(Time.fixedDeltaTime);
    }

}