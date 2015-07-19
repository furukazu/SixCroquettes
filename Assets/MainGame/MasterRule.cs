using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MasterRule : MonoBehaviour {

	/// <summary>
	/// 簡単に全体が参照できるようにするための場所
	/// The rule.
	/// </summary>
	public static MasterRule TheRule;

	// エディタから設定されるそれぞれのラベル
	public Text RemainDisplay;
	public Text ATPDisplay;
	public Text StuffDisplay;

	// 今まで得たコロッケの数、総得点、総アイテム数
	public int Croquettes;
	public long ATPoint;
	public int Stuffs;

	// コンボ数の記録と得たコロッケの種類の記録
	public int NotGomiCombo;
	public Dictionary<int,bool> Korokkes;

	void Awake(){
		// Awakeでstaticにぶち込むことで全体がstaticのアクセスでこいつに到達できる
		TheRule = this;
	}

	void Start () {
		// 初期状態は残りコロッケ数6、あとは基本的に初期値
		Croquettes = 6;
		ATPoint = 0;
		Stuffs = 0;
		NotGomiCombo = 1;
		Korokkes = new Dictionary<int, bool>();
	}
	
	void Update () {
		// 毎フレームの動作は現在の数値をラベルに適用するだけ
		RemainDisplay.text = string.Format("Croquettes to Finish: {0}",Croquettes);
		ATPDisplay.text = string.Format("Anti-Typhoon Point: {0}",ATPoint);
		StuffDisplay.text = string.Format("Stuffs: {0}",Stuffs);
	}

	/// <summary>
	/// ゲームが終わったら呼ばれる。ただちに次のシーンを呼び出す
	/// Finishs the game.
	/// </summary>
	public void FinishGame ()
	{
		Application.LoadLevel(2);
	}
}
