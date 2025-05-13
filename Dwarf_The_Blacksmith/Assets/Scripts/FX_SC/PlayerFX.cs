using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFX : EntityFX
{
    [Header("After image fx")]
    [SerializeField] private float afterImageCooldown;
    [SerializeField] private GameObject afterimagePrefab;
    [SerializeField] private float colotLooseRate;
    private float afterImageCooldownTimer;

    [Header("Player Movement FX")]
    [SerializeField] private GameObject DashFX;

    [Header("Player Attack FX")]
    [SerializeField] private GameObject playerHitFx;
    [SerializeField] private GameObject playerCriticalHitFx;

    [SerializeField] private GameObject BSwordHitFx;
    [SerializeField] private GameObject BSwordCriHitFx;

    [SerializeField] private GameObject BBSwordHitFx;
    [SerializeField] private GameObject BBSwordCriHitFx;

    [SerializeField] private GameObject GGSwordHitFx;
    [SerializeField] private GameObject GGSwordCriHitFx;

    [SerializeField] private GameObject GGSwordHitFx2;
    [SerializeField] private GameObject GGSwordCriHitFx2;

    //BHammer
    [SerializeField] private GameObject BHammerHitFx;
    [SerializeField] private GameObject BHammerCriHitFx;

    [SerializeField] private GameObject IBHammerHitFx;
    [SerializeField] private GameObject IBHammerCriHitFx;

    [SerializeField] private GameObject IBHammer2HitFx;
    [SerializeField] private GameObject IBHammer2CriHitFx;

    [SerializeField] private GameObject TBHammerHitFx;
    [SerializeField] private GameObject TBHammerCriHitFx;

    [SerializeField] private GameObject TBHammer2HitFx;
    [SerializeField] private GameObject TBHammer2CriHitFx;

    //SSword
    [SerializeField] private GameObject SSwordHitFx;
    [SerializeField] private GameObject SSwordCriHitFx;

    [SerializeField] private GameObject BSSwordHitFx;
    [SerializeField] private GameObject BSSwordCriHitFx;

    [SerializeField] private GameObject BSSword2HitFx;
    [SerializeField] private GameObject BSSword2CriHitFx;

    [SerializeField] private GameObject PSSwordHitFx;
    [SerializeField] private GameObject PSSwordCriHitFx;

    [SerializeField] private GameObject PSSword2HitFx;
    [SerializeField] private GameObject PSSword2CriHitFx;

    protected override void Start()
    {
        base.Start();
    }


    void Update()
    {
        afterImageCooldownTimer -= Time.deltaTime;
    }

    public void CreateAfterImage()
    {
        if (afterImageCooldownTimer < 0)
        {
            afterImageCooldownTimer = afterImageCooldown;
            GameObject newAfterImage = Instantiate(afterimagePrefab, transform.position, transform.rotation);
            newAfterImage.GetComponent<AfterImageFX>().SetupAfterImage(colotLooseRate, sr.sprite);
        }
    }

    public override void CreateHitFx(Transform _target, bool _critical)
    {
        CharacterStats _stats = GetComponent<CharacterStats>();

        if (_stats.isDead)
            return;

        float zRotation = Random.Range(-90, 90);
        float xPosition = Random.Range(-.5f, .5f);
        float yPosition = Random.Range(-.5f, .5f);

        Vector3 hitFxRotaion = new Vector3(0, 0, zRotation);

        GameObject hitPrefab = null;
        GameObject CriHitPrefab = null;


        // 무기의 애니메이션 타입을 가져와서 BSword 애니메이션을 사용하는지 확인
        ItemData_Equipment currentWeapon = Inventory.instance.GetEquipment(EquipmentType.WeaponMainHand);

        
        //BSword
        if (currentWeapon != null && currentWeapon.animationType == AnimationType.AttackWithBSword)
        {

            hitPrefab = BSwordHitFx;

            AudioManager.instance.PlaySFX(26, null);

            if (_critical)
            {
                AudioManager.instance.PlaySFX(14, null);

                CriHitPrefab = BSwordCriHitFx;

                float yRotation = 0;
                zRotation = Random.Range(-45, 45);

                if (GetComponent<Entity>().facingDir == -1)
                    yRotation = 180;

                hitFxRotaion = new Vector3(0, yRotation, zRotation);
            }
        }

        //BBSword
        else if (currentWeapon != null && currentWeapon.animationType == AnimationType.AttackWithBBSword)
        {
            hitPrefab = BBSwordHitFx;

            AudioManager.instance.PlaySFX(26, null);

            if (_critical)
            {
                AudioManager.instance.PlaySFX(14, null);

                CriHitPrefab = BSwordCriHitFx;

                float yRotation = 0;
                zRotation = Random.Range(-45, 45);

                if (GetComponent<Entity>().facingDir == -1)
                    yRotation = 180;

                hitFxRotaion = new Vector3(0, yRotation, zRotation);
            }
        }

        //BBSword2
        else if (currentWeapon != null && currentWeapon.animationType == AnimationType.AttackWithBBSword2)
        {
            hitPrefab = BBSwordHitFx;

            AudioManager.instance.PlaySFX(26, null);

            if (_critical)
            {
                AudioManager.instance.PlaySFX(14, null);

                CriHitPrefab = BSwordCriHitFx;

                float yRotation = 0;
                zRotation = Random.Range(-45, 45);

                if (GetComponent<Entity>().facingDir == -1)
                    yRotation = 180;

                hitFxRotaion = new Vector3(0, yRotation, zRotation);
            }
        }

        //GGSword
        else if (currentWeapon != null && currentWeapon.animationType == AnimationType.AttackWithGGSword)
        {
            hitPrefab = GGSwordHitFx;

            AudioManager.instance.PlaySFX(26, null);

            if (_critical)
            {
                AudioManager.instance.PlaySFX(14, null);

                CriHitPrefab = GGSwordCriHitFx;

                float yRotation = 0;
                zRotation = Random.Range(-45, 45);

                if (GetComponent<Entity>().facingDir == -1)
                    yRotation = 180;

                hitFxRotaion = new Vector3(0, yRotation, zRotation);
            }
        }

        //GGSword2
        else if (currentWeapon != null && currentWeapon.animationType == AnimationType.AttackWithGGSword2)
        {
            hitPrefab = GGSwordHitFx2;

            AudioManager.instance.PlaySFX(26, null);

            if (_critical)
            {
                AudioManager.instance.PlaySFX(14, null);

                CriHitPrefab = GGSwordCriHitFx2;

                float yRotation = 0;
                zRotation = Random.Range(-45, 45);

                if (GetComponent<Entity>().facingDir == -1)
                    yRotation = 180;

                hitFxRotaion = new Vector3(0, yRotation, zRotation);
            }
        }

        //수정필요

        //BHammer
        else if (currentWeapon != null && currentWeapon.animationType == AnimationType.AttackWithBHammer)
        {

            hitPrefab = BHammerHitFx;

            AudioManager.instance.PlaySFX(25, null);

            if (_critical)
            {
                AudioManager.instance.PlaySFX(14, null);

                CriHitPrefab = BHammerCriHitFx;

                float yRotation = 0;
                zRotation = Random.Range(-45, 45);

                if (GetComponent<Entity>().facingDir == -1)
                    yRotation = 180;

                hitFxRotaion = new Vector3(0, yRotation, zRotation);
            }
        }

        //IBHammer
        else if (currentWeapon != null && currentWeapon.animationType == AnimationType.AttackWithIBHammer)
        {

            hitPrefab = IBHammerHitFx;

            AudioManager.instance.PlaySFX(31, null);

            if (_critical)
            {
                AudioManager.instance.PlaySFX(14, null);

                CriHitPrefab = IBHammerCriHitFx;

                float yRotation = 0;
                zRotation = Random.Range(-45, 45);

                if (GetComponent<Entity>().facingDir == -1)
                    yRotation = 180;

                hitFxRotaion = new Vector3(0, yRotation, zRotation);
            }
        }

        //IBHammer2
        else if (currentWeapon != null && currentWeapon.animationType == AnimationType.AttackWithIBHammer2)
        {

            hitPrefab = IBHammer2HitFx;

            AudioManager.instance.PlaySFX(31, null);

            if (_critical)
            {
                AudioManager.instance.PlaySFX(14, null);

                CriHitPrefab = IBHammer2CriHitFx;

                float yRotation = 0;
                zRotation = Random.Range(-45, 45);

                if (GetComponent<Entity>().facingDir == -1)
                    yRotation = 180;

                hitFxRotaion = new Vector3(0, yRotation, zRotation);
            }
        }

        //TBHammer
        else if (currentWeapon != null && currentWeapon.animationType == AnimationType.AttackWithTBHammer)
        {

            hitPrefab = TBHammerHitFx;

            AudioManager.instance.PlaySFX(32, null);

            if (_critical)
            {
                AudioManager.instance.PlaySFX(14, null);

                CriHitPrefab = TBHammerCriHitFx;

                float yRotation = 0;
                zRotation = Random.Range(-45, 45);

                if (GetComponent<Entity>().facingDir == -1)
                    yRotation = 180;

                hitFxRotaion = new Vector3(0, yRotation, zRotation);
            }
        }

        //TBHammer2
        else if (currentWeapon != null && currentWeapon.animationType == AnimationType.AttackWithTBHammer2)
        {

            hitPrefab = TBHammer2HitFx;

            AudioManager.instance.PlaySFX(32, null);

            if (_critical)
            {
                AudioManager.instance.PlaySFX(14, null);

                CriHitPrefab = TBHammer2CriHitFx;

                float yRotation = 0;
                zRotation = Random.Range(-45, 45);

                if (GetComponent<Entity>().facingDir == -1)
                    yRotation = 180;

                hitFxRotaion = new Vector3(0, yRotation, zRotation);
            }
        }

        //SSword
        else if (currentWeapon != null && currentWeapon.animationType == AnimationType.AttackWithSSword)
        {

            hitPrefab = SSwordHitFx;

            AudioManager.instance.PlaySFX(34, null);

            if (_critical)
            {
                AudioManager.instance.PlaySFX(14, null);

                CriHitPrefab = SSwordCriHitFx;

                float yRotation = 0;
                zRotation = Random.Range(-45, 45);

                if (GetComponent<Entity>().facingDir == -1)
                    yRotation = 180;

                hitFxRotaion = new Vector3(0, yRotation, zRotation);
            }
        }

        //BSSword
        else if (currentWeapon != null && currentWeapon.animationType == AnimationType.AttackWithBSSword)
        {

            hitPrefab = BSSwordHitFx;

            AudioManager.instance.PlaySFX(34, null);

            if (_critical)
            {
                AudioManager.instance.PlaySFX(14, null);

                CriHitPrefab = BSSwordCriHitFx;

                float yRotation = 0;
                zRotation = Random.Range(-45, 45);

                if (GetComponent<Entity>().facingDir == -1)
                    yRotation = 180;

                hitFxRotaion = new Vector3(0, yRotation, zRotation);
            }
        }

        //BSSword2
        else if (currentWeapon != null && currentWeapon.animationType == AnimationType.AttackWithBSSword2)
        {

            hitPrefab = BSSword2HitFx;

            AudioManager.instance.PlaySFX(34, null);

            if (_critical)
            {
                AudioManager.instance.PlaySFX(14, null);

                CriHitPrefab = BSSword2CriHitFx;

                float yRotation = 0;
                zRotation = Random.Range(-45, 45);

                if (GetComponent<Entity>().facingDir == -1)
                    yRotation = 180;

                hitFxRotaion = new Vector3(0, yRotation, zRotation);
            }
        }

        //PSSword
        else if (currentWeapon != null && currentWeapon.animationType == AnimationType.AttackWithPSSword)
        {

            hitPrefab = PSSwordHitFx;

            AudioManager.instance.PlaySFX(34, null);

            if (_critical)
            {
                AudioManager.instance.PlaySFX(14, null);

                CriHitPrefab = PSSwordCriHitFx;

                float yRotation = 0;
                zRotation = Random.Range(-45, 45);

                if (GetComponent<Entity>().facingDir == -1)
                    yRotation = 180;

                hitFxRotaion = new Vector3(0, yRotation, zRotation);
            }
        }
        
        //PSSword2
        else if (currentWeapon != null && currentWeapon.animationType == AnimationType.AttackWithPSSword2)
        {

            hitPrefab = PSSword2HitFx;

            AudioManager.instance.PlaySFX(34, null);

            if (_critical)
            {
                AudioManager.instance.PlaySFX(14, null);

                CriHitPrefab = PSSword2CriHitFx;

                float yRotation = 0;
                zRotation = Random.Range(-45, 45);

                if (GetComponent<Entity>().facingDir == -1)
                    yRotation = 180;

                hitFxRotaion = new Vector3(0, yRotation, zRotation);
            }
        }



        else
        {
            hitPrefab = playerHitFx;

            if (_critical)
            {
                CriHitPrefab = playerCriticalHitFx;

                float yRotation = 0;
                zRotation = Random.Range(-45, 45);

                if (GetComponent<Entity>().facingDir == -1)
                    yRotation = 180;

                hitFxRotaion = new Vector3(0, yRotation, zRotation);
            }
        }

        GameObject newHitFx = Instantiate(hitPrefab, _target.position + new Vector3(xPosition, yPosition), Quaternion.identity);
        newHitFx.transform.Rotate(hitFxRotaion);
        Destroy(newHitFx, .5f);
    }



}
