using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 20f;
    public float lifetime = 3f;
    private bool hitSomething = false;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnDestroy()
    {
        // If the arrow is destroyed and DIDN'T hit a heart, deduct one
        if (!hitSomething && OrderManager.Instance != null)
        {
            OrderManager.Instance.UseArrow();
        }
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Heart"))
        {
            hitSomething = true;

            Heart heart = other.GetComponent<Heart>();
            if (heart != null)
            {
                OrderManager.Instance.CollectHeart(heart.heartColor, heart.maskType);
            }

            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
