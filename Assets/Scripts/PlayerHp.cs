using UnityEngine.UI;
using UnityEngine;
using Unity.VisualScripting;

public class PlayerHP : MonoBehaviour
{
    // Start is called before the first frame update
    public int HP = 100;
    public Slider HP_slider;
    
    private int currentHp;

    void Start()
    {
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
        Debug.Log($"Осталось - {HP}");

        if (currentHp == 0)
        {
            Destroy(gameObject);
        }
    }


    
    
}
