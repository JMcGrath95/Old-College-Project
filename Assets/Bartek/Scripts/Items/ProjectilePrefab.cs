using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePrefab : MonoBehaviour
{
    public Vector3 Direction;                               //Direction in which the projectile travels. defaults to forward
    public Mesh Mesh;                                       //Mesh of to change the projectile to 
    public float Size;                                      //Size to change the projectile to
    public float Damage;                                    //Damage the projectile does to objects it can damage
    public float Speed;                                     //Speed at which the projectile travels
    public Color OrbColor;                                  //Color of the projectile
    public float Range;                                     //Rnage projectile travels before it dies 

    private float DistanceTraveled;
    private Vector3 origin;

    private void Start()
    {
        origin = transform.position;

        transform.localScale = new Vector3(Size, Size, Size);
    }

    //moves orb every frame
    void Update()
    {
        transform.position += Direction * Speed * Time.deltaTime;

        DistanceTraveled = Vector3.Distance(origin, transform.position);

        if (DistanceTraveled >= Range) Destroy(gameObject);
    }

    //handles collision of projectile, damaging of object hit by projectile and destruction of projectile
    private void OnTriggerEnter(Collider other)
    {

    }

    public void FillValues(Projectile projectileSO, Vector3 dirToFireIn)
    {
        if(dirToFireIn != Vector3.zero) Direction = dirToFireIn;

        if (projectileSO.Mesh != null) gameObject.GetComponent<MeshFilter>().mesh = projectileSO.Mesh;

        if (projectileSO.Size > 0) Size = projectileSO.Size;

        if (projectileSO.Damage > 0) Damage = projectileSO.Damage;

        if (projectileSO.Speed > 0) Speed = projectileSO.Speed;

        gameObject.GetComponent<MeshRenderer>().material.color = projectileSO.Color;

        if (projectileSO.Range > 1) Range = projectileSO.Range;
    }
}
