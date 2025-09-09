using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimientopriemrapersona : MonoBehaviour
{
    [Header("Sensibilidad del Mouse")]
    [SerializeField] private float mouseSensitivity = 100f;
    public Transform playerBody;

    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Cursor.lockState != CursorLockMode.Locked)
            return;

        float delta = Time.unscaledDeltaTime;

        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * delta;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * delta;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}