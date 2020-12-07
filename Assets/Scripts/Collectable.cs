using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    /// <summary>
    /// The rotation speed of this collectible.
    /// </summary>
    public float rotationSpeed;

    private void Start()
    {
        // Start the rotate coroutine
        StartCoroutine(Rotate());
    }

    /// <summary>
    /// Sets the object to inactive once it collides with a player.
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Player>() != null)
        {
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Rotate the object.
    /// </summary>
    /// <returns></returns>
    IEnumerator Rotate()
    {
        // Checks if the object is active
        while(gameObject.activeInHierarchy)
        {
            Vector3 newRotation = transform.localRotation.eulerAngles;
            newRotation += Vector3.up * rotationSpeed;

            transform.localRotation = Quaternion.Euler(newRotation);

            yield return new WaitForEndOfFrame();
        }
    }
}
