using System;
using TMPro;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel = null;
    public TextMeshProUGUI shopButtonText;

    public static event Action<bool> ShopPanel;
    
    // Start is called before the first frame update
    void Start()
    {
        shopButtonText.text = "SHOP";
    }

    public void ToggleShopPanel()
    {
        shopPanel.SetActive(!shopPanel.activeSelf);
        shopButtonText.text = (shopPanel.activeSelf) ? "CLOSE" : "SHOP";
        
        ShopPanel?.Invoke(shopPanel.activeSelf);
    }
}
