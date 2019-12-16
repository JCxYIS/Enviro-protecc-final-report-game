using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Player : MonoBehaviour {
    public int time = 0;
    public int resource;
    public int water;
    public int pollution;
    public int card203WalkCount = 0;
    public List<Card> ownedTreasure = new List<Card>();
    public List<Card> ownedRDeffect = new List<Card>();
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
        if(ownedTreasure.Count > 0)
            s += $"<i>持有寶物：{ownedTreasure.Count}</i>\n";
        if(ownedRDeffect.Count > 0)
            s += $"<i>回合效果：{ownedRDeffect.Count}</i>\n";
        playerText.text = s;

        
        if (isEnded)
        {
            kwonkwonImage.color = Color.clear;
        }
        else if(isMyTurn)
        {
            kwonkwonImage.color = originalColor;
            isMyTurn = false;
        }
        else
        {
            kwonkwonImage.color = Color.black;
        }
    }
}