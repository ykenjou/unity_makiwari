using UnityEngine;
using System.Collections;

public class ObjectCtrl : MonoBehaviour {

	public Transform fireWoodR;
	public Transform fireWoodL;
	
	public Transform apple;
	public Transform pear;
	public Transform chestnut;
	public Transform grapes_g;
	public Transform grapes_p;
	public Transform acorn;
	public Transform persimmon;
	
	public Transform appleRight;
	public Transform appleLeft;
	
	public Transform fwApple;
	public Transform fwPear;
	public Transform fwChesnut;
	public Transform fwAcorn;
	public Transform fwGrapeG;
	public Transform fwGrapeP;
	public Transform fwPersimmon;
	
	public Transform fwAppleRight;
	public Transform fwAppleLeft;

	public static ObjectCtrl GetController() {
		return GameObject.FindGameObjectWithTag ("ObjectCtrl").GetComponent<ObjectCtrl>();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
