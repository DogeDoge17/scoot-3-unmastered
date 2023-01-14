using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteUiCheck : MonoBehaviour
{

    public bool inside;

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.tag);

        if (other.tag == "Player" || other.tag == "Player Off")
            inside = true;
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Out");

        //  Debug.Log(other.Tag);

        if (other.tag == "Player" || other.tag == "Player Off")
            inside = false;
    }
}
