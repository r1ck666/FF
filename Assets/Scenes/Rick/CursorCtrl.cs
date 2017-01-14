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

	void SkillStart () {

		skillPosition = new Vector2 (cursorPosition.x - skill.FixedPosition.x, cursorPosition.y - skill.FixedPosition.y);
		Instantiate(skill, skillPosition, Quaternion.identity);

	}

	void ChangeSkill(int n) {
		skillObject = skillList[n];
		skill = skillObject.GetComponent<Skill>();
	}

}
