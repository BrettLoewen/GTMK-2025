using UnityEngine;

[CreateAssetMenu(fileName = "BuildingData", menuName = "Scriptable Objects/BuildingData")]
public class BuildingData : ScriptableObject
{
    public string buildingName;
    public int cost;
    public Color color = new Color(0,0,0,1);
    //public Sprite sprite;

    public NeedLevel[] needsMet;

    public string GenerateInfoText()
    {
        string infoText = "";
        infoText += buildingName + "\n";
        infoText += "Cost: $" + cost + "\n";
        infoText += "Meets:\n";
        foreach (NeedLevel needLevel in needsMet)
        {
            infoText += "- " + needLevel.ToString() + "\n";
        }
        return infoText;
    }
}

public enum Need { Food, Entertainment, Fitness, Luxury }

[System.Serializable]
public class NeedLevel
{
    public Need need;
    public int level;

    public override string ToString()
    {
        return level.ToString() + " " + need.ToString();
    }
}
