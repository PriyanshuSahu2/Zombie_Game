using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newPlayer : MonoBehaviour
{
    public bool isground;  public Transform groundcheck;  public LayerMask groundmask;
    public Rigidbody rb; public Animator anime; public Transform Cam;
    public float grounArea = 0.3f; public int jumpforce= 3; public float gravity = -9.81f;
    public float moveSpeed = 1.5f; public float mouseSense = 100f; public float mouse;
    
    
    
    //private bool moving = true;

    void Start()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.gameObject;
    }
    private void Update()
    {
        
        isground = Physics.CheckSphere(groundcheck.position, 0.3f, groundmask);
        float mousex = Input.GetAxis("Mouse X") * Time.deltaTime *mouseSense;
        float mousey = Input.GetAxis("Mouse Y") * Time.deltaTime *mouseSense; 
        mouse -= mousey; 
        mouse = Mathf.Clamp(mouse, -45f, 45f);
        transform.Rotate(Vector3.up * mousex);
        Cam.localRotation = Quaternion.Euler(mouse,0f,0f);
        if (Input.GetKey(KeyCode.W)){this.transform.Translate(Vector3.forward*moveSpeed*Time.deltaTime); walk(1);}
        if (Input.GetKeyUp(KeyCode.W)){walk(0);}
        if (Input.GetKey(KeyCode.S)){transform.Translate(Vector3.forward*-moveSpeed*Time.deltaTime); walk(1);}
        if(Input.GetKeyUp(KeyCode.S)){walk(0);}
        if (Input.GetKey(KeyCode.A)){transform.position += new Vector3(-moveSpeed*Time.deltaTime,0f,0f); walk(1);}
        if (Input.GetKeyUp(KeyCode.A)){walk(0);}
        if (Input.GetKey(KeyCode.D)){transform.position += new Vector3(moveSpeed*Time.deltaTime,0f,0f); walk(1);}
        if (Input.GetKeyUp(KeyCode.D)){walk(0);}
        if(Input.GetButtonDown("Jump") && isground)
        {
            
        }
        if(Input.GetKeyDown(KeyCode.R)){moveSpeed = moveSpeed*2;walk(0); run(1);} else if(Input.GetKeyUp(KeyCode.R)){moveSpeed = moveSpeed/2;run(0);}
        if(Input.GetMouseButton(1)){aim(1);} else if(Input.GetMouseButtonUp(1)){aim(0);}
        if(Input.GetMouseButton(1)){fire(1);} else if(Input.GetMouseButtonUp(1)){fire(0);}
    }
    void walk(int w){if(w == 1){anime.SetBool("Walk",true);} else{anime.SetBool("Walk",false);}}
    void run(int r){if(r==1){anime.SetBool("Run",true);} else{anime.SetBool("Run",false);}}
    void fire(int f){if(f==1){anime.SetBool("Fire",true);} else{anime.SetBool("Fire",false);}}
    void aim(int a){if(a==1){anime.SetBool("Aim",true);} else{anime.SetBool("Aim",false);}}
    void knife(int k){anime.SetBool("knife",true);}
    void graned(int g){anime.SetBool("Graned",true);}
}