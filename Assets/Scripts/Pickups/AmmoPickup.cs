using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] AudioClip ammoPickupClip;
    [SerializeField] AmmoType ammoType;
    [SerializeField] WeaponSpecs ammo;
    [SerializeField] int pickupAmt;

    AudioSource audioSource;

    void Start()
    {
        audioSource = FindObjectOfType<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        { 
            gameObject.SetActive(false);
            audioSource.PlayOneShot(ammoPickupClip);
            WeaponsManager.Instance.Weapons.ForEach(weapon =>
            {
                if (weapon.AmmoType == ammoType)
                    weapon.AddCollectedAmmo(pickupAmt);
            });
        }
    }
}
