using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HttpRequest
{
	const string RequestDomain = "127.0.0.1/tower_defense/";

	// POSTリクエスト
	public static IEnumerator PostRequest(string url, Dictionary<string, string> posts)
	{
		// POSTパラメータを登録
		WWWForm form = new WWWForm();
		foreach (var post in posts)
		{
			form.AddField(post.Key, post.Value);
		}

		// リクエスト送信
		UnityWebRequest unityWebRequest = UnityWebRequest.Post(RequestDomain + url, form);
		yield return unityWebRequest.SendWebRequest();

		// 成功したか
		if (unityWebRequest.result != UnityWebRequest.Result.Success)
		{
			// 失敗
			Debug.Log(unityWebRequest.error);
			yield break;
		}

		Debug.Log(url + ": " + unityWebRequest.downloadHandler.text);
		yield return unityWebRequest.downloadHandler.text;
	}
}
