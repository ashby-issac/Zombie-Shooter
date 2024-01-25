using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    GameObject hitVFX;

    public static VFXManager Instance;

    void Awake()
    {
        Instance = this;
    }

    public void CreateHitImpact(RaycastHit hit)
    {
        hitVFX = Instantiate(hitVFX, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(hitVFX);
    }
}
