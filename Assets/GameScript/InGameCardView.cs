using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameCardView : MonoBehaviour
{
	[SerializeField] GameObject cardPrefab;
	[SerializeField] CharacterDataBase characterDataBase;
	[SerializeField] private GameObject[] character;
	[SerializeField] private GameObject enemyCastle;
	[SerializeField] private WalletScript walletScript;

	[SerializeField] Transform deckParent;

	[SerializeField] PlayerScript playerScript;


	public void AddDeck(List<int> cards)
	{
		foreach (var id in cards)
		{
			cardPrefab.transform.GetChild(0).GetComponent<Image>().sprite = LoadCardImage.Load(id - 1);

			GameObject card = Instantiate(cardPrefab, deckParent);

			InstantiateCharacter instantiateCharacter = card.GetComponent<InstantiateCharacter>();

			instantiateCharacter.SetCharacterId(id - 1);
			instantiateCharacter.SetGameObject(character[id - 2], enemyCastle);
			instantiateCharacter.SetWallet(walletScript);
			playerScript.SetInstantiateCharacters(instantiateCharacter);
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
