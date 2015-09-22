using UnityEngine;
using System.Collections;

public class SplitObjectLController : MonoBehaviour {

	private GameObject mainCamera;
	
	// Use this for initialization
	void Start () {
		mainCamera = GameObject.Find ("Main Camera");
		GetComponent<Rigidbody2D>().AddForce (new Vector2(-100,200));
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 cameraPosition = mainCamera.GetComponent<Camera>().ViewportToWorldPoint (new Vector3 (0.0f,0.0f,Camera.main.nearClipPlane));
		
		if(transform.position.y < cameraPosition.y){
			Destroy(this.gameObject);
		}
	}
}
