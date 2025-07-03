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

	static int MaxHp = 10;				//�ő�̗�
	static int AttackDamage = 5;		//�U����
	static int Speed = 4;               //���̑���
	[SerializeField] float AttackCooolDown = 3;	//�U�����x

	int m_hp;				//�̗�
	int m_attackDamage;		//�U����

	bool m_findEnemy;			//�G����������
	bool m_canAttack;			//�G���U�����邱�Ƃ��ł��邩
	float m_attackCooolDown;	//�U�����x

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

		//���S�����Ƃ�
		if (m_hp <= 0)
		{
			gameObject.tag = "Carcass";

			anim.SetTrigger("Deth");

			Invoke("Deth", 2.0f);
		}

		//�G����������G�̂ق��֍s��
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

	//�_���[�W���󂯂鏈��
	public void HitDamege(int damage)
	{
		m_hp -= damage;
	}

	//�G�ɍU�����鏈��
	void AttackEnemy(GameObject fightEnemy)
	{
		m_attackCooolDown -= Time.deltaTime;
		//�U������ꍇ�ɑ����~�߂�
		navMeshAgent.velocity = Vector3.zero;
		navMeshAgent.speed = 0;

		anim.SetTrigger("Idle");

		CharacterScript skeletonScript = fightEnemy.GetComponent<CharacterScript>();

		Debug.Log(m_attackCooolDown);

		if (m_attackCooolDown <= 0)
		{
			//�U���A�j���[�V�������Đ�
			anim.SetTrigger("Attack");
			//�̗͂����炷
			skeletonScript.HitDamege(m_attackDamage);

			m_attackCooolDown = AttackCooolDown;
		}
	}

	void Deth()
	{
		Destroy(gameObject);
	}
}
