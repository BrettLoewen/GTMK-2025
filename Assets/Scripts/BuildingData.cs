using UnityEngine;

[CreateAssetMenu(fileName = "BuildingData", menuName = "Scriptable Objects/BuildingData")]
public class BuildingData : ScriptableObject
{
    public string buildingName;
    public int cost;
    public Color color = new Color(0,0,0,1);
    //public Sprite sprite;
}
