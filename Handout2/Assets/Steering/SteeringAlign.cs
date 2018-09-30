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
        float rotTar = Mathf.Atan2(move.movement.x, move.movement.z);
        float rot = Mathf.Atan2(transform.forward.x, transform.forward.z);
        rotTar *= Mathf.Rad2Deg;
        rot *= Mathf.Rad2Deg;
        float rotDif = Mathf.DeltaAngle(rotTar, rot);

        float rotSize = Mathf.Abs(rotDif);

        if (rotSize > slow_angle)
        {
            rotTar = 1.0f;
        }
        else
        {
            rotTar = 1.0f * rotSize / slow_angle;
        }

        rotTar *= rotDif / rotSize;
        Vector3 charRot = transform.rotation.ToEulerAngles();
        float characterRot = Mathf.Atan2(charRot.x, charRot.z);
        float acceleration = rotTar - characterRot;
        acceleration /= time_to_target;

        float absAcceleration = Mathf.Abs(acceleration);
        if (absAcceleration > move.max_rot_acceleration)
        {
            acceleration /= absAcceleration;
            acceleration *= move.max_rot_acceleration;
        }

        move.AccelerateRotation(acceleration);

    }
}
