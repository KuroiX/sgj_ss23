using System;
using Controller;
using TMPro;
using UnityEngine;

public class BossOnDestroy : MonoBehaviour
{
    [SerializeField] public bool isPlayer;

    private bool _hasLeftRoom;

    private void Start()
    {
        if (isPlayer) return;
        
        MusicAndSound.Instance.StopLevelMusic();
        MusicAndSound.Instance.PlayBossMusic();
    }

    private void OnDestroy()
    {

        if (!isPlayer)
            MusicAndSound.Instance.StopBossMusic();

        if (_hasLeftRoom)
        {
            MusicAndSound.Instance.PlayLevelMusic();
            return;
        }

        if (!isPlayer)
            if (GameObject.Find("UI") != null) GameObject.Find("UI").GetComponent<UIManager>().ShowFinalScreen(isPlayer);
        
        //GameObject.Find("Player").GetComponent<PlayerInput>()._characterInput.Disable();
        //Time.timeScale = 0;
        /*var timer = FindObjectOfType<SpeedrunTimer>();
        string timerString = timer.StopTimer();
        GameObject.Find("UI").GetComponent<UIManager>().ShowFinalScreen(isPlayer);

        string findName = isPlayer ? "YourTimeText2" : "YourTimeText";
        
        GameObject.Find(findName).GetComponent<TextMeshProUGUI>().text = timerString;*/
    }

    public void Disable()
    {
        _hasLeftRoom = true;
    }
}
