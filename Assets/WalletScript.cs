using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WalletScript : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI m_text;

	static int FirstIncreaseMoney = 10;
	static float IncreaseCooldown = 3;

	float m_increaseCooldown;	//�����𑝂₷�N�[���_�E��

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
		//���g�̎����Ă��邨����`��
		m_text.text = m_haveMoney.ToString() + "$";

		//�N�[���_�E���̎��Ԃ����炷
		m_increaseCooldown -= Time.deltaTime;
		//�N�[���_�E�����I���Ƃ����������
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
