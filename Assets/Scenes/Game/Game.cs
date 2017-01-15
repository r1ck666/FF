﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // LoadSceneを使うために必要！！！！！
using UnityEngine.UI;

public class Game : MonoBehaviour {

	public GameObject[] _enemyObj;

	GameObject _gameObj;
	GameManager _gameMng;
	GameObject _scoreText;
	GameObject _timeText;
	float _timeleft;

	public GameObject GameObj { get { return this._gameObj; } set { this._gameObj = value; } }

	int _count;

	void Awake() {
		Global.SetButtonManager();
		Global.SetGameManager();
	}

	void Start () {
		Fader.instance.BlackIn();
		this.SetObject();
		this.SetAction();
		_count = 0;
	}

	void Update () {
		_timeleft -= Time.deltaTime;
		if (_timeleft <= 0.0)
		{
			_timeleft = 1.0f;

			this._gameMng.Time -= 1;
		}

		int div = (int)((50 - this._gameMng.Time) / 10);

		_count++;
		if(_count > 60 * 3) {
			_count = 0;
			for (int i = 0; i < div; i++) {
				int index = Random.Range(0, _enemyObj.Length);
				GameObject enemy = GameObjectUtils.Clone(_enemyObj[index]);
				enemy.transform.position = new Vector3(0, 5, 0);
			}
		}

		if(this._gameMng.Time < 0) {
			this._gameMng.Time = 0;
			Fader.instance.BlackOut();              // フェードアウト
			StartCoroutine(DelayMethod(1.2f));      // 1.2秒後に実行する
		}

		this._scoreText.GetComponent<Text>().text = "SCORE : " + this._gameMng.Score.ToString();
		this._timeText.GetComponent<Text>().text = "TIME : " + this._gameMng.Time.ToString();
	}

	private IEnumerator DelayMethod(float waitTime) {
		yield return new WaitForSeconds(waitTime);  // waitTime後に実行する
		SceneManager.LoadScene("Result");             // シーン切り替え
	}

	void SetAction() {
		GameObject btn = transform.Find("Canvas/ResultButton").gameObject;
		Global.ButtonMng.SetAction(btn, () => {
			Fader.instance.BlackOut();              // フェードアウト
			StartCoroutine(DelayMethod(1.2f));      // 1.2秒後に実行する
		});
	}

	void SetObject() {
		this._gameObj = transform.gameObject;
		this._gameMng = GameManager.Instance;
		this._scoreText = transform.Find("Canvas/ScoreText").gameObject;
		this._timeText = transform.Find("Canvas/TimeText").gameObject;
		this._gameMng.Score = 0;
		this._gameMng.Time = 60;
	}
}
