using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CurveHandler : MonoBehaviour {

	public List<GameObject> curve;
	public GameObject curveSegmentPrefab;
	//public GameObject forkSegmentPrefab;

	public Vector3 startPoint;
	public Vector3 startSegment;

	public Vector3 endPoint;
	public Vector3 endSegment;

	public int numberOfSegments;
	public float angleStep;

	public Vector3 nextTrackStart;
	public Vector3 nextTrackSegment;
	public float nextTrackAngle;

	public bool disableHalfwayMark = false;
	public bool halfwayMark = false;

	public int minAngle;
	public int maxAngle;
	

	// Use this for initialization
	void Start () {

		startPoint = transform.position;
		endPoint = startPoint;
		endSegment = startSegment;
		startSegment = new Vector3 (1, 1, 0);
		minAngle = 2;
		maxAngle = 6;
		numberOfSegments = 300;
		angleStep = 3.0f;
		FillCurve ();

	}
	
	// Update is called once per frame
	void Update () {

	}

	public void FillCurve ()
	{
		GameObject arc;
		//GameObject hazard;
		CurveSegment arcScript;
		//ForkSegment forkScript;
		//CurveSegment hazardScript;

		for (int i=0; i< numberOfSegments; i++)
		{
			{
				arc = (GameObject)Instantiate(curveSegmentPrefab);
				arc.transform.position = endPoint;
				arcScript = arc.GetComponent<CurveSegment>();
				arcScript.ActivateCurveSegment(endPoint, endSegment, angleStep);
				curve.Add(arc);
				arc.transform.parent = this.transform;
				
				//Setup for next iteration
				if(!(i == numberOfSegments-1))
				{
					angleStep = Random.Range (2,6);
					if (i%2 == 0) 
						angleStep = -angleStep ;

				}


				endPoint = arcScript.endPoint;
				endSegment = arcScript.endSegment;
				//nextHazard--;

				if (i == numberOfSegments-2)
				{
					nextTrackAngle = angleStep;
					nextTrackStart = arcScript.endPoint;
					nextTrackSegment = Quaternion.AngleAxis(-angleStep,Vector3.forward)* arcScript.endSegment;
					angleStep = -angleStep;
//					Debug.Log (nextTrackAngle);
//					Debug.Log (nextTrackStart);
//					Debug.Log (nextTrackSegment);
				}
			}
		}
	}
}
