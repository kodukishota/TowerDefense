using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class RangedInfantry : MonoBehaviour
{
	
	[SerializeField] private Animator anim;
	[SerializeField] private CharacterScript characterScript;

	[SerializeField] CharacterDataBase characterDataBase;

	[SerializeField] private NavMeshAgent navMeshAgent;

	[SerializeField] private GameObject projectilePrefab;		//–î‚¾‚Á‚½‚è–‚–@‚È‚Ç‚ÌƒIƒuƒWƒFƒNƒg

	[SerializeField] float AttackCooolDown = 5; //UŒ‚‘¬“x

	private GameObject m_enemyCastle;
	private SearchEnemy m_searchEnemy;
	private CanAttackEnemy m_canAttackEnemy;

	bool m_findEnemy;       //“G‚ğŒ©‚Â‚¯‚½‚©

	int m_attackDamage;     //UŒ‚—Í

	bool m_canAttack;           //“G‚ğUŒ‚‚·‚é‚±‚Æ‚ª‚Å‚«‚é‚©
	float m_attackCooolDown;    //UŒ‚‘¬“x

	bool m_isCreated;   //projectilePrefab‚ğ¶¬‚µ‚½‚©
	bool m_isHit;		//‚Æ‚ÑUŒ‚‚ª“G‚É“–‚½‚Á‚½‚©

	bool m_isIdle;

	int m_id;

	GameObject m_enemy;
	GameObject m_projectile;

	public void SetHitEnemy()
	{
		m_isHit = true;
	}

	void Start()
	{
		m_id = characterScript.GetId();

		navMeshAgent.speed = characterDataBase.datas[m_id].m_speed;

		m_attackDamage = characterDataBase.datas[m_id].m_attackDamage;
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

			navMeshAgent.speed = characterDataBase.datas[m_id].m_speed;

			m_isIdle = false;
		}
		else
		{
			m_enemy = m_searchEnemy.GetEnemy();

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
				projectile.SetSearchEnemy(m_searchEnemy);

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
