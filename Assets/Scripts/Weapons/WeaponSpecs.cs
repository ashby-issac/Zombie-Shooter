using UnityEngine;

[CreateAssetMenu(fileName = "AmmoData", menuName = "AmmoData")]
public class WeaponSpecs : ScriptableObject
{
    [System.Serializable]
    public class WeaponData
    {
        public WeaponType weaponType;
        public AmmoType ammoType;
        public int availAmmo;
        public int magazineSize;
        public float fireRate;
        public int range;
        public int damage;
    }

    [SerializeField] WeaponData[] weaponDatas;

    public WeaponData[] WeaponDatas => weaponDatas;

    public int GetAmmoAmount(WeaponType weaponType) => GetWeaponData(weaponType).availAmmo;
    public void SetAmmoAmount(WeaponType weaponType, int ammoAmt) => GetWeaponData(weaponType).availAmmo = ammoAmt;
    
    private WeaponData GetWeaponData(WeaponType weaponType)
    {
        foreach (WeaponData weaponData in weaponDatas)
            if (weaponData.weaponType == weaponType)
                return weaponData;
        
        return null;
    }
}
