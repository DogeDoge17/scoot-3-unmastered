using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetTrophy : MonoBehaviour {

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject == GameObject.FindGameObjectWithTag("Event System").GetComponent<PlayerStats>().activePlayer)
		{
			GameObject.FindGameObjectWithTag("Event System").GetComponent<Settings>().UnlockTrophy(3);
		}
	}

}
