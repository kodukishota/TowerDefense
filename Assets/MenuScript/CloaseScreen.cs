using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloaseScreen : MonoBehaviour
{
	[SerializeField] GameObject screen;

	public void OnClick()
	{
		screen.SetActive(false);
	}
}
