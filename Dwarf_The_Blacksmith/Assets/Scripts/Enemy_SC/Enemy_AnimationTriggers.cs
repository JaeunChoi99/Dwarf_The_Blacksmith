using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AnimationTriggers : MonoBehaviour
{
 

    private Enemy enemy => GetComponentInParent<Enemy>();

    private void AnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
    }


    


    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckRadius);
        Collider2D[] colliders2 = Physics2D.OverlapBoxAll(enemy.attackCheck2.position, enemy.attackBoxSize, 0f);

        // Combine both arrays
        Collider2D[] allColliders = new Collider2D[colliders.Length + colliders2.Length];
        colliders.CopyTo(allColliders, 0);
        colliders2.CopyTo(allColliders, colliders.Length);

       // if (enemy == E)
       // {
       //     if (BossKobold.enrageTriggered)
       //     {
       //         AudioManager.instance.PlaySFX(16, null);
       //     }
       //     else
       //     {
       //         AudioManager.instance.PlaySFX(17, null);
       //     }
       // }
       // else
       // {
       //     AudioManager.instance.PlaySFX(20, null);
       // }

        // Process all colliders
        foreach (var hit in allColliders)
        {
            PlayerStats playerStats = hit.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                enemy.stats.DoDamage(playerStats);
            }
        }
    }
    

   


    private void SpecialAttackTrigger()
    {
        enemy.AnimationSpecialAttackTrigger();
    }



    private void OpenCounterWindow() => enemy.OpenCounterAttackWindow();
    private void CloseCounterWindow() => enemy.CloseCounterAttackWindow();
}
