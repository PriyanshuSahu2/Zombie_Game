using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    /// <summary>
    /// The next state this player will enter
    /// </summary>
    private string nextState = "";

    /// <summary>
    /// The current state of this player
    /// </summary>
    private string currentState = "Move";

    /// <summary>
    /// The current coroutine of this player
    /// </summary>
    private IEnumerator currentCoroutine;

    /// <summary>
    /// The Rigidbody component of this player
    /// </summary>
    public Rigidbody myRigidbody;

    /// <summary>
    /// The movement speed of this player
    /// </summary>
    public float moveSpeed;

    /// <summary>
    /// The rotation speed of this player
    /// </summary>
    public float rotationSpeed;

    public float jumpForce;

    public float interactionDistance;

    public Transform playerHead;

    private CapsuleCollider collider;
    public Animator anim;
    public GameObject gunPoint;
    public Slider healthUI;
    public float damgeFromZombie;
    public Camera cam;
    public AudioSource audio;
    public AudioClip gunShots;
    public ParticleSystem gunParticle;
    public float damageZombie;
    public ParticleSystem bloodParticle;
    public GameObject GameManger;
    public GameObject gameOverPanel;
    float rotX;
    float rotY;

    private void Awake()
    {
        // Get the Rigidibody component of this player using GetComponent<'ComponentType'>();
        myRigidbody = GetComponent<Rigidbody>();
        // Get the collider for the player object
        collider = GetComponent<CapsuleCollider>();
        //Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        
        
    }

    void Start()
    {
        // Update and start the coroutine of the player
        UpdateCoroutine();
    }

    private void Update()
    {
        Debug.DrawRay(gunPoint.transform.position,transform.forward, Color.green);
        
        // Check if player touches ground and if space is pressed
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            myRigidbody.velocity = Vector3.up * jumpForce;
        }
        if(healthUI.value <=0)// if the player health reaches 0  or less then 0 then it will call PlayerDied function
        {
            PlayerDied();
        }
        if(Input.GetButtonDown("Fire1")) // if the player presses Fire1 Button i.e. Left Mouse button then it will call Shoot function
        {
            Shoot();
        }
        
        if(gameOverPanel.activeSelf)
        {
            audio.Stop();
        }else
        {
            audio.Play();
        }
        //Debug.DrawLine(playerHead.position, playerHead.position + (playerHead.forward * interactionDistance), Color.red);
        // Debug.DrawLine(gunPoint.transform.position, gunPoint.transform.forward * 100f, Color.gr);
        // If the player presses left mouse button (interact btn)
        if (Input.GetButtonDown("Interact"))
        {
            RaycastHit hitInfo;
            // Transform mouse position from camera to ray
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // Get object at mouse pos
            
            if (Physics.Raycast(gunPoint.transform.position,gunPoint.transform.forward,out hitInfo))
            {
                
                Debug.Log(hitInfo.collider.name);
                // Calc distance between player and current object
                float distanceToObject = Vector3.Distance(transform.position, hitInfo.transform.position);
                Debug.Log("Distance to " + hitInfo.collider.name + ": " + distanceToObject);
                
                // Get the interactable class from this object
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                // Check if interactable is null and if the distance is below the limit for interaction
                if (interactable != null && distanceToObject <= interactionDistance)
                {
                    // Do interaction
                    interactable.Interact();
                }
            }
        }
    }

    // Check if player touches ground
    public bool IsGrounded()
    {
        // Do a raycast below the player with a distance of 0.1 and check if it hits to ground
        return Physics.Raycast(transform.position, -Vector3.up, collider.bounds.extents.y + 0.1f);
    }


    /// <summary>
    /// Switches the state of the player to its next state
    /// </summary>
    private void SwitchState()
    {
        if(nextState == currentState)
        {
            return;
        }
        else
        {
            currentState = nextState;
            nextState = "";

            UpdateCoroutine();
        }
    }

    /// <summary>
    /// Update the coroutine of the player to the correct behaviour
    /// </summary>
    private void UpdateCoroutine()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        if (currentState == "Move")
        {
            currentCoroutine = Move();
        }

        StartCoroutine(currentCoroutine);
    }

    /// <summary>
    /// The Move coroutine
    /// </summary>
    /// <returns></returns>
    private IEnumerator Move()
    {
        // While the state is in "Move", run this behaviour
        while(currentState == "Move")
        {
            // Checks for any input is detected from the mouse
            if(Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y")!= 0)
            {
                Vector3 newRotation = transform.localRotation.eulerAngles;
                 rotX += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
                 rotY -= Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
                rotY = Mathf.Clamp(rotY, -30f, 60f);
                // Rotate the player based on how much the mouse has moved.
                //newRotation += transform.up * Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;

                transform.eulerAngles = new Vector3(rotY,rotX, 0f);
            }

            // Get the direction of movement for the player.
            Vector3 movementDir = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));

            movementDir *= moveSpeed * Time.deltaTime;

            Vector3 newPos = transform.position;
            newPos += movementDir;
            myRigidbody.MovePosition(newPos);
            
            // Wait for the fixed update before running the behaviour again.
            yield return new WaitForFixedUpdate();
        }
        

    }
    private void OnCollisionEnter(Collision other)// this function is call when any other gameobject will collide with this gameobject
    {
        if(other.gameObject.CompareTag("Zombie"))// it will check if collide game Object have tag of Zombie 
        {
            healthUI.value -= damgeFromZombie;//if it have tag of zombie then health will decrease
        }
        if(other.gameObject.CompareTag("Key"))// it will check if collide game Object have tag of Key 
        {
            bool haveKey = true;
            GameManger.GetComponent<GameManager>().HaveKey(haveKey);//if it has then it will call the  HaveKey function from GameManger script and pass the haveKey value
            Destroy(other.gameObject);//then destroy the object
        }
        if (other.gameObject.CompareTag("Gun"))// it will check if collide game Object have tag of Gun
        {
            
            bool haveGun = true;
            GameManger.GetComponent<GameManager>().HaveGun(haveGun);//if it has then it will call the  HaveGun function from GameManger script and pass the haveGun value
            Destroy(other.gameObject);//then destroy the object
        }
        if (other.gameObject.CompareTag("Note"))// it will check if collide game Object have tag of Note 
        {
            
            bool haveNote = true;
            GameManger.GetComponent<GameManager>().HaveNote(haveNote);//if it has then it will call the  HaveNote function from GameManger script and pass the haveNote value
            Destroy(other.gameObject);//then destroy the object
        }
    }
    private void PlayerDied()
    {
        if (gameOverPanel == null) return;// if the gameOverPanel is not assigned in Inspector it will return Null
        gameOverPanel.SetActive(true);//if player Died then active Game Over Panel
    }
    private void Shoot()
    {
        audio.PlayOneShot(gunShots);// play gun shot clip
        gunParticle.Play();//play gun particle
        RaycastHit hit;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))// if Ray cast value hit any gameObject it will return true
        {
            if(hit.transform.CompareTag("Zombie"))//if hit gameObject tag is Zombie then it will return true
            {
                hit.transform.GetComponent<Zombie_AI>().Damage(damageZombie);//it will call Damage function from Zombie_AI script and passing damageZombie value 
                if (bloodParticle == null) return;
                Instantiate(bloodParticle, hit.transform.position,Quaternion.identity);// it will spawn blood particle in the place of hit
            }
        }

    }
   

}
