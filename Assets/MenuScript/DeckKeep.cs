using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeckKeep : MonoBehaviour
{
	[SerializeField] GameObject deckCard;

	[SerializeField] TMP_InputField userId;

	[SerializeField] GameObject compositionScreen;

	public void OnColick()
	{
		Invoke("WaitReset", 0.5f);
	}

	IEnumerator AddDeckRequest(string characterId)
	{
		IEnumerator coroutine = HttpRequest.PostRequest(
			"add_deck.php",
			new Dictionary<string, string>()
			{
				{"user_id", userId.text },
				{"character_id",characterId}
			});
		yield return StartCoroutine(coroutine);
	}

	void WaitReset()
	{
		for (int i = 0; i < deckCard.transform.childCount; i++)
		{
			GameObject card = deckCard.transform.GetChild(i).gameObject;

			string cardIdText = card.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text;

			StartCoroutine(AddDeckRequest(cardIdText));
		}

		compositionScreen.SetActive(false);
	}
}
