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

	//�L�����N�^�𐶐�����
	void CreateCharacter()
	{
		for (int i = 0; i < instantiateCharacters.Length; i++)
		{
			//�o�[���N���b�N���邱�Ƃɂ���ďo�������L������I��
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

					//�D���Ȉʒu�Ń}�E�X���N���b�N����ƃL�������o����
					if (Input.GetMouseButtonDown(0))
					{
						instantiateCharacters[i].Instantiate(m_currentPosition);
					}
				}
			}
		}
	}
}
