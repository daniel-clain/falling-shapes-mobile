using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour {

	public Collector collector;

	void Start(){
		collector = GetComponent<Collector>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("left")){
			collector.SetTargetLane(1);
		}
		if(Input.GetKeyDown("right")){
			collector.SetTargetLane(3);
		}
		if(Input.GetKeyUp("left") || Input.GetKeyUp("right")){
			collector.SetTargetLane(2);
		}
	}
}
