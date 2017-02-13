using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Possession
{
	public class SaveManager  {

		public string currentLevelName;

		public SaveManager () {
			
			Debug.Log (GetCurrentLevelName ());
		}

		public string GetCurrentLevelName()
		{
			return currentLevelName;
		}

		public void Save () {
			currentLevelName = GameManager.Instance.GetCurrentLevel ().name;
			File.WriteAllText (Application.dataPath + "/backup.json", JsonUtility.ToJson(this));
			Debug.Log ("Backup file write to " + Application.dataPath);
		}

		public void Load () {
			string filePath = Application.dataPath + "/backup.json";
			if (File.Exists (filePath)) {
				JsonUtility.FromJson<SaveManager>(filePath);
			} else
			{
				Debug.Log ("No backup file");
			}
		}
	}
}