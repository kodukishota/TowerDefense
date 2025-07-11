using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class InfantryCharacter : MonoBehaviour
{
	[SerializeField] private Animator anim;
	[SerializeField] private CharacterScript m_characterScript;

	[SerializeField] private NavMeshAgent navMeshAgent;

	private GameObject m_enemyCastle;
	private SearchEnemy m_searchEnemy;
	private CanAttackEnemy m_canAttackEnemy;

	static int Speed = 4;               //‘«‚Ì‘¬‚³
	static int AttackDamage = 5;        //UŒ‚—Í
	[SerializeField] float AttackCooolDown = 3;	//UŒ‚‘¬“x

	bool m_findEnemy;           //“G‚ğŒ©‚Â‚¯‚½‚©

	int m_attackDamage;     //UŒ‚—Í

	bool m_canAttack;           //“G‚ğUŒ‚‚·‚é‚±‚Æ‚ª‚Å‚«‚é‚©
	float m_attackCooolDown;    //UŒ‚‘¬“x

	GameObject m_enemy;

	void Start()
	{
		navMeshAgent.speed = Speed;

		m_attackDamage = m_characterScript.GetAtk();
		m_findEnemy = false;
		m_canAttack = false;

		m_enemyCastle = m_characterScript.GetenemyCastle();
	}

	void Update()
	{
		m_searchEnemy = m_characterScript.GetSearchEnemy();
		m_canAttackEnemy = m_characterScript.GetCanAttackEnemy();

		m_findEnemy = m_searchEnemy.GetFindEnemy();
		m_canAttack = m_canAttackEnemy.GetCanAttack();

		//“G‚ğŒ©‚Â‚¯‚½‚ç“G‚Ì‚Ù‚¤‚Ös‚­
		if (!m_findEnemy)
		{
			navMeshAgent.SetDestination(m_enemyCastle.transform.position);

			anim.SetTrigger("Walk");

			navMeshAgent.speed = Speed;
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
