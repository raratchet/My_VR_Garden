using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldButton : MonoBehaviour
{
    public delegate void ButtonPress();
    public event ButtonPress buttonPress;
    public float lastTime = 0;
    public void OnButtonPressed()
    {
        if(lastTime + 1f < Time.time)
        {
            buttonPress();
            lastTime = Time.time;
        }
    }
}
