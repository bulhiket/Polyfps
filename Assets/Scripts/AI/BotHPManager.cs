using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class BotHPManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int HP = 100;
    private Rigidbody rb;
    public int impactForce = 5;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    public void TakeDamage(int amount)
    {
        if (amount < 0)
        {
            amount = 0;
        }

        HP -= amount;
        // rb.AddForce(Vector3.back, ForceMode.Impulse);
        Debug.Log($"Осталось - {HP}");

        if (HP <= 0)
        {
            Destroy(gameObject);

            SpawnManager spawnManager = FindObjectOfType<SpawnManager>();
            if (spawnManager != null)
            {
                spawnManager.EnemyDefeated();
            }
        }
    }


    private void Die()
    {
        if (HP == 0)
        {
            Destroy(gameObject);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
