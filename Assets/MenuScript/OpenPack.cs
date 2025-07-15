using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class OpenPack : MonoBehaviour
{
	[SerializeField] TMP_InputField userId;

	[SerializeField] PackCardView packCardView;

	[SerializeField] TextMeshProUGUI gemText;
	[SerializeField] TextMeshProUGUI goldText;

	[SerializeField] int count;
	[SerializeField] int goldOrGem;

	[System.Serializable]
	class Result
	{
		public List<int> character_ids;
	}

	class UserResult
	{
		public int gold;
		public int gem;
	}

	public void OnClick()
	{
		StartCoroutine(Request());
	}

	IEnumerator Request()
	{
		//パックを引く
		IEnumerator coroutine = HttpRequest.PostRequest(
			"open_pack.php",
			new Dictionary<string, string>(){
				{"user_id" ,userId.text},
				{"count" ,count.ToString()},
				{"gold_or_gem",goldOrGem.ToString()}
			});
		yield return StartCoroutine(coroutine);
		var result = JsonUtility.FromJson<Result>((string)coroutine.Current);

		packCardView.Reset();
		packCardView.Add(result.character_ids);

		//ガチャ引いた後にゴールドや石を反映させる
		IEnumerator userCoroutine = HttpRequest.PostRequest(
			"get_user_info.php",
			new Dictionary<string, string>()
			{
				{"user_id", userId.text }
			});
		yield return StartCoroutine(userCoroutine);
		var userResult = JsonUtility.FromJson<UserResult>((string)userCoroutine.Current);

		goldText.text = userResult.gold.ToString();
		gemText.text = userResult.gem.ToString();
	}
}