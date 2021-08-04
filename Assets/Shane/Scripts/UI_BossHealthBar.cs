using TMPro;
using UnityEngine;

//Boss Health bar object is in UI and listens for events for a boss being created / spawned to update itself with information it needs to display about the boss - name and its health component.
//If bosses have different health bars then this would be changed but since all their health bars are the same this is fine for now.
public class UI_BossHealthBar : UI_HealthBar
{
    [SerializeField] TextMeshProUGUI txtBossName;

    private void Start()
    {
        _boss.BossCreatedEvent += OnBossObjectCreated;
        BossHealth.BossHealthInitialisedEvent += OnBossSpawned;
    }

    private void OnBossObjectCreated(_boss bossCreated) => txtBossName.text = bossCreated.Name;
    private void OnBossSpawned(BossHealth bossHealth)
    {
        UpdateHealthEntity(bossHealth);
        gameObject.SetActive(true);
    }

    protected override void OnMyEntityHealthChanged() => base.OnMyEntityHealthChanged();

    protected override void OnMyHealthEntityDeath()
    {
        StopListeningToHealthEntityEvents();
        gameObject.SetActive(false);
    }


    public override void OnDestroy()
    {
        base.OnDestroy();

        _boss.BossCreatedEvent -= OnBossObjectCreated;
        BossHealth.BossHealthInitialisedEvent -= OnBossSpawned;
    }
}
