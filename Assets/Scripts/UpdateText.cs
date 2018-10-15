using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateText : MonoBehaviour {

	private Text debugTextArea;
	private int timeVal;
	public float speed = 0.1F;

	// Use this for initialization
	void Awake () {
		debugTextArea = GetComponent <Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		timeVal = (int)Time.fixedTime;
		//debugTextArea.text = "Time since start: " + timeVal;
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            // Get movement of the finger since last frame
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
			debugTextArea.text = "Ding: " + touchDeltaPosition;
			Debug.Log("Touch Position" + touchDeltaPosition);
            // Move object across XY plane
            transform.Translate(-touchDeltaPosition.x * speed, -touchDeltaPosition.y * speed, 0);
        }
		//updateTextAreaWithValues();
	}

	void updateTextAreaWithValues() {
		debugTextArea.text = "Time since start: " + timeVal;
		debugTextArea.text += "\ntest";
	}
}
