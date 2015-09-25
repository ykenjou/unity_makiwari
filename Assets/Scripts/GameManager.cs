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
	public Transform canR;
	public Transform canL;
	public Transform redCan;
	public Transform redCanR;
	public Transform redCanL;
	public Transform groud;
	public Transform groudL;
	public Transform groudR;
	public int score;
	public string gameMode;
	public bool gameOver;
	public bool gameStart;
	public float normalDestroyInterval;
	public Text scoreTxt;
	public Text assistantMessage;
	public Text gameOverTxt;
	public Text missTxt;
	public Button startBtn;
	public Button restartBtn;
	public Canvas UIcanvas;

	private float objectSetInterval;
	private float normalRepopInterval;
	private float cutWaitInterval;
	private int prevNfRandom = 0;
	Transform[] nfArray;

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
		setBtnBool ("RestartBtn", false);
		setBtnBool ("RestartBtn", false);
		gameOverTxt.text = "";
		nfArray = new Transform [3];
		nfArray [0] = can;
		nfArray [1] = redCan;
		nfArray [2] = groud;
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

					DoCoroutine("messageEnum");

					float objectRandom = Random.Range (0.0f, 10.0f);
					if (objectRandom < 6.5f) {
						LoadObject(firewood);
					} else {
						while(true){
							int notFirewoodRandom = Random.Range(0,3);
							if(notFirewoodRandom == prevNfRandom){
							} else {
								prevNfRandom = notFirewoodRandom;
								LoadObject(nfArray[notFirewoodRandom]);
								break;
							}
						}

					}
					objectSetInterval = 0.0f;
				}
			}
		}
	}

	public void GameOverFunc(){
		System.GC.Collect ();
		StartCoroutine("gameOverEnum");
	}

	public void GameRestartFunc(){
		setBtnBool ("RestartBtn", false);
		score = 0;
		gameOver = false;
		assistantMessage.text = "come on!";
		missTxt.text = "";
		gameOverTxt.text = "";
		objectSetInterval = 0.0f;
	}

	public void setBtnBool(string btnName, bool btnBool){
		foreach (Transform child in UIcanvas.transform){
			if(child.name == btnName){
				Button button1 = child.gameObject.GetComponent<Button>();
				button1.gameObject.SetActive (btnBool);
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

	public void DoCoroutine(string IenumName){
		StartCoroutine(IenumName);
	}

	public IEnumerator gameOverEnum(){
		Time.timeScale = 0.6f;
		gameOver = true;
		assistantMessage.text = "";
		yield return new WaitForSeconds(0.7f);
		Time.timeScale = 1.0f;
		gameOverTxt.text = "Game Over!";
		yield return new WaitForSeconds(1.0f);
		setBtnBool ("RestartBtn", true);
	}

	private IEnumerator messageEnum(){
		RandomMessage ();
		yield return new WaitForSeconds(0.7f);
		assistantMessage.text = "";
	}

	private IEnumerator StartEffect(){
		setBtnBool ("StartBtn", false);
		yield return new WaitForSeconds(0.6f);
		assistantMessage.text = "let's go!";
		yield return new WaitForSeconds(0.6f);
		gameStart = true;
	}

	public void LoadObject(Transform loadObject){
		Vector3 position = new Vector3 (0.0f, -1.3f, 0.0f);
		Instantiate (loadObject, position, Quaternion.identity);
	}

	public void LoadLRObject(Transform L,Transform R){
		Vector3  positionR = new Vector3 (0.6f, -0.6f, 0.0f);
		Instantiate (R, positionR, Quaternion.identity);
		Vector3 positionL = new Vector3 (-0.6f, -0.6f, 0.0f);
		Instantiate (L, positionL, Quaternion.identity);
	}
	
}
