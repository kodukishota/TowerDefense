using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanAttackEnemy : MonoBehaviour
{
	bool m_canAttack = false;

	[SerializeField] private CharacterScript m_characterScript;

	private void Start()
	{
		m_characterScript.SetCanAttackEnemy(this);
	}

	public bool GetCanAttack()
	{
		return m_canAttack;
	}

	private void OnTriggerStay(Collider other)
	{
		if (m_characterScript.GetCharacter().tag == "Red")
		{
			if (other.CompareTag("Blue"))
			{
				m_canAttack = true;
			}
		}
		else if(m_characterScript.GetCharacter().tag == "Blue")
		{
			if (other.CompareTag("Red"))
			{
				m_canAttack = true;
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if(m_characterScript.GetCharacter().tag == "Red")
		{
			if (other.CompareTag("Blue"))
			{
				m_canAttack = false;
			}
		}
		else if(m_characterScript.GetCharacter().tag == "Blue")
		{
			if (other.CompareTag("Red"))
			{
				m_canAttack = false;
			}
		}
			
	}
}
