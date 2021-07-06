using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Projectile List", menuName = "Projectiles/New Projectile List")]
public class SOProjectilesList : ScriptableObject
{
    public List<ProjectileSO> projectiles = new List<ProjectileSO>();
}
