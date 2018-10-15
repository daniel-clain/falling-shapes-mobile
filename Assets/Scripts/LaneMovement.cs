using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneMovement : MonoBehaviour {
    
	private static void MoveToTargetLane (float speed, int targetLane, GameObject gameObject) {

		float xVal = GameController.instance.laneXVals[targetLane - 1];
		if (gameObject.transform.position.x > xVal + 0.05) {
			gameObject.transform.Translate(Vector2.left * speed * Time.deltaTime);
		} else if (gameObject.transform.position.x < xVal - 0.05) {
			gameObject.transform.Translate(Vector2.right * speed * Time.deltaTime);
		}
	}

	public static bool IsAtTargetLane(int targetLane, GameObject gameObject) {
		float xVal = GameController.instance.laneXVals[targetLane - 1];
		if (gameObject.transform.position.x < xVal + 0.05 &&
			gameObject.transform.position.x > xVal - 0.05) {
			return true;
		}
		return false;
	}

    public static void HandleUpdates(float speed, int targetLane, GameObject gameObject){
        if(gameObject.name == "Dispenser"){
            Dispenser dispenser = gameObject.GetComponent<Dispenser>();
            //Debug.Log("dispenser: " + dispenser.GetTargetLane() + " || passed lane: " + targetLane);

        }
        if(IsAtTargetLane(targetLane, gameObject) == false){
            MoveToTargetLane(speed, targetLane, gameObject);
        }
    }

    
}