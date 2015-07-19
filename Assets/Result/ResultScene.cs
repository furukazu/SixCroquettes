using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResultScene : MonoBehaviour {

	/// <summary>
	/// 画面上のラベル。エディタであらかじめあたりをぶっ込んどく
	/// The result.
	/// </summary>
	public Text Result;

	public void Restart(){
		// // 再スタートはさっきのシーンへ
		Application.LoadLevel(1);
	}

	void Start () {
		// 基本的に最初に文言を入れてあとは放置
		Result.text = string.Format(
		@"Result

Stuffs: {0}
Anti-Typoon Point: {1}",MasterRule.TheRule.Stuffs,MasterRule.TheRule.ATPoint);
	}
	
}
