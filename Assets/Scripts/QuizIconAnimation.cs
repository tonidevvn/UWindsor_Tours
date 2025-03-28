using UnityEngine;

public class QuizIconAnimation : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Rotate the object on X, Y, and Z axes by specified amounts, adjusted for frame rate.
        transform.Rotate (new Vector3 (0, 30, 0) * Time.deltaTime);
        // auto move up and down in the y axis and not less than 1.0f and not more than 2.5f        
        transform.position = new Vector3(transform.position.x, Mathf.PingPong(Time.time, 1.5f) + 2f, transform.position.z);
    }
}
