using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    float xRotation = 0f;


    private Vector3 targetRotation;
    private Vector3 currentRotation;

    

    public float snappiness = 7;
    public float returnSpeed = 3;

    public float recoilX;
    public float recoilY;
    public float recoilZ;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState =CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.fixedDeltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.fixedDeltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnSpeed * Time.deltaTime);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, snappiness * Time.deltaTime);

        transform.localRotation = Quaternion.Euler(xRotation + currentRotation.x, currentRotation.y, currentRotation.z);
        playerBody.Rotate(Vector3.up * mouseX);

        // if(Input.GetKey(KeyCode.Z))
        // {
        //     RecoilAdd();
        // }
    }

    public void RecoilAdd()
    {
        targetRotation += new Vector3(-recoilX, Random.Range(-recoilY, recoilY), Random.Range(-recoilZ, recoilZ));
        Debug.Log("Recoil: OK❤✅");
    }
}
