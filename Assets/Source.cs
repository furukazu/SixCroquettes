using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Source : MonoBehaviour {

	public GameObject Prefab;

	public Stack<GameObject> Pool;

	public float InitForceX;
	public float InitForceY;

	void Awake(){
		Pool = new Stack<GameObject>();
	}

	// Use this for initialization
	void Start () {
		StartCoroutine(main ());
	}

	private IEnumerator main(){
		int i = 0;
		while(true){
			GameObject go;

			if(Pool.Count>0){
				go = Pool.Pop();
				//go.GetComponent<Block>().Enter();
			}else{
				go = Instantiate(Prefab) as GameObject;
			}

			var blc = go.GetComponent<Block>();
			blc.Source = this;
			blc.Enter();
			blc.ChooseRandom();


			go.name = string.Format("Block{0}",i);
			go.transform.position = this.transform.position;
			var r2d = go.GetComponent<Rigidbody2D>();
			r2d.AddForce(new Vector2(InitForceX,InitForceY + Random.Range(-60.0f,30.0f)),ForceMode2D.Impulse);
			r2d.angularVelocity = Mathf.Sign(Random.Range(-1.0f,1.0f))*360.0f + Random.Range(-30.0f,30.0f);
			++i;
			yield return new WaitForSeconds(0.5f);
		}
	}

	// Update is called once per frame
	//void Update () {
	//
	//}
}
