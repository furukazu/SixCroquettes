using UnityEngine;
using System.Collections;

public class TitleScene : MonoBehaviour {

	public void GoGame(){
		// UIのボタンから呼ばれるように設定してある。ゲームメインシーンへ
		Application.LoadLevel(1);
	}
}
