using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class linkPlayer : MonoBehaviour {

	public Transform pastParent;


	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject == GameObject.FindGameObjectWithTag("Event System").GetComponent<PlayerStats>().activePlayer)
		{
			//pastParent = GameObject.FindGameObjectWithTag("Event System").GetComponent<PlayerStats>().activePlayer.transform.parent;

			GameObject.FindGameObjectWithTag("Event System").GetComponent<PlayerStats>().activePlayer.transform.SetParent(transform);
			GameObject.FindGameObjectWithTag("Event System").GetComponent<PlayerStats>().scootr.gameObject.transform.SetParent(transform);
			GameObject.FindGameObjectWithTag("Event System").GetComponent<PlayerStats>().walk.gameObject.transform.SetParent(transform);
		}
	}

	void OnCollisionExit(Collision collision)
	{
		if (collision.gameObject == GameObject.FindGameObjectWithTag("Event System").GetComponent<PlayerStats>().activePlayer)
		{
			GameObject.FindGameObjectWithTag("Event System").GetComponent<PlayerStats>().activePlayer.transform.SetParent(pastParent);
			GameObject.FindGameObjectWithTag("Event System").GetComponent<PlayerStats>().scootr.gameObject.transform.SetParent(pastParent);
			GameObject.FindGameObjectWithTag("Event System").GetComponent<PlayerStats>().walk.gameObject.transform.SetParent(pastParent);
		}
	}
}
