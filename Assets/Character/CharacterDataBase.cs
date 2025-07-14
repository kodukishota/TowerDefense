using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CharacterDataBase")]
public class CharacterDataBase : ScriptableObject
{

	public List<CharacterData> datas;

	[System.Serializable]
	public class CharacterData
	{
		public string m_name;
		public int m_hp;
		public int m_attackDamage;
		public int m_speed;
		public int m_cost;

		public void SetStatus(string name,int hp, int atk,int speed,int cost)
		{
			m_name = name;
			m_hp = hp;
			m_attackDamage = atk;
			m_speed = speed;
			m_cost = cost;
		}
	}
}

