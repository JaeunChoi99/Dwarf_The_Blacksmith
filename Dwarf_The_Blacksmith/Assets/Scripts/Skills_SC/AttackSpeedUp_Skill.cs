using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackSpeedUp_Skill : Skill
{
    private Player player;

    // 공격 애니메이션의 재생 속도 증가량
    public float attackSpeedMultiplier = 2f;
    // 공격 애니메이션 재생 속도가 증가되는 시간(초)
    public float attackSpeedDuration = 3f;

    private float currentAttackSpeedDuration = 0f;

    // Start() 메서드에서 player 변수 초기화
    void Start()
    {
        // PlayerManager에서 플레이어 가져오기
        player = PlayerManager.instance.player;

        // 플레이어가 null이면 로그를 출력하고 함수 종료
        if (player == null)
        {
            Debug.LogError("Player is not assigned to PlayerManager.");
            return;
        }
    }

    public override void UseSkill()
    {
        base.UseSkill();

        // 공격 애니메이션의 재생 속도를 증가시킵니다.
        player.anim.speed *= attackSpeedMultiplier;

        // 공격 애니메이션의 재생 속도를 증가한 시간을 초기화합니다.
        currentAttackSpeedDuration = attackSpeedDuration;
    }

    protected override void Update()
    {
        base.Update();

        // 만약 공격 애니메이션의 재생 속도가 증가 중이고,
        // 지정된 시간이 경과하면 재생 속도를 원래대로 되돌립니다.
        if (currentAttackSpeedDuration > 0)
        {
            currentAttackSpeedDuration -= Time.deltaTime;
            if (currentAttackSpeedDuration <= 0)
            {
                player.anim.speed /= attackSpeedMultiplier;
            }
        }
    }
}