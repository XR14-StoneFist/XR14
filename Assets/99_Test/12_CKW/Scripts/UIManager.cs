using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
	public Canvas MainCanvas;
	public GameObject DashArrowPrefab;

	public GameObject CreateDashArrow(Vector3 objectPosition)
	{
		Vector3 screenPosition = Camera.main.WorldToScreenPoint(objectPosition);
		return Instantiate(DashArrowPrefab, screenPosition, Quaternion.identity, MainCanvas.transform);
	}
}
