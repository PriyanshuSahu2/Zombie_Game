using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zombie : MonoBehaviour
{
    public float health = 100f;
    public float maxhealth = 100f;

    void start()
    {
        health = maxhealth;
        
    }

    void update()
    {

    }

    public void takeDamage(float d)
    {
        health -= d;
        if(health <= 0f)
        {
            DieOBJ();
        }
    }

    void DieOBJ()
    {
        Destroy(gameObject);
    }
}
