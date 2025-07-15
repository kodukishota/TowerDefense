using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HaveCardView : MonoBehaviour
{
	[SerializeField] GameObject cardPrefab;
	[SerializeField] CharacterDataBase characterDataBase;

	[SerializeField] Transform parent;

	List<int> m_useIds;	//もうすでにあるカードID保存用

	public void Add(List<int> cards)
	{
		if(m_useIds != null)
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
			if (!m_useIds.Contains(id))
			{
				TextMeshProUGUI cardName = cardPrefab.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
				TextMeshProUGUI cardCost = cardPrefab.transform.GetChild(4).GetComponent<TextMeshProUGUI>();

				cardName.text = characterDataBase.datas[id - 1].m_name;
				cardCost.text = characterDataBase.datas[id - 1].m_cost.ToString() + "$";

				GameObject cardImage = Instantiate(cardPrefab, parent);
				cardImage.transform.GetChild(1).GetComponent<Image>().sprite = LoadCardImage.Load(id);

				m_useIds.Add(id);
			}				
		}
	}

	public void Reset()
	{
		foreach (Transform child in parent)
		{
			Destroy(child.gameObject);
		}
	}
}
