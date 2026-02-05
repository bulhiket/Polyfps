using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    
    private float _timer;
    public float spawnRate = 2.5f;
    private GameObject _player;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    


    public void Spawn(GameObject enemyPrefab)
    {
        GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        // LabubuAI ai = enemy.GetComponent<LabubuAI>();
        AiEnemy ai = enemy.GetComponent<AiEnemy>();
        // ai._player = _player;
        // if(ai._player == null)
        // {
        //     Destroy(enemy);
        // }
        

    }
}
