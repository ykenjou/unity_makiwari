using UnityEngine;
using System.Collections;

public class AssistantCtrl : MonoBehaviour {

	SpriteRenderer spriteRenderer;
	public Sprite idleSprite;
	public Sprite goSprite;
	public Sprite setSprite;
	public Sprite getSprite;
	public Sprite failureSprite;

	public static AssistantCtrl GetController() {
		return GameObject.FindGameObjectWithTag ("Assistant").GetComponent<AssistantCtrl>();
	}

	// Use this for initialization
	void Start () {
		spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void changeIdleSprite(){
		spriteRenderer.sprite = idleSprite;
	}

	public void changeGoSprite(){
		spriteRenderer.sprite = goSprite;
	}

	public void changeSetSprite(){
		spriteRenderer.sprite = setSprite;
	}

	public void changeGetSprite(){
		spriteRenderer.sprite = getSprite;
	}

	public void changeFailureSprite(){
		spriteRenderer.sprite = failureSprite;
	}


}
