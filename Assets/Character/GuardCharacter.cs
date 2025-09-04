using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardCharacter : MonoBehaviour
{
	[SerializeField] private Animator anim;
	[SerializeField] private CharacterScript characterScript;
	[SerializeField] CharacterDataBase characterDataBase;

	private SearchEnemy m_searchEnemy;
	private CanAttackEnemy m_canAttackEnemy;

	int m_attackDamage;     //UŒ‚—Í

	bool m_findEnemy;           //“G‚ğŒ©‚Â‚¯‚½‚©
	bool m_canAttack;           //“G‚ğUŒ‚‚·‚é‚±‚Æ‚ª‚Å‚«‚é‚©
	float m_attackCooolDown;    //UŒ‚‘¬“x

	int m_id;

	bool m_endAnim;

	GameObject m_enemy;

	void Start()
	{
		m_id = characterScript.GetId();

		m_attackDamage = characterDataBase.datas[m_id].m_attackDamage;
		m_attackCooolDown = characterDataBase.datas[m_id].m_attackSpeed;

		m_endAnim = false;
	}

	void Update()
	{
		m_searchEnemy = characterScript.GetSearchEnemy();
		m_canAttackEnemy = characterScript.GetCanAttackEnemy();

		m_findEnemy = m_searchEnemy.GetFindEnemy();
		m_enemy = m_searchEnemy.GetEnemy();

		//“G‚ğŒ©‚Â‚¯‚½‚ç“G‚Ì‚Ù‚¤‚Ös‚­
		if (!m_findEnemy)
		{
			m_canAttack = false;
		}
		else
		{
			m_canAttack = m_canAttackEnemy.GetCanAttack();
		}

		if (m_enemy != null)
		{
			if (m_canAttack)
			{
				AttackEnemy(m_enemy);
			}
		}
	}

	//“G‚ÉUŒ‚‚·‚éˆ—
	void AttackEnemy(GameObject fightEnemy)
	{
		m_attackCooolDown -= Time.deltaTime;

		CharacterScript characterScript = fightEnemy.GetComponent<CharacterScript>();

		if (m_attackCooolDown <= 0)
		{

			if(!m_endAnim)
			{
				//UŒ‚ƒAƒjƒ[ƒVƒ‡ƒ“‚ğÄ¶
				anim.SetTrigger("Attack");

				m_endAnim = true;
			}

			if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0)
			{
				//‘Ì—Í‚ğŒ¸‚ç‚·
				characterScript.HitDamege(m_attackDamage);
				m_attackCooolDown = characterDataBase.datas[m_id].m_attackSpeed;
			}
		}
		else
		{
			anim.SetTrigger("Idle");

			m_endAnim = false;
		}
	}
}
