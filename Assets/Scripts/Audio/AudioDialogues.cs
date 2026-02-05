using UnityEngine;

public class AudioDialogues : MonoBehaviour
{
    [SerializeField] private EventID dialogueIn;
    [SerializeField] private EventID dialogueOut;
    [SerializeField] private EventID dialogueNext;
    [SerializeField] private EventID speak;
    [SerializeField] private EventID startTyping;
    [SerializeField] private EventID stopTyping;


    public void PostDialogueIn()
    {
        AudioManager.Instance.PlayOneShot2D(dialogueIn);
    }
    public void PostDialogueOut()
    {
        AudioManager.Instance.PlayOneShot2D(dialogueOut);
    }
    public void PostDialogueNext()
    {
        AudioManager.Instance.PlayOneShot2D(dialogueNext);
    }

    public void PostSpeak(string speaker, string emotion)
    {
        AudioManager.Instance.PlayOneShot2D(speak);
    }

    public void PostStartTyping()
    {
        AudioManager.Instance.PlayEvent2D(startTyping);
    }

    public void StopTyping()
    {
        AudioManager.Instance.PlayEvent2D(stopTyping);
    }

}

