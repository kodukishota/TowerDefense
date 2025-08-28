using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowCastleHelth : MonoBehaviour
{
    Slider HelthGage;
	[SerializeField] CharacterScript CharacterScript;

	void Start()
	{
		HelthGage = GetComponent<Slider>();
	}

	void Update()
    {
		HelthGage.value = CharacterScript.GetHelth();
	}
}
