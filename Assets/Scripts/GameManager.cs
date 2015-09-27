using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

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

	public bool nfDesFlg;

	public string gameMode;
	public bool gameOver;
	public bool gameStart;
	
	public int score;
	public Text scoreTxt;
	public Text assistantMessage;
	public Text gameOverTxt;
	public Text missTxt;
	public Text gameModeTxt;
	public Text galleryTxt;

	public Button startBtn;
	public Button restartBtn;

	public Canvas UIcanvas;

	public float objectRepopInv;
	public float objectDestroyInv;
	private float objectSetInterval;

	private int prevNfRandom = 0;
	private List<Transform> nfList = new List<Transform>();
	private Transform[] easyNfArray;
	private Transform[] normalNfArray;
	private Transform[] hardNfArray;
	private int listNum;

	private float easyRepopInv = 1.3f;
	private float easyDestroyInv = 1.0f;
	private float normalRepopInv = 1.1f;
	private float normalDestroyInv = 0.8f;
	private float hardRepopInv = 0.9f;
	private float hardDestroyInv = 0.6f;
	private float fwPoint;

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
		setBtnBool ("ReSelectButton", false);
		gameOverTxt.text = "";
		easyNfArray = new Transform [2]{can,groud};
		normalNfArray = new Transform [3]{can,redCan,groud};
		hardNfArray = new Transform [3]{can,redCan,groud};
		setBtnBool ("StartBtn", false);
		nfDesFlg = true;
	}
	
	// Update is called once per frame
	void Update () {

		scoreTxt.text = score.ToString ();
		
		if (gameStart) {


			if (!gameOver) {
				objectSetInterval += Time.deltaTime;

				if (objectSetInterval > objectRepopInv) {

					DoCoroutine("messageEnum");

					float objectRandom = Random.Range (0.0f, 10.0f);
					if (objectRandom < fwPoint) {
						LoadObject(firewood);
					} else {
						while(true){
							int nfRandom = Random.Range(0,listNum);
							if(nfRandom == prevNfRandom){
							} else {
								prevNfRandom = nfRandom;
								LoadObject(nfList[nfRandom]);
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
		setBtnBool ("ReSelectButton", false);
		score = 0;
		gameOver = false;
		assistantMessage.text = "よっしゃー！";
		missTxt.text = "";
		gameOverTxt.text = "";
		objectSetInterval = 0.0f;
		nfDesFlg = true;
	}

	public void GameDataResetFunc(){
		missTxt.text = "";
		gameOverTxt.text = "";
		objectSetInterval = 0.0f;
		score = 0;
		gameOver = false;
		gameStart = false;
	}

	public void setGameMode(string mode){
		gameMode = mode;
		gameModeTxt.text = mode;
		nfList.Clear();
		if(mode == "easy"){
			objectRepopInv = easyRepopInv;
			objectDestroyInv = easyDestroyInv;
			nfList.AddRange(easyNfArray);
			listNum = nfList.Count;
			fwPoint = 7.0f;
		} else if(mode == "normal"){
			objectRepopInv = normalRepopInv;
			objectDestroyInv = normalDestroyInv;
			nfList.AddRange(normalNfArray);
			listNum = nfList.Count;
			fwPoint = 6.5f;
		} else if(mode == "hard"){
			objectRepopInv = hardRepopInv;
			objectDestroyInv = hardDestroyInv;
			nfList.AddRange(hardNfArray);
			listNum = nfList.Count;
			fwPoint = 6.0f;
		}
		DoCoroutine ("StartEffect");
	}

	public void setBtnBool(string btnName, bool btnBool){
		foreach (Transform child in UIcanvas.transform){
			if(child.name == btnName){
				Button button1 = child.gameObject.GetComponent<Button>();
				button1.gameObject.SetActive (btnBool);
			}
		}
	}

	public void showDifficultyBtn(){
		setBtnBool ("EasyButton",  true);
		setBtnBool ("NormalButton", true);
		setBtnBool ("HardButton", true);
		setBtnBool ("RestartBtn", false);
		setBtnBool ("ReSelectButton", false);
	}

	public void hideDifficultyBtn(){
		setBtnBool ("EasyButton",  false);
		setBtnBool ("NormalButton", false);
		setBtnBool ("HardButton", false);
	}

	private void RandomMessage(){
		float messageRandom = Random.Range (0.0f,10.0f);
		if (messageRandom < 3.3f) {
			assistantMessage.text = "よっ！";
		} else if (messageRandom < 6.6f) {
			assistantMessage.text = "はっ！";
		} else {
			assistantMessage.text = "せいっ！";
		}
	}

	private void galleryRdmMs(){
		float messageRandom = Random.Range (0.0f,10.0f);
		if (messageRandom < 3.3f) {
			galleryTxt.text = "お〜";
		} else if (messageRandom < 6.6f) {
			galleryTxt.text = "いいね！";
		} else {
			galleryTxt.text = "やる〜！";
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
		setBtnBool ("ReSelectButton", true);
	}

	private IEnumerator messageEnum(){
		RandomMessage ();
		yield return new WaitForSeconds(objectDestroyInv);
		assistantMessage.text = "";
	}

	private IEnumerator galleryMsEnum(){
		galleryRdmMs ();
		yield return new WaitForSeconds(0.3f);
		galleryTxt.text = "";
	}

	private IEnumerator StartEffect(){
		hideDifficultyBtn ();
		setBtnBool ("StartBtn", false);
		yield return new WaitForSeconds(0.6f);
		assistantMessage.text = "いくよ〜！";
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
