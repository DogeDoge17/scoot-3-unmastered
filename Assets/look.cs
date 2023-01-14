using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class look : MonoBehaviour
{
    public Transform target;
    public GameObject theCamera;

    public bool lookAtPlayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!lookAtPlayer)
            theCamera.transform.LookAt(target.position);
        else
            theCamera.transform.LookAt(GameObject.FindGameObjectWithTag("Event System").GetComponent<PlayerStats>().activePlayer.transform.position);
    }
}
