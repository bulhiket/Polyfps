using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwayWeapon : MonoBehaviour
{
    [Header("Position")]
    public float amount = 0.02f;
    public float maxAmount = 0.06f;
    public float smoothAmount = 6f;

    [Header("Rotation")]
    public float rotationAmount = 4f;
    public float maxRotationAmount = 5f;
    public float smoothRotation = 12f;

    [Space]
    public bool rotationX = true; 
    public bool rotationY = true; 
    public bool rotationZ = true; 

    private Vector3 initalPosition;
    private Quaternion initialRotation;

    private float inputX;
    private float inputY;
    
    void Start()
    {
        initalPosition = transform.localPosition;
        initialRotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateSway();

        MoveSway();
        TiltSway();
    }

    private void CalculateSway()
    {
        inputX = -Input.GetAxis("Mouse X");
        inputY = -Input.GetAxis("Mouse Y");
    }

    private void MoveSway()
    {
        float moveX = Mathf.Clamp(inputX * amount, -maxAmount, maxAmount);
        float moveY = Mathf.Clamp(inputY * amount, -maxAmount, maxAmount);

        Vector3 finalPosition = new Vector3(moveX, moveY, 0);

        transform.localPosition = Vector3.Lerp(
            transform.localPosition, 
            finalPosition + initalPosition, 
            Time.fixedDeltaTime * smoothAmount);


    }

    private void TiltSway()
    {
        float tiltX = Mathf.Clamp(inputX * rotationAmount, -maxRotationAmount, maxRotationAmount);
        float tiltY = Mathf.Clamp(inputY * rotationAmount, -maxRotationAmount, maxRotationAmount);

        Quaternion finalRotation = Quaternion.Euler(new Vector3(
            rotationX ? -tiltX : 0f, 
            rotationY ? tiltY : 0f, 
            rotationZ ? tiltY : 0f));

        transform.localRotation = Quaternion.Slerp(
            transform.localRotation, 
            finalRotation * initialRotation, 
            Time.fixedDeltaTime * smoothRotation);
    }
}
