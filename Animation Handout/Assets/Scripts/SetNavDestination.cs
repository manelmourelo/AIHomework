using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SetNavDestination : MonoBehaviour {

    public NavMeshAgent character;
    public Animator animations;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        character.destination = transform.position;


        if (Vector3.Distance(character.transform.position, transform.position) > 0.0f)
        {
            animations.SetBool("Movement", true);
        }
        //else
        //{
        //    animations.SetBool("Movement", false);
        //}

        Vector3 movement = character.transform.position - transform.position;
        movement.Normalize();

        animations.SetFloat("vel x", movement.x);

        animations.SetFloat("vel y", movement.z);

    }
}
