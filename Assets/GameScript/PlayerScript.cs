using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
	[SerializeField] InstantiateCharacter[] instantiateCharacters;

	[SerializeField] Camera mainCamera;

	Vector3 m_currentPosition = Vector3.zero;

	void Update()
	{
		CreateCharacter();
	}

	//キャラクタを生成する
	void CreateCharacter()
	{
		for (int i = 0; i < instantiateCharacters.Length; i++)
		{
			//バーをクリックすることによって出したいキャラを選ぶ
			if (instantiateCharacters[i].GetOnClick())
			{
				Vector2 touchScreenPosition = Input.mousePosition;

				touchScreenPosition.x = Mathf.Clamp(touchScreenPosition.x, 0.0f, Screen.width);
				touchScreenPosition.y = Mathf.Clamp(touchScreenPosition.y, 0.0f, Screen.height);

				Ray touchPointToRay = mainCamera.ScreenPointToRay(touchScreenPosition);

				RaycastHit hitInfo = new RaycastHit();

				if (Physics.Raycast(touchPointToRay, out hitInfo))
				{
					m_currentPosition = hitInfo.point;

					//好きな位置でマウスをクリックするとキャラを出せる
					if (Input.GetMouseButtonDown(0))
					{
						instantiateCharacters[i].Instantiate(m_currentPosition);
					}
				}
			}
		}
	}
}
