using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponAttributes
{
    private int availAmmo;
    private int magSize;
    private float fireRate;
    private float range;
    private int damage;

    public int AvailAmmo { get => availAmmo; set => availAmmo = value; }
    public int MagSize { get => magSize; set => magSize = value; }
    public float FireRate { get => fireRate; set => fireRate = value; }
    public float Range { get => range; set => range = value; }
    public int Damage { get => damage; set => damage = value; }
}

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera fpCamera;
    [SerializeField] WeaponSpecs weaponSpec;

    [SerializeField] ParticleSystem muzzleFlashVFX;
    [SerializeField] GameObject hitVFX;

    [SerializeField] AudioClip gunShotSFX;
    [SerializeField] AudioClip emptyGunShotSFX;

    AudioSource audioSource;
    bool canShoot;

    public WeaponAttributes WeaponAttributes { get; set; }

    private AmmoType ammoType;
    public AmmoType AmmoType { get => ammoType; set => ammoType = value; }

    private WeaponType weaponType;
    public WeaponType WeaponType { get => weaponType; set => weaponType = value; }

    void OnEnable()
    {
        canShoot = true;
    }

    void Start()
    {
        audioSource = fpCamera.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canShoot && !DeathHandler.IsGameOver)
        {
            StartCoroutine(ShootEnemy());
        }
    }

    IEnumerator ShootEnemy()
    {
        canShoot = false;
        if (WeaponAttributes.AvailAmmo > 0) 
        {
            PlaySFX(gunShotSFX);
            DecreaseAmmo();
            PlayMuzzleVFX();
            ProcessRaycast();
        }
        else
        {
            PlaySFX(emptyGunShotSFX);
        }

        yield return new WaitForSeconds(WeaponAttributes.FireRate);
        canShoot = true;
    }

    void ProcessRaycast()
    {
        RaycastHit hit;
        Vector3 start = fpCamera.transform.position;
        Vector3 direction = fpCamera.transform.forward;

        if (Physics.Raycast(start, direction, out hit, WeaponAttributes.Range))
        {
            CreateHitVFX(hit);
            EnemyHealth enemyHealth = hit.transform?.GetComponent<EnemyHealth>();
            if (enemyHealth)
                enemyHealth.DamageEnemy(WeaponAttributes.Damage);
        }
    }

    void PlaySFX(AudioClip gunShotSFX) => audioSource.PlayOneShot(gunShotSFX);
    void PlayMuzzleVFX() => muzzleFlashVFX.Play();
    
    void CreateHitVFX(RaycastHit hit)
    {
        GameObject tempHitVFX = Instantiate(hitVFX, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(tempHitVFX, 0.1f);
    }

    public void AddCollectedAmmo(int ammoCount) => WeaponAttributes.AvailAmmo += ammoCount;
    public void DecreaseAmmo() => WeaponAttributes.AvailAmmo--;
}
