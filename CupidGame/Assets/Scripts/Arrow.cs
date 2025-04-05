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

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Heart"))
        {
            Heart heart = other.GetComponent<Heart>();
            if (heart != null)
            {
                OrderManager.Instance.CollectHeart(heart.heartColor);
            }

            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

}
