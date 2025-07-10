using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InstantiateCharacter : MonoBehaviour
{
	[SerializeField] private GameObject m_character;
	[SerializeField] private GameObject m_enemyCastle;
	[SerializeField] private WalletScript m_walletScript;

	[SerializeField] TextMeshProUGUI m_text;

	bool m_onClick;
	[SerializeField] int m_costMoney;

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
		m_text.text = m_costMoney.ToString() + "$";
	}

	//キャラクタの生成
	public void Instantiate(Vector3 position)
	{
		//お金があったら出せる
		if(m_walletScript.GetHaveMoney() >= m_costMoney)
		{
			GameObject character = Instantiate(m_character, position, Quaternion.Euler(0,-90,0));

			CharacterScript characterScript = character.GetComponent<CharacterScript>();

			characterScript.SetEnemyCastle(m_enemyCastle);

			character.tag = "Bule";

			m_walletScript.UseMoney(m_costMoney);

			m_onClick = false;
		}
		//なかったら
		else
		{

		}
	}
}
