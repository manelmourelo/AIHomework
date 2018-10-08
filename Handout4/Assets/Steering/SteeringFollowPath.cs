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
    public float distanceToSearch = 2.0f;

    public float liebre = 0.0f;

    // Use this for initialization
    void Start () {
		move = GetComponent<Move>();
		seek = GetComponent<SteeringSeek>();

        // TODO 1: Calculate the closest point in the range [0,1] from this gameobject to the path
        math = path.GetComponent<BGCcMath>();

        //Vector3 closestPoint = math.Curve.Points[0].PositionWorld;
        float distance = 0.0f;
        point = math.CalcPositionByClosestPoint(move.transform.position, out distance);

        liebre = distance / math.GetDistance();

        point = math.CalcByDistanceRatio(BGCurveBaseMath.Field.Position, liebre);
    }
	
	// Update is called once per frame
	void Update () 
	{
        // TODO 2: Check if the tank is close enough to the desired point
        // If so, create a new point further ahead in the path

        float distance = Vector3.Distance(move.transform.position, point);

        if (distance <= distanceToSearch)
        {
            liebre += 0.05f;
            if (liebre > 1.0f)
            {
                liebre = 0.0f;
            }
            point = math.CalcByDistanceRatio(BGCurveBaseMath.Field.Position, liebre);
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
            Gizmos.DrawSphere(point, 0.2f);
		}

	}
}
