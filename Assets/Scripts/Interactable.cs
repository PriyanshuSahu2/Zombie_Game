using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    // Basic Interact Method
    public virtual void Interact()
    {
        Debug.Log("Interacting with object " + gameObject.name);
    }
}
