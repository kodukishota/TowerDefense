using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HttpRequest
{
	const string RequestDomain = "127.0.0.1/tower_defense/";

	// POST���N�G�X�g
	public static IEnumerator PostRequest(string url, Dictionary<string, string> posts)
	{
		// POST�p�����[�^��o�^
		WWWForm form = new WWWForm();
		foreach (var post in posts)
		{
			form.AddField(post.Key, post.Value);
		}

		// ���N�G�X�g���M
		UnityWebRequest unityWebRequest = UnityWebRequest.Post(RequestDomain + url, form);
		yield return unityWebRequest.SendWebRequest();

		// ����������
		if (unityWebRequest.result != UnityWebRequest.Result.Success)
		{
			// ���s
			Debug.Log(unityWebRequest.error);
			yield break;
		}

		Debug.Log(url + ": " + unityWebRequest.downloadHandler.text);
		yield return unityWebRequest.downloadHandler.text;
	}
}
