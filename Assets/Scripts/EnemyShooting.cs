using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab; // The bullet prefab
    [SerializeField] private Transform firePoint; // The point from which the bullet is fired
    [SerializeField] private float bulletSpeed = 10f; // Speed of the bullet
    [SerializeField] private float shootingInterval = 2f; // Interval between shots

    private float shootingTimer;

    void Update()
    {
        shootingTimer -= Time.deltaTime;
        if (shootingTimer <= 0)
        {
            Shoot();
            shootingTimer = shootingInterval;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * bulletSpeed;
    }
}

