using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapesPool {
	
    private int shapePoolSize = 10;
    private GameObject[] shapes;

    private int shapeIndex = 0;
	private Vector2 objectPoolPosition = new Vector2(-1f, 7f);    
    

	// Use this for initialization
	public ShapesPool(GameObject shapesPrefab) {

        
        shapes = new GameObject[shapePoolSize];
        for(int i = 0; i < shapePoolSize; i++){
            shapes[i] = (GameObject)GameObject.Instantiate(shapesPrefab, objectPoolPosition, Quaternion.identity);
        }
	}
    public GameObject GetNextShape(){ 
        GameObject returnShape = shapes[shapeIndex];
        if(shapeIndex != shapePoolSize - 1){
            shapeIndex++;
        } else {
            shapeIndex = 0;
        }

        return returnShape;
    }

    public List<GameObject> GetFallingShapes(){
        List<GameObject> fallingShapes = new List<GameObject>();
        for(int i = 0; i < shapes.Length; i++){
            string shapeFallingState = shapes[i].GetComponent<Shape>().GetFallingState();
            if(shapeFallingState == "falling"){
                fallingShapes.Add(shapes[i]);
            }
        } 

        return fallingShapes;
    }
}
