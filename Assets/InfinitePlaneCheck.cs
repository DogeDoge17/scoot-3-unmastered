using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinitePlaneCheck : MonoBehaviour {

	public bool entered = false;
	public bool left = false;
	public bool inside = false;

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject == GameObject.FindGameObjectWithTag("Event System").GetComponent<PlayerStats>().activePlayer)
		{
			left = false;
			entered = true;
		}
	}

	void OnCollisionStay(Collision collision)
	{
		if (collision.gameObject == GameObject.FindGameObjectWithTag("Event System").GetComponent<PlayerStats>().activePlayer)
		{
			inside = true;
		}
	}

	void OnCollisionExit(Collision collision)
	{
		if (collision.gameObject == GameObject.FindGameObjectWithTag("Event System").GetComponent<PlayerStats>().activePlayer)
		{
			entered = false;
			left = true;
			inside = false;
		}
	}
}
