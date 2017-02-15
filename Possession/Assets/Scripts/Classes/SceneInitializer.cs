using UnityEngine;
using UnityEngine.SceneManagement;
using Possession;

public class SceneInitializer : MonoBehaviour {

    private GameObject rootNode;

    // Use this for initialization
    void Start () {
        Debug.Log("debug : " + IsDebug());
        if (!IsDebug())
        {
            SetRootNode();
            RemoveDebugNode();
            InitializeScientist();
            InitializeCarrier();
        }
    }

    private void SetRootNode()
    {
        GameObject[] rootNodes = GameManager.Instance.GetLoaderScene().GetRootGameObjects();
        foreach (GameObject go in rootNodes)
        {
            if (go.name == "RootNode")
            {
                rootNode = go;
                break;
            }
        }
    }

    private void RemoveDebugNode()
    {
        GameObject go = GameObject.Find("SingleSceneManagement");
        Destroy(go);
    }

    private void InitializeScientist()
    {
        UniqueObjectsHandler uoh = rootNode.transform.Find("Management/UniqueObjectsHandler").GetComponent<UniqueObjectsHandler>();
        GameObject sci = GameObject.Find("/Gameplay/Actors/ScientistInitialPosition");
        Debug.Assert(sci != null, "A scene doesn't have a ScientistInitialPosition object placed in it");
        uoh.SetScientist(sci.transform);
    }

    private void InitializeCarrier()
    {
        UniqueObjectsHandler uoh = rootNode.transform.Find("Management/UniqueObjectsHandler").GetComponent<UniqueObjectsHandler>();
        GameObject carrier = GameObject.Find("/Gameplay/Actors/CarrierInitialPosition");
        Debug.Assert(carrier != null, "A scene doesn't have a CarrierInitialPosition object placed in it");
        uoh.SetCarrierAsActiveZombie(carrier.transform);
    }

    /// <summary>
    /// Returns whether or not the current scene is the Game loader scene
    /// </summary>
    /// <returns>False if the scene is loaded through GameLoaderScene, true if the scene itself is being edited.</returns>
    private bool IsDebug()
    {
        Scene ls = GameManager.Instance.GetLoaderScene();
        // Si la scène est présente, on est dans la GameLoaderScene, donc en jeu
        if (ls.isLoaded)
            return false;

        return true;
    }
}
