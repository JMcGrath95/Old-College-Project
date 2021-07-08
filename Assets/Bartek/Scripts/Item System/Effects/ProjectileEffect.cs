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
    public List<float> ProjectileYRotations = new List<float>();                //Direction projectiles will be shot out in. List size defines how many projectiles there will be

    public override void InitializeEffect(GameObject owner)
    {
        Debug.Log(EffectName + " effect initiliazed.");
        Owner = owner;
        Debug.Log(owner.name);
    }

    public override void TriggerEffect()
    {
        Debug.Log(EffectName + " effect triggered.");


        foreach (float rotation in ProjectileYRotations)
        {
            SpawnProjectile(ProjectilesToUse, Owner.transform.position + new Vector3(0, 1.5f, 0),  Speed, rotation);
        }
    }

    void SpawnProjectile(List<ProjectileSO> projectileSOList, Vector3 spawnPos, float speed, float rotation)
    {
        float r;

        r = Owner.transform.eulerAngles.y + rotation;

        Debug.Log(r);

        ProjectileSO ProjectileSO = projectileSOList[Random.Range(0, projectileSOList.Count)];

        ProjectilePrefab projectile = Instantiate(PrefabToSpawn, spawnPos, Quaternion.Euler(0, r, 0));

        projectile.FillValues(ProjectileSO, Owner, speed);
    }
}
