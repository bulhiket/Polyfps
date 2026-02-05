using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiEnemy : MonoBehaviour
{
    private NavMeshAgent _agent;
    public GameObject _player;
    public float attackRate = 0.5f;
    private float _timer;

    public int damage = 30;
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
    //     float distance = Vector3.Distance(transform.position, _player.transform.position);

    //     if(distance > 5)
    //     {
    //         _agent.isStopped = false;
    //         _agent.SetDestination(_player.transform.position);
    //     }

    //     else
    //     {
    //         _timer += Time.deltaTime;
    //         if(_timer >= attackRate)
    //         {
    //             Attack();
    //             _timer = 0;
    //         }
    //     }
        if(_agent.isActiveAndEnabled && _agent.isOnNavMesh) Run();
        
    }

    private void Attack()
    {
        _agent.isStopped = true;

        transform.LookAt(_player.transform.position);

        float distance = Vector3.Distance(transform.position, _player.transform.position);

        if(distance < 5.4)
        {
            _player.GetComponent<PlayerHP>().TakeDamage(damage);
        }
    }

    void Run()
    {
        _agent.isStopped = false;
        _agent.SetDestination(_player.transform.position);
    }
}
