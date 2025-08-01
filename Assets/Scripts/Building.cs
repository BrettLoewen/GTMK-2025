using UnityEngine;

public class Building : MonoBehaviour
{
    public Transform[] entryPoints;

    private SpriteRenderer spriteRenderer;
    private BuildingData data;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Setup(BuildingData data)
    {
        this.data = data;

        spriteRenderer.color = data.color;
        name = data.buildingName;
    }
}
