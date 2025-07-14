using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCharacterData : MonoBehaviour
{
	[SerializeField] CharacterDataBase characterDataBase;

	[SerializeField] int characterIndex;

	class Result
	{
		public string name;
		public int hp;
		public int atk;
		public int speed;
		public int cost;
	}

	private void Start()
	{
		for (int i = 1; i <= characterIndex; i++)
		{
			StartCoroutine(Request(i));
		}
	}

	public IEnumerator Request(int characterId)
	{
		IEnumerator coroutine = HttpRequest.PostRequest(
			"load_character.php",
			new Dictionary<string, string>()
			{
				{"character_id", characterId.ToString() }
			});
		yield return StartCoroutine(coroutine);
		var result = JsonUtility.FromJson<Result>((string)coroutine.Current);

		characterDataBase.datas[characterId - 1].SetStatus(
			result.name,
			result.hp,
			result.atk,
			result.speed,
			result.cost);
	}
}