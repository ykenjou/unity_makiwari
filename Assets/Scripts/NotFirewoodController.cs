﻿using UnityEngine;
using System.Collections;

public class NotFirewoodController : MonoBehaviour {

	private float fwExistedInterval;
	GameManager gameManager;
	
	// Use this for initialization
	void Start () {
		fwExistedInterval = 0.0f;
		gameManager = GameManager.GetController ();
	}
	
	// Update is called once per frame
	void Update () {
		
		
		if (gameManager.gameMode == "easy") {
		}
		
		
		if (!gameManager.gameOver) {
			fwExistedInterval += Time.deltaTime;
			if(fwExistedInterval > gameManager.normalDestroyInterval){
				Destroy(this.gameObject);
			}
		}
	}
}
