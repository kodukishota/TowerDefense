using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryOrDefeat : MonoBehaviour
{
	[SerializeField] CharacterScript PlayerHelth;
	[SerializeField] CharacterScript EnemyHelth;

	[SerializeField] AudioSource CrashSe;
	[SerializeField] AudioSource VectorySe;
	[SerializeField] AudioSource DefeatSe;

	[SerializeField] private GameObject BomdEffect;

	[SerializeField] Transform[] CastlePosition;

	[SerializeField] GameObject[] Screen;

	bool m_isCrash;

	void Update()
    {
        if(PlayerHelth.GetHelth() <= 0)
		{
			if(!m_isCrash)
			{
				CrashSe.Play();
				Instantiate(BomdEffect, CastlePosition[0]);

				m_isCrash = true;
			}

			Invoke("OpenDefeatScreen", 1.0f);
		}
		else if(EnemyHelth.GetHelth() <= 0)
		{
			if (!m_isCrash)
			{
				CrashSe.Play();
				Instantiate(BomdEffect, CastlePosition[1]);

				m_isCrash = true;
			}

			Invoke("OpenVectoryScereen", 1.0f);
			
		}
    }

	void OpenVectoryScereen()
	{
		VectorySe.Play();
		Screen[0].SetActive(true);
	}

	void OpenDefeatScreen()
	{
		DefeatSe.Play();
		Screen[1].SetActive(true);
	}
}
