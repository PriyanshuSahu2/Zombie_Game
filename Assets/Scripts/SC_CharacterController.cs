
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class SC_CharacterController : MonoBehaviour
{
    #region --Variables---
    [SerializeField] float speed = 7.5f;
    [SerializeField] float jumpSpeed = 10.0f;
    [SerializeField] float gravity = 20.0f;
    [SerializeField] Camera playerCamera;
    [SerializeField] float lookSpeed = 2.0f;
    [SerializeField] float lookXLimit = 45.0f;
    

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    Vector2 rotation = Vector2.zero;
    Animator Anime;

    [HideInInspector]
    [SerializeField] bool canMove = true;
    #endregion
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        rotation.y = transform.eulerAngles.y;
        Anime = GetComponent<Animator>();
    }

    void Update()
    {
        #region ---Character Assign Movement
        if (characterController.isGrounded)
        {
            //If player grounded, so recalculate move direction based on axes
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);
            float curSpeedX = speed * Input.GetAxis("Vertical");
            float curSpeedY = speed * Input.GetAxis("Horizontal");
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);
        #endregion

        #region -------Player Rotaion Based On Camera---------
        // Player and Camera rotation
        if (canMove)
        {
            rotation.y += Input.GetAxis("Mouse X") * lookSpeed;
            rotation.x += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotation.x = Mathf.Clamp(rotation.x, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotation.x, 0, 0);
            transform.eulerAngles = new Vector2(0, rotation.y);
        }
        #endregion
    }
}