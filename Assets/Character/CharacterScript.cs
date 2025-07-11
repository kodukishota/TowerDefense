using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class CharacterScript : MonoBehaviour
{
	[SerializeField] private Animator anim;
	//static int MaxHp = 10;				//Å‘å‘Ì—Í

	[SerializeField] int m_hp;              //‘Ì—Í

	int m_atk;
	int m_cost;

	GameObject m_enemyCastle;
	private SearchEnemy m_searchEnemy;
	private CanAttackEnemy m_canAttackEnemy;
	
	bool m_isDeth;

	public void SetStatus(int hp,int atk,int cost)
	{
		m_hp = hp;
		m_atk = atk;
		m_cost = cost;
	}

	public int GetAtk()
	{
		return m_atk;
	}

	public int GetCost()
	{
		return m_cost;
	}

	public void SetSearchEnemy(SearchEnemy searchEnemy)
	{
		m_searchEnemy = searchEnemy;
	}

	public void SetCanAttackEnemy(CanAttackEnemy canAttackEnemy)
	{
		m_canAttackEnemy = canAttackEnemy;
	}

	public void SetEnemyCastle(GameObject enemyCastle)
	{
		m_enemyCastle = enemyCastle;
	}

	public SearchEnemy GetSearchEnemy()
	{
		return m_searchEnemy;
	}

	public CanAttackEnemy GetCanAttackEnemy()
	{
		return m_canAttackEnemy;
	}

	public GameObject GetenemyCastle()
	{
		return m_enemyCastle;
	}

	void Start()
	{
		m_isDeth = false;
	}

	void Update()
	{
		//€–S‚µ‚½‚Æ‚«
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
