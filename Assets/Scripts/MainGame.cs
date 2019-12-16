using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class MainGame : MonoBehaviour
{
    public Player[] player = new Player[3];
    public Transportation[] transportations = new Transportation[4];
    public Card[] cards;
    private int currentRound = 1;
    private int currentPlayer = 0;
    public RoundPanel roundPanel;
    public GameObject gameoverPanel;
    public Animator charaAnim;



    // Start is called before the first frame update
    void Start()
    {
        roundPanel.gameObject.SetActive(false);
        gameoverPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Transportation trans in transportations)
        {
            trans.UpdateText();
        }
        player[currentPlayer].isMyTurn = true;
    }

    public void ChooseTransportation(int order)
    {
        Transportation usetrans = transportations[order];
        Player cplayer = player[currentPlayer];
        int move = usetrans.PickRandom(usetrans.canMove);
        int res = usetrans.PickRandom(usetrans.addResource);
        int water = usetrans.PickRandom(usetrans.addWater);
        int pollute = usetrans.PickRandom(usetrans.addPollute);

        cplayer.resource += res;
        cplayer.water += water;
        cplayer.pollution += pollute;
        cplayer.time += 1;
        
        roundPanel.SetValues(move, res, water, pollute, currentRound);

        Debug.Log($"Player {currentPlayer} => USE={order} | MOVE={move} | RES={res} WATER={water} POLLUTE={pollute}");
    }

    /// <summary>
    /// call by RoundPanel
    /// </summary>
    public void RoundEnd()
    {       
        int passedCombo = 0;
        while(true)
        {
            currentPlayer++;            
            if(currentPlayer >= player.Length)
            {
                currentPlayer = 0;
                currentRound++;
                Debug.Log($"=== Round {currentRound} ===");
            }

            if(!player[currentPlayer].isEnded)
            {
                break;
            }

            passedCombo++;
            if(passedCombo >= 3)
            {
                Debug.Log("Game Has Ended!");
                gameoverPanel.SetActive(true);
                charaAnim.Play("CHARA_END");
                return;
            }
        }
        Debug.Log($"- Player {currentPlayer}'s phase -");
    }
    public Player GetCurrentPlayer()
    {
        return player[currentPlayer];
    }
    public void ThisPlayerRideBus()
    {
        player[currentPlayer].time += 2;
        Debug.Log($"Player {currentPlayer} decided to ride the bus!");
    }
    public void ThisPlayerHasArrived()
    {
        player[currentPlayer].isEnded = true;
        Debug.Log($"- Player {currentPlayer} has ended! -");
    }



    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }

}


[Serializable]
public class Transportation
{
    [Serializable]
    public struct RandomFormat
    {
        public int less;
        public int most;
    }
    public RandomFormat canMove;
    public RandomFormat addResource;
    public RandomFormat addWater;
    public RandomFormat addPollute;
    [SerializeField] Text transText; 
    public void UpdateText()
    {
        string s = "";
        s += $"-<color=green>可移動 <b>{canMove.less}</b>~<b>{canMove.most}</b> 格</color>-\n";
        s += $"<color=gray>消耗 <b>1</b> 分鐘</color>\n";
        s += $"<color=black>資源<b>－{addResource.less}</b>~<b>{addResource.most}</b></color>\n";
        s += $"<color=blue>水源<b>－{addWater.less}</b>~<b>{addWater.most}</b></color>\n";
        s += $"<color=purple>汙染<b>＋{addPollute.less}</b>~<b>{addPollute.most}</b></color>\n";
        transText.text = s;
    }
    public int PickRandom(RandomFormat rand)
    {
        return UnityEngine.Random.Range(rand.less, rand.most+1);
    }
}


[Serializable]
public class Card 
{
    public enum Type {clamity, treasure, chance};
    public int id;
    public Type type;
    public Sprite sprite;
}
