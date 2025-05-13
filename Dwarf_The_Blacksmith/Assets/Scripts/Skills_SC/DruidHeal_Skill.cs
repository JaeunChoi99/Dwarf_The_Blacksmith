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

        // 스킬을 사용할 때 힐링 영역을 생성합니다.
        CreateHealZone(PlayerManager.instance.player.transform);
    }

    public void CreateHealZone(Transform _healZonePosition)
    {
        // 힐링 영역 프리팹을 인스턴스화합니다.
        GameObject healZone = Instantiate(healZonePrefab, new Vector2(_healZonePosition.position.x , _healZonePosition.position.y -10f), Quaternion.identity);

        // 생성된 힐링 영역에 대해 설정을 수행합니다.
        healZone.GetComponent<DruidHeal_Skill_Controller>().SetupHealZone(_healZonePosition, healZoneDuration);
    }
}
