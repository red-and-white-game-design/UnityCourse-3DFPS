using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerWeaponManagerT : MonoBehaviour
{
    public List<WeaponControllerT> startingWeapons = new List<WeaponControllerT>();

    public Transform weaponParentSocket;

    public UnityAction<WeaponControllerT> onSwitchWeapon;

    private WeaponControllerT[] _weaponSlots = new WeaponControllerT[9];

    private void Start() {
        onSwitchWeapon += OnWeaponSwitched;

        foreach (WeaponControllerT weapon in startingWeapons) {
            AddWeapon(weapon);
        }

        SwitchWeapon();
    }

    public bool AddWeapon(WeaponControllerT weaponPrefab) {
        for (int i = 0; i < _weaponSlots.Length; i++) {
            WeaponControllerT weaponInstance = Instantiate(weaponPrefab, weaponParentSocket);
            weaponInstance.transform.localPosition = Vector3.zero;
            weaponInstance.transform.localRotation = Quaternion.identity;

            weaponInstance.owner = gameObject;
            weaponInstance.sourcePrefab = weaponPrefab.gameObject;
            weaponInstance.ShowWeapon(false);

            _weaponSlots[i] = weaponInstance;

            return true;
        }

        return false;
    }

    public void SwitchWeapon() {
        SwitchWeaponToIndex(0);
    }

    public void SwitchWeaponToIndex(int newWeaponIndex) {
        if (newWeaponIndex >= 0) {
            WeaponControllerT newWeapon = GetWeaponAtSlotIndex(newWeaponIndex);

            if (onSwitchWeapon != null) {
                onSwitchWeapon.Invoke(newWeapon);
            }
        }
    }

    public WeaponControllerT GetWeaponAtSlotIndex(int index) {
        if (index >= 0 && index < _weaponSlots.Length) {
            return _weaponSlots[index];
        }

        return null;
    }

    private void OnWeaponSwitched(WeaponControllerT newWeapon) {
        if (newWeapon != null) {
            newWeapon.ShowWeapon(true);
        }
    }
}
