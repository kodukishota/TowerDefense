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

	static int Speed = 4;               //足の速さ
	static int AttackDamage = 5;        //攻撃力
	[SerializeField] float AttackCooolDown = 3;	//攻撃速度

	bool m_findEnemy;           //敵を見つけたか

	int m_attackDamage;     //攻撃力

	bool m_canAttack;           //敵を攻撃することができるか
	float m_attackCooolDown;    //攻撃速度

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

		//敵を見つけたら敵のほうへ行く
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

	//敵に攻撃する処理
	void AttackEnemy(GameObject fightEnemy)
	{
		m_attackCooolDown -= Time.deltaTime;
		//攻撃する場合に足を止める
		navMeshAgent.velocity = Vector3.zero;
		navMeshAgent.speed = 0;

		CharacterScript characterScript = fightEnemy.GetComponent<CharacterScript>();

		Debug.Log(m_attackCooolDown);

		if (m_attackCooolDown <= 0)
		{
			//攻撃アニメーションを再生
			anim.SetTrigger("Attack");

			if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0)
			{
				//体力を減らす
				characterScript.HitDamege(m_attackDamage);
				m_attackCooolDown = AttackCooolDown;

				anim.SetTrigger("Idle");
			}
		}
	}
}
