using UnityEngine;
using UnityEngine.UI;


public class PlayerStats : MonoBehaviour
{


    public float health = 20;
    private float maxHealth = 20;
    public float voidOutHeight = 20;

    //[HideInInspector]
    public int threatNumber = 0;

    public Image[] healthFill;
    [Space]
    private bool threatened = false;
    private bool introedThreat = false;
    private bool outroedThreat = true;
    [Space]
    public Animator heartAnimator;
    public Animator graditentAnimator;

    public GameObject deathSounds;
    public GameObject hurtSounds;

    private AudioSource[] deathAudio;
    private AudioSource[] hurtAudio;

    public GameObject blackoutScreen;

    public Rigidbody scootRigidbody;

    private Transform startingTransform;

    private float reviveTimer;
    private bool timerEnded = false;
    [HideInInspector]
    public bool deathStarted = false;

    public bool frozen;
    public int resetCount;

    public bool onScooter = true;

    [HideInInspector]
    public scoot scootr;
    public Walk walk;

    [Space]
    public GameObject randomScooter;
    public GameObject activePlayer;
    public GameObject playerS;
    public GameObject playerW;

    public bool isGrounded;

    void Awake()
    {
        if (ES3.KeyExists("Reset Count")) resetCount = ES3.Load<int>("Reset Count");

        playerS = GameObject.FindGameObjectWithTag("Player");
        playerW = GameObject.FindGameObjectWithTag("Player Off");

        voidOutHeight *= -1;
        maxHealth = health;
        deathAudio = deathSounds.GetComponents<AudioSource>();
        hurtAudio = hurtSounds.GetComponents<AudioSource>();
        startingTransform = scootRigidbody.transform;
        scootr = scootRigidbody.gameObject.GetComponent<scoot>();
    }

    // Use this for initialization
    void Start()
    {

    }

    public void HurtPlayer()
    {
        hurtAudio[Random.Range(0, hurtAudio.Length)].Play();
    }

    // Update is called once per frame
    void Update()
    {

        if (onScooter)
        {
            activePlayer = playerS;
            isGrounded = scootr.isGrounded;
        }
        else
        {
            activePlayer = playerW;
            isGrounded = walk.isGrounded;
        }

        if (activePlayer.transform.position.y < voidOutHeight) health = -10;

        randomScooter.SetActive(!onScooter);

        if ((SimpleInput.GetKeyDown(KeyCode.F) || SimpleInput.GetKeyDown(KeyCode.JoystickButton5)) && health > 0)
        {
            onScooter = !onScooter;

            if (onScooter)
            {
                scootr.gameObject.SetActive(true);
                randomScooter.SetActive(false);
                scootr.gameObject.transform.localPosition = new Vector3(walk.transform.localPosition.x, walk.transform.localPosition.y + .5f, walk.transform.localPosition.z);
                scootr.gameObject.transform.localRotation = walk.gameObject.transform.localRotation;
                scootr.gameObject.GetComponent<Rigidbody>().velocity = walk.gameObject.GetComponent<Rigidbody>().velocity;
                walk.gameObject.SetActive(false);
            }
            else
            {
         
                randomScooter.SetActive(true);
                walk.gameObject.transform.localPosition = new Vector3(scootr.gameObject.transform.localPosition.x, scootr.gameObject.transform.localPosition.y + .1f, scootr.gameObject.transform.localPosition.z);
                walk.gameObject.SetActive(true);
                // walk.gameObject.transform.rotation = new Quaternion(walk.gameObject.transform.rotation.x, scootr.gameObject.transform.rotation.y, walk.gameObject.transform.rotation.z, walk.gameObject.transform.rotation.w);
                walk.gameObject.transform.localRotation = scootr.gameObject.transform.localRotation;
                randomScooter.transform.localPosition = new Vector3(scootr.gameObject.transform.localPosition.x, scootr.gameObject.transform.localPosition.y + 2, scootr.gameObject.transform.localPosition.z + 1f);
                scootr.gameObject.SetActive(false);
            }

        }

        foreach (Image img in healthFill)
        {
            img.fillAmount = (health / 100) * 5;
        }

        if (threatNumber > 0)
        {
            threatened = true;
        }
        else
        {
            introedThreat = false;
            threatened = false;
            if (!outroedThreat)
            {
                heartAnimator.Play("Leave");
                graditentAnimator.Play("Fade Out");
                outroedThreat = true;
            }
        }

        if (threatened)
        {
            if (!introedThreat)
            {
                heartAnimator.Play("Enter");
                graditentAnimator.Play("Fade In");
                introedThreat = true;
                outroedThreat = false;
            }
        }

        if (!deathStarted && health <= maxHealth)
            health += 0.1f * Time.deltaTime;

        if (health > maxHealth)
            health = maxHealth;

        if (reviveTimer > 0)
        {
            reviveTimer -= 1 * Time.deltaTime;
            //countDownBegun = true;
        }

        if (reviveTimer <= 0 && deathStarted)
        {
            timerEnded = true;
        }

        if (timerEnded)
        {
            blackoutScreen.SetActive(false);
            scootRigidbody.gameObject.transform.position = startingTransform.position;
            scootRigidbody.gameObject.transform.rotation = startingTransform.rotation;
            scootRigidbody.velocity = new Vector3();
            scootRigidbody.angularVelocity = new Vector3(0, 0, 0);
            if (onScooter)
                scootr.Reset();
            else
                walk.Reset();
            timerEnded = false;
            reviveTimer = 69;
            health = 20;
            deathStarted = false;
        }

        if (health <= 0 && !deathStarted)
        {
            reviveTimer = 5;
            deathStarted = true;
            deathAudio[Random.Range(0, deathAudio.Length)].Play();
            blackoutScreen.SetActive(true);
        }
    }
}
