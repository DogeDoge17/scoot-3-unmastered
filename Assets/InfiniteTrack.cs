using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfiniteTrack : MonoBehaviour
{
    public InfinitePlaneCheck startingCheck;
    public InfinitePlaneCheck check1;
    public InfinitePlaneCheck check2;
    public InfinitePlaneCheck check3;
    [Space]
    public float moveAmount = 2.16089f;
    [Space]
    public GameObject theCanvas;
    public GameObject distancePoint;
    [Space]
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI distaceText;

    public float uiTurnOffTimer = -0.1f;

    // public InfiniteUiCheck ui1;
    //public InfiniteUiCheck ui2;


    void Awake()
    {
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (startingCheck.entered)
        {

            check1.transform.position = new Vector3(startingCheck.gameObject.transform.position.x, startingCheck.gameObject.transform.position.y, startingCheck.gameObject.transform.position.z + moveAmount);
            startingCheck.entered = false;
        }

        if (check1.entered)
        {
            check2.transform.position = new Vector3(check1.gameObject.transform.position.x, check1.gameObject.transform.position.y, check1.gameObject.transform.position.z + moveAmount);
            check1.entered = false;
        }

        if (check2.entered)
        {
            check3.transform.position = new Vector3(check2.gameObject.transform.position.x, check2.gameObject.transform.position.y, check2.gameObject.transform.position.z + moveAmount);
            check2.entered = false;
        }
        if (check3.entered)
        {
            check1.transform.position = new Vector3(check3.gameObject.transform.position.x, check3.gameObject.transform.position.y, check3.gameObject.transform.position.z + moveAmount);
            check3.entered = false;
        }



        if (check1.inside || check2.inside || check3.inside)
        {
            uiTurnOffTimer = 3;

            if (GameObject.FindGameObjectWithTag("Event System").GetComponent<PlayerStats>().activePlayer.GetComponent<Rigidbody>().velocity.magnitude <= 255)
                speedText.text = "<color=#" + ColorUtility.ToHtmlStringRGB(new Color32(255, (byte)(255 - GameObject.FindGameObjectWithTag("Event System").GetComponent<PlayerStats>().activePlayer.GetComponent<Rigidbody>().velocity.magnitude), (byte)(255 - GameObject.FindGameObjectWithTag("Event System").GetComponent<PlayerStats>().activePlayer.GetComponent<Rigidbody>().velocity.magnitude), 1)) + ">" + Mathf.RoundToInt(GameObject.FindGameObjectWithTag("Event System").GetComponent<PlayerStats>().activePlayer.GetComponent<Rigidbody>().velocity.magnitude) + "mph";
            else
                speedText.text = "<color=#" + ColorUtility.ToHtmlStringRGB(new Color32(255, 0, 0, 1)) + ">" + Mathf.RoundToInt(GameObject.FindGameObjectWithTag("Event System").GetComponent<PlayerStats>().activePlayer.GetComponent<Rigidbody>().velocity.magnitude) + "mph";

            distaceText.text = Mathf.Floor(Vector3.Distance(GameObject.FindGameObjectWithTag("Event System").GetComponent<PlayerStats>().activePlayer.transform.position, distancePoint.transform.position)) + " feet";
            theCanvas.SetActive(true);
        }
        else if (uiTurnOffTimer >= 0)
        {
            uiTurnOffTimer -= Time.deltaTime;


            if (GameObject.FindGameObjectWithTag("Event System").GetComponent<PlayerStats>().activePlayer.GetComponent<Rigidbody>().velocity.magnitude <= 255)
                speedText.text = "<color=#" + ColorUtility.ToHtmlStringRGB(new Color32(255, (byte)(255 - GameObject.FindGameObjectWithTag("Event System").GetComponent<PlayerStats>().activePlayer.GetComponent<Rigidbody>().velocity.magnitude), (byte)(255 - GameObject.FindGameObjectWithTag("Event System").GetComponent<PlayerStats>().activePlayer.GetComponent<Rigidbody>().velocity.magnitude), 1)) + ">" + Mathf.RoundToInt(GameObject.FindGameObjectWithTag("Event System").GetComponent<PlayerStats>().activePlayer.GetComponent<Rigidbody>().velocity.magnitude) + "mph";
            else
                speedText.text = "<color=#" + ColorUtility.ToHtmlStringRGB(new Color32(255, 0, 0, 1)) + ">" + Mathf.RoundToInt(GameObject.FindGameObjectWithTag("Event System").GetComponent<PlayerStats>().activePlayer.GetComponent<Rigidbody>().velocity.magnitude) + "mph";

            distaceText.text = Mathf.Floor(Vector3.Distance(GameObject.FindGameObjectWithTag("Event System").GetComponent<PlayerStats>().activePlayer.transform.position, distancePoint.transform.position)) + " feet";
        }
        else
        {
            theCanvas.SetActive(false);
        }

    }
}