using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class SaveManager : MonoBehaviour {

	// TODO: Create a SaveManager constructor with good values (currentLevel, player, etc)

	// Temporary value for game progress
	public Game game = new Game (0, "Jean-Jacques", new int[] { 3, 2, 1, 0 } );

	private string backup;
	private string backupFilePath = "/backup.json";

	public string GetBackUp()
	{
		return backup;
	}

	public void write () {
		JsonData backupJSON = JsonMapper.ToJson(game);
		File.WriteAllText (Application.dataPath + backupFilePath, backupJSON.ToString());
		Debug.Log ("Backup file write to " + Application.dataPath);
	}

	public void read () {
		string filePath = Application.dataPath + backupFilePath;
		if (File.Exists (filePath)) {
			backup = File.ReadAllText (filePath);
		} else
		{
			Debug.Log ("No backup file");
		}
	}
}


// Temporary class to test JSONManager

public class Game {

	public int id;
	public string name;
	public int[] values;

	public Game (int id, string name, int[] values) {
		this.id = id;
		this.name = name;
		this.values = values;
	}

}