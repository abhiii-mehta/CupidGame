using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 20f;
    public float lifetime = 5f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Arrow hit: " + other.name); //  check what we hit

        if (other.CompareTag("Heart"))
        {
            Heart heart = other.GetComponent<Heart>();
            if (heart != null)
            {
                Debug.Log("Detected Heart Color: " + heart.heartColor); //  see the color
                OrderManager.Instance.CollectHeart(heart.heartColor);
            }

            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

}
