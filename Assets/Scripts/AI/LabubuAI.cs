using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class LabubuAI : MonoBehaviour
{
    public GameObject _player;
    private NavMeshAgent _agent;
    private Animator _animator;
        
    public int damage = 25;
    private float lastAttackTime = 0;
    public float attackRate = 1f;
    public float attackRange = 5f;
    bool canAtack = true;

    
    // Start is called before the first frame update
    void Awake()
    {
        PrepareForGame();
    }

    // Update is called once per frame
    
    void Update()
    {
        if(_player == null) return;
        float distance = Vector3.Distance(transform.position, _player.transform.position);

        if(distance > 3.5f)
        {
            _agent.isStopped = false;
            _agent.SetDestination(_player.transform.position);
            
        }

        else
        {
            if(canAtack)
            {
                AttackPlayer();
            }
            
        }
    }

    public void PrepareForGame()
    {
        _animator = GetComponentInChildren<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }


    public void AttackPlayer()
    {
        _agent.isStopped = true;
        // _agent.velocity = Vector3.zero;

        // Vector3 lookPos = _player.transform.position - transform.position;
        // lookPos.y = 0;
        // if(lookPos != Vector3.zero)
        // {
        //     transform.rotation = Quaternion.LookRotation(lookPos);
        // }

        canAtack = false;
        StartCoroutine(Attack(2.6f));

    }

   
    
    
    private IEnumerator Attack(float delay)
    {
        _animator.SetBool("IsAttack", true);
        PlayerHP playerhp = _player.GetComponent<PlayerHP>();
        
        yield return new WaitForSeconds(delay);

        float distance = Vector3.Distance(transform.position, _player.transform.position);
        if(distance <= attackRange * 1.5f)
        {
            playerhp.TakeDamage(damage);
        }
        _animator.SetBool("IsAttack", false);
        canAtack = true;
        

        
    }

}
