using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InstantiateCharacter : MonoBehaviour
{
	[SerializeField] CharacterDataBase characterDataBase;

	[SerializeField] TextMeshProUGUI m_text;

	[SerializeField] private WalletScript walletScript;
	[SerializeField] private GameObject m_character;
	[SerializeField] private GameObject m_enemyCastle;

	bool m_onClick;

	int m_characterId;

	public void SetCharacterId(int id)
	{
		m_characterId = id;
	}

	public void SetGameObject(GameObject character,GameObject emenyCastel)
	{
		m_character = character;
		m_enemyCastle = emenyCastel;
	}

	public void SetWallet(WalletScript wallet)
	{
		walletScript = wallet;
	}

	public bool GetOnClick()
	{
		return m_onClick;
	}

	public void ResetOnClick()
	{
		m_onClick = false;
	}

	public void OnClick()
	{
		m_onClick = true;
	}

	private void Start()
	{
		m_text.text = characterDataBase.datas[m_characterId - 1].m_cost + "$";
	}

	private void Update()
	{
		if (Input.GetMouseButton(0))
		{
			m_onClick = false;
		}
	}

	//キャラクタの生成
	public void Instantiate(Vector3 position)
	{
		//お金があったら出せる
		if(walletScript.GetHaveMoney() >= characterDataBase.datas[m_characterId - 1].m_cost)
		{
			GameObject character = Instantiate(m_character, position, Quaternion.Euler(0,-90,0));

			CharacterScript characterScript = character.GetComponent<CharacterScript>();

			characterScript.SetEnemyCastle(m_enemyCastle);
			characterScript.SetId(m_characterId - 1);

			character.tag = "Blue";

			walletScript.UseMoney(characterDataBase.datas[m_characterId - 1].m_cost);

			m_onClick = false;
		}
		//なかったらメッセージを表示する
		else
		{

		}
	}
}
