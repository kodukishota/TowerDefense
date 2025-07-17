using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInfo : MonoBehaviour
{
	int m_userId;
	string m_userName;

	public int GetUserId()
	{
		return m_userId;
	}

	public void SetUserId(int userId)
	{
		m_userId = userId;
	}

	public string GetUserName()
	{
		return m_userName;
	}

	public void SetUserName(string userName)
	{
		m_userName = userName;
	}
}
