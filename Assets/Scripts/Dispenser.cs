using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Dispenser : MonoBehaviour {
	private int targetLane = 2;
	private float dispenseRate = 2.1f;
	private float timeSinceLastDispensed = 0;
	private float yPos;

	private float speed;

	private bool nextShapeReady;
    private Animator animator;
	private LaneMovement laneMovement;

	// Use this for initialization
	void Start () {
		speed = dispenseRate * 0.2f;
		yPos = transform.position.y;
        animator = GetComponent<Animator>();
		laneMovement = new LaneMovement(gameObject, speed);
		StartCoroutine(DispenseShape());
	}

	// Update is called once per frame
	void Update () {

		if(GameController.instance.state == "active"){

			if(!laneMovement.AlreadyAtTargetLane()){
				laneMovement.ContinueMovingToTarget();
			}

			timeSinceLastDispensed += Time.deltaTime;
			Debug.Log("timeSinceLastDispensed" + timeSinceLastDispensed);
			if(timeSinceLastDispensed >= dispenseRate){
				StartCoroutine(DispenseShape());
			}
		}
	}

	IEnumerator DispenseShape(){

		timeSinceLastDispensed = 0;
		float timeToGetToLane = speed * 2.2f;
		float timeToVomit = 0.5f;
		

		int originalLane = targetLane;
		targetLane = GetRandomLane();
		if(targetLane != originalLane){
			laneMovement.StartMovingToTarget(targetLane);
		}

		yield return new WaitForSeconds(timeToGetToLane);

		animator.SetTrigger("Dispense Shape");
		Debug.Log("Animate Dispense Shape");

		yield return new WaitForSeconds(timeToVomit);	
	
		GameObject nextShape = GameController.instance.GetShapesPool().GetNextShape ();
		Shape shapeScript = nextShape.GetComponent<Shape> ();
		shapeScript.shapeHistoryLog.text += "\n shape dispensed";
		shapeScript.SetColor (GetRandomColor ());
		shapeScript.SetShape (GetRandomShape ());
		shapeScript.SetTargetLane (targetLane);
		shapeScript.StartFalling ();

		yield break;
	}


	public int GetTargetLane(){
		return targetLane;
	}


	private int GetRandomLane () {
		int numberOfLanes = GameController.instance.numberOfLanes;
		int randomLane = Random.Range (1, numberOfLanes + 1);
		return randomLane;
	}
	private string GetRandomShape () {
		string[] shapeList = GameController.instance.GetShapesList();
		int numberOfShapes = shapeList.Length;
		string randomShape = shapeList[Random.Range (0, numberOfShapes)];
		return randomShape;
	}
	private string GetRandomColor () {
		string[] colorNames = GameController.instance.GetColorNames();
		int numberOfColors = colorNames.Length;
		string randomColor = colorNames[Random.Range (0, numberOfColors)];
		
		return randomColor;
	}
}