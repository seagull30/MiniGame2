using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name; //곡 이름
    public AudioClip clip;//곡
}

//SoundManager에서 모든 음향을 조절하도록 한다.
public class SoundManager : MonoBehaviour
{
    //싱글톤 구현
    static public SoundManager instance;
    public bool BGMActivated = true;
    public bool EffectActivated = true;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public Sound[] effectSounds;//효과음 오디오 클립
    public Sound[] bgmSounds; //BGM 오디오 클립

    public AudioSource audioSourceBGM;
    public AudioSource[] audioSourceEffects;

    public string[] playSoundName;  // 재생 중인 효과음 사운드 이름 배열

    void Start()
    {
        playSoundName = new string[audioSourceEffects.Length];
        //PlayBGM("TitleBackground");
        /*        PlaySE("object1");
                PlaySE("object2");
                PlaySE("object3");*/
    }

    public void PlaySE(string _name)
    {
        for (int i = 0; i < effectSounds.Length; i++)
        {
            if (_name == effectSounds[i].name)
            {
                for (int j = 0; j < audioSourceEffects.Length; j++)
                {
                    if (!audioSourceEffects[j].isPlaying)
                    {
                        audioSourceEffects[j].clip = effectSounds[i].clip;
                        audioSourceEffects[j].PlayOneShot(audioSourceEffects[j].clip);
                        playSoundName[j] = effectSounds[i].name;
                        return;
                    }
                }
                Debug.Log("모든 가용 AudioSource가 사용 중입니다.");
                return;
            }
        }
        Debug.Log(_name + "사운드가 SoundManager에 등록되지 않았습니다.");
    }

    public void PlayBGM(string _name)
    {
        for (int i = 0; i < bgmSounds.Length; i++)
        {
            if (_name == bgmSounds[i].name)
            {
                audioSourceBGM.clip = bgmSounds[i].clip;
                audioSourceBGM.Play();
                audioSourceBGM.loop = true;
                return;
            }
        }
        Debug.Log(_name + "사운드가 SoundManager에 등록되지 않았습니다.");
    }

    public void StopBGM_M()
    {
        audioSourceBGM.mute = true;
        BGMActivated = !BGMActivated;
    }

    public void StartBGM_M()
    {
        audioSourceBGM.mute = false;
        BGMActivated = !BGMActivated;
    }

    public void StopEffect_M()
    {
        for (int i = 0; i < audioSourceEffects.Length; i++)
        {
            audioSourceEffects[i].mute = true;
        }
        EffectActivated = !EffectActivated;
    }

    public void StartEffect_M()
    {
        for (int i = 0; i < audioSourceEffects.Length; i++)
        {
            audioSourceEffects[i].mute = false;
        }
        EffectActivated = !EffectActivated;
    }



    public void StopAllSE()
    {
        for (int i = 0; i < audioSourceEffects.Length; i++)
        {
            audioSourceEffects[i].Stop();
        }
    }

    public void StopSE(string _name)
    {
        for (int i = 0; i < audioSourceEffects.Length; i++)
        {
            if (playSoundName[i] == _name)
            {
                audioSourceEffects[i].Stop();
                break;
            }
        }
        Debug.Log("재생 중인" + _name + "사운드가 없습니다. ");
    }

}
