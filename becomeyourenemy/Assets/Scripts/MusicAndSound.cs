using System.Collections;
using UnityEngine;

public class MusicAndSound : MonoBehaviour
{
    private string myName;
    public static MusicAndSound Instance { get; private set; }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private FMOD.Studio.EventInstance instance;
    private FMOD.Studio.EventInstance instance1;

    public FMODUnity.EventReference fmodEvent;
    public FMODUnity.EventReference fmodEvent1;


    void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        instance1 = FMODUnity.RuntimeManager.CreateInstance(fmodEvent1);
    }

    public void PlayLevelMusic()
    {
        instance.start();
    }

    public void PlayBossMusic()
    {
        instance1.start();
    }

    public void PlayStomp()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Froggystomp");
    }

    public void PlayJump()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Froggyjump");
    }

    public void PlayClaw()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Mushroom Claw");
    }

    public void PlayWave()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Gengarwave");
    }

    public void PlayPlayerHit()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Hit-Player");
    }

    public void PlayEnemyHit()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Hit-Enemy");
    }

    public void PlayStep()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Enemiy Foodstep mud");
    }

    public void PlayShift()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Shift");
    }

    public void StopLevelMusic()
    {
        instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    public void StopBossMusic()
    {
        instance1.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    public void SetStress(float value)
    {
        instance.setParameterByName("Stress", value);
    }

    public void SetCurrentEnemy(string enemyName)
    {
        if (name == "Froggy")
        {
            myName = "Froggy";
            StartCoroutine(Fadeout());
            instance.setParameterByName("Froggy", 1);
            instance1.setParameterByName("Froggy", 1);
        }
        else if (name == "Shroom")
        {
            myName = "Shroom";
            StartCoroutine(Fadeout());
            instance.setParameterByName("Crazy Shroom", 1);
            instance1.setParameterByName("Crazy Shroom", 1);
        }
        else if (name == "Gengar")
        {
            myName = "Gengar";
            StartCoroutine(Fadeout());
            instance.setParameterByName("Gengar", 1);
            instance1.setParameterByName("Gengar", 1);
        }
    }

    private IEnumerator Fadeout()
    {
        if (myName == "Froggy")
        {
            for (float value = 1; value <= 0; value = -0.1f)
            {
                instance.setParameterByName("Crazy Shroom", value);
                instance.setParameterByName("Gengar", value);
                instance1.setParameterByName("Crazy Shroom", value);
                instance1.setParameterByName("Gengar", value);
                yield return null;
            }
        }
        else if (myName == "Shroom")
        {
            
            for (float value = 1; value <= 0; value = -0.1f)
            {
                instance.setParameterByName("Froggy", value);
                instance.setParameterByName("Gengar", value);
                instance1.setParameterByName("Froggy", value);
                instance1.setParameterByName("Gengar", value);
                yield return null;
            }
        }
        else if(myName == "Gengar") 
        {
            for (float value = 1; value <= 0; value = -0.1f)
            {
                instance.setParameterByName("Crazy Shroom", value);
                instance.setParameterByName("Froggy", value);
                instance1.setParameterByName("Crazy Shroom", value);
                instance1.setParameterByName("Froggy", value);
                yield return null;
            }
        }
    }
}