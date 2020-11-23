using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouselook : MonoBehaviour
{
    public float mouse_sensivity =100f;
    public Transform View;
    float invertedY = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouse_sensivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouse_sensivity * Time.deltaTime;
        
        invertedY -= mouseY;
        invertedY = Mathf.Clamp(invertedY,-90f,90f);

        transform.localRotation = Quaternion.Euler(invertedY,0f,0f);
        View.Rotate(Vector3.up * mouseX);
    }
}
