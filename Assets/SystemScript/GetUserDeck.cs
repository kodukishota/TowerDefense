using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GetUserDeck : MonoBehaviour
{
	[SerializeField] TMP_InputField userId;

	[SerializeField] DeckCardView deckCardView;

	[SerializeField] private bool m_inGame;

	[System.Serializable]
	class Result
	{
		public List<int> in_deck_cards;
	}

	void Start()
	{
		
	}

	public void OnClick()
	{
		StartCoroutine(Request());
	}

	public IEnumerator Request()
	{
		IEnumerator coroutine = HttpRequest.PostRequest(
			"get_user_deck.php",
			new Dictionary<string, string>()
			{
				{"user_id", userId.text }
			});
		yield return StartCoroutine(coroutine);
		var result = JsonUtility.FromJson<Result>((string)coroutine.Current);

		result.in_deck_cards.Sort();


		if(!m_inGame)
		{
			deckCardView.ResetDeck();
			deckCardView.AddDeck(result.in_deck_cards);
		}
	}
}