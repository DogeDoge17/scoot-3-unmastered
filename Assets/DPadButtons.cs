using UnityEngine;

public class DPadButtons : MonoBehaviour
{
    private float dpadX;
    private float dpadY;
    private bool leftDpadPressed;
    private bool rightDpadPressed;
    private bool upDpadPressed;
    private bool downDpadPressed;
    private bool currentlyReleased;

    public bool IsLeft, IsRight, IsUp, IsDown;

    private void Start()
    {
        currentlyReleased = true;
    }

    private void Update()
    {
        dpadX = Input.GetAxis("DPad X");
        dpadY = Input.GetAxis("DPad Y");



        if (dpadX == -1)
        {
            leftDpadPressed = true;
            if (leftDpadPressed && currentlyReleased)
            {
                //Fire events
                IsLeft = true ;
               // print("LEFT");
            }
            else
            {
                IsLeft = false ;
            }

            currentlyReleased = false;
        }
        if (dpadX == 1)
        {
            rightDpadPressed = true;
            if (rightDpadPressed && currentlyReleased)
            {
                //Fire events
                IsRight = true;
                //print("RIGHT");
            }
            else
            {
                IsRight = false;
            }
            currentlyReleased = false;
        }
        if (dpadY == -1)
        {
            downDpadPressed = true;
            if (downDpadPressed && currentlyReleased)
            {
                //Fire events
                IsDown = true;
                //print("DOWN");
            }
            else
            {
                IsDown = false;
            }
            currentlyReleased = false;
        }
        if (dpadY == 1)
        {
            upDpadPressed = true;
            if (upDpadPressed && currentlyReleased)
            {
                //Fire events
                IsUp = true;
                //print("UP");
            }
            else
            {
                IsUp = false;
            }
            currentlyReleased = false;
        }
        if (dpadY == 0 && dpadX == 0)
        {
            upDpadPressed = false;
            downDpadPressed = false;
            leftDpadPressed = false;
            rightDpadPressed = false;
            currentlyReleased = true;
        }

    }

}
