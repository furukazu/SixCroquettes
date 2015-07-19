using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MasterRule : MonoBehaviour {

	public static MasterRule TheRule;

	public Text RemainDisplay;
	public Text ATPDisplay;
	public Text StuffDisplay;

	public int Croquettes;
	public long ATPoint;
	public int Stuffs;

	public int NotGomiCombo;
	public Dictionary<int,bool> Korokkes;

	void Awake(){
		TheRule = this;
	}

	// Use this for initialization
	void Start () {
		Croquettes = 6;
		ATPoint = 0;
		Stuffs = 0;
		NotGomiCombo = 1;
		Korokkes = new Dictionary<int, bool>();
	}
	
	// Update is called once per frame
	void Update () {
		RemainDisplay.text = string.Format("Croquettes to Finish: {0}",Croquettes);
		ATPDisplay.text = string.Format("Anti-Typhoon Point: {0}",ATPoint);
		StuffDisplay.text = string.Format("Stuffs: {0}",Stuffs);
	}

	public void FinishGame ()
	{
		//Time.timeScale = 0;
		Application.LoadLevel(2);
	}
}
