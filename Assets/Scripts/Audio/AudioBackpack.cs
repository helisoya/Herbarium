using UnityEngine;

public class AudioBackpack : MonoBehaviour
{
    [SerializeField] private EventID backpackOpen;
    [SerializeField] private EventID backpackClose;
    [SerializeField] private EventID backpackHover;
    [SerializeField] private EventID backpackClick;
    [SerializeField] private EventID backpackBack;
    [SerializeField] private EventID inventoryOpen;
    [SerializeField] private EventID inventoryClose;
    [SerializeField] private EventID inventoryHover;
    


    public void PostBackpackOpen()
    {
        AudioManager.Instance.PlayOneShot2D(backpackOpen);
    }

    public void PostBackpackClose()
    {
        AudioManager.Instance.PlayOneShot2D(backpackClose);
    }

    public void PostBackpackHover()
    {
        AudioManager.Instance.PlayOneShot2D(backpackHover);
    }

    public void PostBackpackClick()
    {
        AudioManager.Instance.PlayOneShot2D(backpackClick);
    }

    public void PostBackpackBack()
    {
        AudioManager.Instance.PlayOneShot2D(backpackBack);
    }

    public void PostInventoryOpen()
    {
        AudioManager.Instance.PlayOneShot2D(inventoryOpen);
    }

    public void PostInventoryClose()
    {
        AudioManager.Instance.PlayOneShot2D(inventoryClose);
    }

    

}
