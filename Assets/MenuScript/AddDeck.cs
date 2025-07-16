using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AddDeck : MonoBehaviour
{
	[SerializeField] GameObject haveCardView;

	[SerializeField] Transform parentDeck;

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
		if(m_cardId >= 0)
		{
			Add();
		}
	}

	void Add()
	{
		haveCardView.transform.GetChild(m_cardId).tag = "Deck";

		haveCardView.transform.GetChild(m_cardId).SetParent(parentDeck);

		Debug.Log("’Ç‰Á‚³‚ê‚½");
	}
}
