using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeckCardView : MonoBehaviour
{
	[SerializeField] GameObject cardPrefab;
	[SerializeField] CharacterDataBase characterDataBase;

	[SerializeField] Transform deckParent;

	[SerializeField] AddDeck addDeck;
	[SerializeField] ExclusionDeck exclusionDeck;

	[SerializeField] UserDeck userDeck;

	List<int> m_useIds; //もうすでにあるカードID保存用

	public List<int> GetUseIds()
	{
		return m_useIds;
	}

	public void OnClick()
	{
		AddDeck(userDeck.GetInDeckCards());
	}

	public void AddDeck(List<int> cards)
	{
		if (m_useIds != null)
		{
			m_useIds.Clear();
			m_useIds = new List<int>();
		}
		else
		{
			m_useIds = new List<int>();
		}

		foreach (var id in cards)
		{
			cardPrefab.transform.GetChild(1).GetComponent<Image>().sprite = LoadCardImage.Load(id);
			TextMeshProUGUI cardName = cardPrefab.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
			TextMeshProUGUI cardCost = cardPrefab.transform.GetChild(4).GetComponent<TextMeshProUGUI>();
			TextMeshProUGUI cardId = cardPrefab.transform.GetChild(5).GetComponent<TextMeshProUGUI>();

			cardName.text = characterDataBase.datas[id - 1].m_name;
			cardCost.text = characterDataBase.datas[id - 1].m_cost.ToString() + "$";
			cardId.text = id.ToString();

			GameObject cardImage = Instantiate(cardPrefab, deckParent);

			SelectCard selectCard = cardImage.GetComponent<SelectCard>();

			selectCard.SetAddDeck(addDeck);
			selectCard.SetExclusionDeck(exclusionDeck);

			cardImage.tag = "Deck";

			m_useIds.Add(id);
		}
	}

	public void ResetDeck()
	{
		foreach (Transform child in deckParent)
		{
			Destroy(child.gameObject);
		}
	}
}