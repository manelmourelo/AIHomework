using UnityEngine;
using System.Collections;

public class SteeringWander : MonoBehaviour {

	public float min_distance = 0.1f;
	public float time_to_target = 0.25f;

    public float offset = 5.0f;
    public float radius = 40.0f;
    public float wanderRate = 5.0f;
    public float timer = 0.0f;

	Move move;
    SteeringSeek seek;

	// Use this for initialization
	void Start () {
		move = GetComponent<Move>();
        seek = GetComponent<SteeringSeek>();
        timer = wanderRate;

    }

	// Update is called once per frame
	void Update () 
	{
        if (!move)
        {
            move = GetComponent<Move>();
        }
        Vector3 target = transform.position + offset * transform.forward;
        if (timer <= 0.0f)
        {
            Vector3 wanderOrientation = new Vector3(RandomBinominal(), 0.0f, RandomBinominal());
            wanderOrientation.Normalize();
            Vector3 targetOrientation = wanderOrientation + transform.forward;


            target += radius * targetOrientation;

            timer = wanderRate;
        }
        timer -= Time.deltaTime;
        seek.Steer(target);
        

    }

	void OnDrawGizmosSelected() 
	{
		// Display the explosion radius when selected
		Gizmos.color = Color.white;
		Gizmos.DrawWireSphere(transform.position, min_distance);
        Vector3 target = transform.position + offset * transform.forward;
        Gizmos.DrawWireSphere(target, radius);
	}

    float RandomBinominal()
    {
        return Random.value - Random.value;
    }

}
