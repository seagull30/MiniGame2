using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name; //�� �̸�
    public AudioClip clip;//��
}

//SoundManager���� ��� ������ �����ϵ��� �Ѵ�.
public class SoundManager : MonoBehaviour
{
    //�̱��� ����
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

    public Sound[] effectSounds;//ȿ���� ����� Ŭ��
    public Sound[] bgmSounds; //BGM ����� Ŭ��

    public AudioSource audioSourceBGM;
    public AudioSource[] audioSourceEffects;

    public string[] playSoundName;  // ��� ���� ȿ���� ���� �̸� �迭

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
                Debug.Log("��� ���� AudioSource�� ��� ���Դϴ�.");
                return;
            }
        }
        Debug.Log(_name + "���尡 SoundManager�� ��ϵ��� �ʾҽ��ϴ�.");
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
        Debug.Log(_name + "���尡 SoundManager�� ��ϵ��� �ʾҽ��ϴ�.");
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
        Debug.Log("��� ����" + _name + "���尡 �����ϴ�. ");
    }

}
