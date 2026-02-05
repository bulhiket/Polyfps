using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public UIManager Instance { get; set;}
    public TextMeshProUGUI ammoUi;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            DestroyImmediate(Instance.gameObject);
            Instance = this;
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

    
}
