using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackCardView : MonoBehaviour
{
	[SerializeField] GameObject cardPrefab;

	[SerializeField] Transform parent;

	public void Add(List<int> cards,string name,int cost)
	{
		foreach (var id in cards)
		{
			GameObject cardImage = Instantiate(cardPrefab, parent);
			cardImage.transform.GetChild(1).GetComponent<Image>().sprite = LoadCardImage.Load(id);
			cardImage.transform.GetChild(3).GetComponent<Text>().text = name;
			cardImage.transform.GetChild(4).GetComponent<Text>().text = cost.ToString();
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
