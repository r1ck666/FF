using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	static GameManager _instance;
	public static GameManager Instance
	{
		get
		{
			if (_instance == null)
			{
				GameObject obj = new GameObject("GameManager");
				_instance = obj.AddComponent<GameManager>();
			}
			return _instance;
		}
	}

	int _score;
	public int Score { get { return _score; } set { _score = value; } }

	int _time;
	public int Time { get { return _time; } set { _time = value; } }
}
