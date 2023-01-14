using SimpleInputNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;


public class scoot : MonoBehaviour
{
    public Rigidbody rigidbody;

    public float pushForce = 5f;
    public float verticalForce = 5f;
    public float turnForce = 5f;
    public float jumpForce = 2f;
    [Space]
    private Vector3 staringPosition;
    private Quaternion startingRotation;
    
    private Animator animator;

    public bool isGrounded = true;
    //[HideInInspector]
    public bool onRamp;
    [Space]
    public float scootTime = 0.45f;
    private float scootTimer;

    [HideInInspector]
    public float rampAngle;

    //[HideInInspector]
    public bool onRail;

    DPadButtons DPad;

  
    //[HideInInspector]
    public float freezeAmount = 0;


    private float lThumbX;
    private float lThumbY;

    private PlayerStats playerStats;

    public ParticleSystem zappy;
    public ParticleSystem skirr;
    private bool zappyPlaying = false;

    [SerializeField]
    private float pushTimer = 0;
    public float pushInterval = 0.4f;

    private float resetMapTimer = 1.5f;
    public float resetMapHoldTime = 1.5f;
    public bool resetBruh = true;

    private Quaternion lastPushRotation;

    private Settings settings;
    private MobileControls mobileControls;

    void Awake()
    {
        //zappy.Stop();
        settings = GameObject.FindGameObjectWithTag("Event System").GetComponent<Settings>();
        DPad = GetComponent<DPadButtons>();
        animator = rigidbody.gameObject.GetComponent<Animator>();
        staringPosition = rigidbody.gameObject.transform.position;
        startingRotation = rigidbody.gameObject.transform.rotation;
        scootTimer = scootTime;
        playerStats = GameObject.FindGameObjectWithTag("Event System").GetComponent<PlayerStats>();
        mobileControls = GameObject.FindGameObjectWithTag("Mobile Controls").GetComponent<MobileControls>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!playerStats.onScooter)
            gameObject.SetActive(false);

        // Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {

        if (isGrounded && !onRamp)
        {
            rigidbody.constraints = RigidbodyConstraints.FreezeRotationX;
            rigidbody.gameObject.transform.rotation = Quaternion.Euler(rigidbody.gameObject.transform.eulerAngles.x, rigidbody.gameObject.transform.eulerAngles.y, 0);
        }
        else if (onRamp)
        {
            rigidbody.constraints = RigidbodyConstraints.None;
            if (rampAngle < 0)
                rigidbody.gameObject.transform.rotation = new Quaternion(rigidbody.gameObject.transform.rotation.x, rigidbody.gameObject.transform.rotation.y, Mathf.Clamp(rigidbody.gameObject.transform.rotation.z, rampAngle / 11, (rampAngle / 11) * -1), rigidbody.gameObject.transform.rotation.w);
            else
                rigidbody.gameObject.transform.rotation = new Quaternion(rigidbody.gameObject.transform.rotation.x, rigidbody.gameObject.transform.rotation.y, Mathf.Clamp(rigidbody.gameObject.transform.rotation.z, (rampAngle / 11) * -1, rampAngle / 11), rigidbody.gameObject.transform.rotation.w);
        }
        else
        {
            rigidbody.constraints = RigidbodyConstraints.None;
        }
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.gameObject.transform.rotation = Quaternion.Euler(0, rigidbody.gameObject.transform.eulerAngles.y, rigidbody.gameObject.transform.eulerAngles.z);

        if ((Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.IPhonePlayer && Application.platform != RuntimePlatform.BlackBerryPlayer) && !mobileControls.forceShow)
            lThumbX = Input.GetAxisRaw("Left Thumbstick X");
        else
            lThumbX = SimpleInput.GetAxisRaw("Horizontal");

        if ((Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.IPhonePlayer && Application.platform != RuntimePlatform.BlackBerryPlayer) && !mobileControls.forceShow)
            lThumbY = Input.GetAxisRaw("Left Thumbstick Y");
        else
            lThumbY = SimpleInput.GetAxisRaw("Vertical");

        pushTimer -= 1 * Time.deltaTime;

