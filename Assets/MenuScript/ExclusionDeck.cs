using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExclusionDeck : MonoBehaviour
{
	[SerializeField] GameObject deckCardView;

	[SerializeField] Transform parentHaveCard;

	int m_cardId = -1;

	public void SetCardId(int cardId)
	{
		m_cardId = cardId;
	}

	public int GetCardId()
	{
		return m_cardId;
	}

	public void OnClick()
	{
		if (m_cardId >= 0)
		{
			Exclusion();
		}
	}
	void Exclusion()
	{
		deckCardView.transform.GetChild(m_cardId).tag = "HaveCard";

		deckCardView.transform.GetChild(m_cardId).SetParent(parentHaveCard);

		Debug.Log("–ß‚³‚ê‚½");
	}
}
