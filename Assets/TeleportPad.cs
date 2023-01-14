using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPad : MonoBehaviour
{
    public TelepadCheck pad1;
    public TelepadCheck pad2;

    AudioSource dbzTeleSound;


    public float teleportTimer = 10f;
    public float teleportInterval = 10f;

    void Awake()
    {
        dbzTeleSound = GetComponent<AudioSource>(); 
    }

    // Update is called once per frame
    void Update()
    {
        teleportTimer -= 1 * Time.deltaTime;

        if (pad1.entered /*&& pad2.left*/  && teleportTimer < 0)
        {
            dbzTeleSound.Play();

            teleportTimer = teleportInterval;
            GameObject.FindGameObjectWithTag("Event System").GetComponent<PlayerStats>().activePlayer.transform.position = new Vector3(pad2.gameObject.transform.position.x, pad2.gameObject.transform.position.y + 1, pad2.gameObject.transform.position.z);
            GameObject.FindGameObjectWithTag("Event System").GetComponent<PlayerStats>().activePlayer.GetComponent<Rigidbody>().velocity = new Vector3();

            pad1.entered = false;
        }
        if (pad2.entered /*&& pad2.left*/  && teleportTimer < 0)
        {
            dbzTeleSound.Play();

            teleportTimer = teleportInterval;

            GameObject.FindGameObjectWithTag("Event System").GetComponent<PlayerStats>().activePlayer.transform.position = new Vector3(pad1.gameObject.transform.position.x, pad1.gameObject.transform.position.y + 1, pad1.gameObject.transform.position.z);

            pad2.entered = false;
        }


    }
}
