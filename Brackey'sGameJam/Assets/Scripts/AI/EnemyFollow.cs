using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    public NavMeshAgent enemyAI;

    GameObject player;

    public float distanceToRunToPlayer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if(distance < distanceToRunToPlayer) // if the player is close enough to the distance to player ... we can get rid of this if we want too
        {
            Vector3 dirToPlayer = transform.position - player.transform.position;

            Vector3 newPosition = transform.position - dirToPlayer;

            enemyAI.SetDestination(newPosition);
        }
    }
}
