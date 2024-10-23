using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgressGauge : MonoBehaviour
{
    [SerializeField] 
    private GameObject targetObj;
    
    [SerializeField]
    private bool isplayer;
    
    private float maxheight = 326f;
    private float maxUiheight = 330f;
    private RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
       if (isplayer)
        {
            playerfucknode();
        }
        else
        {
            darknessfucknode();
        }

    }

    void playerfucknode()
    {
        float playerY = targetObj.transform.position.y / maxheight;

        rectTransform.anchoredPosition = new Vector2(-2f, 36 + playerY * maxUiheight);
    }

    void darknessfucknode()
    {
        float playerY = targetObj.transform.position.y / maxheight;

        Image image = GetComponent<Image>();

        image.fillAmount = playerY;
    }
}
