using UnityEngine;
using System.Collections;

public class TrackManager : MonoBehaviour {

	public GameObject trackA;
	public GameObject trackB;

	public CurveHandler trackAScript;
	public CurveHandler trackBScript;

	public bool trackBCreated = false;
	public bool trackACreated = false;

	public bool onTrackA = true;
	public bool trackSwitchFlag;

	public GameObject player;
	public MoveAlongCurve playerScript;

	// Use this for initialization
	void Start () {
		//Time.timeScale = 0.25f;
		trackAScript = trackA.GetComponent<CurveHandler>();
		trackBScript = trackB.GetComponent<CurveHandler>();
		playerScript = player.GetComponent<MoveAlongCurve>();
	}
	
	// Update is called once per frame
	void Update () {
			if (trackAScript.halfwayMark) {	
			RecreateTrack (trackB);
			trackAScript.halfwayMark = false;
		} 
		else if (trackBScript.halfwayMark) {
			RecreateTrack (trackA);
			trackBScript.halfwayMark = false;
		}

		if (onTrackA) {
			if (playerScript.segmentIndex == trackAScript.curve.Count - 2 && playerScript.track == trackA) {
				if (Input.GetKey (KeyCode.Space)) {
					playerScript.SwitchTracks (trackB, true);
					Debug.Log("switching to B");
					onTrackA = false;
				}
			}
		}

		if (!onTrackA) {
			if (playerScript.segmentIndex == trackBScript.curve.Count - 2 && playerScript.track == trackB) {
				if (Input.GetKey (KeyCode.Space)) {
					player.GetComponent<MoveAlongCurve> ().SwitchTracks (trackA, true);
					Debug.Log ("switching to A");
					onTrackA = true;
				}
			}
		}

	}

	void RecreateTrack(GameObject track)
	{
		if (track.name == "TrackA") {
			Debug.Log("recreating track A");
			Debug.Log("index" + playerScript.segmentIndex);
				//if (!trackACreated)
				//{
					for (int i =0 ; i<trackAScript.curve.Count; i++)
					{
						DestroyImmediate(trackAScript.curve[i]);
						Debug.Log("Destroying A's curves");
					}
				trackAScript.curve.Clear ();
				//trackA.SetActive(true);
				trackAScript.startPoint = trackBScript.nextTrackStart;
				trackAScript.endPoint = trackBScript.nextTrackStart;
				trackAScript.startSegment = trackBScript.nextTrackSegment;
				trackAScript.angleStep = trackBScript.nextTrackAngle;
				
				//startPoint = transform.position;
				//endPoint = startPoint;
				//endSegment = startSegment;
				//startSegment = new Vector3 (1, 1, 0);
				trackAScript.numberOfSegments = Random.Range (8, 12);
				//angleStep = 3.0f;
				trackAScript.FillCurve ();
				trackACreated = true;
			//}

		} 
		else if (track.name == "TrackB")
		{
			Debug.Log("recreating track B");
			Debug.Log("index" + playerScript.segmentIndex);
			//if (!trackBCreated)
			//{
				for (int i =0 ; i<trackBScript.curve.Count; i++)
				{
					DestroyImmediate(trackBScript.curve[i]);
				}
				trackBScript.curve.Clear ();
				trackB.SetActive(true);
				//trackB.transform.position = trackAScript.nextTrackStart;
				//trackB.transform.rotation = Quaternion.identity;
				trackBScript.startPoint = trackAScript.nextTrackStart;
				trackBScript.endPoint = trackAScript.nextTrackStart;
				trackBScript.startSegment = trackAScript.nextTrackSegment;
				trackBScript.angleStep = trackAScript.nextTrackAngle;
				
				//startPoint = transform.position;
				//endPoint = startPoint;
				//endSegment = startSegment;
				//startSegment = new Vector3 (1, 1, 0);
				trackBScript.numberOfSegments = Random.Range (8, 12);
				//angleStep = 3.0f;
				trackBScript.FillCurve ();
				trackBCreated = true;
			//}
		}
	}
}
