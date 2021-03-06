﻿using UnityEngine;
using System.Collections;

public class SteeringArrive : MonoBehaviour {

    public float min_distance = 0.1f;
    public float slow_distance = 5.0f;
    public float time_to_target = 0.1f;

    Move move;

    // Use this for initialization
    void Start()
    {
        move = GetComponent<Move>();
    }

    // Update is called once per frame
    void Update()
    {
        Steer(move.target.transform.position);
    }

    public void Steer(Vector3 target)
    {
        if (!move)
            move = GetComponent<Move>();

        // TODO 3: Create a vector to calculate our ideal velocity
        // then calculate the acceleration needed to match that velocity
        // before sending it to move.AccelerateMovement() clamp it to 
        // move.max_mov_acceleration
        Vector3 dis = move.target.transform.position - transform.position;
        Vector3 acceleration = dis * move.max_mov_acceleration;
        float disMag = dis.magnitude;
        dis.Normalize();
        if (disMag <= slow_distance)
        {
            dis = time_to_target * dis * disMag;
            //if (disMag < min_distance)
            //{
            //    move.AccelerateMovement(Vector3.zero);
            //}
            acceleration = (dis - move.movement) * move.max_mov_acceleration / disMag;
        }
        if (disMag < min_distance)
        {
            move.AccelerateMovement(Vector3.zero);
        }
        else
        {
            move.AccelerateMovement(acceleration);
        }
    }

	void OnDrawGizmosSelected() 
	{
		// Display the explosion radius when selected
		Gizmos.color = Color.white;
		Gizmos.DrawWireSphere(transform.position, min_distance);

		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, slow_distance);
	}
}
