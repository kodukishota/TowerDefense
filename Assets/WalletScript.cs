using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WalletScript : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI m_text;

	static int FirstIncreaseMoney = 10;
	static float IncreaseCooldown = 3;

	float m_increaseCooldown;	//お金を増やすクールダウン

	int m_increaseMoney;	
	int m_haveMoney;

	private void Start()
	{
		m_increaseCooldown = IncreaseCooldown;
		m_increaseMoney = FirstIncreaseMoney;
		m_haveMoney = 0;
	}

	void Update()
	{
		//自身の持っているお金を描画
		m_text.text = m_haveMoney.ToString() + "$";

		//クールダウンの時間を減らす
		m_increaseCooldown -= Time.deltaTime;
		//クールダウンを終わるとお金をいれる
		if(m_increaseCooldown <= 0)
		{
			m_haveMoney += m_increaseMoney;

			m_increaseCooldown = IncreaseCooldown;
		}
	}

	public void UseMoney(int useMoney)
	{
		m_haveMoney -= useMoney;
	}
}
