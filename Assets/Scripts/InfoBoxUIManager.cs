using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoBoxUIManager : MonoBehaviour
{
    public GameObject infoBox;
    public TextMeshProUGUI infoText;
    public void ShowInfoBox(string _text)
    {
        infoBox.SetActive(true);
        infoText.text = _text;
    }
    public void HideInfoBox()
    {
        infoBox.SetActive(false);
        infoText.text = "Shouldnt see this";
    }
}
