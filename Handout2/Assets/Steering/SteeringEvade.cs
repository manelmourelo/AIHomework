using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringEvade : MonoBehaviour {

    public float max_prediction;

    Move move;
    SteeringFlee flee;

    // Use this for initialization
    void Start()
    {
        move = GetComponent<Move>();
        flee = GetComponent<SteeringFlee>();
    }

    // Update is called once per frame
    void Update()
    {
        Steer(move.target.transform.position, move.target.GetComponent<Move>().movement);
    }

    public void Steer(Vector3 target, Vector3 velocity)
    {
        // TODO 6: Create a fake position to represent
        // enemies predicted movement. Then call Steer()
        // on our Steering Arrive
        Vector3 direction = target - transform.position;
        float distance = direction.magnitude;

        float speed = velocity.magnitude;

        float prediction = 0.0f;
        if (speed <= (distance / max_prediction))
        {
            prediction = max_prediction;
        }
        else
        {
            prediction = distance / speed;
        }

        target += move.movement * prediction;

        flee.Steer(target);
    }
}
