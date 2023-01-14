using UnityEngine;

public class groundCheck : MonoBehaviour
{
    public scoot scootOwO;
    public Walk walkOwO;

    public bool walk;

    private void OnTriggerStay(Collider other)
    {
        //if(other.gameObject.transform.position.y/* < scootOwO.rigidbody.gameObject.transform.position.y*/)

        //if (walk)
            //walkOwO.isGrounded = true;

        if (!walk)
        {
            scootOwO.isGrounded = true;

            if (other.gameObject.transform.rotation.x > 0 && other.gameObject.transform.localRotation.x < 90 && other.gameObject.layer != 8)
            {
                scootOwO.onRamp = true;
                scootOwO.rampAngle = other.gameObject.transform.rotation.x;
            }
            else if (other.gameObject.transform.rotation.x < 0)
            {
                if (other.gameObject.transform.rotation.x < 0 && other.gameObject.transform.rotation.x > -90)
                {
                    scootOwO.onRamp = true;
                    scootOwO.rampAngle = other.gameObject.transform.rotation.x;
                }
            }
            else
            {
                scootOwO.onRamp = false;
            }

            if (other.gameObject.GetComponent<Rail>() != null)
            {
                scootOwO.onRail = true;
            }
            else
            {
                scootOwO.onRail = false;
            }
        }
      //  Debug.Log("Hit uwa");
    }

    private void OnTriggerExit(Collider other)
    {

        if (!walk)
        {
            scootOwO.isGrounded = false;
            scootOwO.onRamp = false;
        }else
        {
            //walkOwO.isGrounded = false;
        }
    }
}
