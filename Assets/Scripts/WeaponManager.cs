
using TMPro;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject[] weapons;
    public bool[] unlockedWeapons;
    private WeaponCore currentweaponComponent;
    private int currentWeapon = 0;
    public TextMeshProUGUI ammoUi;
    void Start()
    {

        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(i == currentWeapon && unlockedWeapons[i]);
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

            
            if(newWeapon < 0) newWeapon = weapons.Length - 1;
            if(newWeapon > weapons.Length - 1) newWeapon = 0;
            SwitchWeapon(newWeapon);
        }
    }

    private void SwitchWeapon(int newWeaponIndex)
    {
        if(currentweaponComponent.canSwitch)
        {
            
            weapons[currentWeapon].SetActive(false);

            
            weapons[newWeaponIndex].SetActive(true);

            currentWeapon = newWeaponIndex;

            GetCurrentWeapon();

            Debug.Log($"Переключено на оружие: {newWeaponIndex}");
        }

        else
        {
            Debug.Log("Оружие стреляет или перезаряжается");
        }


       



        
    }
    
    private void GetCurrentWeapon()
    {
        currentweaponComponent = weapons[currentWeapon].GetComponent<WeaponCore>();
        
    }

    public void AddAmmo(int amount)
    {
        currentweaponComponent.allBullets += amount;
        currentweaponComponent.UpdateUI();
    }

    
}
