using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class TimerUi : MonoBehaviour
{

    public TMP_Text timerUI;

    float currTime;

    // Update is called once per frame
    void Update()
    {

        if (PlayerStatManager.instance.isRunning)
        {

            TimeSpan time = TimeSpan.FromSeconds(currTime);

            currTime = currTime + Time.deltaTime;


            timerUI.text = "TIME: " + time.Minutes.ToString() + ":" + time.Seconds.ToString() + ":" + time.Milliseconds.ToString();
        }
        else if (!PlayerStatManager.instance.isRunning)
        {
            currTime = 0f;
        }
    }
}
