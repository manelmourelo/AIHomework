using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SetNavDestination : MonoBehaviour {

    public NavMeshAgent tank;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        tank.destination = transform.position;
        
	}
}
