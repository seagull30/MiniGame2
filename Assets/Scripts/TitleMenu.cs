using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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


    public void CallScoreMenu()
    {
        go_ScoreBaseUI.SetActive(true);
        go_GameBaseUI.SetActive(false);
    }

    public void CloseScoreMenu()
    {
        go_ScoreBaseUI.SetActive(false);
        go_GameBaseUI.SetActive(true);
    }

    public void CallAchivementMenu()
    {
        go_AchievementBaseUI.SetActive(true);
        go_GameBaseUI.SetActive(false);
    }

    public void CloseAchivementMenu()
    {
        go_AchievementBaseUI.SetActive(false);
        go_GameBaseUI.SetActive(true);
    }

    public void CallOptionMenu()
    {
        go_OptionBaseUI.SetActive(true);
        go_GameBaseUI.SetActive(false);
    }

    public void CloseOptionMenu()
    {
        go_OptionBaseUI.SetActive(false);
        go_GameBaseUI.SetActive(true);
    }

    public void BgmSoundControl()
    {
        if(SoundManager.BGMActivated)
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

    public void EndGame()
    {
        Application.Quit();
    }


    public void CallGame()
    {
        SceneManager.LoadScene("Main2");
    }

}