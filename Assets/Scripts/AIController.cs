using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AIController : MonoBehaviour
{
    NavMeshAgent agent;
    PlayerController character;
    Transform targetTrans;

    void Start ()
    {
        character = GetComponent<PlayerController>();
        agent = GetComponent<NavMeshAgent>();

        targetTrans = GameObject.FindGameObjectWithTag("Player").transform; //玩家名
        
        InvokeRepeating("FireControl", 1, 3);
    }

    void FireControl()
    {
        character.Fire();
    }

	void Update ()
    {
        agent.destination = targetTrans.position;
        transform.LookAt(targetTrans);
    }
}
