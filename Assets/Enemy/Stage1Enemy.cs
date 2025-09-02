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
	[SerializeField] private Transform[] SpownPosition;

	static float SpownCoolDown = 5.0f;

	int m_characterId;
	float m_spownCoolDown;


	private void Update()
	{
		m_spownCoolDown += Time.deltaTime;		

		if (m_spownCoolDown >= SpownCoolDown)
		{
			int positionIndex = 0;

			positionIndex = Random.Range(1, 3);
			m_characterId = Random.Range(0, Character.Length);

			Instantiate(SpownPosition[positionIndex].position, m_characterId);

			m_spownCoolDown = 0;
		}
	}

	//ÉLÉÉÉâÉNÉ^ÇÃê∂ê¨
	public void Instantiate(Vector3 position, int characterId)
	{
		GameObject character = Instantiate(Character[characterId], position, Quaternion.Euler(0, 0, 0));

		CharacterScript characterScript = character.GetComponent<CharacterScript>();

		characterScript.SetEnemyCastle(EnemyCastle);
		characterScript.SetId(characterId);

		character.tag = "Red";
	}
}
