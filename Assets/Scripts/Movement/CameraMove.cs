using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public ProjectileControl projectilePrefab;
    // Start is called before the first frame update

    public Transform follow;

    public float mouseSensitivity = 100f;
    public float xRotation = 0;
    public float yRotation = 0;

    void Start()
    {
        follow = gameObject.transform;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float mouseY = Input.GetAxis("Mouse Y")*mouseSensitivity*Time.deltaTime;
        xRotation -= mouseY;
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}



