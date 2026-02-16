using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.PackageManager;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; set;}
    public TextMeshProUGUI ammoUi;
    public TextMeshProUGUI waveTxt;

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

    
}
