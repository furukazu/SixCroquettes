using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResultScene : MonoBehaviour {

	public Text Result;

	public void Restart(){
		Application.LoadLevel(1);
	}

	// Use this for initialization
	void Start () {
		Result.text = string.Format(
		@"Result

Stuffs: {0}
Anti-Typoon Point: {1}",MasterRule.TheRule.Stuffs,MasterRule.TheRule.ATPoint);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
