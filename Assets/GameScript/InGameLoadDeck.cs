using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameLoadDeck : MonoBehaviour
{
	[SerializeField] UserInfo userInfo;
	
	[SerializeField] InGameCardView inGameCardView;

	[System.Serializable]
	class Result
	{
		public List<int> in_deck_cards;
	}

	void Start()
	{
		StartCoroutine(Request());
	}

	public IEnumerator Request()
	{
		IEnumerator coroutine = HttpRequest.PostRequest(
			"get_user_deck.php",
			new Dictionary<string, string>()
			{
				{"user_id", userInfo.data.m_id.ToString() }
			});
		yield return StartCoroutine(coroutine);
		var result = JsonUtility.FromJson<Result>((string)coroutine.Current);

		result.in_deck_cards.Sort();
		
		inGameCardView.ResetDeck();
		inGameCardView.AddDeck(result.in_deck_cards);
		
	}
}
