using ES3Internal;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public bool settingsOpen = false;

    [SerializeField]
    private GameObject settingsPanel;

    [SerializeField]
    private int selectedOption = 0;

    [SerializeField]
    private int optionAmounts = 3;

    [SerializeField]
    private Color defaultColor;

    [SerializeField]
    private Color selectedColor;

    [SerializeField]
    private List<GameObject> options = new List<GameObject>();

    [SerializeField]
    private GameObject confirmOutline;
    [SerializeField]
    private GameObject iPod;

    [Space]
    public bool verticalCameraInverted = false;
    public bool musicMuted = false;
    public bool holdScoot = false;
    [Space]
    [Space]
    private int trophies = 6;
    private List<bool> hasTrophy = new List<bool>() { false, false, false, false, false, false };
    private List<string> trophyName = new List<string>() { "Serial killer", "Do you have any idea how fast I am?", "Subaru likes to redo", "Hit the target", "I feel a murderous intent...!", "Pizza" };
    public List<Sprite> trophyTextures = new List<Sprite>();
    public List<GameObject> dots;
    [Space]
    public Image trophyImage;
    public TextMeshProUGUI trophyNameText;
    public Animator trophyAnimator;

    public Vector2 dotSize;
    public Vector2 iconSize;

    public void Awake()
    {

    }

    // Use this for initialization
    void Start()
    {
        if (ES3.KeyExists("Vertical Camera Inverted"))
            verticalCameraInverted = ES3.Load<bool>("Vertical Camera Inverted");
        if (ES3.KeyExists("Music Muted"))
            musicMuted = ES3.Load<bool>("Music Muted");
        if (ES3.KeyExists("Hold Scoot"))
            holdScoot = ES3.Load<bool>("Hold Scoot");
        if (ES3.KeyExists("Trophies Earned"))
            hasTrophy = ES3.Load<List<bool>>("Trophies Earned");

        //LockTrophy(0);
        //LockTrophy(1);
        //LockTrophy(2);
        //LockTrophy(3);
        //LockTrophy(4);
        //LockTrophy(5);

        UpdateStuff();


        settingsPanel.SetActive(settingsOpen);

        //UnlockTrophy(0);
        //ES3.Save<List<bool>>("Trophies Earned", new List<bool>());
        //hasTrophy = ES3.Load<List<bool>>("Trophies Earned");

    }

    void UpdateStuff()
    {

        if (trophies > hasTrophy.Count)
        {
            while (trophies > hasTrophy.Count)
            {
                hasTrophy.Add(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        #region Settings
        if (SimpleInput.GetKeyDown(KeyCode.Tab) || SimpleInput.GetKeyDown(KeyCode.JoystickButton3))
        {
            settingsOpen = !settingsOpen;

            settingsPanel.SetActive(settingsOpen);

        }

        if (SimpleInput.GetKeyDown(KeyCode.C) || SimpleInput.GetKeyDown(KeyCode.JoystickButton1))
        {
            settingsOpen = false;

            settingsPanel.SetActive(false);
        }

        if (settingsOpen)
        {
            Time.timeScale = 0;

            if (SimpleInput.GetKeyDown(KeyCode.UpArrow) || SimpleInput.GetKeyDown(KeyCode.JoystickButton8))
            {
                selectedOption = previousArray(selectedOption, optionAmounts);
            }

            if (SimpleInput.GetKeyDown(KeyCode.DownArrow) || SimpleInput.GetKeyDown(KeyCode.JoystickButton10))
            {
                selectedOption = nextArray(selectedOption, optionAmounts);
            }

            GameObject selectedOptionGm = options[selectedOption];


            for (int i = 0; i < iPod.GetComponents<AudioSource>().Length; i++)
            {
                iPod.GetComponents<AudioSource>()[i].mute = true;
                iPod.GetComponents<AudioSource>()[i].Pause();
            }

            for (int i = 0; i < options.Count; i++)
            {
                if (i != options.Count - 1)
                    options[i].GetComponent<Image>().color = defaultColor;
                confirmOutline.SetActive(false);

                if (i == 0)
                {
                    options[i].GetComponent<optionTicked>().toggled = verticalCameraInverted;
                }

                if (i == 2)
                {
                    options[i].GetComponent<optionTicked>().toggled = musicMuted;
                }


                if (i == 3)
                {
                    options[i].GetComponent<optionTicked>().toggled = holdScoot;
                }

                if (i == 1)
                {
                    options[i].GetComponent<optionTicked>().toggled = GameObject.FindGameObjectWithTag("Safe Mode").GetComponent<SafeMode>().safeMode;
                }
            }

            if (selectedOption != options.Count - 1)
                selectedOptionGm.GetComponent<Image>().color = selectedColor;

            switch (selectedOption)
            {
                case 0:
                    if (SimpleInput.GetKeyDown(KeyCode.X) || SimpleInput.GetKeyDown(KeyCode.JoystickButton0))
                    {
                        selectedOptionGm.GetComponent<optionTicked>().toggled = !selectedOptionGm.GetComponent<optionTicked>().toggled;
                        verticalCameraInverted = selectedOptionGm.GetComponent<optionTicked>().toggled;
                    }
                    break;

                case 1:
                    if (SimpleInput.GetKeyDown(KeyCode.X) || SimpleInput.GetKeyDown(KeyCode.JoystickButton0))
                    {
                        selectedOptionGm.GetComponent<optionTicked>().toggled = !selectedOptionGm.GetComponent<optionTicked>().toggled;
                        GameObject.FindGameObjectWithTag("Safe Mode").GetComponent<SafeMode>().ChangeTo(selectedOptionGm.GetComponent<optionTicked>().toggled);
                    }
                    break;
                case 2:
                    if (SimpleInput.GetKeyDown(KeyCode.X) || SimpleInput.GetKeyDown(KeyCode.JoystickButton0))
                    {
                        selectedOptionGm.GetComponent<optionTicked>().toggled = !selectedOptionGm.GetComponent<optionTicked>().toggled;
                        musicMuted = selectedOptionGm.GetComponent<optionTicked>().toggled;
                    }
                    break;
                case 3:
                    if (SimpleInput.GetKeyDown(KeyCode.X) || SimpleInput.GetKeyDown(KeyCode.JoystickButton0))
                    {
                        selectedOptionGm.GetComponent<optionTicked>().toggled = !selectedOptionGm.GetComponent<optionTicked>().toggled;
                        holdScoot = selectedOptionGm.GetComponent<optionTicked>().toggled;
                    }
                    break;
                case 4:
                    confirmOutline.SetActive(true);
                    if (SimpleInput.GetKeyDown(KeyCode.X) || SimpleInput.GetKeyDown(KeyCode.JoystickButton0))
                    {
                        settingsOpen = false;

                        settingsPanel.SetActive(false);

                        Save();
                    }
                    break;
            }
        }
        else
        {
            Time.timeScale = 1;




            for (int i = 0; i < iPod.GetComponents<AudioSource>().Length; i++)
            {

                iPod.GetComponents<AudioSource>()[i].UnPause();


                if (!musicMuted)
                    iPod.GetComponents<AudioSource>()[i].mute = false;
            }
        }

        if (musicMuted)
            for (int i = 0; i < iPod.GetComponents<AudioSource>().Length; i++)
            {
                iPod.GetComponents<AudioSource>()[i].mute = true;

            }
        #endregion

        #region Trophies

        if (settingsOpen)
        {
            for (int i = 0; i < hasTrophy.Count; i++)
            {
                if (hasTrophy[i])
                {
                    dots[i].GetComponent<RectTransform>().sizeDelta = iconSize;
                    dots[i].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = trophyName[i];
                    dots[i].GetComponent<Image>().sprite = trophyTextures[i];
                }
            }

            for (int i = hasTrophy.Count; i < dots.Count; i++)
            {
                dots[i].SetActive(false);
            }

        }

        #endregion
    }

    public void LockTrophy(int trophy)
    {
        hasTrophy[trophy] = false;
        ES3.Save<List<bool>>("Trophies Earned", hasTrophy);
    }

    public void UnlockTrophy(int trophy, bool animation = true)
    {
        if (!hasTrophy[trophy])
        {
            if (animation)
            {
                trophyAnimator.Play("Get trophy");
            }

            hasTrophy[trophy] = true;

            trophyNameText.text = trophyName[trophy];
            trophyImage.sprite = trophyTextures[trophy];
        }

        ES3.Save<List<bool>>("Trophies Earned", hasTrophy);
    }

    void Save()
    {
        ES3.Save<bool>("Vertical Camera Inverted", verticalCameraInverted);
        ES3.Save<bool>("Music Muted", musicMuted);
        ES3.Save<bool>("Hold Scoot", holdScoot);
    }

    int nextArray(int current, int arrayCount)
    {
        int newCurrent = ++current;
        if (newCurrent > arrayCount - 1)
        {
            return 0;
        }
        return newCurrent;
    }
    int previousArray(int current, int arrayCount)
    {
        int newCurrent = --current;
        if (newCurrent < 0)
        {
            return arrayCount - 1;
        }
        return newCurrent;
    }
}