using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingScreen : MonoBehaviour
{
    // Start is called before the first frame update
    private float count =0;
    public GameObject blackImage;
    float timer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       timer = timer + Time.deltaTime;
        if (count < 3)
        {
            if (timer >= 0.5)
            {
                blackImage.SetActive(true);
            }
            if (timer >= 1)
            {
                blackImage.SetActive(false);
                timer = 0;
                count++;
            }
        }
        
    }
}
