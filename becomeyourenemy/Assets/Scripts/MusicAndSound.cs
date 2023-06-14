using System.Collections;
using UnityEngine;

public class MusicAndSound : MonoBehaviour
{
    private string myName;
    public static MusicAndSound Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private FMOD.Studio.EventInstance instance;
    private FMOD.Studio.EventInstance instance1;
    private FMOD.Studio.EventInstance instance2;

    public FMODUnity.EventReference fmodEvent;
    public FMODUnity.EventReference fmodEvent1;
    public FMODUnity.EventReference fmodEvent2;

    void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        instance1 = FMODUnity.RuntimeManager.CreateInstance(fmodEvent1);
        instance2 = FMODUnity.RuntimeManager.CreateInstance(fmodEvent2);
    }

    public void PlayLevelMusic()
    {
        instance.start();
    }

    public void PlayBossMusic()
    {
        instance1.start();
    }

    public void PlayMenuMusic()
    {
        instance2.start();
    }

    public void PlayStomp()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Froggystomp");
    }

    public void PlayJump()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Froggyjump");
    }

    public void PlayClaw()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Mushroom Claw");
    }

    public void PlayWave()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Gengarwave");
    }

    public void PlayPlayerHit()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Hit-Player");
    }

    public void PlayEnemyHit()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Hit-Enemy");
    }

    public void PlayStep()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Enemiy Foodstep mud");
    }

    public void PlayShift()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Shift");
    }

    public void StopLevelMusic()
    {
        instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    public void StopBossMusic()
    {
        instance1.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    public void StopMenuMusic()
    {
        instance2.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    public void SetStress()
    {
        StartCoroutine(FadeIn());
    }

    public void SetCurrentEnemy(string enemyName)
    {
        if (enemyName == "Froggy")
        {
            myName = "Froggy";
            StartCoroutine(Fadeout());
            instance.setParameterByName("Froggy", 1);
            instance1.setParameterByName("Froggy", 1);
        }
        else if (enemyName == "Shroom")
        {
            myName = "Shroom";
            StartCoroutine(Fadeout());
            instance.setParameterByName("Crazy Shroom", 1);
            instance1.setParameterByName("Crazy Shroom", 1);
        }
        else if (enemyName == "Gengar")
        {
            myName = "Gengar";
            StartCoroutine(Fadeout());
            instance.setParameterByName("Gengar", 1);
            instance1.setParameterByName("Gengar", 1);
        }
    }

    public void ResetCharacter()
    {
        instance.setParameterByName("Crazy Shroom", 0);
        instance.setParameterByName("Gengar", 0);
        instance.setParameterByName("Froggy", 0);
        instance1.setParameterByName("Crazy Shroom", 0);
        instance1.setParameterByName("Gengar", 0);
        instance1.setParameterByName("Froggy", 0);
    }

    private IEnumerator Fadeout()
    {
        if (myName == "Froggy")
        {
            for (float value = 1; value >= 0; value -= 0.1f)
            {
                if (value < 0) value = 0;
                instance.setParameterByName("Crazy Shroom", value);
                instance.setParameterByName("Gengar", value);
                instance1.setParameterByName("Crazy Shroom", value);
                instance1.setParameterByName("Gengar", value);
                yield return new WaitForSeconds(0.1f);
            }
        }
        else if (myName == "Shroom")
        {
            for (float value = 1; value >= 0; value -= 0.1f)
            {
                if (value < 0) value = 0;
                instance.setParameterByName("Froggy", value);
                instance.setParameterByName("Gengar", value);
                instance1.setParameterByName("Froggy", value);
                instance1.setParameterByName("Gengar", value);
                yield return new WaitForSeconds(0.1f);
            }
        }
        else if (myName == "Gengar")
        {
            for (float value = 1; value >= 0; value -= 0.1f)
            {
                if (value < 0) value = 0;
                instance.setParameterByName("Crazy Shroom", value);
                instance.setParameterByName("Froggy", value);
                instance1.setParameterByName("Crazy Shroom", value);
                instance1.setParameterByName("Froggy", value);
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    IEnumerator FadeIn()
    {
        for (float value = 1; value >= 0; value -= 0.1f)
        {
            if (value < 0) value = 0;
            instance.setParameterByName("Stress", value);
            instance1.setParameterByName("Stress", value);
            yield return new WaitForSeconds(0.02f);
        }
    }

    private void OnDestroy()
    {
        StopMenuMusic();
        StopBossMusic();
        StopLevelMusic();
    }
}