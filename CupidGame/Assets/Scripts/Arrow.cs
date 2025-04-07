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
        Debug.Log("Arrow hit: " + other.name);

        if (other.CompareTag("Heart"))
        {
            Heart heart = other.GetComponent<Heart>();
            if (heart != null)
            {
                Debug.Log("Detected: " + heart.heartColor + " + " + heart.maskType);
                OrderManager.Instance.CollectHeart(heart.heartColor, heart.maskType);
            }

            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

}
