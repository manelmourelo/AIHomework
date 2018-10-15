using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Move : MonoBehaviour {

	public GameObject target;
	public GameObject aim;
	public Slider arrow;
	public float max_mov_velocity = 5.0f;
	public float max_mov_acceleration = 0.1f;
	public float max_rot_velocity = 10.0f; // in degrees / second
	public float max_rot_acceleration = 0.1f; // in degrees

    private Vector3[] priority = new Vector3[6];
    private float[] angularPriority = new float[6];

    [Header("-------- Read Only --------")]
	public Vector3 movement = Vector3.zero;
	public float rotation = 0.0f; // degrees

	// Methods for behaviours to set / add velocities
	public void SetMovementVelocity (Vector3 velocity) 
	{
        movement = velocity;
	}

	public void AccelerateMovement (Vector3 velocity, int prior) 
	{
		//movement += velocity;
        if (priority[prior].Equals(Vector3.zero))
        {
            priority[prior] = velocity;
        }
        else
        {
            Vector3 prev_vel = priority[prior];
            Vector3 new_vel = prev_vel + velocity;
            float velMagnitude = new_vel.magnitude;
            Mathf.Clamp(velMagnitude, 0, max_mov_velocity);
            new_vel.Normalize();
            new_vel *= velMagnitude;
            priority[prior] += new_vel;
        }
    }

	public void SetRotationVelocity (float rotation_velocity) 
	{
		rotation = rotation_velocity;
	}

	public void AccelerateRotation (float rotation_acceleration, int prior) 
	{
        //rotation += rotation_acceleration;
        if (angularPriority[prior].Equals(Vector3.zero))
        {
            angularPriority[prior] = rotation_acceleration;
        }
        else
        {
            float new_rot = angularPriority[prior] + rotation_acceleration;
            Mathf.Clamp(new_rot, 0, max_rot_acceleration);
            angularPriority[prior] = new_rot;
        }
    }

	
	// Update is called once per frame
	void Update () 
	{

        for (int i= 1; i < priority.Length; i++)
        {
            if (priority[i].Equals(Vector3.zero) == false)
            {
                movement = priority[i];
                //i = priority.Length + 1;
            }
        }

        for (int i = 1; i < angularPriority.Length; i++)
        {
            if (angularPriority[i].Equals(Vector3.zero) == false)
            {
                rotation = angularPriority[i];
                //i = angularPriority.Length + 1;
            }
        }

        // cap velocity
        if (movement.magnitude > max_mov_velocity)
		{
			movement.Normalize();
			movement *= max_mov_velocity;
		}

		// cap rotation
		Mathf.Clamp(rotation, -max_rot_velocity, max_rot_velocity);

		// rotate the arrow
		float angle = Mathf.Atan2(movement.x, movement.z);
		aim.transform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * angle, Vector3.up);

		// strech it
		arrow.value = movement.magnitude * 4;

		// final rotate
		transform.rotation *= Quaternion.AngleAxis(rotation * Time.deltaTime, Vector3.up);

		// finally move
		transform.position += movement * Time.deltaTime;

        for (int i = priority.Length-1; i >= 0; i--)
        {
            priority[i] = Vector3.zero;
        }

        for (int i = angularPriority.Length-1; i >= 0; i--)
        {
            angularPriority[i] = 0.0f;
        }

    }
    

}
