using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResetDeck : MonoBehaviour
{
	[SerializeField] TMP_InputField userId;

	public void OnColick()
	{
		StartCoroutine(ResetDeckRequest());
	}

	IEnumerator ResetDeckRequest()
	{
		IEnumerator coroutine = HttpRequest.PostRequest(
			"reset_user_deck.php",
			new Dictionary<string, string>()
			{
			{"user_id", userId.text }
			});
		yield return StartCoroutine(coroutine);
	}
}
