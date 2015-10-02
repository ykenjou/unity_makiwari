using UnityEngine;
using System.Collections;


public class BtnController : MonoBehaviour {

	GameManager gameManager;

	// Use this for initialization
	void Start () {
		gameManager = GameManager.GetController ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void StartClick(){
		gameManager.DoCoroutine ("StartEffect");
	}

	public void RestartClick(){
		gameManager.GameRestartFunc ();
	}

	public void EasyBtnClick(){
		gameManager.setGameMode("easy");
	}

	public void noralBtnClick(){
		gameManager.setGameMode("normal");
	}

	public void hardBtnClick(){
		gameManager.setGameMode("hard");
	}

	public void veryHardBtnClick(){
		gameManager.setGameMode("veryHard");
	}

	public void reselectBtnClick(){
		gameManager.showDifficultyBtn ();
		gameManager.GameDataResetFunc ();
	}
}
