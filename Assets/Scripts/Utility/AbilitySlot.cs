using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilitySlot : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public int slotNumber;
    private InfoBoxUIManager infoManager;
    void Start()
    {
        infoManager = GameManager.instance.GetComponent<InfoBoxUIManager>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        infoManager.ShowInfoBox("Ability " + slotNumber + " description");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        infoManager.HideInfoBox();
    }
}
