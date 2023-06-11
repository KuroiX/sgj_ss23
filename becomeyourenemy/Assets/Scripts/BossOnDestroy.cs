using Controller;
using TMPro;
using UnityEngine;

public class BossOnDestroy : MonoBehaviour
{
    [SerializeField] private bool isPlayer;

    private void OnDestroy()
    {
        //GameObject.Find("Player").GetComponent<PlayerInput>()._characterInput.Disable();
        //Time.timeScale = 0;
        var timer = FindObjectOfType<SpeedrunTimer>();
        string timerString = timer.StopTimer();
        GameObject.Find("UI").GetComponent<UIManager>().ShowFinalScreen(isPlayer);

        string findName = isPlayer ? "YourTimeText2" : "YourTimeText";
        
        GameObject.Find(findName).GetComponent<TextMeshProUGUI>().text = timerString;
    }
}
