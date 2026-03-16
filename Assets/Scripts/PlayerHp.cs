using UnityEngine.UI;
using UnityEngine;
using System;
using YG;

public class PlayerHP : MonoBehaviour
{
    // Start is called before the first frame update
    public int HP = 100;
    public Slider HP_slider;
    
    private int currentHp;
    public Vector3 startPos;

    public MonoBehaviour look_script;
    public GameObject weaponHolder;
    private AudioSource audio;
    public AudioClip hitSound;

    void Start()
    {
        look_script = GetComponentInChildren<MouseLook>();
        audio = GetComponent<AudioSource>();
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
        audio.PlayOneShot(hitSound);
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
        
        look_script.enabled = false;
        weaponHolder.SetActive(false);

        Time.timeScale = 0;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        UIManager.Instance.ShowDeathUI();

    }

    public void Revive()
    {
        currentHp = HP;
        UIManager.Instance.ShowGameUI();
        transform.position = startPos;
        HP_slider.value = currentHp;
        look_script.enabled = true;
        weaponHolder.SetActive(true);
        Cursor.lockState =CursorLockMode.Locked;
        Time.timeScale = 1;
    }


    
    
}
