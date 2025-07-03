using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDataBase : ScriptableObject
{
	public CharacterDataBase[] datas;
}

[System.Serializable]
public class CharacterData
{
	string name;
	int hp;
	int attackDamage;
	int speed;
}