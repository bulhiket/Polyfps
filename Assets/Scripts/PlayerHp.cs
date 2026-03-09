using UnityEngine.UI;
using UnityEngine;
using System;
using NUnit.Framework.Constraints;

public class PlayerHP : MonoBehaviour
{
    // Start is called before the first frame update
    public int HP = 100;
    public Slider HP_slider;
    
    private int currentHp;

    public MonoBehaviour look_script;
    public GameObject weaponHolder;

    void Start()
    {
       currentHp = HP;
       HP_slider.maxValue = HP;
       HP_slider.value = currentHp;
    }


    public void TakeDamage(int amount)
    {
        if (amount < 0)
        {
            amount = 0;
        }

        currentHp -= amount;
        currentHp = Mathf.Max(0, currentHp);
        HP_slider.value = currentHp;
        Debug.Log($"Осталось - {HP}");

        if (currentHp == 0)
        {
            Die();
            
        }
    }

    public void AddHP(int amount)
    {
        currentHp = Math.Max(currentHp + amount, HP);
        HP_slider.value = currentHp;
    }

    private void Die()
    {
        look_script = GetComponentInChildren<MouseLook>();
        look_script.enabled = false;
        weaponHolder.SetActive(false);

        Time.timeScale = 0;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }


    
    
}
