using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class CharacterScript : MonoBehaviour
{
	[SerializeField] CharacterDataBase characterData;

	[SerializeField] private Animator anim;

	[SerializeField] int m_hp;              //‘Ì—Í

	GameObject m_enemyCastle;
	private SearchEnemy m_searchEnemy;
	private CanAttackEnemy m_canAttackEnemy;

	int m_id;

	public void SetId(int id)
	{
		m_id = id;
	}

	public int GetId()
	{
		return m_id;
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
		m_hp = characterData.datas[m_id].m_hp;
	}

	void Update()
	{
		//Ž€–S‚µ‚½‚Æ‚«
		if (m_hp <= 0)
		{
			gameObject.tag = "Carcass";

			anim.SetTrigger("Deth");

			Invoke("Deth", 2.0f);
		}
	}

	//ƒ_ƒ[ƒW‚ðŽó‚¯‚éˆ—
	public void HitDamege(int damage)
	{
		m_hp -= damage;
	}

	void Deth()
	{
		Destroy(gameObject);
	}
}
