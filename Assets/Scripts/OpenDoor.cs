using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class OpenDoor : MonoBehaviour
{
    public GameObject switch1; 
    public GameObject switch2;
    public GameObject leftDoor;
    public GameObject rightDoor;
    bool openDoor =true;

    private void Start()
    {
  
    }
    // Update is called once per frame
    void Update()
    {
        
       if(!switch1.activeSelf  && !switch2.activeSelf && openDoor) // to check weather switch 1 ,switch 2  is active or not 
        {
            //if All condition are true then
            leftDoor.transform.Rotate(0f, 107.12f, 0f);//it will rotate left door by 107.12 degree
            rightDoor.transform.Rotate(0f, 258.72f, 0f);//it will rotate right dooe by 258.72 degree
            openDoor = false;
        }
    }


}
