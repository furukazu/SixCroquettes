using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	//Input.mousePosition
		var t = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		t.z = this.transform.position.z;
		this.transform.position = t;
	}

	//void OnCollisionEnter2D(Collision2D coll) {
		//if (coll.gameObject.tag == "Enemy")
		//	coll.gameObject.SendMessage("ApplyDamage", 10);
		//print(coll);
		//coll.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
		//coll.transform.parent = this.transform;

	//}
}
