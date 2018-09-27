using UnityEngine;
using System.Collections;

public class KinematicWander : MonoBehaviour {

	public float max_angle = 0.5f;
    private float timer = 0.0f;
    private Vector3 vel = Vector3.zero;

	Move move;

	// Use this for initialization
	void Start () {
		move = GetComponent<Move>();
	}

	// number [-1,1] values around 0 more likely
	float RandomBinominal()
	{
		return Random.value * Random.value;
	}
	
	// Update is called once per frame
	void Update () 
	{
        // TODO 9: Generate a velocity vector in a random rotation (use RandomBinominal) and some attenuation factor
        if (timer <= 0.0f)
        {
            setNewDirection();
            timer = 5.0f;
        }
        move.SetMovementVelocity(vel);
        float angle = Mathf.Atan2(move.mov_velocity.x, move.mov_velocity.z);
        angle *= 57.2958f;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
        timer -= Time.deltaTime;
    }

    void setNewDirection()
    {
        float angle = 0.8f * RandomBinominal();
        angle *= 57.2958f;
        vel = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
        vel.Normalize();
    }
}
