using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shape : MonoBehaviour{

    private SpriteRenderer sprite;
    private Rigidbody2D rb2d;

    private Animator animator;
    private string fallingState = "idle";
    private float topOfLaneArea;
    private Vector2 hiddenPosition = new Vector2(-5f, 7f);

	private float speed = 2.0f;
    public int lane;    
    public string color;
    public string shape;

    public GUIText shapeHistoryLog;

    void Start(){

        sprite = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();        
        shapeHistoryLog = GetComponent<GUIText>();
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
            LaneMovement.HandleUpdates(speed, lane, gameObject);
            rb2d.velocity = new Vector2(0, -0.7f);
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
        transform.position = new Vector2(xVals[lane-1], topOfLaneArea);
        SetState("falling");
    }

    public void SetShape(string newShape){
        string animationTriggerName = "changeTo" + char.ToUpper(newShape[0]) + newShape.Substring(1);
        animator.SetTrigger(animationTriggerName);
        shape = newShape;
        shapeHistoryLog.text += (", shape: " + shape);
    }

    public void SetColor(string newColor){
        color = newColor;
        sprite.color = GameController.instance.colorsList[newColor]; 
        shapeHistoryLog.text += (", color: " + color);
    }

    public void SetLane(int newLane){
        lane = newLane;
        shapeHistoryLog.text += (", lane: " + lane);
    }


};

