using UnityEngine;

public class CoinHover : MonoBehaviour
{
    public float rotationSpeed = 50f;
    public float hoverSpeed = 2f;
    public float hoverHeight = 0.5f;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        // Rotate the coin
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        // Hover the coin
        float newY = startPosition.y + Mathf.Sin(Time.time * hoverSpeed) * hoverHeight;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
}
