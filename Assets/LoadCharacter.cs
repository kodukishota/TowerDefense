using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LoadCharacter : MonoBehaviour
{
	[SerializeField] CharacterScript characterScript;

	class Result
	{
		public string name;
		public int hp;
		public int atk;
		public int cost;
	}

	public IEnumerator Request(int characterId)
	{
		IEnumerator coroutine = HttpRequest.PostRequest(
			"load_character.php",
			new Dictionary<string, string>()
			{
				{"character_id", characterId.ToString()}
			});
		yield return StartCoroutine(coroutine);
		var result = JsonUtility.FromJson<Result>((string)coroutine.Current);

		characterScript.SetStatus(result.hp, result.atk, result.cost);
	}
}
