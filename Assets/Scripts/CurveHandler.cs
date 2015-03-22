using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CurveHandler : MonoBehaviour {

	public struct Segment {
		public float speedLimit;
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
	public float minSpeed = 0.5f;
	public float segmentReversalChance;
	public int checkpointFrequency = 5;
	public float checkpointTimeAmount = 10f;
	public float checkpointDiminishFactor = 0.9f;

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
				// Add a checkpoint after every `checkpointFrequency` segments.
				if (i % checkpointFrequency == 0) {
					var checkpoint = arc.transform.FindChild("Checkpoint").gameObject;
					int checkpointNum = i / 5;
					var checkpointAddTime = checkpointTimeAmount * Mathf.Pow(checkpointDiminishFactor, checkpointNum);
					checkpoint.GetComponent<ValueContainer>().Set("add_time", checkpointAddTime.ToString());
					// Don't activate the first checkpoint; it's the starting station.
					if (i == 0) {
						checkpoint.GetComponent<BoxCollider>().enabled = false;
					}
					checkpoint.SetActive(true);
				}
				var segment = new Segment();
				segment.speedLimit = minSpeed + (1f - minSpeed) * (1f - ((Mathf.Abs(angleStep) - minAngle) / (maxAngle - minAngle)));
// Debug.Log("speedlimit: " + segment.speedLimit + ", minspeed: " + minSpeed + ", angle step: " + angleStep + ", angle range: " + (maxAngle - minAngle) + ", angle percentage: " + Mathf.Abs(angleStep / (maxAngle - minAngle)));
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
