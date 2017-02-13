using UnityEngine;
using System;
using System.Collections;
using UnityEngine.SceneManagement;
using Possession;

namespace Possession
{

    public class ColliderLoaderScene : MonoBehaviour
    {
        public GameObject levelObject;

        public string tagToVerify = "Scientific";

        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.tag == tagToVerify)
            {
                GameManager gm = GameManager.Instance;
                var levelComponent = levelObject.GetComponent<Level>();
                gm.SetCurrentLevel(levelComponent.GetNameCurrentName());
                //gm.GetSaveManager().Save(); // TODO : uncommented after merge save branch.

                string nextNameScene = levelComponent.nextLevel;
                string previousNameScene = levelComponent.previousLevel;

                // Ask to load the necessary scene.
                this.AskToLoad(nextNameScene, previousNameScene);

                // Retrieve and ask to unload the unnecessary scene
                Scene nextScene = gm.GetScene(nextNameScene);
                Scene previousScene = gm.GetScene(previousNameScene);
                Level nextLevel = this.GetLevelComponent(nextScene);
                Level previousLevel = this.GetLevelComponent(previousScene);
                string nNext = (nextLevel != null) ? nextLevel.nextLevel : "";
                string pPrevious = (previousLevel != null) ? previousLevel.previousLevel : "";

                this.AskToUnload(nNext, pPrevious);
            }
        }

        private Level GetLevelComponent(Scene scene)
        {
            if (scene.isLoaded)
            {
                var gameObjects = scene.GetRootGameObjects();
                foreach (var gameObject in gameObjects)
                {
                    var temp = gameObject.GetComponents<Level>();
                    if (temp.Length != 0)
                        return temp[0];
                }
            }
            return null;
        }

        private void AskToLoad(string next, string previous)
        {
            if (!String.IsNullOrEmpty(next))
            {
                GameManager.Instance.LoadScene(next);
            }
            if (!String.IsNullOrEmpty(previous))
            {
                GameManager.Instance.LoadScene(previous);
            }
        }
        private void AskToUnload(string nextNext, string previousPrevious)
        {
            if (!String.IsNullOrEmpty(nextNext))
            {
                GameManager.Instance.UnloadScene(nextNext);
            }
            if (!String.IsNullOrEmpty(previousPrevious))
            {
                GameManager.Instance.UnloadScene(previousPrevious);
            }
        }
    }
}