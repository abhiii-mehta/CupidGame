using UnityEngine;

public class HeartFall : MonoBehaviour
{
    public float fallSpeed = 2f;

    void Update()
    {
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;
    }
}
