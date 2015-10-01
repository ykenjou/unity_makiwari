using UnityEngine;
using System.Collections;

public class NotFirewoodController : MonoBehaviour {

	private float fwExistedInterval;
	GameManager gameManager;
	private Vector3 mPos;
	private float totalPos;
	
	// Use this for initialization
	void Start () {
		fwExistedInterval = 0.0f;
		gameManager = GameManager.GetController ();
		mPos = transform.localPosition;
		totalPos = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {

		/*
		if(totalPos <= 0.1f){
			transform.localPosition = mPos;
			mPos.x += 0.05f;
			totalPos += 0.05f;
		}
		*/
		
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
