using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	void Update () {
		// // 毎描画時、マウスの場所を追いかけるだけ。z値は念のため変えないで置く
		var t = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		t.z = this.transform.position.z;
		this.transform.position = t;
	}

}
