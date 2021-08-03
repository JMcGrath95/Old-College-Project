using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Controller : MonoBehaviour
{
    [Header("Projectile Settings")]

    public int numberOfProjectiles;
    public float projectileSpeed;
    public float rotationSpeed = 5;
    public GameObject projectilePrefab;

    [Header("Private Variables")]

    private const float radius = 1f;



    private void Start()
    {
        InvokeRepeating("SpawnProjectile", 2, 5);
    }

    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 30 * Time.deltaTime, 0));
    }

    

    private void SpawnProjectile()
    {
        float angleStep = 360f / numberOfProjectiles;
        float angle = 0f;

        for (int i = 0; i < numberOfProjectiles ; i++)
        {
            // Direction Calculations
            float projectilesDirXPosition = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float projectilesDirYPosition = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            Vector3 ProjectileVector = new Vector3(projectilesDirXPosition, projectilesDirYPosition, 0);
            Vector3 projectileMoveDirection = (ProjectileVector - transform.position).normalized * projectileSpeed;
            

            GameObject tmpObj = Instantiate(projectilePrefab, transform);
            tmpObj.GetComponent<Rigidbody>().velocity = new Vector3(projectileMoveDirection.x, 0, projectileMoveDirection.y);
            //angle += 180;

            if(angle >= 360f)
            {
                angle = 0f;
            }
           angle += angleStep;
        }
    }

    
}
