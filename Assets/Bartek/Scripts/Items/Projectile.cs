using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Projectile", menuName = "Projectiles/New Projectile")]
public class Projectile : ScriptableObject
{
    public string Name;                                     //name for display and searching
    public Mesh Mesh;                                       //Mesh to change the projectile to 
    public float Size;                                      //Size to change the projectile to
    public float Damage;                                    //Damage the projectile does to objects it can damage
    public float Speed;                                     //Speed at which the projectile travels
    public Color Color;                                     //Color of the projectile
    public float Range;                                     //Range projectile travels before it dies 

    //can add more values later such as penetration
    //potentially add a list of gameobjects that refer to obejcts the projectile will ignore and not damage
    //make more projectiles spwawn on this projectile hit
}
