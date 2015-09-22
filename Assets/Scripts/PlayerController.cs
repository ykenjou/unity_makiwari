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
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "FireWood") {
			Destroy(other.gameObject);
			gameManager.score += 1;
			gameManager.LoadFireWoodLR();
		}

		if (other.tag == "NotFireWood") {
			Destroy(other.gameObject);
			gameManager.GameOverFunc();
		}
	}
}
