using UnityEngine;
using UnityEngine.UI;

public class UI_InGame : MonoBehaviour
{

    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Slider slider;

    public GameObject BosshealthBar;
    private SecretArea secretArea;
    public Slider bossHealthSlider; // 보스 체력바의 Slider 컴포넌트
    private Entity bossEntity;

    [Header("Skill")]

    [SerializeField] private GameObject defaultSkillUI;
    [SerializeField] private Image defaultSkillImage;

    [SerializeField] private GameObject healSkillUI;
    [SerializeField] private Image healSkillImage;

    [SerializeField] private GameObject healSkill2UI;
    [SerializeField] private Image healSkillImage2;

    [SerializeField] private GameObject attackSpeedUpUI;
    [SerializeField] private Image attackSpeedUpImage;

    [SerializeField] private GameObject attackSpeedUpUI2;
    [SerializeField] private Image attackSpeedUpImage2;
    [Space(5)]

    [Header("Passive")]

    [SerializeField] private GameObject healPassiveUI;

    [SerializeField] private Image healPassiveImage;

    [SerializeField] private GameObject healPassive2UI;

    [SerializeField] private Image healPassiveImage2;

    [SerializeField] private GameObject berserkPassiveUI;

    [SerializeField] private Image berserkPassiveImage;

    [SerializeField] private GameObject berserkPassive2UI;

    [SerializeField] private Image berserkPassiveImage2;






    [SerializeField] private Image currentWeaponIcon;
    [SerializeField] private Image currentFlaskIcon;

    [SerializeField] private GameObject flaskImageUI;
    [SerializeField] private Image flaskImage;


    private SkillManager skills;

    //Buff 긴급
    [SerializeField] public GameObject Buff;
    private PlayerManager player;

    void Start()
    {

        if (playerStats != null)
            playerStats.onHealthChanged += UpdateHealthUI;

        skills = SkillManager.instance;
        player = PlayerManager.instance;

        // SecretArea 컴포넌트 가져오기
        secretArea = FindObjectOfType<SecretArea>();

        // 보스의 Entity 컴포넌트를 가져옴
        bossEntity = GameObject.FindGameObjectWithTag("BossKobold").GetComponent<Entity>();


        // 보스 체력바를 비활성화합니다.
        BosshealthBar.SetActive(false);

    }

    void Update()
    {

        if (player.player.canDoubleJump)
        {
            Buff.SetActive(true);
        }
        else
        {
            Buff.SetActive(false);
        }

        // 플레이어가 비밀방에 들어갔을 때 보스 체력바를 활성화합니다.
        if (secretArea.EnterTheBossRoom)
        {
            BosshealthBar.SetActive(true);
        }
        
        ItemAndSkillChecker();

        // 보스의 체력이 0 이하일 때 체력바를 비활성화
        if (bossEntity != null && bossEntity.stats.currentHealth <= 0)
        {
            BosshealthBar.SetActive(false);
        }



        // 힐 스킬
        if (Input.GetKeyDown(KeyCode.LeftShift) && Inventory.instance.GetAnimType(AnimationType.AttackWithGGSword))
            SetCooldownOf(healSkillImage, skills.heal.cooldown);

        CheckCooldownOf(healSkillImage, skills.heal.cooldown);

        // 힐2 스킬
        if (Input.GetKeyDown(KeyCode.LeftShift) && Inventory.instance.GetAnimType(AnimationType.AttackWithGGSword2))
            SetCooldownOf(healSkillImage2, skills.druidHeal.cooldown);

        CheckCooldownOf(healSkillImage2, skills.druidHeal.cooldown);

        //공속 스킬
        if(Input.GetKeyDown(KeyCode.LeftShift) && Inventory.instance.GetAnimType(AnimationType.AttackWithBBSword))
            SetCooldownOf(attackSpeedUpImage, skills.attackSpeedUp.cooldown);

        //공속2 스킬
        if (Input.GetKeyDown(KeyCode.LeftShift) && Inventory.instance.GetAnimType(AnimationType.AttackWithBBSword2))
            SetCooldownOf(attackSpeedUpImage2, skills.thirdExtraHitSkill.cooldown);

        CheckCooldownOf(attackSpeedUpImage2, skills.thirdExtraHitSkill.cooldown);

        if (Input.GetKeyDown(KeyCode.Space) && Inventory.instance.GetEquipment(EquipmentType.Flask) != null)
        SetCooldownOf(flaskImage, Inventory.instance.flaskCooldown);

        CheckCooldownOf(flaskImage, Inventory.instance.flaskCooldown);



    }

