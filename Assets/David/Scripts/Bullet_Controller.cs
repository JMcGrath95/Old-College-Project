using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Controller : MonoBehaviour
{
    [Header("Projectile Settings")]

    public int numberOfProjectiles;
    public float projectileSpeed;
    public GameObject projectilePrefab;

    [Header("Private Variables")]
    private Vector3 startPoint;
    private const float radius = 1f;

    private void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            startPoint = transform.position;
            SpawnProjectile(numberOfProjectiles);
        }
    }

    private void SpawnProjectile(int _numberOfProjectiles)
    {
        float angleStep = 360f / _numberOfProjectiles;
        float angle = 0f;

        for (int i = 0; i < _numberOfProjectiles -1; i++)
        {
            // Direction Calculations
            float projectilesDirXPosition = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float projectilesDirYPosition = startPoint.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            Vector3 ProjectileVector = new Vector3(projectilesDirXPosition, projectilesDirYPosition, 0);
            Vector3 projectileMoveDirection = (ProjectileVector - startPoint).normalized * projectileSpeed;

            GameObject tmpObj = Instantiate(projectilePrefab, startPoint, Quaternion.identity);
            tmpObj.GetComponent<Rigidbody>().velocity = new Vector3(projectileMoveDirection.x, 0, projectileMoveDirection.y);

            angle += angleStep;
        }
    }
}
