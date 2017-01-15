using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager2 : SingletonMonoBehaviour<GameManager2> {

	//UI関連
	[SerializeField] private Text limitTimeText;
    [SerializeField] Text timeCount;

	//スキルゲージ
	[SerializeField] Slider slider;
	float skillGage = 0;

	//制限時間など
	[SerializeField] int preCount = 5;
	[SerializeField] float limitTime = 180.0f;

	[SerializeField] GameObject playerPrefab;
	[SerializeField] GameObject player;

	bool isPlay = false;

	protected override void Awake() {
		base.Awake();
	}

	void Start() {
		InitializePlayer();
		StartCoroutine(Ready());
	}

	// Update is called once per frame
	void Update () {

	}

	void InitializePlayer() {
		player = Instantiate(playerPrefab, new Vector3(10,1,1), Quaternion.identity);
		player.GetComponent<PlayerController>().CanMove = false;
		Camera.main.GetComponent<FollowCamera>().LookTarget = player.transform;
	}

	IEnumerator Ready () {
		isPlay = false;
		int count = preCount;
		while (true) {
			if (count <= 0) break;
			TimeCount(count.ToString());
			count -= 1;
			yield return new WaitForSeconds(1);
		}
		TimeCount("GO!");
		GameStart();
	}


	IEnumerator LimitTime () {
		float count = limitTime;
		while (isPlay) {
			limitTimeText.text = ((int)count).ToString();
			yield return null;
			count -= Time.deltaTime;
			if (count < 0 ) break;
		}
		GameEnd();
	}

	void GameStart () {
		player.GetComponent<PlayerController>().CanMove = true;
		isPlay = true;
		StartCoroutine(LimitTime());
		StartCoroutine(SkillCount());
	}

	void GameEnd () {

	}

	public void TimeCount (string count) {
        timeCount.transform.parent.gameObject.SetActive(true);
        timeCount.text = count;
        iTween.ScaleFrom(timeCount.gameObject, iTween.Hash(
				"x", 0,
				"y", 0,
				"z", 0,
                "time", 1,
				"oncomplete", "CloseTimeCount",
				"oncompletetarget", this.gameObject
			));
    }

    void CloseTimeCount() {
        timeCount.text = "";
        timeCount.transform.parent.gameObject.SetActive(false);
    }

	IEnumerator SkillCount() {
		float coolTime = 10.0f; //現在選択しているスキルから取得する
		float time = 0;
		skillGage = 0;
		while (skillGage < 1.0) {
			yield return null;
			skillGage += Time.deltaTime / coolTime;
			slider.value = skillGage;
		}
	}

}
