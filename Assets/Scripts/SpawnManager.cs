 using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class SpawnManager : MonoBehaviour
{
    private GameObject[] managers;
    public GameObject enemyPrefab;


    public float spawnRate = 2.5f;
    private float _timer;



    public int currentWave = 0;
    public int numOfEnemy = 3;
    public int currentEnemy = 0;
    public int startEnemyCount = 3;
    public int enemiesAlive = 0;
    public bool isWaveActive = false;
    public int enemyIncrement = 4;
    public float waveBreakTime =3.5f;

    // Start is called before the first frame update
    void Start()
    {
        managers = GameObject.FindGameObjectsWithTag("Spawner");
        Debug.Log($"Spawners : {managers.Length}");

        numOfEnemy = startEnemyCount;
        StartCoroutine(StartWaveWithDelay());


    }

    
    void Update()
    {
        if (!isWaveActive) return;
            
            
            if (currentEnemy < numOfEnemy)
            {
                _timer += Time.deltaTime;
                if (_timer >= spawnRate)
                {
                    SpawnEnemy();
                    _timer = 0;
                }
            }
            
            
            if (currentEnemy >= numOfEnemy && enemiesAlive <= 0)
            {
                CompleteWave();
            }

    }

    private IEnumerator StartWaveWithDelay()
    {
        yield return new WaitForSeconds(waveBreakTime);
        StartNewWave();
    }

    void SpawnEnemy()
    {
        if(managers.Length == 0 )
        {
            Debug.Log("Нет спавнеров");
            return;
        }

        int randomIndex = Random.Range(0, managers.Length);
        Spawner spawner = managers[randomIndex].GetComponent<Spawner>();

        if (spawner != null && enemyPrefab != null)
        {
            spawner.Spawn(enemyPrefab);
            currentEnemy++;
            enemiesAlive++;
            
            
            Debug.Log($"Заспавнен враг {currentEnemy}/{numOfEnemy}. Живых врагов: {enemiesAlive}");
        }
    }


    public void StartNewWave()
    {
        currentWave++;
        currentEnemy = 0;
        enemiesAlive = 0;
        isWaveActive = true;
        
        
        numOfEnemy += Random.Range(0, enemyIncrement);
        
        
        
        Debug.Log($"=== ВОЛНА {currentWave} НАЧАЛАСЬ ===");
        Debug.Log($"Врагов в волне: {numOfEnemy}");
        Debug.Log($"Спавн каждые: {spawnRate} сек");
        
    }

    void CompleteWave()
    {
        isWaveActive = false;
        
        Debug.Log($"=== ВОЛНА {currentWave} ЗАВЕРШЕНА ===");
        Debug.Log($"Всего врагов в волне: {numOfEnemy}");
        
        
        
        
        StartCoroutine(WaveBreak());
    }

    IEnumerator WaveBreak()
    {
        Debug.Log($"Перерыв между волнами: {waveBreakTime} сек");
        yield return new WaitForSeconds(waveBreakTime);
        
        StartNewWave();
    }

    public void EnemyDefeated()
    {
        enemiesAlive--;
        enemiesAlive = Mathf.Max(0, enemiesAlive);
        
        Debug.Log($"Враг убит. Осталось живых: {enemiesAlive}");
        
        
        
    }


   
}
