using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class LoadOptions : MonoBehaviour
{
    public GameObject currentPanel;
    public GameObject panelToLoad;
    public EventSystem eventSystem;
    public void ShowPanel()
    {
        panelToLoad.SetActive(true);
        foreach (Transform ui in panelToLoad.transform)
        {
            if (ui.GetComponent<Button>() != null)
            {
                eventSystem.SetSelectedGameObject(ui.gameObject);
                break;
            }
        }
        currentPanel.SetActive(false);
    }
}
