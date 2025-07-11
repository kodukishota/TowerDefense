using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenPack : MonoBehaviour
{
	[SerializeField] InputField userId;

	[SerializeField] PackCardView packCardView;

	[SerializeField] int count;
	[SerializeField] int goldOrGem;

	[System.Serializable]
	class Result
	{
		public List<int> card_ids;
	}

	public void OnClick()
	{
		StartCoroutine(Request());
	}

	IEnumerator Request()
	{
		IEnumerator coroutine = HttpRequest.PostRequest(
			"open_pack.php",
			new Dictionary<string, string>(){
				{"user_id" ,userId.text},
				{"count" ,count.ToString()},
				{"gold_or_gem",goldOrGem.ToString()}
			});
		yield return StartCoroutine(coroutine);
		var result = JsonUtility.FromJson<Result>((string)coroutine.Current);

		Debug.Log(result.card_ids);
	}
}