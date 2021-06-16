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
    public ProjectilePrefab PrefabToSpawn;                                     //prefab projectile that will spawn and have its values chganed by scriptableobject projectile
    public List<Projectile> ProjectilesToUse = new List<Projectile>();         //projectiles that will be shot out
    public List<Vector3> ProjectileDirections = new List<Vector3>();           //Direction projectiles will be shot out in. Defines how many projectiles there will be
    private GameObject Player;                                                 //reference to player used as spawn point for projectiles spawn

    public override void InitializeEffect()
    {
        Debug.Log(EffectName + " effect initiliazed.");
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    public override void TriggerEffect()
    {
        Debug.Log(EffectName + " effect triggered.");

        foreach (Vector3 dir in ProjectileDirections)
        {
            SpawnOrb(ProjectilesToUse, dir);
        }
    }

    void SpawnOrb(List<Projectile> projectileSOList, Vector3 dirToFireIn)
    {
        Projectile ProjectileSO = projectileSOList[Random.Range(0, projectileSOList.Count)];

        ProjectilePrefab projectile = Instantiate(PrefabToSpawn, Player.transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);
        projectile.FillValues(ProjectileSO, dirToFireIn);
    }
}
