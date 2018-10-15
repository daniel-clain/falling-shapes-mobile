﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Dispenser : MonoBehaviour {
	private int targetLane = 2;
	private float dispenseRate = 2f;

	private float dispenseDelayTimer = 1f;
	private float dispenseDelayDuration = 1f;
	private float timeSinceLastDispensed;

	private float yPos;

	private float speed = 2f;

	private bool nextShapeReady;

    private Animator animator;

	// Use this for initialization
	void Start () {
		yPos = transform.position.y;
        animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () {

		if(GameController.instance.state == "active"){
			dispenseDelayTimer += Time.deltaTime;
			if(dispenseDelayTimer >= dispenseDelayDuration){
				LaneMovement.HandleUpdates(speed, targetLane, gameObject);
			}

			timeSinceLastDispensed += Time.deltaTime;
			if(timeSinceLastDispensed >= dispenseRate){
				DispenseShape();
				timeSinceLastDispensed = 0;
				targetLane = GetRandomLane();
			}
		}
	}

	public int GetTargetLane(){
		return targetLane;
	}

	void DispenseShape () {
		animator.SetTrigger("Dispense Shape");
		dispenseDelayTimer = 0;
		GameObject nextShape = GameController.instance.GetShapesPool().GetNextShape ();
		Shape shapeScript = nextShape.GetComponent<Shape> ();
		shapeScript.SetColor (GetRandomColor ());
		shapeScript.SetShape (GetRandomShape ());
		shapeScript.SetLane (targetLane);
		shapeScript.StartFalling ();

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