using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour {
	private float speed = 0.3f;
	private int targetLane = 2;
	private LaneMovement laneMovement;

	void Start(){
		laneMovement = new LaneMovement(gameObject, speed);
	}
	
	void Update () {
		if(GameController.instance.state == "active"){	
			HandleMovement();	
		}		
	}
	void HandleMovement(){

		int originalLane = targetLane;
		
		if(DirectionIsHeldDown("left"))
			targetLane = 1;
		if(DirectionIsHeldDown("right"))
			targetLane = 3;
		if(DirectionIsHeldDown(null))
			targetLane = 2;

		if(targetLane != originalLane){
			laneMovement.StartMovingToTarget(targetLane);
		}

		if(!laneMovement.AlreadyAtTargetLane()){
			laneMovement.ContinueMovingToTarget();
		}
	}


	bool DirectionIsHeldDown(string key){
		if(key == null){
			return !Input.GetKey("left") && !Input.GetKey("right");
		} else if(key == "left"){
			return Input.GetKey("left");
		} else if(key == "right"){
			return Input.GetKey("right");
		} else {
			Debug.LogError(key + " is not a valid key");
			return false;
		}
	}
	void OnTriggerEnter2D(Collider2D col){
		Shape shapeScript = col.gameObject.GetComponent<Shape>();
		shapeScript.SetState("idle");
		GameController.instance.HandleShapeEffect(shapeScript.color, shapeScript.shape);
    }

}
