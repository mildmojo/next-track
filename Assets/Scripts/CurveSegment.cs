using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CurveSegment : MonoBehaviour {

	public GameObject previousTrack;
	public GameObject nextTrack;

	public Vector3 startPoint;
	public Vector3 startSegment;
	public Vector3 endPoint;
	public Vector3 endSegment;

	public List<Vector3> curveSegment;

	//number of points in this curve
	private int numberOfPoints;
	public Vector3 segment;
	public float turnRate;
	public LineRenderer lineRenderer;

	private int minPoints = 60;
	private int maxPoints = 120;

	void Start () {
	}

	public void ActivateCurveSegment (Vector3 start, Vector3 segment, float angleStep)
	{
		FillCurveSegment (start, segment, angleStep);
		//DrawCurveSegment ();
	}

	// Update is called once per frame
	void Update () {

	}

	//Add points to the Curve Segment
	void FillCurveSegment(Vector3 start, Vector3 segment, float angleStep)
	{
		numberOfPoints = Random.Range (minPoints, maxPoints);
		Vector3 nextPoint;
		this.segment = segment;
		this.startSegment = segment;
		this.startPoint = start;
		this.turnRate = angleStep;
		nextPoint = startPoint;

		//Line renderer stuff
		lineRenderer = gameObject.GetComponent<LineRenderer> ();
		lineRenderer.SetVertexCount (numberOfPoints);

		for (int i=0; i<numberOfPoints; i++)
		{
			curveSegment.Add (nextPoint);
			lineRenderer.SetPosition (i, nextPoint);
			//Rotate Segment
			segment = Quaternion.AngleAxis (turnRate, Vector3.forward) * segment;
			nextPoint += segment;
		}

		//Find the end point and segment
		endPoint = nextPoint;
		endSegment = segment;
	}

//	void DrawCurveSegment()
//	{
//		lineRenderer = gameObject.GetComponent<LineRenderer> ();
//		lineRenderer.SetVertexCount (numberOfPoints);
//		for (int i=0; i <= numberOfPoints; i++) {
//			lineRenderer.SetPosition (i, curveSegment [i]);
//			//Debug.Log(curve[i]);
//		}
//	}
}
