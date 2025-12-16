using UnityEngine;

/// <summary>
/// Represents the main page of the Herbarium
/// </summary>
public class HerbariumMainPage : HerbariumPage
{
    public override void GoLeft()
    {
    }

    public override void GoRight()
    {
        gui.SetPlantIndex(0);
    }

    public override void OnClose()
    {
    }

    public override void OnOpen()
    {
    }

}
