using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class MainGame : MonoBehaviour
{
    public Player[] player = new Player[3];
    public Transportation[] transportations = new Transportation[4];
    private int currentRound = 1;
    private int currentPlayer = 0;
    public RoundPanel roundPanel;



    // Start is called before the first frame update
    void Start()
    {
        roundPanel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Transportation trans in transportations)
        {
            trans.UpdateText();
        }
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
        
        roundPanel.SetValues(move, res, water, pollute, currentRound);

        Debug.Log($"Player {currentPlayer} => USE={order} | MOVE={move} | RES={res} WATER={water} POLLUTE={pollute}");
    }

    /// <summary>
    /// call by RoundPanel
    /// </summary>
    public void RoundEnd()
    {       
        currentPlayer++;
        if(currentPlayer >= player.Length)
        {
            currentPlayer = 0;
            currentRound++;
            Debug.Log($"=== Round {currentRound} ===");
        }
        Debug.Log($"- Player {currentPlayer}'s phase -");
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
        s += $"-<color=green>可移動 {canMove.less}~{canMove.most} 格</color>-\n";
        s += $"<color=black>資源 -{addResource.less}~{addResource.most}</color>\n";
        s += $"<color=blue>水源 -{addWater.less}~{addWater.most}</color>\n";
        s += $"<color=purple>汙染 -{addPollute.less}~{addPollute.most}</color>\n";
        transText.text = s;
    }
    public int PickRandom(RandomFormat rand)
    {
        return UnityEngine.Random.Range(rand.less, rand.most+1);
    }
}
