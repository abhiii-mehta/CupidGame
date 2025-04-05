using UnityEngine;

public class CupidShooter : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform arrowSpawnPoint;
    public float timeSlowFactor = 0.3f;
    public float holdThreshold = 0.2f; // how long you have to hold before you can shoot

    private bool isSlowingTime = false;
    private float holdTime = 0f;
    private bool canShoot = false;

    void Update()
    {
        // HOLD LMB
        if (Input.GetMouseButton(0))
        {
            if (!isSlowingTime)
            {
                Time.timeScale = timeSlowFactor;
                isSlowingTime = true;
                Debug.Log("Slowing time... start aiming.");
            }

            holdTime += Time.unscaledDeltaTime;

            if (holdTime >= holdThreshold && !canShoot)
            {
                canShoot = true;
                Debug.Log("Ready to shoot! Release to fire.");
            }
        }

        // RELEASE LMB
        if (Input.GetMouseButtonUp(0))
        {
            Time.timeScale = 1f;
            isSlowingTime = false;

            if (canShoot)
            {
                Debug.Log("Arrow fired!");
                ShootArrow();
            }
            else
            {
                Debug.Log("Released too early — no shot.");
            }

            holdTime = 0f;
            canShoot = false;
        }
    }

    void ShootArrow()
    {
        if (arrowPrefab && arrowSpawnPoint)
        {
            Instantiate(arrowPrefab, arrowSpawnPoint.position, arrowSpawnPoint.rotation);
        }
    }
}
