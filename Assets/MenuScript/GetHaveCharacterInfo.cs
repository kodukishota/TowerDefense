using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GetHaveCharacterInfo : MonoBehaviour
{
	[SerializeField] TMP_InputField userId;
	[SerializeField] HaveCardView haveCardView;

	[SerializeField] GameObject addUserScreen;

	[System.Serializable]
	class Result
	{ 
		public List<int> character_ids;
	}

	public void OnClick()
	{
		StartCoroutine(Request());
	}

	IEnumerator Request()
	{
		IEnumerator coroutine = HttpRequest.PostRequest(
			"have_character_info.php",
			new Dictionary<string, string>()
			{
				{"user_id", userId.text }
			});
		yield return StartCoroutine(coroutine);
		var result = JsonUtility.FromJson<Result>((string)coroutine.Current);

		result.character_ids.Sort();

		Debug.Log(result.character_ids);

		haveCardView.Reset();
		haveCardView.Add(result.character_ids);
	}
}
