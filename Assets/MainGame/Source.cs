using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// ブロック発生源
/// Source.
/// </summary>
public class Source : MonoBehaviour {

	/// <summary>
	/// 生々するブロックのプレハブ
	/// The prefab.
	/// </summary>
	public GameObject Prefab;

	/// <summary>
	/// 消えたブロックを溜めておく所
	/// The pool.
	/// </summary>
	public Stack<GameObject> Pool;

	/// <summary>
	///  ブロック生成における初期速度の元X
	/// The init force x.
	/// </summary>
	public float InitForceX;

	/// <summary>
	/// ブロック生成における初期速度の元Y
	/// The init force y.
	/// </summary>
	public float InitForceY;

	void Awake(){
		// // とりあえずAwakeタイミングでプールを初期化しておく
		Pool = new Stack<GameObject>();
	}

	void Start () {
		// スタート時にメインの処理を起動させる。
		// Updateじゃなくて時間での待ちを入れたのでこっちにした
		StartCoroutine(main ());
	}

	private IEnumerator main(){
		// 念のための連番
		int i = 0;

		while(true){
			GameObject go;

			// Poolにお古があればそれを使う、そうじゃなければ新規作成する
			if(Pool.Count>0){
				go = Pool.Pop();
			}else{
				go = Instantiate(Prefab) as GameObject;
			}

			// 新しくできた奴の値の初期化
			var blc = go.GetComponent<Block>();
			blc.Source = this;
			blc.Enter();
			blc.ChooseRandom();

			// 名前も無駄にユニークになるように付けてやる
			go.name = string.Format("Block{0}",i);

			// 発生位置はとりあえず自分と一緒。そっから適当な速度を与える
			go.transform.position = this.transform.position;
			var r2d = go.GetComponent<Rigidbody2D>();
			r2d.AddForce(new Vector2(InitForceX,InitForceY + Random.Range(-60.0f,30.0f)),ForceMode2D.Impulse);
			r2d.angularVelocity = Mathf.Sign(Random.Range(-1.0f,1.0f))*360.0f + Random.Range(-30.0f,30.0f);

			// 次の連番へした後しばし待つ
			++i;
			yield return new WaitForSeconds(0.5f);
		}
	}

	// Update is called once per frame
	//void Update () {
	//
	//}
}
