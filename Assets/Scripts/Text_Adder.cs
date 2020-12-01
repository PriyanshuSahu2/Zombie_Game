using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text_Adder : MonoBehaviour
{
    public Game_Manager gm; 
    public bool Chance;

    void Start()
    {
        Chance = true;
    }
    void OnTriggerStay(Collider other) 
    {
        if(other.tag == "Player" && Chance)
        {
            if(Input.GetKeyDown(KeyCode.O))
            {
                gm.a += 1;
                this.transform.Rotate(0f,0f,-60f);
                Chance = false;
            }
            
        }
    }
    void OnTriggerExit(Collider other) 
    {        
        if(other.tag == "Player")
        {
            if(!Chance)
            {
                this.GetComponent<Collider>().isTrigger = false;
            }
        }   
    }

    
}
