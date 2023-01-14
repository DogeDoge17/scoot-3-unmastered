using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Walk : MonoBehaviour
{

    public Transform player;
    public Transform groundChecc;
    public Transform playerGfx;
    public Transform playerCameraT;
    [Space]
    public float speed = 7f;
    public float turnSpeed = 140;
    public float gravity = -20;
    public float normalJumpHeight;
    public float sprintJumpHeight;
    public float jumpHeight = 1.47f;
    public float groundDistance = 1f;
    public float startingSpeed;
    [Space]
    public bool isSprinting = false;
    public bool isGrounded;
    [Space]
    public LayerMask groundMask;
    public Camera playerCamera;
    public Vector3 velocity;

    [Space]
    [Space]
    private float lThumbX;
    private float lThumbY;
    private Settings settings;
    private MobileControls mobileControls;

    public Vector3 staringPosition;
    public Quaternion startingRotation;

    private float resetMapTimer = 1.5f;
    public float resetMapHoldTime = 1.5f;
    public bool resetBruh = true;

    private PlayerStats playerStats;
    [Space]
    public string objectInHand = "Empty";
    //public bool scooterInHand;
    public GameObject scooter;
    public GameObject zaHando;
    public Transform flySpot;
    public Transform handTransform;
    public float flySpeed;
    private bool toggledHand;


    private Animator animator;
    [Space]
    [Space]
    Rigidbody rb;

    private void Awake()
    {
        // defualtScooterRotation = scooter.transform.rotation;
        settings = GameObject.FindGameObjectWithTag("Event System").GetComponent<Settings>();
        mobileControls = GameObject.FindGameObjectWithTag("Mobile Controls").GetComponent<MobileControls>();
        rb = GetComponent<Rigidbody>();
        normalJumpHeight = jumpHeight;
        staringPosition = gameObject.transform.position;
        playerStats = GameObject.FindGameObjectWithTag("Event System").GetComponent<PlayerStats>();
        startingRotation = gameObject.transform.rotation;
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        if (playerStats.onScooter)
            gameObject.SetActive(false);

        startingSpeed = speed;
    }

    private void FixedUpdate()
    {

    }

    void Move()
    {

        Vector3 movement = new Vector3(Mathf.Round(lThumbY), 0, 0) * speed * Time.deltaTime;

        Vector3 newPosition = rb.position + rb.transform.TransformDirection(movement);

        rb.MovePosition(newPosition);
    }

    void Update()
    {
        if ((Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.IPhonePlayer && Application.platform != RuntimePlatform.BlackBerryPlayer) && !mobileControls.forceShow)
            lThumbX = Input.GetAxisRaw("Left Thumbstick X");
        else
            lThumbX = SimpleInput.GetAxisRaw("Horizontal");

        if ((Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.IPhonePlayer && Application.platform != RuntimePlatform.BlackBerryPlayer) && !mobileControls.forceShow)
            lThumbY = Input.GetAxisRaw("Left Thumbstick Y");
        else
            lThumbY = SimpleInput.GetAxisRaw("Vertical");

        if (SimpleInput.GetKey(KeyCode.W) || SimpleInput.GetKey(KeyCode.JoystickButton8))
            lThumbY = -1;
        else if (SimpleInput.GetKey(KeyCode.S) || SimpleInput.GetKey(KeyCode.JoystickButton10))
            lThumbY = 1;

        if (SimpleInput.GetKey(KeyCode.A) || SimpleInput.GetKey(KeyCode.JoystickButton11))
            lThumbX = -1;
        else if (SimpleInput.GetKey(KeyCode.D) || SimpleInput.GetKey(KeyCode.JoystickButton9))
            lThumbX = 1;

        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.BlackBerryPlayer || mobileControls.forceShow)
        {
            lThumbY = -SimpleInput.GetAxis("Vertical");
        }

        if (playerStats.health > 0)
        {
            if (!playerStats.frozen && !settings.settingsOpen)
            {
                Debug.DrawRay(groundChecc.position, Vector3.down * groundDistance, Color.red);

                isGrounded = Physics.Raycast(groundChecc.position, Vector3.down, groundDistance);

                if ((SimpleInput.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton0)) && isGrounded)
                {
                    rb.AddForce(rb.gameObject.transform.up * jumpHeight * 100, ForceMode.Force);
                }

                Move();

                transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);

                if (objectInHand != "Scooter")
                {
                    scooter.GetComponent<Rigidbody>().isKinematic = false;
                    scooter.GetComponent<BoxCollider>().isTrigger = false;

                    if ((SimpleInput.GetKey(KeyCode.Q) || SimpleInput.GetKey(KeyCode.JoystickButton4)) && !toggledHand)
                    {

                        scooter.GetComponent<BoxCollider>().isTrigger = true;
                        scooter.transform.LookAt(flySpot.position);
                        scooter.transform.position = Vector3.MoveTowards(scooter.transform.position, flySpot.position, flySpeed * Time.deltaTime);
                        scooter.GetComponent<Rigidbody>().velocity = Vector3.zero;
                        animator.SetBool("Forcing", true);
                        //scooter.GetComponent<Rigidbody>().isKinematic = true;

                        if (Vector3.Distance(scooter.transform.position, flySpot.position) < 0.2f)
                        {
                            scooter.transform.SetParent(zaHando.transform);
                            scooter.transform.localPosition = Vector3.zero;
                            objectInHand = "Scooter";
                            toggledHand = true;
                        }
                    }
                    else
                    {
                        scooter.GetComponent<BoxCollider>().isTrigger = false;
                        animator.SetBool("Forcing", false);
                    }
                }

                if (objectInHand == "Scooter")
                {
                    animator.SetBool("Forcing", false);

                    scooter.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    scooter.GetComponent<Rigidbody>().isKinematic = true;
                    scooter.GetComponent<BoxCollider>().isTrigger = true;
                    scooter.transform.localPosition = Vector3.zero;
                    scooter.transform.SetParent(zaHando.transform);
                    scooter.transform.rotation = handTransform.rotation;
                    scooter.transform.position = handTransform.position;
                    scooter.transform.localScale = handTransform.localScale;

                    if ((SimpleInput.GetKey(KeyCode.Q) || SimpleInput.GetKey(KeyCode.JoystickButton4 )) && !toggledHand)
                    {
                        scooter.transform.SetParent(GameObject.FindGameObjectWithTag("Player Main").transform);
                        scooter.GetComponent<Rigidbody>().isKinematic = false;
                        scooter.GetComponent<BoxCollider>().isTrigger = false;
                        objectInHand = "Empty";
                        toggledHand = true;
                    }
                }

                if (SimpleInput.GetKeyUp(KeyCode.Q) || SimpleInput.GetKeyUp(KeyCode.JoystickButton4)) toggledHand = false;

                if (SimpleInput.GetKeyDown(KeyCode.E) || SimpleInput.GetKeyDown(KeyCode.JoystickButton2))
                {
                    animator.Play("Attack");
                }

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

                if (isSprinting == true)
                {
                    jumpHeight = sprintJumpHeight;
                    speed = 15;
                }

                if (SimpleInput.GetKeyDown(KeyCode.LeftShift))
                {
                    isSprinting = true;
                }

                if (SimpleInput.GetKeyUp(KeyCode.LeftShift))
                {
                    isSprinting = false;
                    jumpHeight = normalJumpHeight;
                    speed = startingSpeed;
                }



                if (lThumbY > 0.9 || lThumbY < -0.9)
                    animator.SetBool("Walking", true);
                else
                    animator.SetBool("Walking", false);


                //isGrounded = Physics.CheckSphere(groundChecc.position, groundDistance, groundMask);
                // Debug.DrawLine(groundChecc.position, new Vector3(groundChecc.position.x, groundChecc.position.y - groundDistance, groundChecc.position.z), Color.red);

                if (isGrounded && velocity.y < 0)
                {
                    velocity.y = -2f;
                }

                // rb.angularVelocity = new Vector3 (rb.angularVelocity.x, rb.angularVelocity.y, rb.angularVelocity.z);

                //Turn Right
                if (SimpleInput.GetKey(KeyCode.D) || Input.GetKey(KeyCode.JoystickButton9) || lThumbX > 0.9)
                {
                    //rb.AddTorque(rb.gameObject.transform.up  * turnSpeed * Time.deltaTime);

                    rb.angularVelocity = new Vector3(rb.angularVelocity.x, turnSpeed, rb.angularVelocity.z);


                    // transform.localRotation = new Quaternion(transform.rotation.x, transform.rotation.y + turnSpeed * Time.deltaTime, transform.rotation.z, transform.rotation.w);

                    //player.Rotate(Vector3.up * turnSpeed * Time.deltaTime);
                }

                //Turn Left
                else if (SimpleInput.GetKey(KeyCode.A) || Input.GetKey(KeyCode.JoystickButton11) || lThumbX < -0.9)
                {
                    //rb.AddTorque(rb.gameObject.transform.up  * turnSpeed * Time.deltaTime * -1);
                    rb.angularVelocity = new Vector3(rb.angularVelocity.x, -turnSpeed, rb.angularVelocity.z);

                    //  transform.localRotation = Quaternion.AngleAxis(-1, transform.up); //new Quaternion(transform.rotation.x, transform.rotation.y - turnSpeed * Time.deltaTime, transform.rotation.z, transform.rotation.w);
                    // player.Rotate(Vector3.up * -turnSpeed * Time.deltaTime);

                }
                else
                {
                    rb.angularVelocity = Vector3.zero;
                }
                //Vector3 move = transform.right * lThumbY + transform.forward * 0;

                //if (Input.GetButtonDown("Jump") && isGrounded)
                //{
                //    velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                //}

                //controller.Move(move * speed * Time.deltaTime);

                //velocity.y += gravity * Time.deltaTime;

                //controller.Move(velocity * Time.deltaTime);
            }
        }
    }

    public void Reset()
    {
        rb.gameObject.transform.position = staringPosition;
        rb.gameObject.transform.rotation = startingRotation;
        rb.velocity = new Vector3();
        rb.angularVelocity = new Vector3(0, 0, 0);

    }


}

