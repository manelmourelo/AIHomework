using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GetPath : MonoBehaviour {

    Move move;
    SteeringSeek seek;

    public GameObject target;
    private NavMeshPath path;

	// Use this for initialization
	void Start () {
        move = GetComponent<Move>();
        seek = GetComponent<SteeringSeek>();

        path = new NavMeshPath();
	}
	
	// Update is called once per frame
	void Update () {
        NavMesh.CalculatePath(transform.position, target.transform.position, NavMesh.AllAreas, path);
        for (int i = 0; i < path.corners.Length - 1; i++)
            Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);

        if (path.corners.Length >= 1)
        {
            seek.Steer(path.corners[0]);
        }
        else
        {
            seek.Steer(transform.position);
        }
    }
}
