using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class InfantryCharacter : MonoBehaviour
{
	[SerializeField] GameObject enemyCastle;
	[SerializeField] private Animator anim;

	[SerializeField] private NavMeshAgent navMeshAgent;

	[SerializeField] private SearchEnemy searchEnemy;
	[SerializeField] private CanAttackEnemy canAttackEnemy;

	static int Speed = 4;               //‘«‚Ì‘¬‚³
	static int AttackDamage = 5;        //UŒ‚—Í
	[SerializeField] float AttackCooolDown = 3;	//UŒ‚‘¬“x

	bool m_findEnemy;           //“G‚ğŒ©‚Â‚¯‚½‚©

	int m_attackDamage;     //UŒ‚—Í

	bool m_canAttack;           //“G‚ğUŒ‚‚·‚é‚±‚Æ‚ª‚Å‚«‚é‚©
	float m_attackCooolDown;    //UŒ‚‘¬“x

	bool m_isIdle;

	GameObject m_enemy;

	void Start()
	{
		navMeshAgent.speed = Speed;

		m_attackDamage = AttackDamage;
		m_canAttack = false;
	}

	void Update()
	{
		m_findEnemy = searchEnemy.GetFindEnemy();
		m_canAttack = canAttackEnemy.GetCanAttack();

		//“G‚ğŒ©‚Â‚¯‚½‚ç“G‚Ì‚Ù‚¤‚Ös‚­
		if (!m_findEnemy)
		{
			navMeshAgent.SetDestination(enemyCastle.transform.position);

			anim.SetTrigger("Walk");

			navMeshAgent.speed = Speed;

			m_isIdle = false;
		}
		else
		{
			m_enemy = searchEnemy.GetEnemy();

			navMeshAgent.SetDestination(m_enemy.transform.position);
		}

		if (m_enemy != null)
		{
			if(m_canAttack)
			{
				AttackEnemy(m_enemy);

				if(!m_isIdle)
				{
					anim.SetTrigger("Idle");
					m_isIdle = true;
				}
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

		Debug.Log(m_attackCooolDown);

		if (m_attackCooolDown <= 0)
		{
			//UŒ‚ƒAƒjƒ[ƒVƒ‡ƒ“‚ğÄ¶
			anim.SetTrigger("Attack");

			if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0)
			{
				//‘Ì—Í‚ğŒ¸‚ç‚·
				characterScript.HitDamege(m_attackDamage);
				m_attackCooolDown = AttackCooolDown;

				anim.SetTrigger("Idle");
			}
		}
	}
}
