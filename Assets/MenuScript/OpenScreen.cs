using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenScreen : MonoBehaviour
{
	[SerializeField] GameObject screen;

	public void OnClick()
	{
		screen.SetActive(true);
	}
}
