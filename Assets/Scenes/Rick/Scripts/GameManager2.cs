using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  // LoadSceneを使うために必要！！！！！

public class GameManager2 : SingletonMonoBehaviour<GameManager2> {
	public GameObject[] _enemyObj;

	public GameObject playerObject;
	PlayerController player;

	//UI関連
	[SerializeField] private Text limitTimeText;
    [SerializeField] Text timeCount;

	float skillGage = 0;

	//制限時間など
	[SerializeField] int preCount = 5;
	[SerializeField] float limitTime = 180.0f;

	bool[] canSkill = new bool[5];
	public Slider[] _slider;

	bool isPlay = false;
	public bool IsPlay {
		get { return isPlay; }
		set { isPlay = value; }
	}

	protected override void Awake() {
		base.Awake();
	}

	void Start() {
		player = playerObject.GetComponent<PlayerController>();
		InitializePlayer();
		StartCoroutine(Ready());
		foreach(var slider in _slider) {
			slider.value = 1.0f;
		}
		for (int i=0; i<canSkill.Length; i++){
			canSkill[i] = true;
		}
	}

	// Update is called once per frame
	void Update () {

	}

	void InitializePlayer() {
		player.CanMove = false;
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
		isPlay = true;
		player.CanMove = true;
		StartCoroutine(LimitTime());
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

	public bool StartSkillGage(int n) {
		if (canSkill[n]) {
			StartCoroutine(SkillCount(n));
			return true;
		} else {
			return false;
		}
	}
	/*
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
	*/

	IEnumerator SkillCount(int n) {
		canSkill[n] = false;
		float coolTime = 0;
		switch (n) {
			case 0:
				coolTime = 2.0f;
				break;
			case 1:
				coolTime = 3.0f;
				break;
			case 2:
				coolTime = 3.0f;
				break;
			case 3:
				coolTime = 3.0f;
				break;
			case 4:
				coolTime = 3.0f;
				break;
		}
		float skillGage = 0;
		while (skillGage < 1.0) {
			yield return null;
			skillGage += UnityEngine.Time.deltaTime / coolTime;
			_slider[n].value = skillGage;
		}
		canSkill[n] = true;
	}

}
