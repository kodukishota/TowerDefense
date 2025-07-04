using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class CharacterScript : MonoBehaviour
{
	[SerializeField] private Animator anim;
	//static int MaxHp = 10;				//最大体力

	[SerializeField] int m_hp;				//体力
	GameObject m_enemy;

	void Start()
	{

	}

	void Update()
	{
		//死亡したとき
		if (m_hp <= 0)
		{
			gameObject.tag = "Carcass";

			anim.SetTrigger("Deth");

			Invoke("Deth", 2.0f);
		}
	}

	//ダメージを受ける処理
	public void HitDamege(int damage)
	{
		m_hp -= damage;
	}

	void Deth()
	{
		Destroy(gameObject);
	}
}
