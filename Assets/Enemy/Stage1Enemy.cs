using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Stage1Enemy : MonoBehaviour
{
	[SerializeField] CharacterDataBase CharacterDataBase;

	[SerializeField] private GameObject[] Character;

	[SerializeField] private GameObject EnemyCastle;
	[SerializeField] private Transform[] SpownPositionX;
	[SerializeField] private Transform[] SpownPositionZ;

	static float SpownCoolDown = 10.0f;

	int m_characterId;
	float m_spownCoolDown;

	private void Update()
	{
		m_spownCoolDown += Time.deltaTime;		

		if (m_spownCoolDown >= SpownCoolDown)
		{
			m_characterId = Random.Range(0, Character.Length);

			CharacterInstantiate( m_characterId);

			m_spownCoolDown = 0;
		}
	}

	//ÉLÉÉÉâÉNÉ^ÇÃê∂ê¨
	public void CharacterInstantiate(int characterId)
	{
		float spownPosX = Random.Range(SpownPositionX[0].position.x, SpownPositionX[1].position.x);
		float spownPosZ = Random.Range(SpownPositionZ[0].position.z, SpownPositionZ[1].position.z);

		GameObject character = Instantiate(Character[characterId],
			new Vector3(spownPosX, 0, spownPosZ),
			Quaternion.Euler(0, 0, 0));

		CharacterScript characterScript = character.GetComponent<CharacterScript>();

		characterScript.SetEnemyCastle(EnemyCastle);
		characterScript.SetId(characterId);

		character.tag = "Red";
	}
}