        if (playerStats.health > 0)
        {
            if (!playerStats.frozen && !settings.settingsOpen)
            {
                int verticalCameraInvert = 1;

                //Debug.Log(Vector3.Distance(new Vector3(lastPushRotation.x, lastPushRotation.y, lastPushRotation.z), new Vector3(transform.localRotation.x, transform.localRotation.y, transform.localRotation.z)));

               // rigidbody.velocity = Vector3.MoveTowards(rigidbody.velocity, new Vector3(0,0,0), Vector3.Distance(new Vector3(lastPushRotation.x, lastPushRotation.y, lastPushRotation.z), new Vector3(transform.localRotation.x, transform.localRotation.y, transform.localRotation.z)) * Time.deltaTime);

                if (settings.verticalCameraInverted)
                    verticalCameraInvert = -1;

                if (rigidbody.velocity.magnitude >= 255)
                    settings.UnlockTrophy(1);

                //push forward
                if (((SimpleInput.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton2)) && isGrounded) && !settings.holdScoot)
                {
                    rigidbody.AddForce(rigidbody.gameObject.transform.right * -1 * pushForce, ForceMode.Impulse);
                    animator.Play("Scoot");
                    lastPushRotation = transform.localRotation;

                }
                else if (((SimpleInput.GetKey(KeyCode.E) || Input.GetKey(KeyCode.JoystickButton2)) && isGrounded) && pushTimer <= 0 && settings.holdScoot)
                {
                    rigidbody.AddForce(rigidbody.gameObject.transform.right * -1 * pushForce, ForceMode.Impulse);
                    animator.Play("Scoot");
                    pushTimer = pushInterval;
                }

                if ((SimpleInput.GetKeyUp(KeyCode.E) || Input.GetKeyUp(KeyCode.JoystickButton2)) || (SimpleInput.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.JoystickButton10) || Input.GetKeyUp(KeyCode.JoystickButton1) || SimpleInput.GetKeyUp(KeyCode.Alpha1)))
                    pushTimer = 0;

