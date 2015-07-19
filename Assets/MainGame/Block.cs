using UnityEngine;
using System.Collections;

/// <summary>
/// 画面を舞うあいつら
/// Block.
/// </summary>
public class Block : MonoBehaviour {

	/// <summary>
	/// 寿命。毎描画Update時にこれが1ずつ減っていき、0になったら消える。
	/// Updateに依存させると描画落ちする環境ではさらに消えるまで時間がかかるという悪循環になるので、
	/// 本当は消えるまでの秒数を覚えておいて、それを都度deltaTimeだけひく、みたいな実装の方が本当はベスト
	/// The ttl.
	/// </summary>
	public int ttl;

	/// <summary>
	/// プレイヤーにキャッチされて永遠になったかどうか？
	/// The is eternal.
	/// </summary>
	public bool IsEternal;

	/// <summary>
	/// このBlockの発生源。消えるときこいつの持っているPoolへ回収される
	/// The source.
	/// </summary>
	public Source Source ;

	/// <summary>
	/// いちいちGetComponentするのがめんどうだったのでエディタの上で関連づけておく
	/// The this renderer.
	/// </summary>
	public SpriteRenderer ThisRenderer;

	/// <summary>
	/// ブロックが使うSpriteの候補。
	/// 本当はここに持たないでSpriteManagerみたいな
	/// 外部の管理クラスから受け取る方がそれっぽいんだけど
	/// めんどうなのでやらなかった。
	/// 値はエディタの上で関連づけられている
	/// The source sprites.
	/// </summary>
	public Sprite [] SourceSprites;

	/// <summary>
	/// こいつがゴミなのかコロッケなのかその他なのかの識別子
	/// The type of the stuff.
	/// </summary>
	public int StuffType;

	/// <summary>
	/// 消えるときの処理。物理演算しないようにして様々な値を初期化してPoolへと突っ込む
	/// Exit this instance.
	/// </summary>
	public void Exit(){
		R2d.isKinematic = true;
		del = null;
		gameObject.SetActive(false);
		ttl = 0;
		Source.Pool.Push(this.gameObject);
	}

	/// <summary>
	/// 出てくるときの処理。物理演算するようにして様々な値を新人向けの値に設定する
	/// Enter this instance.
	/// </summary>
	public void Enter(){
		R2d.isKinematic = false;
		del = null;
		gameObject.SetActive(true);
		ttl = 600;
		IsEternal = false;
	}

	/// <summary>
	/// こいつがゴミなのかコロッケなのかそれ以外なのかを決める
	/// Chooses the random.
	/// </summary>
	public void ChooseRandom(){
		// gomi
		// korokke 1-6
		// stuff1-4
		var i = Random.Range(0,11);
		ThisRenderer.sprite = SourceSprites[i];
		StuffType = i;
	}

	/// <summary>
	/// いちいちGetComponentするのがめんどうだったので/値はエディタの上で関連づけられている
	/// The r2d.
	/// </summary>
	public Rigidbody2D R2d;

	private Transform del;

	void Update () {
		// プレイヤーにキャッチされていたら何もしない
		if(IsEternal){ return; }

		// // それ以外は寿命を減らし尽きたら消える
		--ttl;
		if(ttl<0){
			Exit();
		}
	}

	/// <summary>
	/// 何かとぶつかったら
	/// Raises the collision enter2 d event.
	/// </summary>
	/// <param name="coll">Coll.</param>
	void OnCollisionEnter2D(Collision2D coll) {
		// こいつにとってdelが入ってると言うことはすでにプレイヤーにキャッチされているはずなので何もしない
		if(del!=null){ return; }

		// 以下、新しくプレイヤーにキャッチされたものであるという前提

		// ぶつかった相手がPlayerだったらそいつの子になって、Playerへの参照を持っておく
		if(coll.gameObject.name == "Player"){
			this.transform.parent = coll.transform;//.parent;
			del = coll.transform;//.parent;
		}else{
			// ぶつかった相手がPlayer以外だったら

			// 相手はすでにキャッチされているブロックのはずで、そうじゃないのなら何もしない
			del = coll.gameObject.GetComponent<Block>().del;
			if(del == null){ return; }

			// 相手がキャッチされているブロックだったら、相手が持っているPlayerへの参照を使って自分がそいつの子になる
			this.transform.parent = del;
		}

		// 物理演算しないように
		R2d.isKinematic = true;
		// Playerにキャッチされましたフラグを立てる
		IsEternal = true;

		// コロッケだったら残りコロッケ回数を減らして0になったらゲームを終わる
		if(1<= StuffType && StuffType <= 6){
			MasterRule.TheRule.Korokkes[StuffType] = true;
			MasterRule.TheRule.Croquettes -= 1;
			if(MasterRule.TheRule.Croquettes<=0){
				MasterRule.TheRule.FinishGame();
			}
		}

		// ゴミならコンボを消し、そうじゃないなら増やす
		if(StuffType==0){
			MasterRule.TheRule.NotGomiCombo = 0;
		}else{
			MasterRule.TheRule.NotGomiCombo += 1;
		}

		// 点数計算と得たアイテム数を+1する
		var korokkes = MasterRule.TheRule.Korokkes.Count;

		MasterRule.TheRule.ATPoint += 200*MasterRule.TheRule.NotGomiCombo*(korokkes*korokkes+1);
		MasterRule.TheRule.Stuffs += 1;
	}
}
