using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    public int time = 40;
    public int resource;
    public int water;
    public int pollution;
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
        s += $"<color=gray>剩餘 {time} 分鐘</color>\n";
        s += $"<color=black>資源 -{resource}</color>\n";
        s += $"<color=blue>水源 -{water}</color>\n";
        s += $"<color=magenta>汙染 -{pollution}</color>\n";
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
            kwonkwonImage.color = Color.gray;
        }
    }
}