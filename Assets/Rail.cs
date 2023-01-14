using System;
using UnityEngine;

public class Rail : MonoBehaviour
{

    public string grindAxis = "z";

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Math.Max(Math.Max(this.transform.localScale.x, this.transform.localScale.y), this.transform.localScale.z) == this.transform.localScale.z)
        {
            grindAxis = "z";

        }
        else if(Math.Max(Math.Max(this.transform.localScale.x, this.transform.localScale.y), this.transform.localScale.z) == this.transform.localScale.x)
        {
            grindAxis = "x";
        }
        else if (Math.Max(Math.Max(this.transform.localScale.x, this.transform.localScale.y), this.transform.localScale.z) == this.transform.localScale.y)
        {
            grindAxis = "y";
        }

        



    }
}
