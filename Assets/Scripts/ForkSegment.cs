using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ForkSegment : MonoBehaviour {

	public GameObject previousTrack;
	public GameObject nextTrackA;
	public GameObject nextTrackB;

	public Vector3 startPoint;
	public Vector3 startSegment;
	public Vector3 endPoint;
	public Vector3 endSegment;

	public List<Vector3> curveSegment;

	//number of points in this curve
	public int numberOfSegments;
	public Vector3 segment;
	public float turnRate = 8.0f;
	public LineRenderer lineRenderer;
	// Use this for initialization
	void Start () {

	}

	public void ActivateCurveSegment (Vector3 start, Vector3 segment, float angleStep)
	{
		FillCurveSegment (start, segment, angleStep);
		DrawCurveSegment ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//Add points to the Curve Segment
	void FillCurveSegment(Vector3 start, Vector3 segment, float angleStep)
	{
		numberOfSegments = Random.Range (10, 75);
		Vector3 next;
		this.segment = segment;
		this.startSegment = segment;
		this.startPoint = start;
		this.turnRate = angleStep;
		//curveSegment.Add (startPoint);
		next = startPoint;
		//curveSegment.Add (startPoint);
		for (int i=0; i<numberOfSegments; i++) 
		{
			curveSegment.Add (next);
			//Rotate Segment
			segment = Quaternion.AngleAxis (turnRate, Vector3.forward) * segment;
			next += segment;
			//curveSegment.Add (next);
		}
		//curveSegment.Add (next);

		//Find the end point and segment
		endPoint = next;
		endSegment = segment;
	}

	void DrawCurveSegment()
	{
		lineRenderer = gameObject.GetComponent<LineRenderer> ();
		lineRenderer.SetVertexCount (numberOfSegments);
		for (int i=0; i < numberOfSegments; i++) {
			lineRenderer.SetPosition (i, curveSegment [i]);
			//Debug.Log(curve[i]);
		}
	}
}
