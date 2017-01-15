using UnityEngine;
using System.Collections;

public class CursorCtrl : MonoBehaviour {

	//マウスの位置座標
	//Vector3 position;
	//スクリーン座標をワールド座標に変換した位置座標
	public Vector3 cursorPosition;
	//スキルの格納
	[SerializeField] GameObject skillObject;
	[SerializeField] GameObject[] skillList;

	//選択中のスキル番号
	int skillNum = 0;
	//スキルの発動位置
	Vector3 skillPosition;
	Skill skill;

	void Start ()
	{
		//カーソルをゲーム画面からはみ出さないようにする
		Cursor.lockState = CursorLockMode.Confined;
		//OSカーソルの非表示
		Cursor.visible = false;
		//スキルをキャッシュ
		skill = skillObject.GetComponent<Skill>();
	}

	void Update ()
	{
		//Vector3でマウスの位置座標を取得
		Vector3 position = Input.mousePosition;
		//Z軸修正
		position.z = 5.0f;
		//マウス位置座標をスクリーン座標からワールド座標に変換する
		cursorPosition = Camera.main.ScreenToWorldPoint(position);
		//ワールド座標に変換されたマウス座標を代入
		transform.position = cursorPosition;

		if (GameManager2.Instance.IsPlay) {
			// スキル発動
			if (Input.GetButtonDown("Fire1")){
				SkillStart();
			}

			// スキル変更
			if (Input.GetButtonDown("Fire2")) {
				skillNum++;
				if (skillNum == skillList.Length) {
					skillNum = 0;
				}
				ChangeSkill(skillNum);
			}
		}
	}

	void SkillStart () {
		if (GameManager2.Instance.StartSkillGage(skillNum)){
			skillPosition = new Vector2 (cursorPosition.x - skill.FixedPosition.x, cursorPosition.y - skill.FixedPosition.y);
			Instantiate(skill, skillPosition, Quaternion.identity);
		}
	}

	void ChangeSkill(int n) {
		skillObject = skillList[n];
		skill = skillObject.GetComponent<Skill>();
	}

	/*
	void OnTriggerStay2D(Collider2D coll)
	{
		Debug.Log("カーソルがエネミーに当たりました");

		//レイヤー名を取得
		string layerName = LayerMask.LayerToName(coll.gameObject.layer);
		if (layerName == "Enemy")
		{
			//if (Input.GetButtonDown("Fire1"))
			//{
				Debug.Log("押したぜ");

				float x = Random.Range(0.0f, 10.0f);
				float y = Random.Range(0.0f, 10.0f);
				float z = Random.Range(0.0f, 10.0f);
				Vector3 diff = new Vector3(x, y, z);
				diff = diff.normalized;
				coll.gameObject.GetComponent<Rigidbody2D>().AddForce(diff * 20f, ForceMode2D.Impulse);
			//}
		}

	}
	*/

}
