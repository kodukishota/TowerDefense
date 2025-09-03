using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class CharacterScript : MonoBehaviour
{
	[SerializeField] CharacterDataBase characterData;
	//[SerializeField] GameObject Character;

	[SerializeField] private Animator anim;

	[SerializeField] int m_hp;              //‘Ì—Í

	GameObject m_enemyCastle;
	private SearchEnemy m_searchEnemy;
	private CanAttackEnemy m_canAttackEnemy;

	[SerializeField] int m_id;

	[SerializeField] AudioSource DethSe;

	bool m_endAnim;

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

	public int GetHelth()
	{
		return m_hp;
	}

	public GameObject GetCharacter()
	{
		return this.gameObject;
	}

	void Start()
	{
		m_hp = characterData.datas[m_id].m_hp;

		m_endAnim = false;
	}

	void Update()
	{
		if(m_id != 0)
		{
			//Ž€–S‚µ‚½‚Æ‚«
			if (m_hp <= 0)
			{
				gameObject.tag = "Carcass";

				if(!m_endAnim)
				{
					m_endAnim = true;
					anim.SetTrigger("Deth");
					DethSe.Play();
				}

				Invoke("OnDestroy", 2.0f);
			}
		}

	}

	//ƒ_ƒ[ƒW‚ðŽó‚¯‚éˆ—
	public void HitDamege(int damage)
	{
		m_hp -= damage;
	}

	void OnDestroy()
	{
		Destroy(gameObject);
	}
}
