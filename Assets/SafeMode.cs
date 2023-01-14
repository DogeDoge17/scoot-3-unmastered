using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SafeMode : MonoBehaviour
{

    public AudioSource[] badClips;

    private Animator animator;

    public bool safeMode = false;

    Text theText;

    void Awake()
    {
        theText = GetComponent<Text>();
        animator = GetComponent<Animator>();
    }

    //Use this for initialization

    void Start()
    {
        if (ES3.KeyExists("Safe Mode"))
        {
            ChangeTo(ES3.Load<bool>("Safe Mode"), false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightAlt) || Input.GetKey(KeyCode.JoystickButton6))
        {
            Toggle();
        }
    }

    public void Toggle(bool useAnimation = true)
    {
        safeMode = !safeMode;

        if (safeMode)
        {
            foreach (AudioSource source in badClips)
            {
                source.mute = true;
            }
            theText.text = "Safe Mode Enabled";
            if (useAnimation) animator.Play("Enable Safe Mode");
        }
        else
        {
            foreach (AudioSource source in badClips)
            {
                source.mute = false;
            }
            theText.text = "Safe Mode Disabled";
            if (useAnimation) animator.Play("Disable Safe Mode");
        }

        if (ES3.KeyExists("Safe Mode"))
        {
            ES3.Save<bool>("Safe Mode", safeMode);
        }
    }

    public void ChangeTo(bool value, bool useAnimation = true)
    {
        safeMode = value;

        if (safeMode)
        {
            foreach (AudioSource source in badClips)
            {
                source.mute = true;
            }
            theText.text = "Safe Mode Enabled";
            if (useAnimation) animator.Play("Enable Safe Mode");
        }
        else
        {
            foreach (AudioSource source in badClips)
            {
                source.mute = false;
            }
            theText.text = "Safe Mode Disabled";
            if (useAnimation) animator.Play("Disable Safe Mode");
        }

        //if (ES3.KeyExists("Safe Mode"))
        //{
        ES3.Save<bool>("Safe Mode", safeMode);
        //}
    }
}
