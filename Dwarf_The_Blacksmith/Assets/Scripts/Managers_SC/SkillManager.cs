using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    public DruidHeal_Skill druidHeal { get; private set; }
    public Heal_Skill heal { get; private set; }

    public AttackSpeedUp_Skill attackSpeedUp { get; private set; }
    
    public ThirdExtraHit_Skill thirdExtraHitSkill { get; private set; }


    //public ThirdExtraHit_Skill thirdExtraHit { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        druidHeal = GetComponent<DruidHeal_Skill>();
        heal = GetComponent<Heal_Skill>();
        attackSpeedUp = GetComponent<AttackSpeedUp_Skill>();
        thirdExtraHitSkill = GetComponent<ThirdExtraHit_Skill>();
    }

    private void Update()
    {
        
    }
}