    private void UpdateHealthUI()
    {
        slider.maxValue = playerStats.GetMaxHealthValue();
        slider.value = playerStats.currentHealth;
    }


    //Skills
    private void SetCooldownOf(Image _image, float _cooldown)
    {
        if (_image.fillAmount <= 0)
            _image.fillAmount = 1;
    }


    private void CheckCooldownOf(Image _image, float _cooldown)
    {
        if (_image.fillAmount > 0)
            _image.fillAmount -= 1 / _cooldown * Time.deltaTime;
    }

    private void ItemAndSkillChecker()
    {
        //Weapon_BSword
        bool isBSwordEquipped = Inventory.instance.GetAnimType(AnimationType.AttackWithBSword);
        bool isGGSwordEquipped = Inventory.instance.GetAnimType(AnimationType.AttackWithGGSword);
        bool isGGSword2Equipped = Inventory.instance.GetAnimType(AnimationType.AttackWithGGSword2);
        bool isBBSwordEquipped = Inventory.instance.GetAnimType(AnimationType.AttackWithBBSword);
        bool isBBSword2Equipped = Inventory.instance.GetAnimType(AnimationType.AttackWithBBSword2);

        //Weapon_BHammer
        bool isBHammerEquipped = Inventory.instance.GetAnimType(AnimationType.AttackWithBHammer);
        bool isIBHammerEquipped = Inventory.instance.GetAnimType(AnimationType.AttackWithIBHammer);
        bool isIBHammer2Equipped = Inventory.instance.GetAnimType(AnimationType.AttackWithIBHammer2);
        bool isTBHammerEquipped = Inventory.instance.GetAnimType(AnimationType.AttackWithTBHammer);
        bool isTBHammer2Equipped = Inventory.instance.GetAnimType(AnimationType.AttackWithTBHammer2);

        //Weapon_SSword
        bool isSSwordEquipped = Inventory.instance.GetAnimType(AnimationType.AttackWithSSword);
        bool isBSSwordEquipped = Inventory.instance.GetAnimType(AnimationType.AttackWithBSSword);
        bool isBSSword2Equipped = Inventory.instance.GetAnimType(AnimationType.AttackWithBSSword2);
        bool isPSSwordEquipped = Inventory.instance.GetAnimType(AnimationType.AttackWithPSSword);
        bool isPSSword2Equipped = Inventory.instance.GetAnimType(AnimationType.AttackWithPSSword2);


        //Armors
        bool isNatureEquipped = Inventory.instance.GetArmorType(ArmorType.Nature);
        bool isNature2Equipped = Inventory.instance.GetArmorType(ArmorType.Nature2);

        bool isBerserkEquipped = Inventory.instance.GetArmorType(ArmorType.Berserk);
        bool isBerserk2Equipped = Inventory.instance.GetArmorType(ArmorType.Berserk2);

        bool isFlaskEquipped = Inventory.instance.GetEquipment(EquipmentType.Flask);



        #region BSword
        //BSword
        if (isBSwordEquipped)
        {
            SetSkillUI(defaultSkillUI, defaultSkillImage);
        }

        // BBSword
        if (isBBSwordEquipped)
        {
            SetSkillUI(attackSpeedUpUI, attackSpeedUpImage);
        }
        else
        {
            attackSpeedUpUI.SetActive(false);
        }
        
        // BBSword2
        if (isBBSword2Equipped)
        {
            SetSkillUI(attackSpeedUpUI2, attackSpeedUpImage2);
        }
        else
        {
            attackSpeedUpUI2.SetActive(false);
        }


        // GBSword
        if (isGGSwordEquipped)
        {
            SetSkillUI(healSkillUI, healSkillImage);
        }
        else
        {
            healSkillUI.SetActive(false);
        }

        // GBSword2
        if (isGGSword2Equipped)
        {
            SetSkillUI(healSkill2UI, healSkillImage2);
        }
        else
        {
            healSkill2UI.SetActive(false);
        }
        #endregion

        #region BHammer
        //BHammer
        if (isBHammerEquipped)
        {
            SetSkillUI(defaultSkillUI, defaultSkillImage);
        }

        //IBHammer
        if (isIBHammerEquipped)
        {
            SetSkillUI(defaultSkillUI, defaultSkillImage);
        }

        //IBHammer2
        if (isIBHammer2Equipped)
        {
            SetSkillUI(defaultSkillUI, defaultSkillImage);
        }

        //TBHammer
        if (isTBHammerEquipped)
        {
            SetSkillUI(defaultSkillUI, defaultSkillImage);
        }

        //TBHammer2
        if (isTBHammer2Equipped)
        {
            SetSkillUI(defaultSkillUI, defaultSkillImage);
        }
        #endregion

        #region SSword
        //SSword
        if (isSSwordEquipped)
        {
            SetSkillUI(defaultSkillUI, defaultSkillImage);
        }

        //BSSword
        if (isBSSwordEquipped)
        {
            SetSkillUI(defaultSkillUI, defaultSkillImage);
        }

        //BSSword2
        if (isBSSword2Equipped)
        {
            SetSkillUI(defaultSkillUI, defaultSkillImage);
        }

        //PSSword
        if (isPSSwordEquipped)
        {
            SetSkillUI(defaultSkillUI, defaultSkillImage);
        }

        //PSSword2
        if (isPSSword2Equipped)
        {
            SetSkillUI(defaultSkillUI, defaultSkillImage);
        }

        #endregion


        //Armor


        // Nature
        if (isNatureEquipped)
        {
            SetSkillUI(healPassiveUI, healPassiveImage);
        }
        else
        {
            healPassiveUI.SetActive(false);
        }

        // Nature2
        if (isNature2Equipped)
        {
            SetSkillUI(healPassive2UI, healPassiveImage2);
        }
        else
        {
            healPassive2UI.SetActive(false);
        }

        if (isBerserkEquipped)
        {
            SetSkillUI(berserkPassiveUI, berserkPassiveImage);
        }
        else
        {
           berserkPassiveUI.SetActive(false);
        }


        if (isBerserk2Equipped)
        {
            SetSkillUI(berserkPassive2UI, berserkPassiveImage2);
        }
        else
        {
            berserkPassive2UI.SetActive(false);
        }

        if (isFlaskEquipped)
        {
            SetSkillUI(flaskImageUI, flaskImage);
        }
        else
        {
            flaskImageUI.SetActive(false);
        }



        // 두 무기가 모두 장착되지 않은 경우에도 아이콘 초기화
        if (!isBSwordEquipped && !isGGSwordEquipped && !isGGSword2Equipped && !isBBSwordEquipped && !isBBSword2Equipped && !isBHammerEquipped && !isIBHammerEquipped && !isIBHammer2Equipped && !isTBHammerEquipped && !isTBHammer2Equipped && !isSSwordEquipped  && !isBSSwordEquipped && !isBSSword2Equipped && !isPSSwordEquipped && !isPSSword2Equipped && !isNatureEquipped && !isNature2Equipped && !isBerserkEquipped && !isBerserk2Equipped )
        {
            ClearSkillIcon();
        }

       
    }

