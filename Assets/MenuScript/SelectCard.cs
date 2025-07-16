using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectCard : MonoBehaviour
{
	[SerializeField] GameObject characterInfo;
	[SerializeField] CharacterDataBase characterData;

	[SerializeField] GameObject pickUpFrame;
	[SerializeField] GameObject card;

	[SerializeField] private AddDeck addDeck;
	[SerializeField] private ExclusionDeck exclusionDeck;

	int m_cardId;

	GameObject m_frame;

	public void SetAddDeck(AddDeck addDeckArgument)
	{
		addDeck = addDeckArgument;
	}

	public void SetExclusionDeck(ExclusionDeck exclusionDeckArgument)
	{
		exclusionDeck = exclusionDeckArgument;
	}

	void Update()
	{
		//string cardIdText = card.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text;

		//m_cardId = int.Parse(cardIdText);

		if (Input.GetMouseButton(0) && m_frame != null)
		{
			Destroy(m_frame);

			//addDeck.SetCardId(-1);
		}
	}

	public void OnClick()
	{
		SelectCardView();
	}

	private void SelectCardView()
	{
		int childId = transform.GetSiblingIndex();

		m_frame = Instantiate(pickUpFrame, card.transform.position, Quaternion.identity, card.transform);
		Debug.Log("‚¨‚³‚ê‚½");

		if(tag == "HaveCard")
		{
			addDeck.SetCardId(childId);
		}
		if (tag == "Deck")
		{
			exclusionDeck.SetCardId(childId);
		}

		/*
		characterInfo.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = characterData.datas[m_cardId].m_name;
		characterInfo.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = characterData.datas[m_cardId].m_attackDamage.ToString();
		characterInfo.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = characterData.datas[m_cardId].m_hp.ToString();
		characterInfo.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = 
		characterInfo.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = characterData.datas[m_cardId].m_name;
		*/
	}
}
