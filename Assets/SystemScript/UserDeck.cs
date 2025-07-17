using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserDeck : MonoBehaviour
{
	List<int> m_inDeckCards;

	public List<int> GetInDeckCards()
	{
		return m_inDeckCards;
	}

	public void AddDeckInfo(List<int> inDeckCards)
	{
		m_inDeckCards.Clear();

		m_inDeckCards = inDeckCards;
	}
}
