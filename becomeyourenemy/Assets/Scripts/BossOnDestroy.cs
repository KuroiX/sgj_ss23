using System;
using Controller;
using TMPro;
using UnityEngine;

public class BossOnDestroy : MonoBehaviour
{
    [SerializeField] private bool isPlayer;

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
        {
            MusicAndSound.Instance.StopBossMusic();
            MusicAndSound.Instance.PlayLevelMusic();
        }
        
        if (_hasLeftRoom) return;
        
        //GameObject.Find("Player").GetComponent<PlayerInput>()._characterInput.Disable();
        //Time.timeScale = 0;
        var timer = FindObjectOfType<SpeedrunTimer>();
        string timerString = timer.StopTimer();
        GameObject.Find("UI").GetComponent<UIManager>().ShowFinalScreen(isPlayer);

        string findName = isPlayer ? "YourTimeText2" : "YourTimeText";
        
        GameObject.Find(findName).GetComponent<TextMeshProUGUI>().text = timerString;
    }

    public void Disable()
    {
        _hasLeftRoom = true;
    }
}
