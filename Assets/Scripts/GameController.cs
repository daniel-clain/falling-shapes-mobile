using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public static GameController instance;
	public int numberOfLanes = 3;
	public string state = "active";
	public GameObject shapesPrefab;

	private float timeRemaining = 60f;
	private int score = 0;
	private int lives = 3;

	public TextMeshProUGUI scoreText;
	public TextMeshProUGUI timeText;
	public TextMeshProUGUI livesText;
	public TextMeshProUGUI lossReasonText;
	public GameObject gameOverText;
	private string[] colorNames = new string[] { "green", "red", "yellow", "blue" };

	private string[] shapesList = new string[] { "circle", "triangle", "square", "star" };

	private ShapesPool shapesPool;

	public GameObject lanesArea;

	public float[] laneXVals;

	public Dictionary<string, Color> colorsList = new Dictionary<string, Color> ();


	void Awake () {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}


		shapesPool = new ShapesPool(shapesPrefab);

		SpriteRenderer lanesSprite = lanesArea.GetComponent<SpriteRenderer> ();
		float laneWidth = lanesSprite.size.x;
		laneXVals = new float[numberOfLanes];
		for (int i = 0; i < numberOfLanes; i++) {
			int fraction = i + 1;
			laneXVals[i] = (float) ((laneWidth * fraction / (numberOfLanes + 1)) - laneWidth / 2);
		}
	}
	void Start () {

		colorsList.Add ("green", new Color (0.250f, 0.802f, 0.208f, 1.000f));
		colorsList.Add ("red", new Color (0.962f, 0.132f, 0.132f, 1.000f));
		colorsList.Add ("yellow", new Color (0.981f, 0.910f, 0.319f, 1.000f));
		colorsList.Add ("blue", new Color (0.222f, 0.568f, 0.925f, 1.000f));

		livesText.text = "Lives: " + lives;
		scoreText.text = "Score: " + score;
		timeText.text = "Time: " + timeRemaining;

		
	}

	void Update () {

		if(GameController.instance.state == "active"){    

			timeRemaining -= Time.deltaTime;
			timeText.text = "Time: " + (int) Math.Round (timeRemaining);

			CheckForGameOver ();
        }

		if(GameController.instance.state == "gameover"){  
			if(Input.GetMouseButtonDown(0)){
				SceneManager.LoadScene("InGame");
			}
		}
	}

	public ShapesPool GetShapesPool(){
		return shapesPool;
	}

	public string[] GetColorNames(){
		return colorNames;
	}
	public string[] GetShapesList(){
		return shapesList;
	}

	public void HandleShapeEffect (string color, string shape) {

		if (shape == "square") {
			Debug.Log("square effect");
			SquareColorEffect (color);
		} else {
			NonSquareColorEffect (color);
		}

		if (shape == "star") {
			Debug.Log("star effect");
			StarEffect ();

		}
		if (shape == "triangle") {
			Debug.Log("triangle effect");
			TriangleEffect();
		}
		if (shape == "circle") {
			Debug.Log("circle effect");
			CircleEffect();
		}
	}

	void NonSquareColorEffect (string color) {
		if (color == "blue") {
			Debug.Log("blue effect");
			timeRemaining += 10f;
			timeText.text = "Time: " + (int) Math.Round (timeRemaining);
			lives -= 1;
			livesText.text = "Lives: " + lives;
		}

		if (color == "yellow") {
			Debug.Log("yellow effect");
			score += 10;
			scoreText.text = "Score: " + score;
		}

		if (color == "red") {
			Debug.Log("red effect");
			score -= 10;
			scoreText.text = "Score: " + score;
		}

		if (color == "green") {	
			Debug.Log("green effect");		
			if(lives < 3){
				lives += 1;
				livesText.text = "Lives: " + lives;
			}
		}
	}

	void SquareColorEffect (string color) {
		if (color == "blue") {
			Debug.Log("blue square effect");
			timeRemaining -= 10f;
			timeText.text = "Time: " + (int) Math.Round (timeRemaining);
			if(lives < 3){
				lives += 1;
				livesText.text = "Lives: " + lives;
			}
		}

		if (color == "yellow") {
			Debug.Log("yellow square effect");
			score -= 10;
			scoreText.text = "Score: " + score;
		}

		if (color == "red") {
			Debug.Log("red square effect");
			score += 10;
			scoreText.text = "Score: " + score;
		}

		if (color == "green") {
			Debug.Log("green square effect");
			lives -= 1;
			livesText.text = "Lives: " + lives;
		}
	}

	void StarEffect() {
		List<GameObject> fallingShapes = shapesPool.GetFallingShapes ();

		for (int i = 0; i < fallingShapes.Count; i++) {
			Shape shapeScript = fallingShapes[i].GetComponent<Shape> ();
			int indexOfCurrentShape = Array.IndexOf (shapesList, shapeScript.shape);
			string nextShape;
			if (indexOfCurrentShape == shapesList.Length - 1) {
				nextShape = shapesList[0];
			} else {
				nextShape = shapesList[indexOfCurrentShape + 1];
			}

			shapeScript.SetShape (nextShape);
		}
	}

	void CircleEffect() {
		List<GameObject> fallingShapes = shapesPool.GetFallingShapes ();
		for (int i = 0; i < fallingShapes.Count; i++) {
			Shape shapeScript = fallingShapes[i].GetComponent<Shape> ();
			int indexOfCurrentColor = Array.IndexOf (colorNames, shapeScript.color);
			string nextColor;
			if (indexOfCurrentColor == colorNames.Length - 1) {
				nextColor = colorNames[0];
			} else {
				nextColor = colorNames[indexOfCurrentColor + 1];
			}

			shapeScript.SetColor (nextColor);
		}
	}

	
	void TriangleEffect() {
		List<GameObject> fallingShapes = shapesPool.GetFallingShapes ();
		for (int i = 0; i < fallingShapes.Count; i++) {
			Shape shapeScript = fallingShapes[i].GetComponent<Shape> ();
			int currentLane = shapeScript.targetLane;
			int nextLane;
			if (currentLane == 1 || currentLane == 3) {
				nextLane = 2;
			} else {
				int randomOfTwo = UnityEngine.Random.Range (0, 2);
				if(randomOfTwo == 0){
					nextLane = 1;
				} else {
					nextLane = 3;
				}
			}
			shapeScript.SetTargetLane(nextLane);		}
	}

	void CheckForGameOver () {
		string reason = lives <= 0 ? "out of lives" : "out of time";
		if (lives <= 0 || timeRemaining <= 0) {
			state = "gameover";
			lossReasonText.text = reason;
			gameOverText.SetActive(true);
		}
	}
}