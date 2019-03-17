using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CronometroText : MonoBehaviour
{
    Text tiempoText;
    public float time = 120;

    private void Awake()
    {
        tiempoText = GetComponent<Text>();
    }

    void Update()
    {
        StarChronometer();
    }

    void StarChronometer()
    {
        time -= Time.deltaTime;

        //float TimerControl = Time.time - time;
        string mins = ((int)time / 60).ToString("00");//
        int min = (int)time/60;
        string segs = (time -(min*60)).ToString("00");//

        if (time <= 30.0f && time > 10.0f)
        { tiempoText.color = new Color(255, 184, 0, 255); }
        if (time <= 10.0f)
        { tiempoText.color = Color.red; }

        string timerString = string.Format("{00}:{01}", mins, segs);

        tiempoText.text = timerString;

    }
}
