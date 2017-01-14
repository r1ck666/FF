using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class StageManager : SingletonMonoBehaviour<StageManager> {


	// Stageが格納されているオブジェクト
	GameObject stageObject;
	// BlockのPrefabが格納されているリスト
	List<GameObject> blockList;

	uint[,] blocks;
	public uint[,] Blocks { get { return blocks; } }
	GameObject[,] blocksObject;
	public GameObject[,] BlocksObject { get { return blocksObject; }}
	uint x;
	public uint X { get { return x; }}
	uint y;
	public uint Y { get { return Y; }}

	protected override void Awake() {
		base.Awake();
		string stageData = ResourcesLoad ("TestMap");
		MapLoad(stageData);
		MapCreate();
	}


	string ResourcesLoad (string mapName)
	{
		// BlockPrefabの読み込み
		BlockLoad();

		//マップデータを読み込み
		return Resources.Load <TextAsset> ("MapData/"+mapName).text;
	}

	void BlockLoad ()
	{
		GameObject[] blockArray  = Resources.LoadAll<GameObject>("Blocks/");
		blockList = new List<GameObject>();
		blockList.AddRange(blockArray);
	}

	void MapLoad (string stageData)
	{
		// 配列に格納
		var lines = stageData.Split(new[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries)
			.Where(line => line.IndexOf ("#") == -1) // コメント行を取り除く
			.ToArray();

		// ステージ名
		string stageName = lines[0];

		// ステージサイズの読み込み
		var stageSize = lines[1].Split(' ').Select(size => uint.Parse(size)).ToArray();
		x = stageSize[0];
		y = stageSize[1];

		blocks = new uint[x, y];

		// ステージデータの読み込み
		for (uint j = 0; j < y; j++) {
				var xLine = lines[j + 2]
							.Split(new[] {' '}, System.StringSplitOptions.RemoveEmptyEntries)
							.Select(l => uint.Parse(l)).ToArray();
				for (uint i = 0; i < x; i++) {
					blocks[i, y-j-1] = xLine[i];
				}
		}
		stageData = null;	//メモリ解放
	}

	void MapCreate()
	{
		stageObject = new GameObject("Stage");
		blocksObject = new GameObject[x,y];
		for (uint j = 0; j < y; j++) {
			for (uint i = 0; i < x; i++) {
				if (blocks[i,j] == 0) continue;
				GameObject go = Instantiate (blockList[((int)blocks[i, j])-1], new Vector3(i, j, 0), Quaternion.identity);
				go.transform.parent = stageObject.transform;
				blocksObject[i, j] = go;
			}
		}
	}


}
