using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetParam : MonoBehaviour
{
    public MainGame mainGame;
    public InputField[] input;

    
    public void InitPanel()
    {
        foreach(InputField iff in input)
        {
            iff.text = "0";
        }
        gameObject.SetActive(true);
    }

    public void OnDone()
    {
        Player p = mainGame.GetCurrentPlayer();
        try
        {
            p.time += int.Parse(input[0].text);
            p.resource += int.Parse(input[1].text);
            p.water += int.Parse(input[2].text);
            p.pollution += int.Parse(input[3].text);
        }
        catch{}
        Debug.Log("已微調參數");
        gameObject.SetActive(false);
    }
}
