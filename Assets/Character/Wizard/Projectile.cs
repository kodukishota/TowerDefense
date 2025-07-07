using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	[SerializeField] private RangedInfantry m_rangedInfantry;
	[SerializeField] private SearchEnemy m_searchEnemy;

	GameObject m_enemy;

	public void SetRangedInfantry(RangedInfantry rangedInfantry)
	{
		m_rangedInfantry = rangedInfantry;
	}

	public void SetSearchEnemy(SearchEnemy searchEnemy)
	{
		m_searchEnemy = searchEnemy;
	}

	void Update()
	{
		m_enemy = m_searchEnemy.GetEnemy();

		if (m_enemy == null)
		{
			Invoke("OnDestroy", 1f);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Enemy"))
		{
			m_rangedInfantry.SetHitEnemy();

			Invoke("OnDestroy", 1f);
		}
	}

	private void OnDestroy()
	{
		Destroy(gameObject);
	}
}
