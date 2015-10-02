﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	[System.NonSerialized] public Animator animator;
	GameManager gameManager;

	// Use this for initialization
	void Start () {
		animator = GetComponent <Animator>();
		gameManager = GameManager.GetController ();
	}

	protected virtual void Awake () {

	}
	
	// Update is called once per frame
	void Update () {

		#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_WEBPLAYER || UNITY_EDITOR
		
		#endif

		if (!gameManager.gameOver && gameManager.gameStart) {
			if (Input.GetMouseButtonDown(0)) {
				animator.SetTrigger("Down_Up");
				DesFalse();
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "FireWood") {
			Destroy(other.gameObject);
			gameManager.score += 1;
			gameManager.LoadLRObject(gameManager.fireWoodL,gameManager.fireWoodR);
		}

		if (other.tag == "FwApple") {
			Destroy(other.gameObject);
			gameManager.score += 1;
			gameManager.LoadLRObject(gameManager.fireWoodL,gameManager.fireWoodR);
		}

		if (other.tag == "FwPear") {
			Destroy(other.gameObject);
			gameManager.score += 1;
			gameManager.LoadLRObject(gameManager.fireWoodL,gameManager.fireWoodR);
		}

		if (other.tag == "FwChesnut") {
			Destroy(other.gameObject);
			gameManager.score += 1;
			gameManager.LoadLRObject(gameManager.fireWoodL,gameManager.fireWoodR);
		}

		if (other.tag == "FwAcorn") {
			Destroy(other.gameObject);
			gameManager.score += 1;
			gameManager.LoadLRObject(gameManager.fireWoodL,gameManager.fireWoodR);
		}

		if (other.tag == "FwGrapeG") {
			Destroy(other.gameObject);
			gameManager.score += 1;
			gameManager.LoadLRObject(gameManager.fireWoodL,gameManager.fireWoodR);
		}

		if (other.tag == "FwGrapeP") {
			Destroy(other.gameObject);
			gameManager.score += 1;
			gameManager.LoadLRObject(gameManager.fireWoodL,gameManager.fireWoodR);
		}

		if (other.tag == "FwPersimmon") {
			Destroy(other.gameObject);
			gameManager.score += 1;
			gameManager.LoadLRObject(gameManager.fireWoodL,gameManager.fireWoodR);
		}

		if (other.tag == "Can") {
			Destroy(other.gameObject);
			gameManager.GameOverFunc();
			gameManager.missTxt.text = "ぶっぶ〜！";
			gameManager.LoadLRObject(gameManager.canL,gameManager.canR);
		}

		if (other.tag == "RedCan") {
			Destroy(other.gameObject);
			gameManager.GameOverFunc();
			gameManager.missTxt.text = "ぶっぶ〜！";
			gameManager.LoadLRObject(gameManager.redCanL,gameManager.redCanR);
		}

		if (other.tag == "Groud") {
			Destroy(other.gameObject);
			gameManager.GameOverFunc();
			gameManager.missTxt.text = "ぶっぶ〜！";
			gameManager.LoadLRObject(gameManager.groudL,gameManager.groudR);
		}

		if (other.tag == "Apple") {
			Destroy(other.gameObject);
			gameManager.GameOverFunc();
			gameManager.missTxt.text = "ぶっぶ〜！";
		}

		if (other.tag == "Pear") {
			Destroy(other.gameObject);
			gameManager.GameOverFunc();
			gameManager.missTxt.text = "ぶっぶ〜！";
		}

		if (other.tag == "Chestnut") {
			Destroy(other.gameObject);
			gameManager.GameOverFunc();
			gameManager.missTxt.text = "ぶっぶ〜！";
		}

		if (other.tag == "Acorn") {
			Destroy(other.gameObject);
			gameManager.GameOverFunc();
			gameManager.missTxt.text = "ぶっぶ〜！";
		}

		if (other.tag == "Grapes_g") {
			Destroy(other.gameObject);
			gameManager.GameOverFunc();
			gameManager.missTxt.text = "ぶっぶ〜！";
		}

		if (other.tag == "Grapes_p") {
			Destroy(other.gameObject);
			gameManager.GameOverFunc();
			gameManager.missTxt.text = "ぶっぶ〜！";
		}

		if (other.tag == "Persimmon") {
			Destroy(other.gameObject);
			gameManager.GameOverFunc();
			gameManager.missTxt.text = "ぶっぶ〜！";
		}
	}

	public void DesFalse(){
		gameManager.nfDesFlg = false;
	}

	public void DesTrue(){
		gameManager.nfDesFlg = true;
	}


}
