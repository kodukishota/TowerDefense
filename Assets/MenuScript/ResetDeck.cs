using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResetDeck : MonoBehaviour
{
	[SerializeField] UserInfo userInfo;

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
			{"user_id", userInfo.data.m_id.ToString()}
			});
		yield return StartCoroutine(coroutine);
	}
}
