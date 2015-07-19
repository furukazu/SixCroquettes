using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {

	public int ttl;

	public bool IsEternal;

	public Source Source ;

	public SpriteRenderer ThisRenderer;

	public Sprite [] SourceSprites;

	public int StuffType;

	public void Exit(){
		R2d.isKinematic = true;
		del = null;
		gameObject.SetActive(false);
		ttl = 0;
		Source.Pool.Push(this.gameObject);
	}

	public void Enter(){
		R2d.isKinematic = false;
		del = null;
		gameObject.SetActive(true);
		ttl = 600;
		IsEternal = false;
	}

	public void ChooseRandom(){
		// gomi
		// korokke 1-6
		// stuff1-4
		var i = Random.Range(0,11);
		ThisRenderer.sprite = SourceSprites[i];
		StuffType = i;
	}

	public Rigidbody2D R2d;

	private Transform del;

	// Use this for initialization
	void Start () {
		//R2d.
	}
	
	// Update is called once per frame
	void Update () {
		if(IsEternal){ return; }
		--ttl;
		if(ttl<0){
			Exit();
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if(del!=null){ return; }
		// = false;

		if(coll.gameObject.name == "Player"){
			this.transform.parent = coll.transform;//.parent;
				del = coll.transform;//.parent;
		}else{
			del = coll.gameObject.GetComponent<Block>().del;
			if(del == null){ return; }
			this.transform.parent = del;
		}

		R2d.isKinematic = true;
		IsEternal = true;

		if(1<= StuffType && StuffType <= 6){
			MasterRule.TheRule.Korokkes[StuffType] = true;
			MasterRule.TheRule.Croquettes -= 1;
			if(MasterRule.TheRule.Croquettes<=0){
				MasterRule.TheRule.FinishGame();
			}
		}

		if(StuffType==0){
			MasterRule.TheRule.NotGomiCombo = 0;
		}else{
			MasterRule.TheRule.NotGomiCombo += 1;
		}

		var korokkes = MasterRule.TheRule.Korokkes.Count;


		MasterRule.TheRule.ATPoint += 200*MasterRule.TheRule.NotGomiCombo*(korokkes*korokkes+1);
		MasterRule.TheRule.Stuffs += 1;
	//foreach (ContactPoint contact in collision.contacts) {
		//	Debug.DrawRay(contact.point, contact.normal, Color.white);
		//}
		//if (collision.relativeVelocity.magnitude > 2)
		//	audio.Play();
		//print (collision);
		//this.R2d.gravityScale = 0;
	}
}
