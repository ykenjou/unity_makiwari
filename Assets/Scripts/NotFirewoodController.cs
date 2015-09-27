using UnityEngine;
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
		
		if (!gameManager.gameOver) {
			fwExistedInterval += Time.deltaTime;
			if(fwExistedInterval > gameManager.objectDestroyInv){
				//gameManager.DoCoroutine("galleryMsEnum");
				Destroy(this.gameObject);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Axe") {
			/*
			if(this.tag == "Can"){
				Destroy(this.gameObject);
				gameManager.GameOverFunc();
			}
			*/
		}
	}
}
