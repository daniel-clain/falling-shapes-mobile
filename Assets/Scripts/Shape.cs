using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shape : MonoBehaviour{

    SpriteRenderer spriteRenderer;
    Rigidbody2D rb2d;

    Animator animator;
    string fallingState = "idle";
    float topOfLaneArea;
    Vector2 hiddenPosition = new Vector2(-5f, 7f);

	float speed = 0.4f;

    float fallSpeed = -1f;
    public int targetLane;    
    public string color;
    public string shape;

    public Text shapeHistoryLog;

    public Sprite star;
    public Sprite triangle;
    public Sprite square;
    public Sprite circle;

	private LaneMovement laneMovement;


    void Start(){

        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();        
        shapeHistoryLog = GetComponent<Text>();
        
		laneMovement = new LaneMovement(gameObject, speed);

        float laneYVal = GameController.instance.lanesArea.transform.position.y;
        float laneHeight = GameController.instance.lanesArea.GetComponent<SpriteRenderer>().size.y;
        topOfLaneArea = laneHeight/2 + laneYVal;
        
    }

    public string GetFallingState(){
        return fallingState;
    }

    void Update(){

        

        if(fallingState == "idle"){
            transform.position = hiddenPosition;

        }
        if(fallingState == "falling"){
			if(!laneMovement.AlreadyAtTargetLane()){
				laneMovement.ContinueMovingToTarget();
			}
            rb2d.velocity = new Vector2(0, fallSpeed);
        }

        if(GameController.instance.state != "active"){
            fallingState = "idle";
        }
    }

    public void SetState(string state){
        fallingState = state;
        shapeHistoryLog.text += (", state: " + state);
    }


    public void StartFalling(){
        float[] xVals = GameController.instance.laneXVals;
        transform.position = new Vector2(xVals[targetLane-1], topOfLaneArea);
        SetState("falling");
    }

    public void SetShape(string newShape){
        animator.Play("shape_" + newShape);
        shape = newShape;
        shapeHistoryLog.text += (", shape: " + shape);
    }

    public void SetColor(string newColor){
        color = newColor;
        spriteRenderer.color = GameController.instance.colorsList[newColor]; 
        shapeHistoryLog.text += (", color: " + color);
    }

    public void SetTargetLane(int lane){
		if(targetLane != lane){
			targetLane = lane;
			laneMovement.StartMovingToTarget(targetLane);
            shapeHistoryLog.text += (", lane: " + targetLane);
		}	
    }


};

