using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();
    private GameObject effectPrefab; // ����Ʈ�� ���� ������ ����

    private void AnimationTrigger()
    {
        player.AnimationTrigger();
    }

    private void AttackTrigger()
    {
        // ���⿡ ���� ���� �Ҹ� �ٸ���
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

                    // 3��° ������ ��� �߰� ȿ�� ����
                    if (player.primaryAttackState.comboCounter == 2 && Inventory.instance.GetAnimType(AnimationType.AttackWithBBSword2))// 0���� �����ϹǷ� 2�� 3Ÿ
                    {
                        // ����Ʈ�� �����Ͽ� ������ ����
                        GameObject effectInstance = EffectManager.instance.PlayEffect("BBSword2Attack2FX", _target.transform.position, _target.transform);
                        effectInstance.transform.SetParent(_target.transform); // ���� �ڽ����� ����
                        effectInstance.transform.localPosition = Vector3.zero; // ���� ��ġ�� �°� ����

                        // �߰� ������ ����
                        _target.TakeDamage(10); // ������ 10�� �߰� ������

                        // ����Ʈ�� ���� �ð��� �����ϰ� �ı�
                        Destroy(effectInstance, 1f); // 1�� �Ŀ� ����Ʈ�� �ı�
                    }
                }

                ItemData_Equipment weaponData = Inventory.instance.GetEquipment(EquipmentType.WeaponMainHand);
                if (weaponData != null)
                    weaponData.Effect(_target.transform);
            }
        }
    }
}