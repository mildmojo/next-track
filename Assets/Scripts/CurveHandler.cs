using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CurveHandler : MonoBehaviour {

	public List<GameObject> curve;
	public GameObject curveSegmentPrefab;

	public Vector3 startPoint;
	public Vector3 startSegment;

	public Vector3 endPoint;
	public Vector3 endSegment;

	public int numberOfSegments;
	public float angleStep;
	

	// Use this for initialization
	void Start () {

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

	void FillCurve ()
	{
		GameObject arc;
		for (int i=0; i< numberOfSegments; i++)
		{

			arc = (GameObject)Instantiate(curveSegmentPrefab);
			arc.transform.position = endPoint;
			CurveSegment arcScript = arc.GetComponent<CurveSegment>();
			arcScript.ActivateCurveSegment(endPoint, endSegment, angleStep);
			curve.Add(arc);
			arc.transform.parent = this.transform;

			//Setup for next iteration
			angleStep = Random.Range (1,9);
			if (i%2 ==0) angleStep = -angleStep ;
			endPoint = arcScript.endPoint;
			endSegment = arcScript.endSegment;
		}
	}
}
