using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GetUserInfo : MonoBehaviour
{
	[SerializeField] TMP_InputField userId;

	[SerializeField] TextMeshProUGUI gemText;
	[SerializeField] TextMeshProUGUI goldText;

	[SerializeField] TextMeshProUGUI displayUesrId;
	[SerializeField] TextMeshProUGUI displayUesrName;

	[SerializeField] GameObject AddUserScreen;

	[System.Serializable]
	class Result
	{
		public string name;
		public int gem;
		public int gold;
		public List<int> cards;
	}

	public void OnClick()
	{
		StartCoroutine(Request());
	}

	IEnumerator Request()
	{
		IEnumerator coroutine = HttpRequest.PostRequest(
			"get_user_info.php",
			new Dictionary<string, string>()
			{
				{"user_id", userId.text }
			});
		yield return StartCoroutine(coroutine);
		var result = JsonUtility.FromJson<Result>((string)coroutine.Current);

		Debug.Log(result.name);

		displayUesrName.text += result.name;
		displayUesrId.text += userId.text;
		gemText.text += result.gem;
		goldText.text += result.gold;

		AddUserScreen.SetActive(false);
	}
}
