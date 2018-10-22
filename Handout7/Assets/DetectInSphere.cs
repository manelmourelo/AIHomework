using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectInSphere : MonoBehaviour {

    public LayerMask mask;
    public LayerMask obstacles;
    public float distance = 10.0f;
    public Camera camera;
    Ray ray;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

            Collider[] detected = Physics.OverlapSphere(transform.position, distance, mask);

            foreach (Collider col in detected)
            {
                Plane[] frustrum = GeometryUtility.CalculateFrustumPlanes(camera);
                bool inFrustrum = GeometryUtility.TestPlanesAABB(frustrum, col.bounds);

                if (inFrustrum == true)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(new Vector3(transform.position.x, 1.0f, transform.position.z), col.transform.position, out hit, distance, obstacles) == true)
                    {
                        Debug.Log("PlayerDetected");
                    }
                Debug.DrawLine(transform.position, col.transform.position, Color.red,distance);
                }
            }

    }

    void OnDrawGizmosSelected()
    {

        if (isActiveAndEnabled)
        {
            // Display the explosion radius when selected
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, distance);
        }

    }

}
