using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPad : MonoBehaviour
{
    GameObject player;
    scoot playerSCOOT;

    public float speedChange = 10f;
    float startingChange;

    // Start is called before the first frame update
    void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player");
        playerSCOOT = FindObjectOfType<scoot>();
        startingChange = playerSCOOT.pushForce;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == player)
        {           
            playerSCOOT.pushForce = speedChange;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject == player)
        {           
            playerSCOOT.pushForce = startingChange;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
