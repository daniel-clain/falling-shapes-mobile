using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour {
	private float speed = 0.3f;
	[SerializeField] AudioSource moveSound;

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
			moveSound.Play();
			laneMovement.StartMovingToTarget(targetLane);
		}

		if(!laneMovement.AlreadyAtTargetLane()){
			laneMovement.ContinueMovingToTarget();
		}
	}


	bool DirectionIsHeldDown(string direction){
		if(direction == null){
			return (!KeyDown("left") && !KeyDown("right")) && (!ScreenTouch("left") && !ScreenTouch("right"));
		} else {
			return KeyDown(direction) || ScreenTouch(direction);
		}
	}

	bool KeyDown(string direction){
		return Input.GetKey(direction);
	}
	bool ScreenTouch(string direction){
		if(Input.touchCount > 0){
			Touch touch = Input.GetTouch(0);
			bool leftTouch = touch.position.x < Screen.width / 2;
			bool rightTouch = touch.position.x > Screen.width / 2;

			if(direction == "left" && leftTouch){
				Debug.Log("touch left");
				return true;
			}
			if(direction == "right" && rightTouch){
				Debug.Log("touch right");
				return true;					
			}
		}
		return false;
		
	}

	
	void OnTriggerEnter2D(Collider2D col){
		//catchSound.Play();
		Shape shapeScript = col.gameObject.GetComponent<Shape>();
		shapeScript.SetState("idle");
		GameController.instance.HandleShapeEffect(shapeScript.color, shapeScript.shape);
    }

}
