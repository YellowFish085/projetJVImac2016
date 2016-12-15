using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Possession {

	public class GameManager : MonoBehaviour
	{
		enum State {PAUSE, IN_GAME, MAIN_MENU};

		private static GameManager _instance;

		private static object _lock = new object();

		private Hashtable levelsList = new Hashtable();

		public static GameManager Instance
		{
			get
			{
				if (applicationIsQuitting) {
					Debug.LogWarning("[Singleton] Instance '"+ typeof(GameManager) +
						"' already destroyed on application quit." +
						" Won't create again - returning null.");
					return null;
				}

				lock(_lock)
				{
					if (_instance == null)
					{
						_instance = (GameManager) FindObjectOfType(typeof(GameManager));

						if ( FindObjectsOfType(typeof(GameManager)).Length > 1 )
						{
							Debug.LogError("[Singleton] Something went really wrong " +
								" - there should never be more than 1 singleton!" +
								" Reopening the scene might fix it.");
							return _instance;
						}

						if (_instance == null)
						{
							GameObject singleton = new GameObject();
							_instance = singleton.AddComponent<GameManager>();
							singleton.name = "(singleton) "+ typeof(GameManager).ToString();

							DontDestroyOnLoad(singleton);

							_instance.Init ();

							Debug.Log("[Singleton] An instance of " + typeof(GameManager) + 
								" is needed in the scene, so '" + singleton +
								"' was created with DontDestroyOnLoad.");
						} else {
							Debug.Log("[Singleton] Using instance already created: " +
								_instance.gameObject.name);
						}
					}
					return _instance;
				}
			}
		}

		private static bool applicationIsQuitting = false;
		/// <summary>
		/// When Unity quits, it destroys objects in a random order.
		/// In principle, a Singleton is only destroyed when application quits.
		/// If any script calls Instance after it have been destroyed, 
		///   it will create a buggy ghost object that will stay on the Editor scene
		///   even after stopping playing the Application. Really bad!
		/// So, this was made to be sure we're not creating that buggy ghost object.
		/// </summary>
		public void OnDestroy () {
			applicationIsQuitting = true;
		}

		private void Init () {
			_instance.RetrieveLevels ();

		}

		private void RetrieveLevels () {
			var numScenes = SceneManager.sceneCount;

			for (int i=0; i < numScenes; ++i)
			{
				levelsList.Add(SceneManager.GetSceneAt(i).name, SceneManager.GetSceneAt(i));
			}

			Debug.Log("List levels size : " + levelsList.Count);
		}
	}
} // namespace Possession
