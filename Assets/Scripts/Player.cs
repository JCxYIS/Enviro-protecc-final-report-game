using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    public float resource;
    public float water;
    public float pollution;
    [SerializeField] Text playerText;
    public void UpdateText()
    {
        string s = "";
        s += $"<color=black>資源 -{resource}</color>\n";
        s += $"<color=blue>水源 -{water}</color>\n";
        s += $"<color=purple>汙染 -{pollution}</color>\n";
        playerText.text = s;
    }
}