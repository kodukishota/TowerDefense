using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene: MonoBehaviour
{
	[SerializeField] string SceneName;

	[SerializeField] string DeleteSceneName;

	public void OnClick()
	{
		//SceneManager.UnloadSceneAsync(DeleteSceneName);

		SceneManager.LoadScene(SceneName);
	}
}
