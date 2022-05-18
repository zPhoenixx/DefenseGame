using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{
    private List<MinimapTracker> objects = new List<MinimapTracker>();
    private List<Image> markers = new List<Image>();

    public Terrain terrain;
    public RectTransform minimapPanel;
    private float terrainWidth;
    private float terrainHeight;
    private float minimapWidth;
    private float minimapHeight;

    public GameObject minimapMarker;
    private void Start()
    {
        terrainWidth = terrain.terrainData.size.x;
        terrainHeight = terrain.terrainData.size.z;
        Debug.Log("Terrain : " + terrainWidth + "," + terrainHeight);
        minimapWidth = minimapPanel.rect.width;
        minimapHeight = minimapPanel.rect.height;
        Debug.Log("Minimap : " + minimapWidth + "," + minimapHeight);
    }
    void Update()
    {

    }
    public void AddToMinimap(MinimapTracker _object)
    {
        objects.Add(_object);
        Image _marker = Instantiate(minimapMarker, minimapPanel.transform).GetComponent<Image>();
        if (_object.type == MinimapTracker.Type.Player)
        {
            _marker.color = Color.green;
        }
        if (_object.type == MinimapTracker.Type.Enemy)
        {
            _marker.color = Color.red;
        }
        _object.SetMarker(_marker);
        UpdateOnMinimap(_object, _marker);
    }
    public void RemoveFromMinimap(MinimapTracker _object, Image _marker)
    {
        objects.Remove(_object);
        markers.Remove(_marker);
        Destroy(_marker.gameObject);
    }
    public void UpdateOnMinimap(MinimapTracker _object, Image _marker)
    {
        float objectX = _object.transform.position.x - terrain.transform.position.x;
        float objectZ = _object.transform.position.z - terrain.transform.position.z;
        float ratioX = terrainWidth / minimapWidth;
        float ratioZ = terrainHeight / minimapHeight;
        float coordX = objectX / ratioX;
        float coordZ = objectZ / ratioZ;
        _marker.transform.localPosition = new Vector3(coordX, coordZ, 0);
    }

}
