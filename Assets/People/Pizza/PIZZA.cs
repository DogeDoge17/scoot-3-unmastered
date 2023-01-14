using UnityEngine;
using UnityEngine.AI;

public class PIZZA : MonoBehaviour
{


    public GameObject pizzaBox;
    Rigidbody rb;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == GameObject.FindGameObjectWithTag("Event System").GetComponent<PlayerStats>().activePlayer)
        {

            GetComponent<NavMeshAgent>().enabled = false;
            pizzaBox.SetActive(true);
            rb.isKinematic = false;
            pizzaBox.transform.position = this.transform.position;
            transform.position =  new Vector3(0,-50,0); 
            this.gameObject.SetActive(false);
        }
    }

    // Use this for initialization
    void Start()
    {
        rb = pizzaBox.GetComponent<Rigidbody>();
    }
}
