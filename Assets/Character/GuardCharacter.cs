using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardCharacter : MonoBehaviour
{
	[SerializeField] private Animator anim;
	[SerializeField] private CharacterScript m_characterScript;

	private SearchEnemy m_searchEnemy;
	private CanAttackEnemy m_canAttackEnemy;

	static int AttackDamage = 10;        //攻撃力
	[SerializeField] float AttackCooolDown = 6; //攻撃速度秒数

	int m_attackDamage;     //攻撃力

	bool m_findEnemy;           //敵を見つけたか
	bool m_canAttack;           //敵を攻撃することができるか
	float m_attackCooolDown;    //攻撃速度

	GameObject m_enemy;

	void Start()
	{
		m_attackDamage = AttackDamage;
		m_attackCooolDown = AttackCooolDown;
	}

	void Update()
	{
		m_searchEnemy = m_characterScript.GetSearchEnemy();
		m_canAttackEnemy = m_characterScript.GetCanAttackEnemy();

		m_findEnemy = m_searchEnemy.GetFindEnemy();
		m_enemy = m_searchEnemy.GetEnemy();

		//敵を見つけたら敵のほうへ行く
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

	//敵に攻撃する処理
	void AttackEnemy(GameObject fightEnemy)
	{
		m_attackCooolDown -= Time.deltaTime;

		CharacterScript characterScript = fightEnemy.GetComponent<CharacterScript>();

		if (m_attackCooolDown <= 0)
		{
			//攻撃アニメーションを再生
			anim.SetTrigger("Attack");

			if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0)
			{
				//体力を減らす
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
