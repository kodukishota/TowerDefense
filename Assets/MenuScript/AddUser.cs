using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddUser : MonoBehaviour
{
	[SerializeField] TMP_InputField userName;

	//[SerializeField] GameObject panel;

	[SerializeField] TMP_InputField userId;

	[System.Serializable]
	class Result
	{
		public int user_id;
	}

	public void OnClick()
	{
		StartCoroutine(Request());
	}

	IEnumerator Request()
	{
		IEnumerator coroutine = HttpRequest.PostRequest(
			"add_user.php",
			new Dictionary<string, string>(){
				{"name" ,userName.text}
			});
		yield return StartCoroutine(coroutine);
		var result = JsonUtility.FromJson<Result>((string)coroutine.Current);

		Debug.Log(result.user_id);

		userId.text = result.user_id.ToString();
	}
}
