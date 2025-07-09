using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchEnemy : MonoBehaviour 
{
	bool m_findEnemy = false; 
	GameObject m_enemy;

	[SerializeField] private CharacterScript m_characterScript;

	private void Start()
	{
		m_characterScript.SetSearchEnemy(this);
	}

	public bool GetFindEnemy()
	{
		return m_findEnemy;
	}

	public GameObject GetEnemy()
	{
		return m_enemy;
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("Enemy"))
		{
			m_enemy = other.gameObject;
			m_findEnemy = true;
		}
		if (other.CompareTag("Carcass"))
		{
			m_findEnemy = false;

			m_enemy = null;
		}
	}
}
