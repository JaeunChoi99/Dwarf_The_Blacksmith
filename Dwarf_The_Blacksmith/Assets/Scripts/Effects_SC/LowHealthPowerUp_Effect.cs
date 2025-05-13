using UnityEngine;
[CreateAssetMenu(fileName = "Low health power up effect", menuName = "Data/Item effect/Low health power up effect")]
public class LowHealthPowerUp_Effect : Buff_Effect
{
    // �ּ� ü�� �ۼ�Ʈ
    [SerializeField] private float minHealthPercent = 0.6f;


    public override void ExecuteEffect(Transform _enemyPosition)
    {
        base.ExecuteEffect(_enemyPosition); // �θ� Ŭ������ Buff_Effect�� ExecuteEffect �޼��� ȣ��

        // �÷��̾��� ���� ��������
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
        float currentHealth = playerStats.currentHealth;
        float maxHealth = playerStats.GetMaxHealthValue();

        // ���� ü���� �ִ� ü���� �ּ� ü�� �ۼ�Ʈ �̻��� ���
        if (currentHealth >= maxHealth * minHealthPercent)
        {
           return;
        }
    }

}