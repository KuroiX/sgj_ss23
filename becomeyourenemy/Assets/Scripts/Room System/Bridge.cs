using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    public GameObject wall;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player") wall.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") wall.SetActive(false);
    }
}
