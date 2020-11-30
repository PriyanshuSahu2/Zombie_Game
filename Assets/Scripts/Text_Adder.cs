using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text_Adder : MonoBehaviour
{
    public Game_Manager gm; 
    void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Player")
        {
            
            this.transform.Rotate(0f,0f,-60f);
        }
    }
    void OnTriggerExit(Collider other) 
    {        
        if(other.tag == "Player")
        {
            if(gm.a < 3 ){gm.a += 1;}
            this.GetComponent<Collider>().isTrigger = false;
        }   
    }

    
}
