using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FpsCounter : MonoBehaviour {

    //Declare these in your class
    int frameCounter = 0;
    float timeCounter = 0.0f;
    float lastFramerate = 0.0f;
    public float refreshTime = 0.5f;

    public TextMeshProUGUI fpsText;

    void Awake()
    {
        fpsText.gameObject.SetActive(true);
    }
    void Update()
    {
//        Debug.Log(Time.deltaTime);

        if (timeCounter < refreshTime)
        {
            timeCounter += Time.deltaTime;
            frameCounter++;
        }
        else
        {
            //This code will break if you set your m_refreshTime to 0, which makes no sense.
            lastFramerate = (float)frameCounter / timeCounter;
            frameCounter = 0;
            timeCounter = 0.0f;
        }
    }

    void LateUpdate()
    {
        fpsText.text = "Fps: " + Mathf.Floor(lastFramerate);
    }
}
