using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class RangedInfantry : MonoBehaviour
{
	
	[SerializeField] private Animator anim;
	[SerializeField] private CharacterScript m_characterScript;

	[SerializeField] private NavMeshAgent navMeshAgent;

	[SerializeField] private GameObject projectilePrefab;		//矢だったり魔法などのオブジェクト

	static int Speed = 4;               //足の速さ
	static int AttackDamage = 5;        //攻撃力
	[SerializeField] float AttackCooolDown = 5; //攻撃速度

	private GameObject m_enemyCastle;
	private SearchEnemy m_searchEnemy;
	private CanAttackEnemy m_canAttackEnemy;

	bool m_findEnemy;       //敵を見つけたか

	int m_attackDamage;     //攻撃力

	bool m_canAttack;           //敵を攻撃することができるか
	float m_attackCooolDown;    //攻撃速度

	bool m_isCreated;   //projectilePrefabを生成したか
	bool m_isHit;		//とび攻撃が敵に当たったか

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

		m_enemyCastle = m_characterScript.GetenemyCastle();
	}

	void Update()
	{
		m_searchEnemy = m_characterScript.GetSearchEnemy();
		m_canAttackEnemy = m_characterScript.GetCanAttackEnemy();

		m_findEnemy = m_searchEnemy.GetFindEnemy();
		m_canAttack = m_canAttackEnemy.GetCanAttack();

		//敵を見つけたら敵のほうへ行く
		if (!m_findEnemy)
		{
			navMeshAgent.SetDestination(m_enemyCastle.transform.position);

			anim.SetTrigger("Walk");

			navMeshAgent.speed = Speed;

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

	//敵に攻撃する処理
	void AttackEnemy(GameObject fightEnemy)
	{
		m_attackCooolDown -= Time.deltaTime;

		//攻撃する場合に足を止める
		navMeshAgent.velocity = Vector3.zero;
		navMeshAgent.speed = 0;

		CharacterScript characterScript = fightEnemy.GetComponent<CharacterScript>();

		if (m_attackCooolDown <= 0)
		{
			//攻撃アニメーションを再生
			anim.SetTrigger("Attack");

			//攻撃時の飛びアイテム生成
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
				//体力を減らす
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
