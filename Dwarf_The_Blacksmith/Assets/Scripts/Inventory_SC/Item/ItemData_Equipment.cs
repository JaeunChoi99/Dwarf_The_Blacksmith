using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
    WeaponMainHand,
    Armor,
    Amulet,
    Flask
}

public enum AnimationType
{
    Attack,

    //Sword
    AttackWithBSword,
    AttackWithGGSword,
    AttackWithGGSword2,
    AttackWithBBSword,
    AttackWithBBSword2,
   
    //Hammer
    AttackWithBHammer,
    AttackWithIBHammer,
    AttackWithIBHammer2,
    AttackWithTBHammer,
    AttackWithTBHammer2,

    //ShortSword
    AttackWithSSword,
    AttackWithBSSword,
    AttackWithBSSword2,
    AttackWithPSSword,
    AttackWithPSSword2


}

public enum ArmorType
{
    None,
    Nature,
    Nature2,
    Berserk,
    Berserk2
}

[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Equipment")]
public class ItemData_Equipment : ItemData
{
    public AnimationType animationType;
    public EquipmentType equipmentType;
    public ArmorType armorType;

    [Header("Unique effect")]
    public float itemCooldown;
    public ItemEffect[] itemEffects;
    [TextArea]
    public string itemEffectDescription;


    [Header("Major stats")]
    public int strength;
    public int agility;
    public int intelligence;
    public int vitality;

    [Header("Offensive stats")]
    public int damage;
    public int critChance;
    public int critPower;

    [Header("Defensive stats")]
    public int health;
    public int armor;
    public int evasion;
    public int magicResistance;

    [Header("Magic stats")]
    public int fireDamage;
    public int iceDamage;
    public int lightingDamage;
    public int bleedingDamage;


    [Header("Craft requirements")]
    public List<InventoryItem> craftingMaterials;

    private int descriptionLength;

    public void Effect(Transform _enemyPosition)
    {
        foreach(var item in itemEffects)
        {
            item.ExecuteEffect(_enemyPosition);
        }
    }



    public void AddModifiers()
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        playerStats.strength.AddModifier(strength);
        playerStats.agility.AddModifier(agility);
        playerStats.intelligence.AddModifier(intelligence);
        playerStats.vitality.AddModifier(vitality);

        playerStats.damage.AddModifier(damage);
        playerStats.critChance.AddModifier(critChance);
        playerStats.critPower.AddModifier(critPower);

        playerStats.maxHealth.AddModifier(health);
        playerStats.armor.AddModifier(armor);
        playerStats.evasion.AddModifier(evasion);
        playerStats.magicResistance.AddModifier(magicResistance);
    }

    public void RemoveModifiers()
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        playerStats.strength.RemoveModifier(strength);
        playerStats.agility.RemoveModifier(agility);
        playerStats.intelligence.RemoveModifier(intelligence);
        playerStats.vitality.RemoveModifier(vitality);


        playerStats.damage.RemoveModifier(damage);
        playerStats.critChance.RemoveModifier(critChance);
        playerStats.critPower.RemoveModifier(critPower);


        playerStats.maxHealth.RemoveModifier(health);
        playerStats.armor.RemoveModifier(armor);
        playerStats.evasion.RemoveModifier(evasion);
        playerStats.magicResistance.RemoveModifier(magicResistance);
    }

    public override string GetDescription()
    {
        sb.Length = 0;

        descriptionLength = 0;

        AddItemDescription(strength, "힘");
        AddItemDescription(agility, "민첩");
        AddItemDescription(intelligence, "지능");
        AddItemDescription(vitality, "생명력");

        AddItemDescription(damage, "피해량");
        AddItemDescription(critChance, "치명타 확률");
        AddItemDescription(critPower, "치명타 피해");

        AddItemDescription(health, "체력");
        AddItemDescription(evasion, "회피");
        AddItemDescription(armor, "방어도");
        AddItemDescription(magicResistance, "Magic Resist.");

        AddItemDescription(fireDamage, "Fire damage");
        AddItemDescription(iceDamage, "Ice damage");
        AddItemDescription(lightingDamage, "Lighting dmg. ");
        AddItemDescription(bleedingDamage, "Bleeding dmg. ");

        if(descriptionLength < 5)
        {
            for(int i = 0; i < 5 - descriptionLength; i++)
            {
                sb.AppendLine();
                sb.Append("");
            }
        }

        if (itemEffectDescription.Length > 0)
        {
            sb.AppendLine();
            sb.AppendLine(itemEffectDescription);
        }

        return sb.ToString();
    }

    private void AddItemDescription(int _value, string _name)
    {
        if(_value != 0)
        {
            if(sb.Length > 0)
                sb.AppendLine();

            //값 넣는 형식 그냥 이름 : 값 이거였는데 + 값 이름 으로 바꿈
            if (_value > 0)
                sb.Append("+ " + _value +  " " + _name);

            descriptionLength++;
        }
    }

    public void EquipItemToSlot(ItemData_Equipment item, EquipmentType slotType)
    {
        item.equipmentType = slotType; // 슬롯 유형에 따라 아이템의 장비 유형을 업데이트
    }

}
