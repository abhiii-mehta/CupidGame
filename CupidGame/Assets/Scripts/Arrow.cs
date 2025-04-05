using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 20f;
    public float lifetime = 5f;

    void Start()
    {
        Destroy(gameObject, lifetime); // Destroy after a few seconds
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Heart"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
