using UnityEngine;

public class AudioDialogues : MonoBehaviour
{
    [SerializeField] private EventID DialogueIn;
    [SerializeField] private EventID DialogueOut;
    [SerializeField] private EventID DialogueNext;
    [SerializeField] private EventID Speak;


    public void PostDialogueIn()
    {
        AudioManager.Instance.PlayOneShot2D(DialogueIn);
    }
    public void PostDialogueOut()
    {
        AudioManager.Instance.PlayOneShot2D(DialogueOut);
    }
    public void PostDialogueNext()
    {
        AudioManager.Instance.PlayOneShot2D(DialogueNext);
    }

    public void PostSpeak()
    {
        AudioManager.Instance.PlayOneShot2D(Speak);
    }

}

