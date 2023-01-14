using UnityEngine;

public class HeadHurt : MonoBehaviour
{

    private scoot theScoot;
    private Rigidbody rb;
    private PlayerStats stats;

    [SerializeField]
    private AudioSource headCrack;

    void Awake()
    {
        theScoot = GameObject.FindGameObjectWithTag("Player").GetComponent<scoot>();
        rb = theScoot.gameObject.GetComponent<Rigidbody>();
        stats = GameObject.FindGameObjectWithTag("Event System").GetComponent<PlayerStats>();

    }

    void OnTriggerEnter()
    {        
        if (rb.velocity.y > 3 || rb.velocity.y < -3)
        {
            stats.health = -10;
            headCrack.Play();
        }
    }
}
