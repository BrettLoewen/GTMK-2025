// Interface used for objects that can be hovered or selected to view info in the side panel
public interface ISelectable
{
    public string name { get; set; }
    public string GetInfoText();
}
