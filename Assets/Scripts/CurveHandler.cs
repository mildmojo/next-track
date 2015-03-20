using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CurveHandler : MonoBehaviour {

	public struct Segment {
		public float speed;
		public GameObject gameObject;
	}

	public List<Segment> curve;
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

	public int minAngle = 2;
	public int maxAngle = 6;
	public float minSpeed = 0.4f;
	public float segmentReversalChance;


	// Use this for initialization
	void Awake () {

		curve = new List<Segment>();
		startPoint = transform.position;
		endPoint = startPoint;
		endSegment = startSegment;
		startSegment = new Vector3 (1, 1, 0);
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
				var segment = new Segment();
				segment.speed = minSpeed + (1f - minSpeed) * (1f - Mathf.Abs(angleStep / (maxAngle - minAngle)));
				segment.gameObject = arc;
				curve.Add(segment);
				arc.transform.parent = this.transform;

				//Setup for next iteration
				if(i != numberOfSegments-1)
				{
					var probabilities = new List<float>();
					var range = maxAngle - minAngle;
					// Generate probability list for selecting max angle step.
					// e.g. [2,2,2,3,3,4], weighted toward wider angles (smaller steps)
					for (var max = 0; max <= range; max++) {
						for (var j = 0; j < range + 1 - max; j++) {
							probabilities.Add(minAngle + max);
						}
					}
					var randomMax = probabilities[Random.Range(0, probabilities.Count)];
					angleStep = Random.Range ((float)minAngle, (float)randomMax);
					if (i % 2 == 0 && Random.value < segmentReversalChance)
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
