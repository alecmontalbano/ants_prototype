using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Slug_Controller : MonoBehaviour
{
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;

    private string currentPath;

    private NavMeshAgent agent;

    private void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();

        currentPath = "Start";
    }

    private void Update()
    {
        if (!agent.hasPath)
        {
            if ( currentPath == null )
            {
                currentPath = "Start";
                agent.SetDestination(startPoint.position);
            }
            else if ( currentPath == "Start" )
            {
                currentPath = "End";
                agent.SetDestination(endPoint.position);
            }
            else if ( currentPath == "End" )
            {
                currentPath = "Start";
                agent.SetDestination(startPoint.position);
            } 
        }
    }
}
