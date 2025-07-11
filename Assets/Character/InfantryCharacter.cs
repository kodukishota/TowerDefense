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

	static int Speed = 4;               //���̑���
	static int AttackDamage = 5;        //�U����
	[SerializeField] float AttackCooolDown = 3;	//�U�����x

	bool m_findEnemy;           //�G����������

	int m_attackDamage;     //�U����

	bool m_canAttack;           //�G���U�����邱�Ƃ��ł��邩
	float m_attackCooolDown;    //�U�����x

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

		//�G����������G�̂ق��֍s��
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

	//�G�ɍU�����鏈��
	void AttackEnemy(GameObject fightEnemy)
	{
		m_attackCooolDown -= Time.deltaTime;
		//�U������ꍇ�ɑ����~�߂�
		navMeshAgent.velocity = Vector3.zero;
		navMeshAgent.speed = 0;

		CharacterScript characterScript = fightEnemy.GetComponent<CharacterScript>();

		if (m_attackCooolDown <= 0)
		{
			//�U���A�j���[�V�������Đ�
			anim.SetTrigger("Attack");

			if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0)
			{
				//�̗͂����炷
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
