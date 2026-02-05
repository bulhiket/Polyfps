using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpForce = 8f;
    public float mouseSensitivity = 100f;
    public float gravity = -9.81f;

    [Header("References")]
    public Transform playerCam;

    private CharacterController controller;
    private float xRotation = 0f;
    private Vector3 jumpVelocity;
    private bool isGrounded;
    private float currentSpeed;

    public Texture2D crosshair;
    public float crosshairSize = 25f;

    private Shake camShake;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentSpeed = walkSpeed;

        camShake = playerCam.GetComponent<Shake>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            currentSpeed = runSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            currentSpeed = walkSpeed;
        }


        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * currentSpeed * Time.deltaTime);


        
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Math.Clamp(xRotation, -90f, 90f);
        transform.Rotate(Vector3.up * mouseX);

        
    }

    void OnGUI()
    {
        float centerX = Screen.width / 2;
        float centerY = Screen.height / 2;

        GUI.DrawTexture(new Rect(centerX - crosshairSize / 2, centerY - crosshairSize / 2, crosshairSize, crosshairSize), crosshair);
    }
}
