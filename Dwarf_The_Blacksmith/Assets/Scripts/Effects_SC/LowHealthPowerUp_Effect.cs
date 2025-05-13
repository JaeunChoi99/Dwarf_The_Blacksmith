using UnityEngine;
[CreateAssetMenu(fileName = "Low health power up effect", menuName = "Data/Item effect/Low health power up effect")]
public class LowHealthPowerUp_Effect : Buff_Effect
{
    // 최소 체력 퍼센트
    [SerializeField] private float minHealthPercent = 0.6f;


    public override void ExecuteEffect(Transform _enemyPosition)
    {
        base.ExecuteEffect(_enemyPosition); // 부모 클래스인 Buff_Effect의 ExecuteEffect 메서드 호출

        // 플레이어의 스탯 가져오기
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
        float currentHealth = playerStats.currentHealth;
        float maxHealth = playerStats.GetMaxHealthValue();

        // 현재 체력이 최대 체력의 최소 체력 퍼센트 이상인 경우
        if (currentHealth >= maxHealth * minHealthPercent)
        {
           return;
        }
    }

}