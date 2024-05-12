using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject weaponRoot;

    public bool isWeaponActive { get; private set; }
    public GameObject owner { get; set; }
    public GameObject sourcePrefab { get; set; }

    public void ShowWeapon(bool show) {
        weaponRoot.SetActive(show);
        isWeaponActive = show;
    }
}
