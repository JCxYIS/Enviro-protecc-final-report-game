using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGay : MonoBehaviour
{
    [SerializeField] MainGame mainGame;
    [SerializeField] Text calamityCountText;
    [SerializeField] Text treasureCountText;
    [SerializeField] Image cardImg;
    [SerializeField] Transform cardBack;
    [SerializeField] CanvasGroup gameoverText;
    Card thisRoundDrawCard;
    List<Card> obtainedTreasure = new List<Card>();
    int calamityCount = 0;
    bool isInDrawAnim = false;



    // Update is called once per frame
    void Update()
    {
        calamityCountText.text = calamityCount.ToString("0");
        treasureCountText.text = obtainedTreasure.Count.ToString("0");
    }

    public void StartGame()
    {
        isInDrawAnim = false;
        gameObject.SetActive(true);
        gameoverText.alpha = 0;
        obtainedTreasure = new List<Card>();
        calamityCount = 0;
        DrawNext();
    }

    public void DrawNext()
    {
        List<Card> cardpool = new List<Card>();
        foreach(Card c in mainGame.cards)
        {
            if(c.type != Card.Type.chance)
            {
                if(!mainGame.GetCurrentPlayer().ownedTreasure.Contains(c) && !obtainedTreasure.Contains(c))
                    cardpool.Add(c);
            }
        }
        int getCard = Random.Range(0, cardpool.Count);
        thisRoundDrawCard = cardpool[getCard];
        
        cardImg.sprite = thisRoundDrawCard.sprite;
        cardBack.localScale = Vector3.one;
    }

    public void OnDraw()
    {
        if(!isInDrawAnim)
            StartCoroutine(DrawAnim());
    }
    IEnumerator DrawAnim()
    {
        isInDrawAnim = true;
        float s = 1;
        while(s >= 0)
        {
            cardBack.transform.localScale = new Vector3(1,s,1);
            s -= 0.025f;
            yield return new WaitForSeconds(0.01f);
        }
        cardBack.transform.localScale = new Vector3(0,0,1);

        if(thisRoundDrawCard.type == Card.Type.clamity)
            calamityCount++;
        else
            obtainedTreasure.Add(thisRoundDrawCard);

        if(calamityCount >= 3)
        {
            while(s < 1)
            {
                s += 0.015f;
                gameoverText.alpha = s;
                yield return new WaitForSeconds(0.01f);
            }
            yield return new WaitForSeconds(1f);
            Exit(false);
        }

        while(true)
        {
            if(Input.anyKeyDown)
                break;
            yield return 1;
        }
        DrawNext();
        isInDrawAnim = false;
    }
    public void Exit(bool writeTreasure)
    {
        if(writeTreasure)
            mainGame.GetCurrentPlayer().ownedTreasure.AddRange(obtainedTreasure);
        gameObject.SetActive(false);
    }
}
