using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	static int hashSpeed = Animator.StringToHash ("Speed");
	static int hashFallSpeed = Animator.StringToHash ("FallSpeed");
	static int hashGroundDistance = Animator.StringToHash ("GroundDistance");
	static int hashIsCrouch = Animator.StringToHash ("IsCrouch");

	//static int hashDamage = Animator.StringToHash ("Damage");
	//static int hashIsDead = Animator.StringToHash ("IsDead");

	[SerializeField, HideInInspector] Animator animator;
	[SerializeField, HideInInspector] SpriteRenderer spriteRenderer;
	[SerializeField, HideInInspector] Rigidbody2D rig2d;

	[SerializeField] private float characterHeightOffset = 0.2f;
	[SerializeField] LayerMask groundMask;

	// 移動スピード
    [SerializeField] float speed = 5;
	[SerializeField] float jumpPower = 10.0f;

	bool canMove = true;
	public bool CanMove {
		get { return canMove; }
		set { canMove = value; }
	}
	bool canJump = true;

	float x;
	[SerializeField] float smashTime = 1.0f;
	// Debug用
	[SerializeField] float distance;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer> ();
		rig2d = GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	void Update () {

		if (canMove) {
			// 右・左
			x = Input.GetAxisRaw ("Horizontal");
			rig2d.velocity = new Vector2 (x * speed, rig2d.velocity.y);
		}

		bool isDown = Input.GetAxisRaw ("Vertical") < 0;

		var distanceFromGround = Physics2D.Raycast (transform.position, Vector3.down, 1, groundMask);
		distance = distanceFromGround.distance - characterHeightOffset;

		if ( distanceFromGround.distance == 0) {
			canJump = false;
		} else {
			distance = distanceFromGround.distance - characterHeightOffset;
			if ( distance < 0.05f ) {
				canJump = true;
			} else {
				canJump = false;
			}
		}

		if (Input.GetButtonDown ("Jump") && canJump && canMove) {
			rig2d.velocity = new Vector2 (rig2d.velocity.x, jumpPower);
		}

		// update animator parameters
		animator.SetBool (hashIsCrouch, isDown);
		animator.SetFloat (hashGroundDistance, distanceFromGround.distance == 0 ? 99 : distanceFromGround.distance - characterHeightOffset);
		animator.SetFloat (hashFallSpeed, rig2d.velocity.y);
		animator.SetFloat (hashSpeed, Mathf.Abs (x));

		//if( Input.GetKeyDown(KeyCode.Z) ){  animator.SetTrigger(hashAttack1); }
		//if( Input.GetKeyDown(KeyCode.X) ){  animator.SetTrigger(hashAttack2); }
		//if( Input.GetKeyDown(KeyCode.C) ){  animator.SetTrigger(hashAttack3); }

		// flip sprite
		if (x != 0)
			spriteRenderer.flipX = x < 0;

		// 落ちたな(確信)
		if (transform.position.y <= -0.5f) {
			Vector3 pos = transform.position;
			pos.x = 0.5f;
			pos.y = 2f;
			transform.position = pos;
		}
	}

	public int direction = 0;

	//敵に当たった時に死ぬ
	void OnTriggerEnter2D(Collider2D coll) {
		//レイヤー名を取得
	  string layerName = LayerMask.LayerToName(coll.gameObject.layer);
		if (layerName == "Enemy"){
			float x = Random.Range(0.0f, vec.x);
			float y = Random.Range(0.0f, vec.y);
			float z = Random.Range(0.0f, vec.z);
			Vector3 diff = new Vector3(x, y, z);
			diff = diff.normalized;

			diff = vec;
			//this.gameObject.GetComponent<Rigidbody2D>().AddForce(diff * test, ForceMode2D.Impulse);
			if ( canMove ) {
				if (coll.gameObject.transform.localScale.x > 0) {
					direction = -1;
				} else {
					direction = 1;
				}
				StartCoroutine(Smashed(direction));
			}
			Debug.Log(diff + "hoge");

		}
	}

	public float test = 20f;
	public Vector3 vec;

	IEnumerator Smashed(int dic) {
		canMove = false;
		float time = 0;
		float x1 = 0;
		float y1 = 0;
		float z1 = 0;

		// xを0に
		var vel = rig2d.velocity;
		vel.x = 0.0f;
		vel.y = 5.0f;
		rig2d.velocity = vel;

		while (time < smashTime) {
			Debug.Log("smashed");
			yield return null;
			time += Time.deltaTime;
			x1 += Time.deltaTime;
			y1 += Time.deltaTime;
			z1 += Time.deltaTime;
			if (time < 0.5f) {
				this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(dic * x1 * test, y1 * test , z1 * test), ForceMode2D.Impulse);
			}
		}
		canMove = true;
	}

}
