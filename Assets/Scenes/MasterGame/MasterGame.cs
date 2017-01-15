using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // LoadSceneを使うために必要！！！！！
using UnityEngine.UI;

public class MasterGame : MonoBehaviour {

	public GameObject[] _enemyObj;

	GameObject _gameObj;
	GameManager _gameMng;
	GameObject _scoreText;
	GameObject _timeText;
	float _timeleft;

	public GameObject GameObj { get { return this._gameObj; } set { this._gameObj = value; } }

	int _count;

	void Awake()
	{
		Global.SetButtonManager();
		Global.SetGameManager();
	}

	void Start()
	{
		Fader.instance.BlackIn();
		this.SetObject();
		_count = 0;
	}

	void Update()
	{
		_timeleft -= Time.deltaTime;
		if (_timeleft <= 0.0)
		{
			_timeleft = 1.0f;

			this._gameMng.Time -= 1;
		}

		int enemyCnt = (int)((60 - this._gameMng.Time) / 4);

		_count++;
		if (_count > 60 * 3)
		{
			_count = 0;
			for (int i = 0; i < enemyCnt; i++)
			{
				int index = Random.Range(0, _enemyObj.Length);
				GameObject enemy = GameObjectUtils.Clone(_enemyObj[index]);
				enemy.transform.position = new Vector3(0, 5, 0);
			}
		}

		if (this._gameMng.Time < 0)
		{
			this._gameMng.Time = 0;
			Fader.instance.BlackOut();              // フェードアウト
			StartCoroutine(DelayMethod(1.2f));      // 1.2秒後に実行する
		}

		this._scoreText.GetComponent<Text>().text = "SCORE : " + this._gameMng.Score.ToString();
		this._timeText.GetComponent<Text>().text = "TIME : " + this._gameMng.Time.ToString();
	}

	private IEnumerator DelayMethod(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);  // waitTime後に実行する
		SceneManager.LoadScene("Result");             // シーン切り替え
	}

	void SetObject()
	{
		this._gameObj = transform.gameObject;
		this._gameMng = GameManager.Instance;
		this._scoreText = transform.Find("Canvas/Top_UI/ScoreText").gameObject;
		this._timeText = transform.Find("Canvas/Top_UI/LimitaTimeText").gameObject;
		this._gameMng.Score = 0;
		this._gameMng.Time = 2;
	}

}
