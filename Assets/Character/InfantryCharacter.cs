using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class InfantryCharacter : MonoBehaviour
{
	[SerializeField] private Animator anim;
	[SerializeField] private CharacterScript characterScript;

	[SerializeField] private NavMeshAgent navMeshAgent;
	[SerializeField] CharacterDataBase characterDataBase;

	private GameObject m_enemyCastle;
	private SearchEnemy m_searchEnemy;
	private CanAttackEnemy m_canAttackEnemy;	

	[SerializeField] float AttackCooolDown = 3;	//UŒ‚‘¬“x

	bool m_findEnemy;           //“G‚ğŒ©‚Â‚¯‚½‚©

	int m_attackDamage;     //UŒ‚—Í

	bool m_canAttack;           //“G‚ğUŒ‚‚·‚é‚±‚Æ‚ª‚Å‚«‚é‚©
	float m_attackCooolDown;    //UŒ‚‘¬“x

	int m_id;

	GameObject m_enemy;

	void Start()
	{
		m_id = characterScript.GetId();

		navMeshAgent.speed = characterDataBase.datas[m_id].m_speed;

		m_attackDamage = characterDataBase.datas[m_id].m_attackDamage;

		m_findEnemy = false;
		m_canAttack = false;

		m_enemyCastle = characterScript.GetenemyCastle();
	}

	void Update()
	{
		m_searchEnemy = characterScript.GetSearchEnemy();
		m_canAttackEnemy = characterScript.GetCanAttackEnemy();

		m_findEnemy = m_searchEnemy.GetFindEnemy();
		m_canAttack = m_canAttackEnemy.GetCanAttack();

		//“G‚ğŒ©‚Â‚¯‚½‚ç“G‚Ì‚Ù‚¤‚Ös‚­
		if (!m_findEnemy)
		{
			navMeshAgent.SetDestination(m_enemyCastle.transform.position);

			anim.SetTrigger("Walk");

			//‘«‚Ì‘¬‚³‚ğ‚à‚Æ‚É–ß‚·
			navMeshAgent.speed = characterDataBase.datas[m_id].m_speed;
		}
		else
		{
			m_enemy = m_searchEnemy.GetEnemy();

			navMeshAgent.SetDestination(m_enemy.transform.position);
		}

		if (m_enemy != null)
		{
			if(m_canAttack)
			{
				AttackEnemy(m_enemy);
			}
		}
	}

	//“G‚ÉUŒ‚‚·‚éˆ—
	void AttackEnemy(GameObject fightEnemy)
	{
		m_attackCooolDown -= Time.deltaTime;
		//UŒ‚‚·‚éê‡‚É‘«‚ğ~‚ß‚é
		navMeshAgent.velocity = Vector3.zero;
		navMeshAgent.speed = 0;

		CharacterScript characterScript = fightEnemy.GetComponent<CharacterScript>();

		if (m_attackCooolDown <= 0)
		{
			//UŒ‚ƒAƒjƒ[ƒVƒ‡ƒ“‚ğÄ¶
			anim.SetTrigger("Attack");

			if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0)
			{
				//‘Ì—Í‚ğŒ¸‚ç‚·
				characterScript.HitDamege(m_attackDamage);
				m_attackCooolDown = AttackCooolDown;
			}
		}
		else
		{
			anim.SetTrigger("Idle");
		}
	}
}