    private void SetSkillUI(GameObject skillUI, Image _skillImage)
    {
        skillUI.SetActive(true);

        // 현재 장착된 무기의 아이콘을 가져와 할당합니다.
        ItemData_Equipment mainHandWeapon = Inventory.instance.GetEquipment(EquipmentType.WeaponMainHand);

        if (mainHandWeapon != null)
        {
            currentWeaponIcon.sprite = mainHandWeapon.itemIcon;
            currentWeaponIcon.color = Color.white; // 이미지의 알파값을 1로 설정합니다.
        }
        else
        {
            // 무기가 없는 경우에는 currentWeaponIcon을 비웁니다.
            ClearSkillIcon();
        }

        // 플라스크 장착 여부 확인
        ItemData_Equipment flask = Inventory.instance.GetEquipment(EquipmentType.Flask);
        if (flask != null)
        {
            currentFlaskIcon.sprite = flask.itemIcon; // 플라스크 아이콘 할당
            currentFlaskIcon.color = Color.white; // 알파값을 1로 설정
        }
        else
        {
            ClearFlaskIcon(); // 플라스크가 장착되지 않은 경우 아이콘 초기화
        }
    }

    private void ClearSkillIcon()
    {
        // 무기가 없는 경우에도 currentWeaponIcon을 비웁니다.
        currentWeaponIcon.sprite = null;
        currentWeaponIcon.color = Color.clear; // 이미지의 알파값을 0으로 설정합니다.
    }

    private void ClearFlaskIcon()
    {
        currentFlaskIcon.sprite = null; // 플라스크 아이콘 비우기
        currentFlaskIcon.color = Color.clear; // 알파값을 0으로 설정
    }



}