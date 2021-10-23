using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPanelController : MonoBehaviour
{
    public GameObject[] itemPanels;

    public void SwitchPanels(int panelToShow)
    {
        for(int i = 0; i < itemPanels.Length; i++)
        {
            itemPanels[i].SetActive(i == panelToShow);
        }
    }
}
