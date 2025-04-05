using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 20f;
    public float lifetime = 5f;

    void Start()
    {
        Destroy(gameObject, lifetime); // Clean up after time
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Heart"))
        {
            Debug.Log("Hit heart: " + other.gameObject.name);
            Destroy(other.gameObject);  // Destroy heart
            Destroy(gameObject);        // Destroy arrow too
        }
    }
}
