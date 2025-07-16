using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeckKeep : MonoBehaviour
{
	[SerializeField] GameObject deckCard;

	[SerializeField] TMP_InputField userId;

	public void OnColick()
	{
		for (int i = 0; i < deckCard.transform.childCount; i++)
		{
			GameObject card = deckCard.transform.GetChild(i).gameObject;

			string cardText = card.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text;

			StartCoroutine(Request(cardText));
		}
	}

	IEnumerator Request(string characterId)
	{
		IEnumerator coroutine = HttpRequest.PostRequest(
			"add_deck.php",
			new Dictionary<string, string>()
			{
				{"user_id", userId.text },
				{"character_id",characterId}
			});
		yield return StartCoroutine(coroutine);
		//var result = JsonUtility.FromJson<Result>((string)coroutine.Current);

		//result.character_ids.Sort();

	}
}
