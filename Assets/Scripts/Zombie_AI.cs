using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Zombie_AI : MonoBehaviour
{
    private NavMeshAgent agent;
    public GameObject player;
    public float zombieHealth = 3;
    public float damage = 1f;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

        agent.SetDestination(player.transform.position);

    }
    public void Damage(float damage)
    {
        zombieHealth--;
        Debug.Log("DAmaged");
        if(zombieHealth <= 0f)
        {
            Debug.Log("DAAAAA");
            Destroy(gameObject);
        }
    }
}
