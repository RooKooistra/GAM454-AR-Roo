using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel = null;
    public TextMeshProUGUI shopButtonText;
    
    // Start is called before the first frame update
    void Start()
    {
        shopButtonText.text = "SHOP";
    }

    public void ToggleShopPanel()
    {
        shopPanel.SetActive(!shopPanel.activeSelf);
        shopButtonText.text = (shopPanel.activeSelf) ? "CLOSE" : "SHOP";
    }
}
