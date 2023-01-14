using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class vitaInputTest : MonoBehaviour {

	Text text;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.JoystickButton0)) text.text = "0";
		if(Input.GetKeyDown(KeyCode.JoystickButton1)) text.text = "1";
		if(Input.GetKeyDown(KeyCode.JoystickButton2)) text.text = "2";
		if(Input.GetKeyDown(KeyCode.JoystickButton3)) text.text = "3";
		if(Input.GetKeyDown(KeyCode.JoystickButton4)) text.text = "4";
		if(Input.GetKeyDown(KeyCode.JoystickButton5)) text.text = "5";
		if(Input.GetKeyDown(KeyCode.JoystickButton6)) text.text = "6";
		if(Input.GetKeyDown(KeyCode.JoystickButton7)) text.text = "7";
		if(Input.GetKeyDown(KeyCode.JoystickButton8)) text.text = "8";
		if(Input.GetKeyDown(KeyCode.JoystickButton9)) text.text = "9";
		if(Input.GetKeyDown(KeyCode.JoystickButton10)) text.text = "10";
		if(Input.GetKeyDown(KeyCode.JoystickButton11)) text.text = "11";
		if(Input.GetKeyDown(KeyCode.JoystickButton12)) text.text = "12";
		if(Input.GetKeyDown(KeyCode.JoystickButton13)) text.text = "13";
		if(Input.GetKeyDown(KeyCode.JoystickButton14)) text.text = "14";
		if(Input.GetKeyDown(KeyCode.JoystickButton15)) text.text = "15";
		if(Input.GetKeyDown(KeyCode.JoystickButton16)) text.text = "16";
		if(Input.GetKeyDown(KeyCode.JoystickButton17)) text.text = "17";
		if(Input.GetKeyDown(KeyCode.JoystickButton18)) text.text = "18";
		if(Input.GetKeyDown(KeyCode.JoystickButton19)) text.text = "19";


	}
}
