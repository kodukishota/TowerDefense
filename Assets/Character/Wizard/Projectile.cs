using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	[SerializeField] private RangedInfantry m_rangedInfantry;
	[SerializeField] private SearchEnemy m_searchEnemy;

	Collider m_collider;

	GameObject m_enemy;

	public void SetRangedInfantry(RangedInfantry rangedInfantry)
	{
		m_rangedInfantry = rangedInfantry;
	}

	public void SetSearchEnemy(SearchEnemy searchEnemy)
	{
		m_searchEnemy = searchEnemy;
	}

	void Start()
	{
		m_collider = GetComponent<Collider>();
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
		if(this.gameObject.tag == "RedRanged")
		{
			//“G‚É“–‚½‚Á‚½‚ç“–‚½‚Á‚½‚Æ•Ô‚·
			if (other.gameObject.CompareTag("Blue"))
			{
				m_rangedInfantry.SetHitEnemy();

				OffCollider();

				Invoke("OnDestroy", 0.5f);
			}
		}
		else
		{
			//“G‚É“–‚½‚Á‚½‚ç“–‚½‚Á‚½‚Æ•Ô‚·
			if (other.gameObject.CompareTag("Red"))
			{
				m_rangedInfantry.SetHitEnemy();

				OffCollider();

				Invoke("OnDestroy", 0.5f);
			}
		}
		
	}

	private void OnDestroy()
	{
		Destroy(gameObject);
	}

	private void OffCollider()
	{
		m_collider.gameObject.SetActive(false);
	}
}
