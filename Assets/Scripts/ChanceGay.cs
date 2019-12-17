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
            Player p = mainGame.GetCurrentPlayer();
            if(drawedCard.id == 201)
            {
                p.time += 1;
            }
            else if(drawedCard.id == 202)
            {
                p.ownedRDeffect.Add(drawedCard);
            }
            else if(drawedCard.id == 203)
            {
                p.ownedTreasure.Add(drawedCard);
            }
            else if(drawedCard.id == 204)
            {
                p.time += 5;
                p.ownedTreasure.Add(drawedCard);
                p.resource -= 15;
            }
            else if(drawedCard.id == 205)
            {
                p.pollution += 100;
                p.water += 100;
                p.resource += 100;
                mainGame.AllPlayerHasEnded();
            }
            Debug.Log($"Used card {drawedCard.id}");
        }
        gameObject.SetActive(false);
    }
}
