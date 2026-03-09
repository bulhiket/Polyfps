
using UnityEngine;

public class Take : MonoBehaviour
{
    public PlayerHP playerHP;
    public WeaponManager wp_manager;

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Heal"))
        {
            
            playerHP.AddHP(Random.Range(5, 45));
            Destroy(other.gameObject);
        }

        if(other.CompareTag("Ammo"))
        {
            wp_manager.AddAmmo(Random.Range(20, 55));
            Destroy(other.gameObject);
        }
    }
}
