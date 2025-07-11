using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CharacterDataBase")]
public class CharacterDataBase : ScriptableObject
{
	public List<CharacterData> datas;
}

[System.Serializable]
public class CharacterData
{
	string name;
	int hp;
	int attackDamage;
	int speed;
	int cost;
}