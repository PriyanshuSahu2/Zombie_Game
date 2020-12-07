using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    
    public bool haveKey = false;// to check player have Key or not
    public bool haveNote = false;// to check player have Note or not
    public bool haveGun = false;// to check player have secondry gun or not
    public GameObject gameEndPanel;
    public Text totalZombies; //assign the total number of zombies in the text

    // Update is called once per frame
    private void Start()
    {
        gameEndPanel.SetActive(false);//close the game Over Panel in start of the game
    }
    void Update()
    {
        GameObject[] gb = GameObject.FindGameObjectsWithTag("Zombie");//to  find the total number of Game Object with the tag of zombie and assign it in the array of gameobject
        totalZombies.GetComponent<Text>().text = gb.Length.ToString();//to update the total number of zombies  and show it in the text
        if (gb.Length == 0 && haveKey && haveNote && haveGun) //to check  if there are not a single zombie left in the scene ,and if the player have key , note  and sceondry Gun
        {
            gameEndPanel.SetActive(true);// if all condition are true then the player have win  the game and show game End Panel
        }
    }
    public void HaveKey(bool key)
    {
        haveKey = key;
    }
    public void HaveGun(bool Gun)
    {
        haveGun= Gun;
    }
    public void HaveNote(bool note)
    {
        haveNote = note;
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene(1);// it will load scene number 1 from build setting
    }
    public void Exit()
    {
        SceneManager.LoadScene(0);// it will load scene number 0 from build setting
    }
}


