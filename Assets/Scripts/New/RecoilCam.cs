using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class RecoilCam : MonoBehaviour
{
    private Vector3 targetRotation;
    private Vector3 currentRotation;

    private Vector3 normalRotation = new Vector3(0, 1.4f, 0);

    public float snappiness = 7;
    public float returnSpeed = 3;

    public float recoilX;
    public float recoilY;
    public float recoilZ;
    

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Z))
        {
            RecoilAdd();
        }

        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnSpeed * Time.deltaTime);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, snappiness * Time.deltaTime);

        transform.rotation = Quaternion.Euler(currentRotation);
    }

    public void RecoilAdd()
    {
        targetRotation += new Vector3(-recoilX, Random.Range(-recoilY, recoilY), Random.Range(-recoilZ, recoilZ));
        Debug.Log("Recoil: OK❤✅");
    }
}
