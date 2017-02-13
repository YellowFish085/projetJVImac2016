using UnityEngine;
using System.Collections;
using Possession;

public class QuitApplication : MonoBehaviour {

	public void Quit()
	{
		GameManager.Instance.GetSaveManager().Save();

		//If we are running in a standalone build of the game
		#if UNITY_STANDALONE
			//Quit the application
			Application.Quit();
		#endif

		//If we are running in the editor
		#if UNITY_EDITOR
			//Stop playing the scene
			UnityEditor.EditorApplication.isPlaying = false;
		#endif
	}
}
