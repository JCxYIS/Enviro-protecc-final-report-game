using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Player : MonoBehaviour {
    public int time = 40;
    public int resource;
    public int water;
    public int pollution;
    public List<Card> ownedTreasure = new List<Card>();
    [SerializeField] Text playerText;  

    [HideInInspector] public bool isMyTurn;
    [HideInInspector] public bool isEnded;
    Color originalColor;
    Image kwonkwonImage;


    void Start()
    {
        kwonkwonImage = transform.Find("FrameLight").GetComponent<Image>();
        originalColor = kwonkwonImage.color;
    }
    void Update()
    {
        string s = "";
        s += $"<color=gray>已過 <b>{time}</b> 分鐘</color>\n";
        s += $"<color=black>資源<b>－{resource}</b></color>\n";
        s += $"<color=blue>水源<b>－{water}</b></color>\n";
        s += $"<color=magenta>汙染<b>＋{pollution}</b></color>\n";
        playerText.text = s;

        
        if (isEnded)
        {
            kwonkwonImage.color = Color.white;
        }
        else if(isMyTurn)
        {
            kwonkwonImage.color = originalColor;
            isMyTurn = false;
        }
        else
        {
            kwonkwonImage.color = Color.clear;
        }
    }
}