using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heal_Skill : Skill
{
    public bool onCooldown = false;

    public override void UseSkill()
    {
        base.UseSkill();
        Debug.Log("ÈúÀÌ¾ß!");
        onCooldown = true;
    }

    protected override void Update()
    {
        base.Update();

        if (cooldownTimer <= 0 && onCooldown)
        {
            onCooldown = false;
        }
    }
}
