using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour {
	public float speed = 2.0f;
	private int targetLane = 2;
	
	void Update () {
		if(GameController.instance.state == "active"){
			LaneMovement.HandleUpdates(speed, targetLane, gameObject);			
		}		
	}
	void OnTriggerEnter2D(Collider2D col){
		Shape shapeScript = col.gameObject.GetComponent<Shape>();
		shapeScript.SetState("idle");
		GameController.instance.HandleShapeEffect(shapeScript.color, shapeScript.shape);
    }

	public void SetTargetLane(int lane){
		targetLane = lane;
	}

}
