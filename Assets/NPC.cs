using UnityEngine;
using UnityEngine.AI;


public class NPC : MonoBehaviour
{
    [HideInInspector]
    public int arrayIdx = 0;
    [HideInInspector]
    public string PersonType = "Black Male";

    public bool female = false;

    public float walkingSpeed = 2;
    public float runningSpeed = 5;
    [Space]

    public bool randomHeight = true;
    public float minRndHeight = 0.7f;
    public float maxRndHeight = 1.2f;

    private scoot playerScoot;

    private Quaternion defaultRotation;

    [SerializeField]
    private bool knockedDown = false;

    private Rigidbody rb;

    private float recoverTimer = 5f;

    private float anger = 0;
    public float patience = 3;

    private float attackTimer;
    public float attackCoolDown = 2f;

    private GameObject taserPrefab;
    private GameObject taser;

    private float freezeTimer = 0f;
    private bool freezeOn = false;

    public float strength = 5;

    private PlayerStats stats;

    private AudioSource[] attackSound;
    private AudioSource[] angerStartSound;
    private AudioSource[] fallSound;
    private AudioSource[] angeredFallSound;
    private AudioSource[] recoverSound;
    private AudioSource[] killSound;

    private bool originallyFallen = false;
    private bool recoverSoundPlayed = false;

    private bool angerStart = false;

    private NavMeshAgent agent;

    private Transform ogPosition;

    private float angerResetTimer;

    private float changeWanderPointTimer;

    public bool customNavMeshHeightOffset;
    public float navMeshHeightOffset;
    private Settings settings;

    private void Awake()
    {
        if (female) taserPrefab = GameObject.FindGameObjectWithTag("Taser");

        if (randomHeight) this.transform.localScale = new Vector3(this.transform.localScale.x, Random.Range(minRndHeight, maxRndHeight), this.transform.localScale.z);

        settings = GameObject.FindGameObjectWithTag("Event System").GetComponent<Settings>();
        this.gameObject.AddComponent<NavMeshAgent>();
        agent = GetComponent<NavMeshAgent>();
        attackSound = GameObject.FindGameObjectWithTag("Attack Sound").GetComponents<AudioSource>();
        angerStartSound = GameObject.FindGameObjectWithTag("Start Anger Sounds").GetComponents<AudioSource>();
        fallSound = GameObject.FindGameObjectWithTag("Fall Sound").GetComponents<AudioSource>();
        angeredFallSound = GameObject.FindGameObjectWithTag("Angered Fall Sound").GetComponents<AudioSource>();
        recoverSound = GameObject.FindGameObjectWithTag("Recover Sounds").GetComponents<AudioSource>();
        killSound = GameObject.FindGameObjectWithTag("Kill Player Sounds").GetComponents<AudioSource>();
        ogPosition = this.transform;
        stats = GameObject.FindGameObjectWithTag("Event System").GetComponent<PlayerStats>();
        attackTimer = attackCoolDown;
        rb = GetComponent<Rigidbody>();
        defaultRotation = this.transform.rotation;
        playerScoot = GameObject.FindGameObjectWithTag("Player").GetComponent<scoot>();
    }



    // Start is called before the first frame update
    void Start()
    {


        if (female) taser = Instantiate(taserPrefab, new Vector3(), new Quaternion(), this.transform);
        // taserPrefab.transform.localPosition = new Vector3(0.919f, 2.68f, 0.853f);
        agent.stoppingDistance = 3;
        agent.speed = walkingSpeed;
        if (customNavMeshHeightOffset)
        {
            agent.baseOffset = navMeshHeightOffset;
        }
        LateStart();
    }

    void LateStart()
    {
        try
        {
            taser.transform.localPosition = new Vector3(0.919f, 2.68f, 0.853f);
        }
        catch
        {

        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == stats.activePlayer)
        {
            if (!knockedDown)
            {
                if (anger >= patience) angeredFallSound[RandomInt(0, angeredFallSound.Length - 1)].Play();
                else fallSound[RandomInt(0, fallSound.Length - 1)].Play();

                agent.enabled = false;
                rb.isKinematic = false;
                rb.AddRelativeForce((stats.activePlayer.transform.right * -1) * playerScoot.pushForce * RandomInt(2, 3), ForceMode.Impulse);

            }
            originallyFallen = true;
            knockedDown = true;
            recoverSoundPlayed = false;
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;

        randomDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);

        return navHit.position;
    }

    int RandomInt(int min, int max) { return Random.Range(min, max + 1); }



