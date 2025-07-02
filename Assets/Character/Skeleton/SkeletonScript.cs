using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonScript : MonoBehaviour
{
	[SerializeField] GameObject enemyCastle;

	[SerializeField] private NavMeshAgent navMeshAgent;
	[SerializeField] private Animator anim;

	[SerializeField] private SearchEnemy searchEnemy;

	static int MaxHp = 10;
	static int AttackDamage = 5;
	static int Speed = 4;
	static float SearchRange = 5;

	int m_hp;
	int m_attackDamage;

	bool m_findEnemy;

	GameObject m_enemy;


	void Start()
    {
		m_hp = MaxHp;
		m_attackDamage = AttackDamage;

		navMeshAgent.speed = Speed;
	}

    void Update()
    {
		navMeshAgent.SetDestination(enemyCastle.transform.position);

		m_findEnemy = searchEnemy.GetFindEnemy();

		if(m_findEnemy)
		{
			navMeshAgent.SetDestination(m_enemy.transform.position);
		}
	}

	private void OnTriggerStay(Collider other)
	{
		GameObject obj = other.gameObject;

		if (other.gameObject.CompareTag("Enemy"))
		{
			AttackEnemy(obj);
		}
	}

	public void HitDamege(int damage)
	{
		m_hp -= damage;
	}

	void AttackEnemy(GameObject fightEnemy)
	{
		float attackCooolDown = Time.deltaTime;

		if(attackCooolDown>= 3)
		{
			anim.SetTrigger("Attack");

			SkeletonScript skeletonScript = fightEnemy.GetComponent<SkeletonScript>();

			skeletonScript.HitDamege(m_attackDamage);

			attackCooolDown = 0;
		}
	}

}
