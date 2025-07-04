using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class CharacterScript : MonoBehaviour
{
	[SerializeField] private Animator anim;
	//static int MaxHp = 10;				//Å‘å‘Ì—Í

	[SerializeField] int m_hp;				//‘Ì—Í
	GameObject m_enemy;

	void Start()
	{

	}

	void Update()
	{
		//€–S‚µ‚½‚Æ‚«
		if (m_hp <= 0)
		{
			gameObject.tag = "Carcass";

			anim.SetTrigger("Deth");

			Invoke("Deth", 2.0f);
		}
	}

	//ƒ_ƒ[ƒW‚ğó‚¯‚éˆ—
	public void HitDamege(int damage)
	{
		m_hp -= damage;
	}

	void Deth()
	{
		Destroy(gameObject);
	}
}
