using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyplayerController : MonoBehaviour
{
    public CharacterController characterController;
    public float speed = 8;

    float gravity = 9.87f;
    float verticalSpeed = 0;

    public Transform cameraHolder;
    public float mouseSensitivity = 2f;
    public float uplimit = -50;
    public float downlimit = 50;

    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        float herizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        Vector3 gravityMove = new Vector3(0, verticalSpeed, 0);
        Vector3 move = transform.forward * verticalMove + transform.right * herizontalMove;
        characterController.Move(speed * Time.deltaTime * move + gravityMove * Time.deltaTime);
        animator.SetBool("IsWalking", verticalMove != 0 || herizontalMove != 0);
    }

    public void Rotate()
    {
        float horizotalRotate = Input.GetAxis("Mouse X");
        float verticalRotate = Input.GetAxis("Mouse Y");

        transform.Rotate(0, horizotalRotate * mouseSensitivity, 0);
        cameraHolder.Rotate(-verticalRotate * mouseSensitivity, 0, 0);

        Vector3 currectRotation = cameraHolder.localEulerAngles;
        if(currectRotation.x> 180)
        {
            currectRotation.x -= 360;

        }
        currectRotation.x = Mathf.Clamp(currectRotation.x, uplimit, downlimit);
        cameraHolder.localRotation = Quaternion.Euler(currectRotation);
    }
}
