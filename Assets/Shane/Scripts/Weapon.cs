using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    Collider weaponCollider;

    [SerializeField] private int Damage;

    //Start.
    private void Awake() => weaponCollider = GetComponent<Collider>();
    private void Start() => DisableHitbox();

    //Hitbox enabling / disabling.
    private void EnableHitbox() => weaponCollider.enabled = true;
    private void DisableHitbox() => weaponCollider.enabled = false;
    public void EnableAndDisableHitbox() => StartCoroutine(EnableAndDisableHitboxCoroutine());
    private IEnumerator EnableAndDisableHitboxCoroutine()
    {
        EnableHitbox();
        yield return new WaitForFixedUpdate();
        DisableHitbox();
    }

    private void OnTriggerEnter(Collider other)
    {
        iDamageable iDamageable;

        if (other.gameObject.TryGetComponent(out iDamageable))
        {
            iDamageable.TakeDamage(Damage);
        }
    }
}
