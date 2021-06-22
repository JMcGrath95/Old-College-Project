using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Projectile", menuName = "Projectiles/New Projectile")]
public class ProjectileSO : ScriptableObject
{
    public string Name = "New Projectile";                      //name for display and searching
    public Mesh Mesh;                                           //Mesh to change the projectile to 
    public float Size = 1;                                      //Size to change the projectile to
    public float Damage = 10;                                   //Damage the projectile does to objects it can damage
    public Color Color = Color.green;                           //Color of the projectile
    public float Range = 20;                                    //Range projectile travels before it dies 

    //can add more values later such as penetration
    //potentially add a list of gameobjects that refer to obejcts the projectile will ignore and not damage
    //make more projectiles spwawn on this projectile hit
}
