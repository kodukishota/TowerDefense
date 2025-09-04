using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class StageEnemy : MonoBehaviour
{
	[SerializeField] CharacterDataBase CharacterDataBase;

	[SerializeField] private GameObject[] Character;

	[SerializeField] private GameObject EnemyCastle;
	[SerializeField] private Transform[] SpownPositionX;
	[SerializeField] private Transform[] SpownPositionZ;

	[SerializeField] float SpownCoolDown;
	[SerializeField] float StrongEnemyTime;

	int m_characterId;
	float m_spownCoolDown;
	float m_gameTime;

	bool m_strongEnemyTime;

	private void Update()
	{
		m_spownCoolDown += Time.deltaTime;		
		m_gameTime += Time.deltaTime;

		if(!m_strongEnemyTime)
		{
			if (m_spownCoolDown >= SpownCoolDown)
			{
				m_characterId = Random.Range(0, 3);

				CharacterInstantiate(m_characterId);

				m_spownCoolDown = 0;
			}
		}
		else
		{
			if (m_spownCoolDown >= SpownCoolDown)
			{
				m_characterId = Random.Range(3, Character.Length);

				CharacterInstantiate(m_characterId);

				m_spownCoolDown = 0;
			}
		}

		if (m_gameTime >= StrongEnemyTime)
		{
			m_strongEnemyTime = true;
		}
	}

	//ÉLÉÉÉâÉNÉ^ÇÃê∂ê¨
	public void CharacterInstantiate(int characterId)
	{
		float spownPosX = Random.Range(SpownPositionX[0].position.x, SpownPositionX[1].position.x);
		float spownPosZ = Random.Range(SpownPositionZ[0].position.z, SpownPositionZ[1].position.z);

		GameObject character = Instantiate(Character[characterId],
			new Vector3(spownPosX, 0, spownPosZ),
			Quaternion.Euler(0, 90, 0));

		CharacterScript characterScript = character.GetComponent<CharacterScript>();

		characterScript.SetEnemyCastle(EnemyCastle);
		characterScript.SetId(characterId + 1);

		character.tag = "Red";
	}
}