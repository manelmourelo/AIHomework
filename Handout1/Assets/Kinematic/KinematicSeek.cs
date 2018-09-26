﻿using UnityEngine;
using System.Collections;

public class KinematicSeek : MonoBehaviour {

	Move move;

	// Use this for initialization
	void Start () {
		move = GetComponent<Move>();
	}
	
	// Update is called once per frame
	void Update () 
	{
        // TODO 5: Set movement velocity to max speed in the direction of the target
        Vector3 direction = move.transform.position + move.target.transform.position;
        direction.Normalize();
        move.SetMovementVelocity(direction);
        // Remember to enable this component in the inspector
	}
}