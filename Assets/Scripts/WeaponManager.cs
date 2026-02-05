using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening.Core.Easing;
using TMPro;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject[] weapons;
    private WeaponCore currentweaponComponent;
    private int currentWeapon = 0;
    public TextMeshProUGUI ammoUi;
    void Start()
    {
        weapons = GameObject.FindGameObjectsWithTag("Weapon");
        for (int i = 1; i <= weapons.Length; i++)
        {
            weapons[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentweaponComponent == null)
        {
            GetCurrentWeapon();
        }
        
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            int newWeapon = currentWeapon + (scroll > 0 ? 1 : -1);

            // Проверка границ массива
            if (newWeapon >= 0 && newWeapon < weapons.Length)
            {
                SwitchWeapon(newWeapon);
            }
        }
    }

    private void SwitchWeapon(int newWeaponIndex)
    {
        // Выключаем текущее оружие
        weapons[currentWeapon].SetActive(false);

        // Включаем новое
        weapons[newWeaponIndex].SetActive(true);



        currentWeapon = newWeaponIndex;

        GetCurrentWeapon();

        Debug.Log($"Переключено на оружие: {newWeaponIndex}");
    }
    
    private void GetCurrentWeapon()
    {
        currentweaponComponent = weapons[currentWeapon].GetComponent<WeaponCore>();
    }

    void OnGUI()
    {
        ammoUi.text = $"{currentweaponComponent.bulletsLeft}/{currentweaponComponent.allBullets}";
    }
}
