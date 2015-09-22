using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

	public Transform player;
	public Transform Axe;
	public Transform firewood;
	public Transform fireWoodR;
	public Transform fireWoodL;
	public Transform can;
	public int score;
	public string gameMode;
	public bool gameOver;
	public bool gameStart;
	public float normalDestroyInterval;
	public Text scoreTxt;
	public Text assistantMessage;
	public Button startBtn;
	public Button restartBtn;
	public Canvas UIcanvas;

	private float objectSetInterval;
	private float normalRepopInterval;
	private float cutWaitInterval;

	public static GameManager GetController() {
		return GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager>();
	}


	// Use this for initialization
	void Start () {
		System.GC.Collect ();
		Application.targetFrameRate = 60;
		gameMode = "easy";
		gameStart = false;
		gameOver = false;
		objectSetInterval = 0.0f;
		score = 0;
		scoreTxt.text = score.ToString();
		HideRestartBtn ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (gameStart) {

			scoreTxt.text = score.ToString ();

			if (gameMode == "easy") {
				normalRepopInterval = 1.0f;
				normalDestroyInterval = 0.7f;
			}

			if (!gameOver) {
				objectSetInterval += Time.deltaTime;

				if (objectSetInterval > normalRepopInterval) {

					setAssistantMessage();

					float objectRandom = Random.Range (0.0f, 10.0f);
					if (objectRandom < 6.5f) {
						LoadFireWood ();
					} else {
						LoadCan ();
					}
					objectSetInterval = 0.0f;
				}
			}
		}
	}

	public void GameOverFunc(){
		assistantMessage.text = "a-a...";
		gameOver = true;
		ShowRestartBtn ();
	}

	public void GameStartFunc(){
		StartCoroutine("StartEffect");
	}

	public void GameRestartFunc(){
		HideRestartBtn ();
		score = 0;
		gameOver = false;
		assistantMessage.text = "come on!";
		objectSetInterval = 0.0f;
	}

	public void HideStartBtn(){
		foreach (Transform child in UIcanvas.transform){
			if(child.name == "StartBtn"){
				Button button1 = child.gameObject.GetComponent<Button>();
				button1.gameObject.SetActive (false);
			}
		}
	}

	public void ShowStartBtn(){
		foreach (Transform child in UIcanvas.transform){
			if(child.name == "StartBtn"){
				Button button1 = child.gameObject.GetComponent<Button>();
				button1.gameObject.SetActive (true);
			}
		}
	}

	public void HideRestartBtn(){
		foreach (Transform child in UIcanvas.transform){
			if(child.name == "RestartBtn"){
				Button button1 = child.gameObject.GetComponent<Button>();
				button1.gameObject.SetActive (false);
			}
		}
	}

	public void ShowRestartBtn(){
		foreach (Transform child in UIcanvas.transform){
			if(child.name == "RestartBtn"){
				Button button1 = child.gameObject.GetComponent<Button>();
				button1.gameObject.SetActive (true);
			}
		}
	}

	private void RandomMessage(){
		float messageRandom = Random.Range (0.0f,10.0f);
		if (messageRandom < 3.3f) {
			assistantMessage.text = "yo!";
		} else if (messageRandom < 6.6f) {
			assistantMessage.text = "ha!";
		} else {
			assistantMessage.text = "se!";
		}
	}

	private IEnumerator messageEnum(){
		RandomMessage ();
		yield return new WaitForSeconds(0.7f);
		assistantMessage.text = "";
	}

	
	private void setAssistantMessage(){
		StartCoroutine("messageEnum");
	}

	private IEnumerator StartEffect(){
		HideStartBtn ();
		yield return new WaitForSeconds(0.7f);
		assistantMessage.text = "let's go!";
		yield return new WaitForSeconds(0.7f);
		gameStart = true;
	}


	private void LoadFireWood(){
		Vector3 fireWoodPosition = new Vector3 (0.0f, -1.3f, 0.0f);
		Instantiate (firewood, fireWoodPosition, Quaternion.identity);
	}

	public void LoadFireWoodLR(){
		Vector3 fireWoodPositionR = new Vector3 (0.6f, -0.6f, 0.0f);
		Instantiate (fireWoodR, fireWoodPositionR, Quaternion.identity);
		Vector3 fireWoodPositionL = new Vector3 (-0.6f, -0.6f, 0.0f);
		Instantiate (fireWoodL, fireWoodPositionL, Quaternion.identity);
	}

	public void LoadFireWoodL(){
	}

	private void LoadCan(){
		Vector3 canPosition = new Vector3 (0.0f, -1.3f, 0.0f);
		Instantiate (can, canPosition, Quaternion.identity);
	}
}
