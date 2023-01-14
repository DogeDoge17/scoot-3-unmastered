using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelepadCheck : MonoBehaviour {

	public bool entered = false;
	public bool left = false;


	void OnCollisionEnter(Collision collision)
    {
		if (collision.gameObject == GameObject.FindGameObjectWithTag("Event System").GetComponent<PlayerStats>().activePlayer)
		{
			left = false;
			entered = true;
		}
	}

	void OnCollisionExit(Collision collision)
	{
		if (collision.gameObject == GameObject.FindGameObjectWithTag("Event System").GetComponent<PlayerStats>().activePlayer)
		{
			entered = false;
			left = true;
		}
	}
}
