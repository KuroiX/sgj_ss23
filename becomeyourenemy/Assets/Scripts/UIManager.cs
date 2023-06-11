using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [HideInInspector] public int killCount;
    [HideInInspector] public string speedrunTime;
    [HideInInspector] public float rawSpeedrunTime;
    [SerializeField] private GameObject victoryScreen;
    [SerializeField] private GameObject gameoverScreen;

    [SerializeField] private Sprite[] actionSprites;
    private string[] descriptions;

    [HideInInspector] public string currentDescription;
    [HideInInspector] public Sprite currentSprite;

    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI description;

    // Start is called before the first frame update
    void Awake()
    {
        killCount = 0;
        currentDescription = "Shoot";
        currentSprite = actionSprites[1];
        descriptions = new[] { "Slash", "Shoot", "Triple Shoot", "Leap" };
        SwitchAbility(1);
    }

    public void ShowFinalScreen(bool playerDied)
    {
        victoryScreen.SetActive(!playerDied);
        gameoverScreen.SetActive(playerDied);
    }

    public void SwitchAbility(int index)
    {
        currentDescription = descriptions[index];
        currentSprite = actionSprites[index];
        image.sprite = currentSprite;
        description.text = currentDescription;
    }
}
