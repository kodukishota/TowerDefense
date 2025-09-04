using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	void Start()
	{
		Invoke("OnDestroy", 1.0f);
	}
	private void OnDestroy()
	{
		Destroy(this.gameObject);
	}
}
