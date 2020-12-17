using UnityEngine;

public class FirstPersonView : MonoBehaviour
{
    [SerializeField]
    private float _mouseSensitivity = 100f;
    [SerializeField]
    private Transform _playerBody;

    private float _xRotation = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;

        _xRotation = CalculateCameraXRotation(mouseY, _xRotation);

        transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
        _playerBody.Rotate(Vector3.up * mouseX);
    }

    public float CalculateCameraXRotation(float mouseY, float xRotation)
    {
        xRotation -= mouseY;

        return Mathf.Clamp(xRotation, -90f, 90f);
    }
}
