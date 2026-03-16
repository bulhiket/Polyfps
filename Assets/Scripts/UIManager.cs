using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using YG;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; set;}
    public TextMeshProUGUI ammoUi;
    public TextMeshProUGUI waveTxt;
    public TextMeshProUGUI enemyTxt;
    public GameObject Hitmarker;

    public GameObject gameUI;
    public GameObject deathUI;
    public PlayerHP playerHP;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            
        }
    }

    private void Onable()
    {
        YandexGame.RewardVideoEvent += GetReward;     
    }

    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= GetReward; 
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
    

    public void ShowDeathUI()
    {
        gameUI.SetActive(false);
        deathUI.SetActive(true);
    }

    public void ShowGameUI()
    {
        deathUI.SetActive(false);
        gameUI.SetActive(true);
        
    }

    public void RestartBtn()
    {
        YandexGame.FullscreenShow();
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;

    }
    public void ReviveBtn()
    {
        playerHP.Revive();
    }
    
    void OpenReward(int id)
    {
        YandexGame.RewVideoShow(id);
    }

    void GetReward(int id)
    {
        if(id == 1)
        {
            ReviveBtn();
        }
    }
}
