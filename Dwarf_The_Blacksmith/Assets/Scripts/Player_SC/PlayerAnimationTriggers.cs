using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();
    private GameObject effectPrefab; // 이펙트를 위한 프리팹 변수

    private void AnimationTrigger()
    {
        player.AnimationTrigger();
    }

    private void AttackTrigger()
    {
        // 무기에 따라 공격 소리 다르게
        ItemData_Equipment currentWeapon = Inventory.instance.GetEquipment(EquipmentType.WeaponMainHand);

        if (currentWeapon != null && currentWeapon.animationType == AnimationType.AttackWithBSword)
        {
            AudioManager.instance.PlaySFX(35, null);
        }
        else if (currentWeapon != null && (currentWeapon.animationType == AnimationType.AttackWithBBSword ||
                                           currentWeapon.animationType == AnimationType.AttackWithBBSword2 ||
                                           currentWeapon.animationType == AnimationType.AttackWithGGSword ||
                                           currentWeapon.animationType == AnimationType.AttackWithGGSword2))
        {
            AudioManager.instance.PlaySFX(35, null);
        }
        else if (currentWeapon != null && (currentWeapon.animationType == AnimationType.AttackWithBHammer ||
                                           currentWeapon.animationType == AnimationType.AttackWithIBHammer ||
                                           currentWeapon.animationType == AnimationType.AttackWithIBHammer2 ||
                                           currentWeapon.animationType == AnimationType.AttackWithTBHammer ||
                                           currentWeapon.animationType == AnimationType.AttackWithTBHammer2))
        {
            AudioManager.instance.PlaySFX(36, null);
        }
        else if (currentWeapon != null && (currentWeapon.animationType == AnimationType.AttackWithSSword ||
                                           currentWeapon.animationType == AnimationType.AttackWithBSSword ||
                                           currentWeapon.animationType == AnimationType.AttackWithBSSword2 ||
                                           currentWeapon.animationType == AnimationType.AttackWithPSSword ||
                                           currentWeapon.animationType == AnimationType.AttackWithPSSword2))
        {
            AudioManager.instance.PlaySFX(37, null);
        }
        else
        {
            AudioManager.instance.PlaySFX(0, null);
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                EnemyStats _target = hit.GetComponent<EnemyStats>();

                if (_target != null)
                {
                    player.stats.DoDamage(_target);

                    // 3번째 공격일 경우 추가 효과 적용
                    if (player.primaryAttackState.comboCounter == 2 && Inventory.instance.GetAnimType(AnimationType.AttackWithBBSword2))// 0부터 시작하므로 2가 3타
                    {
                        // 이펙트를 생성하여 적에게 적용
                        GameObject effectInstance = EffectManager.instance.PlayEffect("BBSword2Attack2FX", _target.transform.position, _target.transform);
                        effectInstance.transform.SetParent(_target.transform); // 적의 자식으로 설정
                        effectInstance.transform.localPosition = Vector3.zero; // 적의 위치에 맞게 조정

                        // 추가 데미지 적용
                        _target.TakeDamage(10); // 적에게 10의 추가 데미지

                        // 이펙트의 지속 시간을 설정하고 파괴
                        Destroy(effectInstance, 1f); // 1초 후에 이펙트를 파괴
                    }
                }

                ItemData_Equipment weaponData = Inventory.instance.GetEquipment(EquipmentType.WeaponMainHand);
                if (weaponData != null)
                    weaponData.Effect(_target.transform);
            }
        }
    }
}