using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class CharacterScript : MonoBehaviour
{
	[SerializeField] GameObject enemyCastle;

	[SerializeField] private NavMeshAgent navMeshAgent;
	[SerializeField] private Animator anim;

	[SerializeField] private SearchEnemy searchEnemy;
	[SerializeField] private CanAttackEnemy canAttackEnemy;

	static int MaxHp = 10;				//Å‘å‘Ì—Í
	static int AttackDamage = 5;		//UŒ‚—Í
	static int Speed = 4;               //‘«‚Ì‘¬‚³
	[SerializeField] float AttackCooolDown = 3;	//UŒ‚‘¬“x

	int m_hp;				//‘Ì—Í
	int m_attackDamage;		//UŒ‚—Í

	bool m_findEnemy;			//“G‚ğŒ©‚Â‚¯‚½‚©
	bool m_canAttack;			//“G‚ğUŒ‚‚·‚é‚±‚Æ‚ª‚Å‚«‚é‚©
	float m_attackCooolDown;	//UŒ‚‘¬“x

	GameObject m_enemy;

	void Start()
	{
		m_hp = MaxHp;
		m_attackDamage = AttackDamage;
		m_attackCooolDown = AttackCooolDown;

		navMeshAgent.speed = Speed;
	}

	void Update()
	{
		m_findEnemy = searchEnemy.GetFindEnemy();
		m_enemy = searchEnemy.GetEnemy();

		//€–S‚µ‚½‚Æ‚«
		if (m_hp <= 0)
		{
			gameObject.tag = "Carcass";

			anim.SetTrigger("Deth");

			Invoke("Deth", 2.0f);
		}

		//“G‚ğŒ©‚Â‚¯‚½‚ç“G‚Ì‚Ù‚¤‚Ös‚­
		if (!m_findEnemy)
		{
			navMeshAgent.SetDestination(enemyCastle.transform.position);

			anim.SetTrigger("Walk");

			navMeshAgent.speed = Speed;

			m_canAttack = false;
		}
		else
		{
			navMeshAgent.SetDestination(m_enemy.transform.position);

			m_canAttack = canAttackEnemy.GetCanAttack();
		}

		if(m_canAttack)
		{
			AttackEnemy(m_enemy);
		}
	}

	//ƒ_ƒ[ƒW‚ğó‚¯‚éˆ—
	public void HitDamege(int damage)
	{
		m_hp -= damage;
	}

	//“G‚ÉUŒ‚‚·‚éˆ—
	void AttackEnemy(GameObject fightEnemy)
	{
		m_attackCooolDown -= Time.deltaTime;
		//UŒ‚‚·‚éê‡‚É‘«‚ğ~‚ß‚é
		navMeshAgent.velocity = Vector3.zero;
		navMeshAgent.speed = 0;

		anim.SetTrigger("Idle");

		CharacterScript skeletonScript = fightEnemy.GetComponent<CharacterScript>();

		Debug.Log(m_attackCooolDown);

		if (m_attackCooolDown <= 0)
		{
			//UŒ‚ƒAƒjƒ[ƒVƒ‡ƒ“‚ğÄ¶
			anim.SetTrigger("Attack");
			//‘Ì—Í‚ğŒ¸‚ç‚·
			skeletonScript.HitDamege(m_attackDamage);

			m_attackCooolDown = AttackCooolDown;
		}
	}

	void Deth()
	{
		Destroy(gameObject);
	}
}
