using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchEnemy : MonoBehaviour 
{
	bool m_findEnemy = false; 

	public bool GetFindEnemy()
	{
		return m_findEnemy;
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("Enemy"))
		{
			m_findEnemy = true;
		}
	}
}
