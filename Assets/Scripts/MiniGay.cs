using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGay : MonoBehaviour
{
    [SerializeField] MainGame mainGame;
    [SerializeField] Text calamityCount;
    [SerializeField] Text treasureCount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    public void drawNext()
    {
        List<Card> cardpool = new List<Card>();
        foreach(Card c in mainGame.cards)
        {
            if(c.type != Card.Type.chance)
            {
                if(mainGame.GetCurrentPlayer().ownedTreasure)
                cardpool.Add(c);
            }
        }
        Random.Range()
    }

}
