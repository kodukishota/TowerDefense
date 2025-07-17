using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadStage : MonoBehaviour
{
	[SerializeField] string SceneName;

	[SerializeField] string DeleteSceneName;

	public void OnClick()
	{
		SceneManager.LoadScene(SceneName.ToString(), LoadSceneMode.Additive);

		SceneManager.UnloadSceneAsync(DeleteSceneName);
	}
}
