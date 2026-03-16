
using UnityEngine;

public class BotHPManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int HP = 100;
    private Rigidbody rb;
    public int impactForce = 5;
    private SpawnManager spawnManager;
    public GameObject[] drop;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        spawnManager = FindObjectOfType<SpawnManager>();
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
            Die();

            
            if (spawnManager != null)
            {
                spawnManager.EnemyDefeated();
            }
        }
    }


    private void Die()
    {
        
        
        Drop();
        Destroy(gameObject);
        
    }

    private void Drop()
    {
        if(drop.Length == 0) return;

        float chance = Random.Range(10f, 75f);
        if(chance > 50)
        {
            int drop_index = Random.Range(0, drop.Length);
            Instantiate(drop[drop_index], transform.position + Vector3.up, Quaternion.identity);
        }
        
    }

}
