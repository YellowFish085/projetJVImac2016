using UnityEngine;
using System.Collections;
using Possession;

public class GamePreLoader : MonoBehaviour {

    public string initialScene;

	void Awake () {
		GameManager gm = GameManager.Instance;
        gm.LoadScene(initialScene);
    }
}
