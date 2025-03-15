using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform followTarget;

    [SerializeField] float rotationSpeed = 2f;

    [SerializeField] float distance = 5f;

    [SerializeField] float minVerticalAngle = 0f;
    [SerializeField] float maxVerticalAngle = 45f;

    [SerializeField] Vector2 framingOffset = new Vector2(0.5f, 1f);

    float rotationX;
    float rotationY;

    float invertXVal;
    float invertYVal;

    [SerializeField] bool invertX;

    [SerializeField] bool invertY;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        (invertXVal, invertYVal) = (invertX ? -1 : 1, invertY ? -1 : 1);

        rotationX += Input.GetAxis("Camera Y") * invertYVal * rotationSpeed;
        rotationX = Mathf.Clamp(rotationX, minVerticalAngle, maxVerticalAngle);
        rotationY += Input.GetAxis("Camera X") * invertXVal * rotationSpeed;
        var targetRotation = Quaternion.Euler(rotationX, rotationY, 0);
        var focusPosition = followTarget.position + new Vector3(framingOffset.x, framingOffset.y);
        transform.position = focusPosition - targetRotation * new Vector3(0, 0, distance);
        transform.rotation = targetRotation;
    }

    public Quaternion PlanarRotation => Quaternion.Euler(0, rotationY, 0);
}
