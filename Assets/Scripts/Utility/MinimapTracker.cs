using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapTracker : MonoBehaviour
{
    private Minimap minimap;
    private Image myMarker;
    private bool onMap = false;
    private Vector3 lastSentPos;
    public enum Type {Player,Enemy};
    public Type type;
    void Start()
    {
        minimap = GameManager.instance.GetComponent<Minimap>();
    }
    void Update()
    {
        if (!onMap)
        {
            minimap.AddToMinimap(this);
            onMap = true;
        }
        if (lastSentPos != transform.position)
        {
            minimap.UpdateOnMinimap(this, myMarker);
            lastSentPos = transform.position;
        }
    }
    public void SetMarker(Image _marker)
    {
        myMarker = _marker;
    }
    private void OnDestroy()
    {
        minimap.RemoveFromMinimap(this, myMarker);
    }
}
