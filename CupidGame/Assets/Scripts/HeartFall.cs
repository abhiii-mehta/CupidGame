using UnityEngine;

public class HeartFall : MonoBehaviour
{
    public float fallSpeed = 2f;
    private Transform playerCamera;

    void Start()
    {
        playerCamera = Camera.main.transform;

        // Destroy this heart after 10 seconds
        Destroy(gameObject, 10f);
    }

    void Update()
    {
        // Move downward
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;

        // Face the camera
        if (playerCamera != null)
        {
            transform.LookAt(playerCamera);
            transform.Rotate(0f, 90f, 0f); // Adjust so +X faces target instead of +Z

        }
    }
}
