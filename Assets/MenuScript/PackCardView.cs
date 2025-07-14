using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PackCardView : MonoBehaviour
{
	[SerializeField] GameObject cardPrefab;
	[SerializeField] CharacterDataBase characterDataBase;

	[SerializeField] Transform parent;

	public void Add(List<int> cards)
	{
		foreach (var id in cards)
		{
			TextMeshProUGUI cardName = cardPrefab.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
			TextMeshProUGUI cardCost = cardPrefab.transform.GetChild(4).GetComponent<TextMeshProUGUI>();

			cardName.text = characterDataBase.datas[id - 1].m_name;
			cardCost.text = characterDataBase.datas[id - 1].m_cost.ToString() + "$";

			GameObject cardImage = Instantiate(cardPrefab, parent);
			cardImage.transform.GetChild(1).GetComponent<Image>().sprite = LoadCardImage.Load(id);
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
