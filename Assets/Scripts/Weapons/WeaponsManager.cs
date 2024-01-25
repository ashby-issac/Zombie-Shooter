using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WeaponsManager : MonoBehaviour
{
    [SerializeField] private List<Weapon> weapons;
    [SerializeField] private WeaponSpecs weaponSpecs;

    private static WeaponsManager instance;
    public static WeaponsManager Instance => instance;

    public List<Weapon> Weapons => weapons;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    void Start()
    {
        InitWeaponData();
    }

    private void InitWeaponData()
    {
        if (weapons.Count > 0)
        {
            for (int i = 0; i < weapons.Count; i++)
            {
                weapons[i].WeaponAttributes.FireRate = weaponSpecs.WeaponDatas[i].fireRate;
                weapons[i].WeaponAttributes.MagSize = weaponSpecs.WeaponDatas[i].magazineSize;
                weapons[i].WeaponAttributes.Range = weaponSpecs.WeaponDatas[i].range;
                weapons[i].WeaponType = weaponSpecs.WeaponDatas[i].weaponType;
                weapons[i].WeaponAttributes.AvailAmmo = weaponSpecs.WeaponDatas[i].availAmmo;
                weapons[i].AmmoType = weaponSpecs.WeaponDatas[i].ammoType;
                weapons[i].WeaponAttributes.Damage = weaponSpecs.WeaponDatas[i].damage;
            }
        }
    }
}
