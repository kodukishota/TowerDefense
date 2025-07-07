using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class CharacterScript : MonoBehaviour
{
	[SerializeField] private Animator anim;
	//static int MaxHp = 10;				//�ő�̗�

	[SerializeField] int m_hp;              //�̗�

	bool m_isDeth;

	void Start()
	{
		m_isDeth = false;
	}

	void Update()
	{
		//���S�����Ƃ�
		if (m_hp <= 0)
		{
			gameObject.tag = "Carcass";

			if (!m_isDeth)
			{
				anim.SetTrigger("Deth");

				m_isDeth = true;
			}
			Invoke("Deth", 2.0f);
		}
	}

	//�_���[�W���󂯂鏈��
	public void HitDamege(int damage)
	{
		m_hp -= damage;
	}

	void Deth()
	{
		Destroy(gameObject);
	}
}
