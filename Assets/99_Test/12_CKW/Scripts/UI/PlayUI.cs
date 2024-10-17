using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayUI : MonoBehaviour
{
    [SerializeField] private Image[] healths;
    [SerializeField] private Sprite healthOn;
    [SerializeField] private Sprite healthOff;

    private void Update()
    {
        UpdateHealthSprite();
    }

    private void UpdateHealthSprite()
    {
        healths[0].sprite = GameManager.Instance.Health >= 1 ? healthOn : healthOff;
        healths[1].sprite = GameManager.Instance.Health >= 2 ? healthOn : healthOff;
        healths[2].sprite = GameManager.Instance.Health >= 3 ? healthOn : healthOff;
    }
}
