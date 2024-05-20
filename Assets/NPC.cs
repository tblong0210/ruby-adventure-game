using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    [SerializeField] private float displayTime;
    [SerializeField] private GameObject dialogBox;
    float timerDisplay;

    void Start()
    {
        dialogBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(timerDisplay >= 0)
        {
            timerDisplay -= Time.deltaTime;

            if (timerDisplay < 0)
                dialogBox.SetActive(false);
        }
    }

    public void ShowDialog()
    {
        timerDisplay = displayTime;
        dialogBox.SetActive(true);
    }
}
