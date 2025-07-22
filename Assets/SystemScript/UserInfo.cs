using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(menuName = "UserInfo")]
public class UserInfo : ScriptableObject
{
	public List<UserData> datas;

	[System.Serializable]
	public class UserData
	{
		public int m_id;
		public string m_name;

		public void SetStatus(int id,string name)
		{
			m_id = id;
			m_name = name;
		}
	}
}
