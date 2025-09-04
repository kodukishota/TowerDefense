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
		if(m_characterScript.GetCharacter().tag == "Red")
		{
			if (other.CompareTag("Blue"))
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
		else if(m_characterScript.GetCharacter().tag == "Blue")
		{
			if (other.CompareTag("Red"))
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

	private void OnTriggerExit(Collider other)
	{
		if (m_characterScript.GetCharacter().tag == "Red")
		{
			if (other.CompareTag("Blue"))
			{
				m_findEnemy = false;
			}
		}
		else if (m_characterScript.GetCharacter().tag == "Blue")
		{
			if (other.CompareTag("Red"))
			{
				m_findEnemy = false;
			}
		}
	}
}
