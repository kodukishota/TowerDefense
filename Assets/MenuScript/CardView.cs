using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
	[SerializeField] GameObject cardPrefab;
	[SerializeField] CharacterDataBase characterDataBase;
	[SerializeField] DeckCardView deckCardView;

	[SerializeField] Transform haveCardParent;

	[SerializeField] AddDeck addDeck;
	[SerializeField] ExclusionDeck exclusionDeck;

	List<int> m_useIds; //もうすでにあるカードID保存用

	public void AddHaveCard(List<int> cards)
	{
		if (m_useIds != null)
		{
			m_useIds.Clear();
			m_useIds = new List<int>(); 

			if (deckCardView.GetUseIds() != null)
			{
				m_useIds = deckCardView.GetUseIds();
			}
		}
		else
		{
			m_useIds = new List<int>();

			if (deckCardView.GetUseIds() != null)
			{
				m_useIds = deckCardView.GetUseIds();
			}
		}

		foreach (var id in cards)
		{
			if (!m_useIds.Contains(id))
			{
				cardPrefab.transform.GetChild(1).GetComponent<Image>().sprite = LoadCardImage.Load(id);
				TextMeshProUGUI cardName = cardPrefab.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
				TextMeshProUGUI cardCost = cardPrefab.transform.GetChild(4).GetComponent<TextMeshProUGUI>();
				TextMeshProUGUI cardId = cardPrefab.transform.GetChild(5).GetComponent<TextMeshProUGUI>();

				cardName.text = characterDataBase.datas[id - 1].m_name;
				cardCost.text = characterDataBase.datas[id - 1].m_cost.ToString() + "$";
				cardId.text = id.ToString();

				GameObject cardImage = Instantiate(cardPrefab, haveCardParent);

				SelectCard selectCard = cardImage.GetComponent<SelectCard>();

				selectCard.SetAddDeck(addDeck);
				selectCard.SetExclusionDeck(exclusionDeck);

				cardImage.tag = "HaveCard";

				m_useIds.Add(id);
			}				
		}
	}

	public void ResetHaveCard()
	{
		foreach (Transform child in haveCardParent)
		{
			Destroy(child.gameObject);
		}
	}
}
