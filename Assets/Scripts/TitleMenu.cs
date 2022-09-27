using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{

    [SerializeField]
    private GameObject go_ScoreBaseUI; //���ھ� �г�

    [SerializeField]
    private GameObject go_GameBaseUI; //���� �г�

    [SerializeField]
    private GameObject go_AchievementBaseUI; //���� �г�

    [SerializeField]
    private GameObject go_OptionBaseUI; //�ɼ� �г�

    private readonly string _effectSound = "Button";


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
            //true ���� ���ٸ� ����
            SoundManager.instance.StopBGM_M();
        }
        else
        {
            SoundManager.instance.StartBGM_M();
        }
        SoundManager.instance.PlaySE(_effectSound);
    }

    public void EffectSoundControl()
    {
        if (SoundManager.instance.EffectActivated)
        {
            //true ���� ���ٸ� ����
            SoundManager.instance.StopEffect_M();
        }
        else
        {
            SoundManager.instance.StartEffect_M();
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