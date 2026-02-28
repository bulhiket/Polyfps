using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; set;}
    public TextMeshProUGUI ammoUi;
    public TextMeshProUGUI waveTxt;
    public TextMeshProUGUI enemyTxt;
    public GameObject Hitmarker;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAmmoUI(int maxAmmo, int currentAmmo)
    {
        ammoUi.text = $"{currentAmmo}/{maxAmmo}";
    }

    public void SetWave(int wave)
    {
        waveTxt.text = $"Волна: {wave}";
    }

    public void SetEnemy(int enemys)
    {
        enemyTxt.text = $"Врагов: {enemys}";
    }

    public void SetHitWithDelay()
    {
        StopAllCoroutines();
        StartCoroutine(SetHit());
    }

    private IEnumerator SetHit()
    {
        Hitmarker.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        Hitmarker.SetActive(false);
    }
    
}
