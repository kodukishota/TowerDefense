using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[DefaultExecutionOrder(+5)]
public class GetHaveCharacterInfo : MonoBehaviour
{
	[SerializeField] TMP_InputField userId;
	[SerializeField] CardView haveCardView;

	[System.Serializable]
	class Result
	{ 
		public List<int> character_ids;
	}

	public void OnClick()
	{
		Invoke("WaitRequest", 0.5f);
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

		haveCardView.ResetHaveCard();
		haveCardView.AddHaveCard(result.character_ids);
	}

	void WaitRequest()
	{
		StartCoroutine(Request());
	}
}