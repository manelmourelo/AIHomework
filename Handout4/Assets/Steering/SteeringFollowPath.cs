using UnityEngine;
using System.Collections;
using BansheeGz.BGSpline.Components;
using BansheeGz.BGSpline.Curve;

public class SteeringFollowPath : MonoBehaviour {

	Move move;
	SteeringSeek seek;

    private BGCcMath math;
    Vector3 point = Vector3.zero;
    public GameObject path;
    private uint currentPoint = 0;
    public float distanceToSearch = 1.0f;

    // Use this for initialization
    void Start () {
		move = GetComponent<Move>();
		seek = GetComponent<SteeringSeek>();

        // TODO 1: Calculate the closest point in the range [0,1] from this gameobject to the path
        math = path.GetComponent<BGCcMath>();

        Vector3 closestPoint = math.Curve.Points[0].PositionWorld;

        for (uint i = 0; i<math.Curve.PointsCount; i++)
        {
            Vector3 distance1 = transform.position - closestPoint;
            Vector3 distance2 = transform.position - math.Curve.Points[i].PositionWorld;
            if (distance2.magnitude < distance1.magnitude)
            {
                closestPoint = math.Curve.Points[i].PositionWorld;
                currentPoint = i;
            }
        }

        point = closestPoint;

    }
	
	// Update is called once per frame
	void Update () 
	{
        // TODO 2: Check if the tank is close enough to the desired point
        // If so, create a new point further ahead in the path

        Vector3 distance = transform.position - point;
        if (distance.magnitude <= distanceToSearch)
        {
            currentPoint++;
            if (currentPoint > math.Curve.PointsCount)
            {
                currentPoint = 0;
            }
            point = math.Curve.Points[currentPoint].PositionWorld;
        }



        seek.Steer(point);

	}

	void OnDrawGizmosSelected() 
	{

		if(isActiveAndEnabled)
		{
			// Display the explosion radius when selected
			Gizmos.color = Color.green;
			// Useful if you draw a sphere were on the closest point to the path
		}

	}
}
