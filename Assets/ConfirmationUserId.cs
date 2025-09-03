using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConfirmationUserId : MonoBehaviour
{
	[SerializeField] UserInfo UserInfo;
	[SerializeField] GetUserInfo GetUserInfo;

	[SerializeField] GameObject AddUserScreen;
	[SerializeField] TMP_InputField UserId;

    // Start is called before the first frame update
    void Start()
    {
		if (UserInfo.data.m_id == 0)
		{
			AddUserScreen.SetActive(true);
		}
		else
		{
			AddUserScreen.SetActive(true);

			UserId.text = UserInfo.data.m_id.ToString();

			GetUserInfo.OnClick();
		}
	}
}
