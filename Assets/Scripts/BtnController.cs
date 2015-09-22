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
		gameManager.GameStartFunc ();
	}

	public void RestartClick(){
		gameManager.GameRestartFunc ();
	}
}
