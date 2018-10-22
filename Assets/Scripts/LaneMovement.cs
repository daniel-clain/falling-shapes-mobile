using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneMovement {

	GameObject gameObj;
	float speed;
	float timeStartedMoving = 0f;

	Vector2 start;
	Vector2 end;

	public LaneMovement(GameObject g, float s){
		gameObj = g;
		speed = s;
	}

	public void StartMovingToTarget(int targetLane){
		
		float laneXVal = GameController.instance.laneXVals[targetLane - 1];		
		float yVal = gameObj.transform.position.y;
		timeStartedMoving = Time.time;
		start = gameObj.transform.position;
		end = new Vector2(laneXVal, yVal);

	}

	public void ContinueMovingToTarget(){

		float timeSinceStarted = Time.time - timeStartedMoving;
		float distance = end.x - start.x;
		if(distance < 0){
			distance = distance * (-1);
		}
		float duration = speed * distance;
		float percentageComplete = timeSinceStarted / duration;
		
		gameObj.transform.position = Vector3.Lerp(start, end, percentageComplete);
	}

	public bool AlreadyAtTargetLane(){
		return gameObj.transform.position.x == end.x;
	}
	
	public void MoveToTargetLane(int targetLane){
		float laneXVal = GameController.instance.laneXVals[targetLane - 1];		
		float yVal = gameObj.transform.position.y;
		Vector2 start = gameObj.transform.position;
		Vector2 end = new Vector2(laneXVal, yVal);
		float distance = end.x - start.x;
		if(distance < 0){
			distance = distance * (-1);
		}
		Debug.Log("Distance: " + distance);
		float timeToMove = speed * distance;
		float timeSinceStarted = Time.time - timeStartedMoving;
		Debug.Log("timeSinceStarted: " + timeSinceStarted);
		float percentageComplete = timeSinceStarted / 20f;
		Debug.Log("percentageComplete: " + percentageComplete);
		gameObj.transform.position = Vector3.Lerp(start, end, percentageComplete);
	}

    
}