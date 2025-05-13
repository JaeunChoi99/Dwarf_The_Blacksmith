using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DruidHeal_Skill : Skill
{
    [Header("Healzone info")]
    [SerializeField] private GameObject healZonePrefab;
    [SerializeField] private float healZoneDuration;
    private Transform zonePosition;

    public override void UseSkill()
    {
        base.UseSkill();

        // ��ų�� ����� �� ���� ������ �����մϴ�.
        CreateHealZone(PlayerManager.instance.player.transform);
    }

    public void CreateHealZone(Transform _healZonePosition)
    {
        // ���� ���� �������� �ν��Ͻ�ȭ�մϴ�.
        GameObject healZone = Instantiate(healZonePrefab, new Vector2(_healZonePosition.position.x , _healZonePosition.position.y -10f), Quaternion.identity);

        // ������ ���� ������ ���� ������ �����մϴ�.
        healZone.GetComponent<DruidHeal_Skill_Controller>().SetupHealZone(_healZonePosition, healZoneDuration);
    }
}
