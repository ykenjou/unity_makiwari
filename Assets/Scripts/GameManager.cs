﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public Transform player;
	public Animator playerAnim;
	public Transform Axe;

	public Transform assistant;
	[System.NonSerialized] public Animator assistantAnim;
	[System.NonSerialized] public Sprite assinstantSprite;
	AssistantCtrl assistantCtrl;
	PlayerController playerCtrl;

	public Transform hitEffect;

	public Transform firewood;
	public Transform fireWoodR;
	public Transform fireWoodL;

	public Transform apple;
	public Transform pear;
	public Transform chestnut;
	public Transform grapes_g;
	public Transform grapes_p;
	public Transform acorn;
	public Transform persimmon;

	public Transform appleRight;
	public Transform appleLeft;

	public Transform fwApple;
	public Transform fwPear;
	public Transform fwChesnut;
	public Transform fwAcorn;
	public Transform fwGrapeG;
	public Transform fwGrapeP;
	public Transform fwPersimmon;

	public Transform fwAppleRight;
	public Transform fwAppleLeft;

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
	private float animStartInv;

	private int prevNfRandom = 0;
	private List<Transform> nfList = new List<Transform>();
	private Transform[] easyNfArray;
	private Transform[] normalNfArray;
	private Transform[] hardNfArray;
	private Transform[] veryHardNfArray;
	private int listNum;
	public int nfCount;

	private List<Transform> fwList = new List<Transform> ();
	private Transform[] easyFwArray;
	private Transform[] normalFwArray;
	private Transform[] hardFwArray;
	private Transform[] veryHardFwArray;
	private int fwListNum;

	private float easyRepopInv = 1.12f;
	private float easyDestroyInv = 0.9f;
	private float normalRepopInv = 1.0f;
	private float normalDestroyInv = 0.7f;
	private float hardRepopInv = 0.9f;
	private float hardDestroyInv = 0.6f;
	private float veryHardRepopInv = 0.85f;
	private float veryHardDestroyInv = 0.55f;
	private float fwPoint;

	public static GameManager GetController() {
		return GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager>();
	}


	// Use this for initialization
	void Start () {
		System.GC.Collect ();
		Application.targetFrameRate = 60;
		gameMode = "";
		gameStart = false;
		gameOver = false;
		objectSetInterval = 0.0f;
		score = 0;
		scoreTxt.text = score.ToString();
		setBtnBool ("RestartBtn", false);
		setBtnBool ("ReSelectButton", false);
		gameOverTxt.text = "";

		easyNfArray = new Transform [3]{apple,pear,acorn};
		normalNfArray = new Transform [4]{apple,pear,chestnut,grapes_g};
		hardNfArray = new Transform [5]{apple,pear,chestnut,persimmon,grapes_p};
		veryHardNfArray = new Transform[7]{apple,pear,acorn,chestnut,persimmon,grapes_g,grapes_p};
		nfCount = 0;

		easyFwArray = new Transform [3]{fwApple,fwPear,fwAcorn};
		normalFwArray = new Transform [4]{fwApple,fwPear,fwChesnut,fwGrapeG};
		hardFwArray = new Transform [5]{fwApple,fwPear,fwChesnut,fwPersimmon,fwGrapeP};
		veryHardFwArray = new Transform[7]{fwApple,fwPear,fwAcorn,fwChesnut,fwPersimmon,fwGrapeG,fwGrapeP};

		setBtnBool ("StartBtn", false);
		nfDesFlg = true;
		assistantAnim = assistant.GetComponent <Animator> ();
		assistantCtrl = AssistantCtrl.GetController ();
		playerAnim = player.GetComponent<Animator> ();
		//Screen.orientation = ScreenOrientation.AutoRotation;
	}
	
	// Update is called once per frame
	void Update () {

		scoreTxt.text = score.ToString ();
		
		if (gameStart) {


			if (!gameOver) {
				objectSetInterval += Time.deltaTime;

				if (objectSetInterval > objectRepopInv) {

					DoCoroutine("messageEnum");
					DoCoroutine("setGetEnum");

					float objectRandom = Random.Range (0.0f, 10.0f);
					if (objectRandom < fwPoint) {
						int fwRandom = Random.Range(0,fwListNum);
						LoadObject(fwList[fwRandom]);
						nfCount = 0;
					} else {
						nfCount++;
						if(nfCount < 4){
							while(true){
								int nfRandom = Random.Range(0,listNum);
								if(nfRandom == prevNfRandom){
								} else {
									prevNfRandom = nfRandom;
									LoadObject(nfList[nfRandom]);
									break;
								}
							}
						} else {
							int fwRandom = Random.Range(0,fwListNum);
							LoadObject(fwList[fwRandom]);
							nfCount = 0;
						}

					}
					objectSetInterval = 0.0f;

				}
			}
		}
	}


	public void GameOverFunc(){
		System.GC.Collect ();
		assistantCtrl.changeFailureSprite();
		StartCoroutine("gameOverEnum");
		playerAnim.SetBool ("GameOver",true);
	}

	public void GameRestartFunc(){
		setBtnBool ("RestartBtn", false);
		setBtnBool ("ReSelectButton", false);
		score = 0;
		gameOver = false;
		playerAnim.SetBool ("GameOver",false);
		assistantMessage.text = "よっしゃー！";
		assistantCtrl.changeGoSprite ();
		missTxt.text = "";
		gameOverTxt.text = "";
		objectSetInterval = 0.0f;
		nfDesFlg = true;
		nfCount = 0;
	}

	public void GameDataResetFunc(){
		missTxt.text = "";
		gameOverTxt.text = "";
		objectSetInterval = 0.0f;
		score = 0;
		gameOver = false;
		gameStart = false;
		assistantCtrl.changeIdleSprite ();
	}

	public void setGameMode(string mode){
		gameMode = mode;
		gameModeTxt.text = mode;
		nfList.Clear();
		fwList.Clear();
		nfCount = 0;
		playerAnim.SetTrigger ("Idle");
		if (mode == "easy") {
			objectRepopInv = easyRepopInv;
			objectDestroyInv = easyDestroyInv;
			nfList.AddRange (easyNfArray);
			listNum = nfList.Count;
			fwPoint = 7.0f;
			Shuffle (easyFwArray);
			for (int i = 0; i < 1; i++) {
				fwList.Add (easyFwArray [i]);
			}
			fwListNum = fwList.Count;
		} else if (mode == "normal") {
			objectRepopInv = normalRepopInv;
			objectDestroyInv = normalDestroyInv;
			nfList.AddRange (normalNfArray);
			listNum = nfList.Count;
			fwPoint = 6.5f;
			Shuffle (normalFwArray);
			for (int i = 0; i < 2; i++) {
				fwList.Add (normalFwArray [i]);
			}
			fwListNum = fwList.Count;
		} else if (mode == "hard") {
			objectRepopInv = hardRepopInv;
			objectDestroyInv = hardDestroyInv;
			nfList.AddRange (hardNfArray);
			listNum = nfList.Count;
			fwPoint = 6.0f;
			Shuffle (hardFwArray);
			for (int i = 0; i < 3; i++) {
				fwList.Add (hardFwArray [i]);
			}
			fwListNum = fwList.Count;
		} else if (mode == "veryHard") {
			objectRepopInv = veryHardRepopInv;
			objectDestroyInv = veryHardDestroyInv;
			nfList.AddRange (veryHardNfArray);
			listNum = nfList.Count;
			fwPoint = 5.9f;
			fwList.AddRange(veryHardFwArray);
			fwListNum = fwList.Count;
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
		setBtnBool ("VeryHardButton", true);
		setBtnBool ("RestartBtn", false);
		setBtnBool ("ReSelectButton", false);
	}

	public void hideDifficultyBtn(){
		setBtnBool ("EasyButton",  false);
		setBtnBool ("NormalButton", false);
		setBtnBool ("HardButton", false);
		setBtnBool ("VeryHardButton", false);
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
		gameOver = true;
		Time.timeScale = 0.6f;
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
		assistantCtrl.changeGoSprite ();
		assistantMessage.text = "いくよ〜！";
		yield return new WaitForSeconds(0.6f);
		gameStart = true;
	}

	private IEnumerator setGetEnum(){
		assistantCtrl.changeSetSprite ();
		yield return new WaitForSeconds(objectDestroyInv);
		if (!gameOver) {
			assistantCtrl.changeGetSprite ();
			yield return new WaitForSeconds(0.15f);
			assistantCtrl.changeGet2Sprite ();
		}
	}

	public void LoadObject(Transform loadObject){
		Vector3 position = new Vector3 (0.0f, -1.5f, 0.0f);
		Instantiate (loadObject, position, Quaternion.identity);
	}

	public void LoadLRObject(Transform L,Transform R){
		Vector3  positionR = new Vector3 (0.6f, -0.9f, 0.0f);
		Instantiate (R, positionR, Quaternion.identity);
		Vector3 positionL = new Vector3 (-0.6f, -0.9f, 0.0f);
		Instantiate (L, positionL, Quaternion.identity);
	}

	void Shuffle (Transform[] deck) {
		for (int i = 0; i < deck.Length; i++) {
			Transform temp = deck[i];
			int randomIndex = Random.Range(0, deck.Length);
			deck[i] = deck[randomIndex];
			deck[randomIndex] = temp;
		}
	}

}