                //tilt forward
                if ((SimpleInput.GetKey(KeyCode.W) || Input.GetKey(KeyCode.JoystickButton8) || lThumbY > 0.9) && !isGrounded)
                {
                    rigidbody.AddTorque(rigidbody.gameObject.transform.forward * 500 * verticalForce * Time.deltaTime * verticalCameraInvert);
                }
                //turn left
                if (SimpleInput.GetKey(KeyCode.A) || Input.GetKey(KeyCode.JoystickButton11) || lThumbX < -0.9)
                {
                    rigidbody.AddTorque(rigidbody.gameObject.transform.up * 500 * turnForce * Time.deltaTime * -1);
                    //rigidbody.velocity -= new Vector3(turnForce * Time.deltaTime, turnForce * Time.deltaTime, turnForce * Time.deltaTime);
                    rigidbody.velocity = Vector3.MoveTowards(rigidbody.velocity, new Vector3(0, rigidbody.velocity.y, 0), 10 * Time.deltaTime);

                }
                //tilt backwards
                if ((SimpleInput.GetKey(KeyCode.S) || Input.GetKey(KeyCode.JoystickButton10) || lThumbY < -0.9) && !isGrounded)
                {
                    rigidbody.AddTorque(rigidbody.gameObject.transform.forward * 500 * verticalForce * Time.deltaTime * -1 * verticalCameraInvert);
                }
                else if (((SimpleInput.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.JoystickButton10) || Input.GetKeyDown(KeyCode.JoystickButton1)) || SimpleInput.GetKeyDown(KeyCode.Alpha1) && isGrounded) && !settings.holdScoot)
                {
                    rigidbody.AddForce(rigidbody.gameObject.transform.right * pushForce, ForceMode.Impulse);
                    animator.Play("Reverse");
                }
                else if (((SimpleInput.GetKey(KeyCode.S) || Input.GetKey(KeyCode.JoystickButton10) || Input.GetKey(KeyCode.JoystickButton1)) || SimpleInput.GetKey(KeyCode.Alpha1) && isGrounded) && pushTimer <= 0 && settings.holdScoot)
                {
                    rigidbody.AddForce(rigidbody.gameObject.transform.right * pushForce, ForceMode.Impulse);
                    animator.Play("Reverse");
                    pushTimer = pushInterval;
                }
                //turn right
                if (SimpleInput.GetKey(KeyCode.D) || Input.GetKey(KeyCode.JoystickButton9) || lThumbX > 0.9)
                {
                    rigidbody.AddTorque(rigidbody.gameObject.transform.up * 500 * turnForce * Time.deltaTime);
                    //rigidbody.velocity -= new Vector3(turnForce * Time.deltaTime, turnForce * Time.deltaTime, turnForce * Time.deltaTime);
                    rigidbody.velocity = Vector3.MoveTowards(rigidbody.velocity, new Vector3(0, rigidbody.velocity.y, 0), 10 * Time.deltaTime);
                }
                //jump
                if ((SimpleInput.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton0)) && isGrounded)
                {
                    rigidbody.AddForce(rigidbody.gameObject.transform.up * jumpForce * 100, ForceMode.Force);
                }
                //brake
                if ((SimpleInput.GetKey(KeyCode.LeftShift) || SimpleInput.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.JoystickButton4)) && isGrounded)
                {
                    rigidbody.velocity = Vector3.MoveTowards(rigidbody.velocity, new Vector3(), Time.deltaTime * 120);
                    // skirr.Play();
                    if (rigidbody.velocity.magnitude > 0)
                        skirr.gameObject.SetActive(true);
                    else
                        skirr.gameObject.SetActive(false);
                }
                else
                {
                    skirr.Stop();
                    skirr.gameObject.SetActive(false);
                }
                //trick
                if (SimpleInput.GetKey(KeyCode.Z))
                {
                    //animator.Play("Trick One");
                }
                //reset player
                if ((SimpleInput.GetKeyUp(KeyCode.R) || Input.GetKeyUp(KeyCode.JoystickButton7)) && resetMapTimer > 0 && !resetBruh)
                {
                    Reset();
                    playerStats.resetCount++;

                    if (playerStats.resetCount >= 100)
                        settings.UnlockTrophy(2);

                    ES3.Save<int>("Reset Count", playerStats.resetCount);
                }
                else if (!SimpleInput.GetKey(KeyCode.R) || !Input.GetKey(KeyCode.JoystickButton7))
                {
                    resetBruh = false;
                }

                if (SimpleInput.GetKey(KeyCode.R) || Input.GetKey(KeyCode.JoystickButton7))
                {
                    GameObject.FindGameObjectWithTag("Reset Text").GetComponent<Animator>().SetBool("Resetting", true);
                    resetMapTimer -= 1 * Time.deltaTime;
                }
                else
                {
                    GameObject.FindGameObjectWithTag("Reset Text").GetComponent<Animator>().SetBool("Resetting", false);
                    GameObject.FindGameObjectWithTag("Reset Text").GetComponent<Animator>().Play("Hidden");
                    resetMapTimer = resetMapHoldTime;
                }
                if (resetMapTimer <= 0)
                {
                    GameObject.FindGameObjectWithTag("Reset Text").GetComponent<Animator>().Play("Hidden");
                    GameObject.FindGameObjectWithTag("Reset Text").GetComponent<Animator>().SetBool("Resetting", false);
                    resetBruh = true;
                    resetMapTimer = resetMapHoldTime;
                    SceneManager.LoadScene(0);
                }

            }
        }

        //zappy.emission.enabled = frozen;
        if (freezeAmount > 0)
        {
            playerStats.frozen = true;

            if (!zappyPlaying)
            {
                zappy.Play();
                // zappy.enableEmission = true;
                //  zappy.emission.enabled = true;
                zappyPlaying = true;
            }
        }
        else
        {
            playerStats.frozen = false;
            if (zappyPlaying)
            {
                zappy.Stop();
                zappyPlaying = false;
            }

        }
    }

    public void Reset()
    { 
        rigidbody.gameObject.transform.position = staringPosition;
        rigidbody.gameObject.transform.rotation = startingRotation;
        rigidbody.velocity = new Vector3();
        rigidbody.angularVelocity = new Vector3(0, 0, 0);
    }

}
