using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class RangedInfantry : MonoBehaviour
{
	[SerializeField] GameObject enemyCastle;
	[SerializeField] private Animator anim;

	[SerializeField] private NavMeshAgent navMeshAgent;

	[SerializeField] private SearchEnemy searchEnemy;
	[SerializeField] private CanAttackEnemy canAttackEnemy;

	[SerializeField] private GameObject projectilePrefab;		//–î‚¾‚Á‚½‚è–‚–@‚È‚Ç‚ÌƒIƒuƒWƒFƒNƒg

	static int Speed = 4;               //‘«‚Ì‘¬‚³
	static int AttackDamage = 5;        //UŒ‚—Í
	[SerializeField] float AttackCooolDown = 5; //UŒ‚‘¬“x

	bool m_findEnemy;       //“G‚ğŒ©‚Â‚¯‚½‚©

	int m_attackDamage;     //UŒ‚—Í

	bool m_canAttack;           //“G‚ğUŒ‚‚·‚é‚±‚Æ‚ª‚Å‚«‚é‚©
	float m_attackCooolDown;    //UŒ‚‘¬“x

	bool m_isCreated;   //projectilePrefab‚ğ¶¬‚µ‚½‚©
	bool m_isHit;		//‚Æ‚ÑUŒ‚‚ª“G‚É“–‚½‚Á‚½‚©

	bool m_isIdle;

	GameObject m_enemy;
	GameObject m_projectile;

	public void SetHitEnemy()
	{
		m_isHit = true;
	}

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
		m_enemy = searchEnemy.GetEnemy();

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
			navMeshAgent.SetDestination(m_enemy.transform.position);
		}

		if (m_enemy != null)
		{
			if (m_canAttack)
			{
				AttackEnemy(m_enemy);

				if (!m_isIdle)
				{
					anim.SetTrigger("AttackStandby");
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

			//UŒ‚‚Ì”ò‚ÑƒAƒCƒeƒ€¶¬
			if(!m_isCreated)
			{
				m_projectile = Instantiate(
					projectilePrefab,
					new Vector3(m_enemy.transform.position.x, m_enemy.transform.position.y + 3, m_enemy.transform.position.z),
					Quaternion.identity);

				Projectile projectile = m_projectile.GetComponent<Projectile>();

				projectile.SetRangedInfantry(this);
				projectile.SetSearchEnemy(searchEnemy);

				m_isCreated = true;
			}

			if(m_isHit)
			{
				//‘Ì—Í‚ğŒ¸‚ç‚·
				characterScript.HitDamege(m_attackDamage);

				m_attackCooolDown = AttackCooolDown;

				m_isCreated = false;
			}	
		}
		else
		{
			anim.SetTrigger("AttackStandby");
		}
	}
}
