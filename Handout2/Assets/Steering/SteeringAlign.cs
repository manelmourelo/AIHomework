using UnityEngine;
using System.Collections;

public class SteeringAlign : MonoBehaviour {

	public float min_angle = 0.01f;
	public float slow_angle = 0.1f;
	public float time_to_target = 0.1f;

	Move move;

	// Use this for initialization
	void Start () {
		move = GetComponent<Move>();
	}

	// Update is called once per frame
	void Update () 
	{
        // TODO 4: As with arrive, we first construct our ideal rotation
        // then accelerate to it. Use Mathf.DeltaAngle() to wrap around PI
        // Is the same as arrive but with angular velocities
        float rotArrow = Mathf.Atan2(move.arrow.transform.forward.x, move.arrow.transform.forward.z);
        Vector3 axis = new Vector3(0,1,0);
        move.target.transform.Rotate(axis, rotArrow);
        float rotTar = Mathf.Atan2(move.target.transform.forward.x, move.target.transform.forward.z);
        float rot = Mathf.Atan2(transform.forward.x, transform.forward.z);
        rotTar *= Mathf.Rad2Deg;
        rot *= Mathf.Rad2Deg;
        float rotDif = Mathf.DeltaAngle(rotTar, rot);
        //rotDif = Mathf.Clamp(rotDif, -move.max_rot_acceleration, move.max_rot_acceleration);

        float rotSize = Mathf.Abs(rotDif);

        if (rotSize > slow_angle)
        {
            rotTar = 180.0f;
        }
        else
        {
            rotTar = 180.0f * rotSize / slow_angle;
        }

        rotTar *= rotDif / rotSize;

        float acceleration = rotTar - rot;
        acceleration /= time_to_target;

        float absAcceleration = Mathf.Abs(acceleration);
        if (absAcceleration > move.max_rot_acceleration)
        {
            acceleration /= absAcceleration;
            acceleration *= move.max_rot_acceleration;
        }

        if (rotSize < min_angle)
        {
            move.SetRotationVelocity(0.0f);
        }
        else
        {
            move.AccelerateRotation(-acceleration);
        }

    }
}
