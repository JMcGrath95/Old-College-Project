using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePrefab : MonoBehaviour
{
    public Mesh Mesh;                                       //Mesh of to change the projectile to 
    public float Size = 1;                                  //Size of Projectile
    public float Damage = 10;                               //Damage the projectile does to objects it can damage
    public float Speed = 5;                                 //Speed at which the projectile travels
    public Color ProjectileColor = Color.green;             //Color of the projectile
    public float Range = 20;                                //Distance projectile can travel before it dies 
    public GameObject Owner;                                //Owner of projectile(used so projectile can't hit its creator)

    private float DistanceTraveled;                         //how far the projectile travelled(used to check when it hits range)
    private Vector3 origin;                                 //starting position of projectile 

    bool canhit = false;                                    //bool which prevents projectile from doing damage(used to prevent owner getting hit before owner value is set - might change)

    private void Start()
    {
        origin = transform.position;
        canhit = true;
    }

    //moves projectile every frame
    void Update()
    {
        transform.position += transform.forward * Speed * Time.deltaTime;           //projectile moves in the direction at the speed

        DistanceTraveled = Vector3.Distance(origin, transform.position);            //distance travelled is update based on the distance between current position and origin

        if (DistanceTraveled >= Range) Destroy(gameObject);                         //if distnace travelled is bigger than range projectile dies
    }

    //handles collision of projectile, damaging of object hit by projectile and destruction of projectile
    private void OnTriggerEnter(Collider other)
    {
        if (canhit)
        {
            iDamageable iDamageable;
            Debug.Log("Hit");

            if (other.gameObject != Owner && other.GetComponent<ProjectilePrefab>() == false)
            {
                if (other.TryGetComponent(out iDamageable))
                {
                    iDamageable.TakeDamage((int)Damage);
                    Destroy(gameObject);
                    Debug.Log("Damaged Object");
                }
                else
                {
                    Destroy(gameObject);
                    Debug.Log("Hit a wall");
                }
            }
        }
    }

    public void FillValues(ProjectileSO projectileSO, GameObject owner, float speed)
    {
        if (projectileSO.Mesh != null) Mesh = projectileSO.Mesh;                    //Mesh

        if (projectileSO.Size > 0) Size = projectileSO.Size;                        //Size

        if (projectileSO.Damage > 0) Damage = projectileSO.Damage;                  //Damage

        if (speed > 0) Speed = speed;                                               //Speed

        ProjectileColor = projectileSO.Color;                                       //Color

        if (projectileSO.Range > 1) Range = projectileSO.Range;                     //Range

        Owner = owner;                                                              //Owner

        UpdatePrefab();
    }

    public void UpdatePrefab()
    {
        gameObject.GetComponent<MeshFilter>().mesh = Mesh;                          //Mesh

        gameObject.GetComponent<MeshRenderer>().material.color = ProjectileColor;   //Color

        transform.localScale = new Vector3(Size, Size, Size);                       //Size
    }
}
