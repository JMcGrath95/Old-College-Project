using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This effect spawns a number of projectiles in a number of directions at a speed of a number doing a number of damage
/// number refers to corresponding value in scriptable object when filling projectiles list
/// </summary>

[CreateAssetMenu(fileName = "Projectile Effect", menuName = "Effects/New Projectile Effect")]
public class ProjectileEffect : BaseEffect
{
    public ProjectilePrefab PrefabToSpawn;                                      //prefab projectile that will spawn and have its values chganed by scriptableobject projectile
    public float Speed = 5;                                                     //Speed at which the projectile travels
    public List<ProjectileSO> ProjectilesToUse = new List<ProjectileSO>();      //projectiles that will be shot out
    public List<Vector3> ProjectileDirections = new List<Vector3>();            //Direction projectiles will be shot out in. Defines how many projectiles there will be

    public override void InitializeEffect(GameObject owner)
    {
        Debug.Log(EffectName + " effect initiliazed.");
        Owner = owner;
        Debug.Log(owner.name);
    }

    public override void TriggerEffect()
    {
        Debug.Log(EffectName + " effect triggered.");

        foreach (Vector3 dir in ProjectileDirections)
        {
            SpawnProjectile(ProjectilesToUse, Owner.transform.position + new Vector3(0, 1.5f, 0), dir, Speed);
        }
    }

    void SpawnProjectile(List<ProjectileSO> projectileSOList, Vector3 spawnPos, Vector3 dirToFireIn, float speed)
    {
        dirToFireIn = dirToFireIn.normalized;

        ProjectileSO ProjectileSO = projectileSOList[Random.Range(0, projectileSOList.Count)];

        ProjectilePrefab projectile = Instantiate(PrefabToSpawn, spawnPos, Quaternion.identity);

        projectile.FillValues(ProjectileSO,  dirToFireIn, Owner, speed);
    }
}
