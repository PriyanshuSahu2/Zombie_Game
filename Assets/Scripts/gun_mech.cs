using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun_mech : MonoBehaviour
{
    public float Range = 100f;
    public float Damage = 10f;
    public Camera fps;
    public ParticleSystem muzzelflash, paticlflash;
    // Start is called before the first frame update
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            muzzelflash.Play();
            paticlflash.Play();
            Shoot();
        }   
    }
    void Shoot()
    {
        RaycastHit hit;
        if(Physics.Raycast(fps.transform.position, fps.transform.forward, out hit, Range))
        {
            Zombie target = hit.transform.GetComponent<Zombie>();
            if(target != null)
            {
                target.takeDamage(Damage);
            }
        }
    }
}
