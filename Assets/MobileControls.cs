using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MobileControls : MonoBehaviour
{
	public bool forceShow;

	public List<GameObject> dpadImages;
	public List<GameObject> buttonOn;
	public List<GameObject> buttonOff;
	public List<GameObject> bottomImages;
    [Space]
	public GameObject dPadControl;
	public GameObject crossButton;
	public List<GameObject> otherButtons;

	public float dpadPressScale; 
	public float dpadDefaultScale;
	[Space]
	public float buttonPressScale;
	public float buttonDefaultScale;
	[Space]
	public float bottomPressScale;
	public float bottomDefaultScale;

	[Space]
	public List<Sprite> zaHandoStuff;


	public SimpleInput.KeyInput downKey = new SimpleInput.KeyInput();

	public GameObject stuff;

	[Space]
	public GameObject onscooter;
	public GameObject offscooter;

	private Settings settings;
	private PlayerStats PlayerStats;

	void Awake()
    {
		if((Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.IPhonePlayer && Application.platform != RuntimePlatform.BlackBerryPlayer) && !forceShow)
        {
			Destroy(gameObject);
        }
        else
        {
			stuff.SetActive(true);
			settings = GameObject.FindGameObjectWithTag("Event System").GetComponent<Settings>();
			PlayerStats = GameObject.FindGameObjectWithTag("Event System").GetComponent<PlayerStats>();
		}
	}

	// Use this for initialization	
	void Start () 
	{
		if ((Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.IPhonePlayer && Application.platform != RuntimePlatform.BlackBerryPlayer) && !forceShow)
			Screen.SetResolution(1980, 1080, true);
	}

	private void OnEnable()
	{
		downKey.StartTracking();
	}

	private void OnDisable()
	{
		downKey.StopTracking();
	}

	// Update is called once per frame
	void Update () 
	{

        //Debug.Log(SimpleInput.GetAxis("Vertical"));

        if (settings.settingsOpen)
        {
			dPadControl.SetActive(false);
			crossButton.SetActive(true);

			foreach(GameObject button in otherButtons)
            {
				button.SetActive(false);
            }
		}
        else
		{
			dPadControl.SetActive(true);
			crossButton.SetActive(false);

			foreach (GameObject button in otherButtons)
			{
				button.SetActive(true);
			}
		}


			onscooter.SetActive(PlayerStats.onScooter);
			offscooter.SetActive(!PlayerStats.onScooter);
      


		//Up
		if (SimpleInput.GetAxis("Vertical") > 0.9 || (SimpleInput.GetKey(KeyCode.UpArrow) && settings.settingsOpen)) 
		{
			dpadImages[0].gameObject.GetComponent<RectTransform>().localScale = new Vector3(dpadPressScale, dpadPressScale, dpadPressScale);
        }
        else
        {
			dpadImages[0].gameObject.GetComponent<RectTransform>().localScale = new Vector3(dpadDefaultScale, dpadDefaultScale, dpadDefaultScale);
		}

		//Left
		if (SimpleInput.GetAxis("Horizontal") < -0.9 || (SimpleInput.GetKey(KeyCode.LeftArrow) && settings.settingsOpen))
		{
			dpadImages[1].gameObject.GetComponent<RectTransform>().localScale = new Vector3(dpadPressScale, dpadPressScale, dpadPressScale);
		}
		else
		{
			dpadImages[1].gameObject.GetComponent<RectTransform>().localScale = new Vector3(dpadDefaultScale, dpadDefaultScale, dpadDefaultScale);
		}

		//Right
		if (SimpleInput.GetAxis("Horizontal") > 0.9 || (SimpleInput.GetKey(KeyCode.RightArrow) && settings.settingsOpen))
		{
			dpadImages[2].gameObject.GetComponent<RectTransform>().localScale = new Vector3(dpadPressScale, dpadPressScale, dpadPressScale);
		}
		else
		{
			dpadImages[2].gameObject.GetComponent<RectTransform>().localScale = new Vector3(dpadDefaultScale, dpadDefaultScale, dpadDefaultScale);
		}

		//Down
		if (SimpleInput.GetAxis("Vertical")  < -0.9 || (SimpleInput.GetKey(KeyCode.DownArrow) && settings.settingsOpen))
		{
			dpadImages[3].gameObject.GetComponent<RectTransform>().localScale = new Vector3(dpadPressScale, dpadPressScale, dpadPressScale);
			downKey.value = true;
		}
		else
		{
			dpadImages[3].gameObject.GetComponent<RectTransform>().localScale = new Vector3(dpadDefaultScale, dpadDefaultScale, dpadDefaultScale);
			downKey.value = false;
		}

		if (PlayerStats.onScooter)
		{
			if (SimpleInput.GetKey(KeyCode.E))
			{
				buttonOn[0].transform.GetChild(0).gameObject.GetComponent<RectTransform>().localScale = new Vector3(buttonPressScale, buttonPressScale, buttonPressScale);
			}
			else
			{
				buttonOn[0].transform.GetChild(0).gameObject.GetComponent<RectTransform>().localScale = new Vector3(buttonDefaultScale, buttonDefaultScale, buttonDefaultScale);
			}

			if (SimpleInput.GetKey(KeyCode.Space))
			{
				buttonOn[1].transform.GetChild(0).gameObject.GetComponent<RectTransform>().localScale = new Vector3(buttonPressScale, buttonPressScale, buttonPressScale);
			}
			else
			{
				buttonOn[1].transform.GetChild(0).gameObject.GetComponent<RectTransform>().localScale = new Vector3(buttonDefaultScale, buttonDefaultScale, buttonDefaultScale);
			}

			if (SimpleInput.GetKey(KeyCode.Alpha1) || (GameObject.FindGameObjectWithTag("Event System").GetComponent<PlayerStats>().isGrounded && SimpleInput.GetKey(KeyCode.S)))
			{
				buttonOn[2].transform.GetChild(0).gameObject.GetComponent<RectTransform>().localScale = new Vector3(buttonPressScale, buttonPressScale, buttonPressScale);
			}
			else
			{
				buttonOn[2].transform.GetChild(0).gameObject.GetComponent<RectTransform>().localScale = new Vector3(buttonDefaultScale, buttonDefaultScale, buttonDefaultScale);
			}

			if (SimpleInput.GetKey(KeyCode.Q) || SimpleInput.GetKey(KeyCode.LeftShift))
			{
				buttonOn[3].transform.GetChild(0).gameObject.GetComponent<RectTransform>().localScale = new Vector3(buttonPressScale, buttonPressScale, buttonPressScale);
			}
			else
			{
				buttonOn[3].transform.GetChild(0).gameObject.GetComponent<RectTransform>().localScale = new Vector3(buttonDefaultScale, buttonDefaultScale, buttonDefaultScale);
			}

			if (SimpleInput.GetKey(KeyCode.F))
			{
				buttonOn[4].transform.GetChild(0).gameObject.GetComponent<RectTransform>().localScale = new Vector3(bottomPressScale, bottomPressScale, bottomPressScale);
			}
			else
			{
				buttonOn[4].transform.GetChild(0).gameObject.GetComponent<RectTransform>().localScale = new Vector3(bottomDefaultScale, bottomDefaultScale, bottomDefaultScale);
			}

		}
		if (SimpleInput.GetKey(KeyCode.X))
		{
			crossButton.transform.GetChild(0).gameObject.GetComponent<RectTransform>().localScale = new Vector3(buttonPressScale, buttonPressScale, buttonPressScale);
		}
		else
		{
			crossButton.transform.GetChild(0).gameObject.GetComponent<RectTransform>().localScale = new Vector3(buttonDefaultScale, buttonDefaultScale, buttonDefaultScale);
		}


		if (SimpleInput.GetKey(KeyCode.R))
		{
			bottomImages[0].gameObject.GetComponent<RectTransform>().localScale = new Vector3(buttonPressScale, buttonPressScale, buttonPressScale);
		}
		else
		{
			bottomImages[0].gameObject.GetComponent<RectTransform>().localScale = new Vector3(buttonDefaultScale, buttonDefaultScale, buttonDefaultScale);
		}

		if (SimpleInput.GetKey(KeyCode.Tab))
		{
			bottomImages[1].gameObject.GetComponent<RectTransform>().localScale = new Vector3(bottomPressScale, bottomPressScale, bottomPressScale);
		}
		else
		{
			bottomImages[1].gameObject.GetComponent<RectTransform>().localScale = new Vector3(bottomDefaultScale, bottomDefaultScale, bottomDefaultScale);
		}


		if (SimpleInput.GetKey(KeyCode.E))
		{
			buttonOff[0].transform.GetChild(0).gameObject.GetComponent<RectTransform>().localScale = new Vector3(buttonPressScale, buttonPressScale, buttonPressScale);
		}
		else
		{
			buttonOff[0].transform.GetChild(0).gameObject.GetComponent<RectTransform>().localScale = new Vector3(buttonDefaultScale, buttonDefaultScale, buttonDefaultScale);
		}

		if (SimpleInput.GetKey(KeyCode.Space))
		{
			buttonOff[1].transform.GetChild(0).gameObject.GetComponent<RectTransform>().localScale = new Vector3(buttonPressScale, buttonPressScale, buttonPressScale);
		}
		else
		{
			buttonOff[1].transform.GetChild(0).gameObject.GetComponent<RectTransform>().localScale = new Vector3(buttonDefaultScale, buttonDefaultScale, buttonDefaultScale);
		}

		if (SimpleInput.GetKey(KeyCode.LeftShift))
		{
			buttonOff[2].transform.GetChild(0).gameObject.GetComponent<RectTransform>().localScale = new Vector3(buttonPressScale, buttonPressScale, buttonPressScale);
		}
		else
		{
			buttonOff[2].transform.GetChild(0).gameObject.GetComponent<RectTransform>().localScale = new Vector3(buttonDefaultScale, buttonDefaultScale, buttonDefaultScale);
		}

        if (PlayerStats.walk.objectInHand == "Scooter") buttonOff[3].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = zaHandoStuff[1];
		else buttonOff[3].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = zaHandoStuff[0];

		if (SimpleInput.GetKey(KeyCode.Q))
		{
			buttonOff[3].transform.GetChild(0).gameObject.GetComponent<RectTransform>().localScale = new Vector3(buttonPressScale, buttonPressScale, buttonPressScale);
		}
		else
		{
			buttonOff[3].transform.GetChild(0).gameObject.GetComponent<RectTransform>().localScale = new Vector3(buttonDefaultScale, buttonDefaultScale, buttonDefaultScale);
		}

		if (SimpleInput.GetKey(KeyCode.F))
		{
			buttonOff[4].transform.GetChild(0).gameObject.GetComponent<RectTransform>().localScale = new Vector3(bottomPressScale, bottomPressScale, bottomPressScale);
		}
		else
		{
			buttonOff[4].transform.GetChild(0).gameObject.GetComponent<RectTransform>().localScale = new Vector3(bottomDefaultScale, bottomDefaultScale, bottomDefaultScale);
		}

		//dpadButtons[0].mo	
	}
}
