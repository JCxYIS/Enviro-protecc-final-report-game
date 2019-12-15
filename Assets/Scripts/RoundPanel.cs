using UnityEngine;
using UnityEngine.UI;

public class RoundPanel : MonoBehaviour {
    [SerializeField] Text[] valueText;
    [SerializeField] Text rdText;
    [SerializeField] MainGame mainGame;
    const int deltaSpeed = 50; // how many frames?
    float[] delta = new float[4];
    float[] values = new float[4];
    int frameCount = 0;



    void FixedUpdate()
    {
        if(frameCount < deltaSpeed)
        {
            for(int i = 0; i < 4; i++)
            {
                values[i] += delta[i];
            }
            frameCount++;
        }

        for(int i = 0; i < 4; i++)
        {
            valueText[i].text = values[i].ToString("0");
        }
    }

    public void SetValues(int walk, int res, int wat, int pol, int rd)
    {
        values[0] = walk;
        values[1] = res;
        values[2] = wat;
        values[3] = pol;

        for(int i = 0; i < 4; i++)
        {
            delta[i] = values[i] / deltaSpeed;
            values[i] = 0;
        }
        frameCount = 0;

        rdText.text = $"ROUND {rd.ToString("00")}";
        gameObject.SetActive(true);
    }

    public void EndRound()
    {
        mainGame.RoundEnd();
        gameObject.SetActive(false);
    }
    public void StartMiniGame()
    {
        throw new System.Exception();
    }
    public void PlayerArrived()
    {
        throw new System.Exception();
    }

}