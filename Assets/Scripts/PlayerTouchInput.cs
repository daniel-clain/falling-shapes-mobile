using System.Collections;
using UnityEngine;

public class PlayerTouchInput : MonoBehaviour {

	private Collector collector;

	void Start () {
	}
	void Update () {
		if(Input.touchCount > 0){
			Debug.Log("Input.touchCount: " + Input.touchCount);
			Touch touch = Input.GetTouch(0);
			if (touch.phase == TouchPhase.Began) {
				if(touch.position.x < Screen.width/2){
					Debug.Log("LEFT");
					collector.SetTargetLane(1);
				}
				
				if(touch.position.x > Screen.width/2){
					Debug.Log("RIGHT");
					collector.SetTargetLane(3);
				}
			}	
		}
	}


}