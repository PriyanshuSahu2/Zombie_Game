using System.Collections;
using UnityEngine;

public class Switch : Interactable
{

    public override void Interact()
    {

        StartCoroutine(SwitchPressed());
    }
    IEnumerator SwitchPressed()
    {
        gameObject.SetActive(false);
        yield return null;
    }


}
