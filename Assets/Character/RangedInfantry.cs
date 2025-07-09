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

	[SerializeField] private GameObject projectilePrefab;		//������薂�@�Ȃǂ̃I�u�W�F�N�g

	static int Speed = 4;               //���̑���
	static int AttackDamage = 5;        //�U����
	[SerializeField] float AttackCooolDown = 5; //�U�����x

	private GameObject m_enemyCastle;
	private SearchEnemy m_searchEnemy;
	private CanAttackEnemy m_canAttackEnemy;

	bool m_findEnemy;       //�G����������

	int m_attackDamage;     //�U����

	bool m_canAttack;           //�G���U�����邱�Ƃ��ł��邩
	float m_attackCooolDown;    //�U�����x

	bool m_isCreated;   //projectilePrefab�𐶐�������
	bool m_isHit;		//�ƂэU�����G�ɓ���������

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

		//�G����������G�̂ق��֍s��
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

			//�U�����̔�уA�C�e������
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
				//�̗͂����炷
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
