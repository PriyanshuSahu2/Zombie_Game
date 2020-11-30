using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newPlayer : MonoBehaviour
{                  /*---checking the if the player is in ground or not---*/
    public bool isground;                                            // "isground" is a bool to check if the player is in ground or not.
    public Transform groundcheck;                                    // "groundcheck' is a transform where we will place a collider to check if its in ground or not.
    public LayerMask groundmask;                     /* "goundmask" is the layer we will add in the editor and assign it the terrain or 
                                                         floor and collider will check if the floors collided with is ground or not.*/
    float grounArea = 0.3f;                                         // "groundArea" is the area of the collider we will ceate to check.
                     
    
    public Rigidbody rb;                                            // "rb" is a component of the main player rigidbody.

    public Transform Cam;                                           //"Cam" is a transform from which we can access the camera's position ad rotation.
    public float mouse_sensitivity = 100f;                          //"mouse_sensitivity" is a float which we will used to change the sensitivity of mouse.  
    public float mouseyinverted;                                    //---this will be used later.--
    public Animator anime;                                          // bringing AnimatorController and assigning it's name as "anime".
    public GameObject gun_aim;                                      // this is the white dot = the aming point of the gun.

    public bool moving = false;                                     // this bool is to check if the player is moving or not.
    public float walking_speed = 40f;                               //"walking_speed" is the idle speed of the player while walking.
    public float Running_speed;                                     //"running_speed" will take the idle speed and double it.
    public float main_Speed;                                        //"main_speed" will be assigned to the player and its value will switch between (walking and running speed).
    public int jumpforce= 6;                                        // "jumpfoce" is the force applied when we jump.





    void Start()
    {
        
        rb = GetComponent<Rigidbody>();                            //  this will find rigidbody from the object and assign it to "rb".
        main_Speed = walking_speed;                                //  at start main_Speed will be equal to the walking speed.
    }



    private void Update()
    {
        
        isground = Physics.CheckSphere(groundcheck.position, grounArea, groundmask);            /*this will create a Spherecollider at position groundcheck.position,with area of groundArea,
                                                                                               , and check if it collides with an object with layer called groundmask.*/ 

        

                                  /*---Camera movements---*/

        float mousex = Input.GetAxis("Mouse X") * Time.deltaTime *mouse_sensitivity;            // we created a float named mousex to record the input of x axis of Mouse
        float mousey = Input.GetAxis("Mouse Y") * Time.deltaTime *mouse_sensitivity;            // we created a float named mousex to record the input of y axis of Mouse
        mouseyinverted -= mousey;                                                               // input of y-axis is invert by default so we have to reivert it, by doing this.  
        mouseyinverted = Mathf.Clamp(mouseyinverted, -45f, 45f);                                // you dont't wanna make your player head go 360degree up so by this we create a range for this value.
        transform.Rotate(Vector3.up * mousex);                                                  //we use this to rotate the character in x-axis so that camera which is in it will also rotate 
        Cam.localRotation = Quaternion.Euler(mouseyinverted,0f,0f);                             //we used this to rotate the y-axis of the camera not the player.

                                  /*---Character movements---*/

        float xdir = Input.GetAxisRaw("Horizontal")*main_Speed*Time.deltaTime;                  // again this float is to record the Horizontal & Vetical controllers and  
        float zdir = Input.GetAxisRaw("Vertical")*main_Speed*Time.deltaTime;                    // multiply them with main_Speed and Time.deltaTime.
        Vector3 newpos = transform.right*xdir + transform.forward*zdir;                         //we assign the sum of x direction and z direction in a Vector3 called "newpos".
        Vector3 newmovepos = new Vector3(newpos.x,rb.velocity.y,newpos.z);                      /*we again create a Vector3 called "newmovepos" and its value will be update with the |newpos.x| which is Horizontal controles 
                                                                                                  and |velocity.y| which is the default value of rigidbody and |newpos.z| which is Vertical controles*/ 

        rb.velocity = newmovepos;                                                               // now to move the player in the scene we use the rigidbody and here we are linking rb.velocity which is a vector3 to newmovepos.

                                /*---Animation assigning ---*/

        Running_speed = main_Speed * 2f;                                                        //Running_speed will be equal to the double of the main speed;
        if(rb.velocity.x != 0f){walk(1); moving = true;}                                        //if rb.velocity.x will not equal to 0 then it will play a walking animation from walk.and moving bool will be true.
        else{run(0); walk(0); moving =false;}                                                   //if rb.velocity.x will be 0 means if the character is not moving then it will stop the animation.and moving bool will be false.
        if(rb.velocity.z != 0f)
        {                                                                                       
            walk(1);                                                                             
            moving = true;                                                                                         //same as the above but this is for the forward direction so we also have to increase the speed
            if(Input.GetKeyDown(KeyCode.LeftShift) && moving ==true ){main_Speed=Running_speed;walk(0); run(1);}   //by switching  main_speed to Running_speed and stopping walking animation and start running animation.
        }
        else{walk(0); moving =false;}
        
        if(Input.GetKeyUp(KeyCode.LeftShift)){main_Speed=walking_speed; run(0);}  // the GetKeyUp is necessary to set globally cause if we put it under the if statement where GetKeyDown then there will be a glitch that if we unpress the movement key first then running animation will be stuck.             

        
        if(Input.GetButtonDown("Jump") && isground){rb.velocity = new Vector3(rb.velocity.x,jumpforce,rb.velocity.z);}         // by this code we are asking if the Space button is pressed and the player is in ground and if yes then we will add jumpforce to the y-axis.
        
        if(Input.GetMouseButton(1)){gun_aim.active=false;aim(1);}                                // if mouse right click is pressed then them Aiming animation will be turn on . and the dot will disappear.
        else if(Input.GetMouseButtonUp(1)){gun_aim.active=true;aim(0);}                          // if mouse right click unpressed then dot will turn on and Aiming animation will stop. 

        if(Input.GetMouseButton(0)){fire(1);}                                                    //if mouse left button is pressed then it will turn on the firing animation. 
        else if(Input.GetMouseButtonUp(0)){fire(0);}                                             //if mouse left button is unpressed then it will turn off the firing animation.  this is just form animation other functions of gun is in gun's mechanics.
    }



                              /*---Animations methods  an organised methods to call different animations at different moments.---*/ 

    void walk(int w)  {if(w == 1){anime.SetBool("Walk",true);} else{anime.SetBool("Walk",false);}}      // if we call these method we have to give a int parameter for each method.
    void run(int r)   {if(r==1){anime.SetBool("Run",true);} else{anime.SetBool("Run",false);}}          //and i have putted a  if statement that if the parameter is equal to 1
    void fire(int f)  {if(f==1){anime.SetBool("fire",true);} else{anime.SetBool("fire",false);}}        //then animation will be played like Run(1); or Walk(1)
    void aim(int a)   {if(a==1){anime.SetBool("Aim",true);} else{anime.SetBool("Aim",false);}}          //and in the else statement means if parameter is other than 1 then animation will be stopped like Run(3); or Walk(0);.  
}