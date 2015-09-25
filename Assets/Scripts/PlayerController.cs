using UnityEngine;
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
			gameManager.LoadLRObject(gameManager.fireWoodL,gameManager.fireWoodR);
		}

		if (other.tag == "NotFireWood") {
			Destroy(other.gameObject);
			gameManager.GameOverFunc();
		}

		if (other.tag == "Can") {
			Destroy(other.gameObject);
			gameManager.GameOverFunc();
			gameManager.missTxt.text = "miss!";
			gameManager.LoadLRObject(gameManager.canL,gameManager.canR);
		}

		if (other.tag == "RedCan") {
			Destroy(other.gameObject);
			gameManager.GameOverFunc();
			gameManager.missTxt.text = "miss!";
			gameManager.LoadLRObject(gameManager.redCanL,gameManager.redCanR);
		}

		if (other.tag == "Groud") {
			Destroy(other.gameObject);
			gameManager.GameOverFunc();
			gameManager.missTxt.text = "miss!";
			gameManager.LoadLRObject(gameManager.groudL,gameManager.groudR);
		}
	}
}
