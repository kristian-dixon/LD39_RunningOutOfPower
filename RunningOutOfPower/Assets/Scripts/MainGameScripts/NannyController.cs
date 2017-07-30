using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NannyController : MonoBehaviour
{
    NavMeshAgent mAgent;

    public Transform mGoal;

	// Use this for initialization
	void Start ()
    {
        mAgent = GetComponent<NavMeshAgent>();
        mAgent.destination = mGoal.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
	}
}
