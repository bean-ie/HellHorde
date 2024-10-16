using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;
    public bool running;
    public bool absoluteRunning = true;

    private void Awake()
    {
        instance = this;
    }

    public void SwitchTime()
    {
        if (!absoluteRunning) return;
        if (running) running = false;
        else running = true;
    }

    public void StopTime()
    {
        running = false;
        absoluteRunning = false;
    }

    public void ResumeTime()
    {
        absoluteRunning = true;
        running = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwitchTime();
        }
    }
}
