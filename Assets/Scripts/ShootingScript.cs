using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform leftFirePoint;
    public Transform rightFirePoint;
    public float bulletSpeed = 30f;
    public float maxBulletTravelDistance = 100f;
    public int maxShots = 5;
    public float resetTime = 5f;

    private int currentShots = 0;
    private float resetTimer = 0f;

    void Update()
    {
        // Handle shooting input
        if (Input.GetButtonDown("Fire1") && currentShots < maxShots)
        {
            ShootFromBothHands();
            currentShots++;
        }

        // Reset the shot counter after the cooldown period
        if (currentShots >= maxShots)
        {
            resetTimer += Time.deltaTime;
            if (resetTimer >= resetTime)
            {
                currentShots = 0;
                resetTimer = 0f;
            }
        }
    }

    private void ShootFromBothHands()
    {
        if (leftFirePoint != null) Shoot(leftFirePoint);
        if (rightFirePoint != null) Shoot(rightFirePoint);
    }

    private void Shoot(Transform firePoint)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.velocity = firePoint.forward * bulletSpeed;
        }

        // Start coroutine to track bullet travel distance
        StartCoroutine(DestroyBulletAfterTravel(bullet, firePoint.position));
    }

    private IEnumerator DestroyBulletAfterTravel(GameObject bullet, Vector3 startPosition)
    {
        while (bullet != null)
        {
            // Check if the bullet has traveled the maximum distance
            if (Vector3.Distance(startPosition, bullet.transform.position) >= maxBulletTravelDistance)
            {
                Destroy(bullet);
                yield break;
            }

            yield return null;
        }
    }
}