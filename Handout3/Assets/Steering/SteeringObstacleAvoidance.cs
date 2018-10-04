using UnityEngine;
using System.Collections;

[System.Serializable]
class Test : System.Object
{
    public RaycastHit[] hits;
}

public class SteeringObstacleAvoidance : MonoBehaviour {

	public LayerMask mask;
	public float avoid_distance = 5.0f;
    private RaycastHit hit1;

    Move move;
	SteeringSeek seek;

	// Use this for initialization
	void Start () {
		move = GetComponent<Move>(); 
		seek = GetComponent<SteeringSeek>();
	}
	
	// Update is called once per frame
	void Update () 
	{
        // TODO 2: Agents must avoid any collider in their way
        // 1- Create your own (serializable) class for rays and make a public array with it
        // 2- Calculate a quaternion with rotation based on movement vector
        // 3- Cast all rays. If one hit, get away from that surface using the hitpoint and normal info
        // 4- Make sure there is debug draw for all rays (below in OnDrawGizmosSelected)

        Quaternion rotation = Quaternion.LookRotation(move.movement);

        if(Physics.Raycast(transform.position, move.movement, out hit1, mask))
        {
            if (hit1.distance <= avoid_distance)
            {
                Vector3 acceleration = hit1.point - hit1.normal;
                acceleration.y = 0;
                move.AccelerateMovement(acceleration);
            }
        }


    }

	void OnDrawGizmosSelected() 
	{
		if(move && this.isActiveAndEnabled)
		{
			Gizmos.color = Color.red;
			float angle = Mathf.Atan2(move.movement.x, move.movement.z);
			Quaternion q = Quaternion.AngleAxis(Mathf.Rad2Deg * angle, Vector3.up);

            // TODO 2: Debug draw thoise rays (Look at Gizmos.DrawLine)
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit1.distance, Color.white);
        }
	}
}
