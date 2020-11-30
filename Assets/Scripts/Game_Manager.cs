using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_Manager : MonoBehaviour
{
    public Text Count;

    [TextArea(0,4)]
    public string[] dil;
    public int a;

    public GameObject DoorCollider;
    // Start is called before the first frame update
    void Start()
    {
        a=0;
        Count = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Count.text = dil[a];
        if(a==2)
        {
            DoorCollider.GetComponent<Collider>().isTrigger=true;
            StartCoroutine(end());
            
        }
    }
    IEnumerator end(){yield return new WaitForSeconds(5f); a=3; }
}
