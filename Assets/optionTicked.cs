using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class optionTicked : MonoBehaviour {

	public Image imageee;

	public bool toggled;

	public Sprite checkedSprite;
	public Sprite uncheckedSprite;

	// Update is called once per frame
	void Update () {

        if (toggled)
        {
			imageee.sprite = checkedSprite;
        }
        else
        {
			imageee.sprite = uncheckedSprite;
		}
	}
}
