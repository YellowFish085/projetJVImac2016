using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class WriteJson : MonoBehaviour {

	public Character player = new Character (0);

	JsonData playerJSON;

	// Use this for initialization
	void Start () {
		playerJSON = JsonMapper.ToJson(player);
		Debug.Log (playerJSON);
		File.WriteAllText (Application.dataPath + "/Player.json", playerJSON.ToString());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

public class Character {

	public int id;

	public Character(int id) {
		this.id = id;
	}

}