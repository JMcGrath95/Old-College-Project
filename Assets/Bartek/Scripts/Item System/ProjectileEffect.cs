using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This effect spawns a number of projectiles in a number of directions at a speed of a number doing a number of damage
/// number refers to corresponding value in scriptable object when filling projectiles list
/// </summary>

[CreateAssetMenu(fileName = "ProjectileEffect", menuName = "Effects/New Projectile Effect")]
public class ProjectileEffect : BaseEffect
{
    public ProjectilePrefab PrefabToSpawn;                                      //prefab projectile that will spawn and have its values chganed by scriptableobject projectile
    public float offsetValue = 1.2f;                                            //Multiplier which offsets projectile from player e.g 1.5 is 1.5 times direction value away from player (direction is normalized and can only be 1/-1 even is different value is input)
    public float Speed = 5;                                                     //Speed at which the projectile travels
    public List<ProjectileSO> ProjectilesToUse = new List<ProjectileSO>();      //projectiles that will be shot out
    public List<Vector3> ProjectileDirections = new List<Vector3>();            //Direction projectiles will be shot out in. Defines how many projectiles there will be

    public override void InitializeEffect(GameObject owner)
    {
        Debug.Log(EffectName + " effect initiliazed.");
        Owner = owner;
    }

    public override void TriggerEffect()
    {
        Debug.Log(EffectName + " effect triggered.");

        foreach (Vector3 dir in ProjectileDirections)
        {
            SpawnOrb(ProjectilesToUse, dir, Speed);
        }
    }

    void SpawnOrb(List<ProjectileSO> projectileSOList, Vector3 dirToFireIn, float speed)
    {
        dirToFireIn = dirToFireIn.normalized;

        ProjectileSO ProjectileSO = projectileSOList[Random.Range(0, projectileSOList.Count)];

        Vector3 offset = Owner.transform.position + (offsetValue * dirToFireIn);

        Vector3 spawnPos = (Owner.transform.position + new Vector3(0, 1.5f, 0)) + offset;

        ProjectilePrefab projectile = Instantiate(PrefabToSpawn, spawnPos, Quaternion.identity);

        projectile.FillValues(ProjectileSO, dirToFireIn, Owner, speed);
    }
}
