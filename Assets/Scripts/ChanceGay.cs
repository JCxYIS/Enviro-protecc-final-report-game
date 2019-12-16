using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChanceGay : MonoBehaviour
{
    [SerializeField] MainGame mainGame;
    [SerializeField] Image cardImg;
    [SerializeField] Transform cardBack;
    Card drawedCard;
    bool isInDrawAnim = false;


    public void StartGame()
    {
        isInDrawAnim = false;
        gameObject.SetActive(true);
        DrawNext();
    }

    public void DrawNext()
    {
        List<Card> cardpool = new List<Card>();
        foreach(Card c in mainGame.cards)
        {
            if(c.type == Card.Type.chance)
            {
                cardpool.Add(c);
            }
        }
        int getCard = Random.Range(0, cardpool.Count);
        drawedCard = cardpool[getCard];
        
        cardImg.sprite = drawedCard.sprite;
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

        isInDrawAnim = false;
    }
    public void Exit(bool writeTreasure)
    {
        if(writeTreasure)
        {
            if(drawedCard.id == 201)
            {
                mainGame.GetCurrentPlayer().time += 1;
            }
            else if(drawedCard.id == 202)
            {
                mainGame.GetCurrentPlayer().ownedRDeffect.Add(drawedCard);
            }
            else if(drawedCard.id == 203)
            {
                mainGame.GetCurrentPlayer().ownedTreasure.Add(drawedCard);
            }
            else if(drawedCard.id == 204)
            {
                mainGame.GetCurrentPlayer().time += 5;
                mainGame.GetCurrentPlayer().ownedTreasure.Add(drawedCard);
                mainGame.GetCurrentPlayer().resource -= 15;
            }
        }
        gameObject.SetActive(false);
    }
}