    // Update is called once per frame
    void Update()
    {
        if (!settings.settingsOpen)
        {
            if (anger >= patience)
            {
                if (!angerStart)
                {
                    angerStartSound[RandomInt(0, angerStartSound.Length - 1)].Play();
                    recoverSoundPlayed = true;
                    stats.threatNumber++;
                }

                if (female)
                {
                    taser.SetActive(true);
                }

                angerStart = true;
                Angered();
                if (Vector3.Distance(this.transform.position, stats.activePlayer.transform.position) > 25 || stats.health <= 0)
                {
                    StopAnger();
                }
            }
            else
            {
                if (female)
                {
                    taser.SetActive(false);
                }
            }


            if (recoverTimer <= 0)
            {
                knockedDown = false;
                anger++;
            }

            if (Vector3.Distance(stats.activePlayer.transform.position, this.transform.position) >= 3 || knockedDown)
            {
                this.transform.GetChild(0).transform.localRotation = defaultRotation;
            }
            else
            {
            }

            if (Vector3.Distance(stats.activePlayer.transform.position, this.transform.position) < 3 && !knockedDown)
            {
                this.transform.GetChild(0).transform.LookAt(stats.activePlayer.transform.position * -1);
            }
            else if (!knockedDown)
            {
                this.transform.rotation = defaultRotation;
                this.transform.GetChild(0).transform.rotation = defaultRotation;

            }

            if (knockedDown)
            {
                agent.enabled = false;
                rb.isKinematic = false;

                recoverTimer -= 1 * Time.deltaTime;
                //this.transform.rotation = new Quaternion(90,this.transform.localRotation.y, transform.localRotation.z, transform.localRotation.w);
            }
            else
            {
                if (!recoverSoundPlayed && originallyFallen && anger != patience)
                {
                    recoverSound[RandomInt(0, recoverSound.Length - 1)].Play();
                }
                agent.enabled = true;
                recoverSoundPlayed = true;
                recoverTimer = 5;
                rb.isKinematic = true;
            }

            changeWanderPointTimer -= 1 * Time.deltaTime;

            if (changeWanderPointTimer <= 0 && !knockedDown && anger < patience)
            {
                SafeSetDestinationSet(RandomNavSphere(this.transform.position, 30, -1));
                // agent.destination = RandomNavSphere(this.transform.position, 30, -1);
                changeWanderPointTimer = RandomInt(1, 30);
                agent.speed = walkingSpeed;
            }

            if (angerResetTimer > 0 && angerResetTimer != 900.23f)
            {
                angerResetTimer -= 1 * Time.deltaTime;
            }

            if (angerResetTimer <= 0)
            {
                angerResetTimer = 900.23f;
            }

            if (angerResetTimer == 900.23f)
            {
                this.transform.position = ogPosition.position;
                this.transform.rotation = ogPosition.rotation;
                this.transform.localScale = ogPosition.localScale;
            }

            //this.transform.Translate(Player.transform.position * Time.deltaTime);

            freezeTimer -= 1 * Time.deltaTime;

            if (freezeTimer <= 0 && freezeOn)
            {
                freezeOn = false;
                playerScoot.freezeAmount--;
            }
        }
        //Debug.Log(Vector3.Distance(Player.transform.position, me.transform.position));
    }

    void StopAnger()
    {
        anger = 0;
        angerStart = false;
        stats.threatNumber--;
        playerScoot.freezeAmount--;
        freezeOn = false;
        freezeTimer = 0;
        BrokeyAngerStop();
    }

    void BrokeyAngerStop()
    {
        agent.Stop();
        agent.isStopped = true;
        SafeSetDestinationSet(ogPosition.position);
        //agent.destination = ogPosition.position;
        this.transform.rotation = ogPosition.rotation;
        angerResetTimer = 5;
    }

    void AttackPlayer()
    {
        if (!knockedDown && stats.health > 0 && !settings.settingsOpen)
        {
            attackTimer = attackCoolDown;
            stats.health -= strength;
            stats.HurtPlayer();

            if (female)
            {
                if (!freezeOn)
                    playerScoot.freezeAmount += 1;
                freezeTimer = 4;
                freezeOn = true;
            }
            if (stats.health <= 0)
            {
                killSound[RandomInt(0, killSound.Length - 1)].Play();
            }
            else
            {
                attackSound[RandomInt(0, attackSound.Length - 1)].Play();
            }
        }
        else
        {
            return;
        }

    }

    void SafeSetDestinationSet(Vector3 destination)
    {
        agent.enabled = true;
        rb.isKinematic = true;
        agent.SetDestination(destination);

        //agent.destination = destination;
    }

    void Angered()
    {
        agent.speed = runningSpeed;

        settings.UnlockTrophy(4);

        if (!knockedDown) { SafeSetDestinationSet(stats.activePlayer.transform.position); this.transform.LookAt(stats.activePlayer.transform); }

        if (Vector3.Distance(this.transform.position, stats.activePlayer.transform.position) <= 3.5)
        {

            if (attackTimer <= 0)
            {

                AttackPlayer();
            }
        }

        if (attackTimer != 0)
        {
            attackTimer -= 1 * Time.deltaTime;
        }
    }

}