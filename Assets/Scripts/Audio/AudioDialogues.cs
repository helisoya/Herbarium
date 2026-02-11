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
        switch (speaker)
        {
            case "Nour":

                AudioManager.Instance.SetGlobalParameterByName("Character", 1);

                switch (emotion)
                {
                    case "Hi":
                        AudioManager.Instance.SetGlobalParameterByName("Emotion", 0);
                        AudioManager.Instance.PlayOneShot2D(speak);
                        Debug.Log("Barbrok says hi");
                        break;

                    case "Excited":
                        AudioManager.Instance.SetGlobalParameterByName("Emotion", 1);
                        AudioManager.Instance.PlayOneShot2D(speak);
                        break;

                    case "Question":
                        AudioManager.Instance.SetGlobalParameterByName("Emotion", 2);
                        AudioManager.Instance.PlayOneShot2D(speak);
                        break;

                    case "Frustration":
                        AudioManager.Instance.SetGlobalParameterByName("Emotion", 3);
                        AudioManager.Instance.PlayOneShot2D(speak);
                        break;

                    case "Impressed":
                        AudioManager.Instance.SetGlobalParameterByName("Emotion", 4);
                        AudioManager.Instance.PlayOneShot2D(speak);
                        break;

                    case "Menacing":
                        AudioManager.Instance.SetGlobalParameterByName("Emotion", 5);
                        AudioManager.Instance.PlayOneShot2D(speak);
                        break;

                    case "Content":
                        AudioManager.Instance.SetGlobalParameterByName("Emotion", 6);
                        AudioManager.Instance.PlayOneShot2D(speak);
                        break;

                    case "Pensive":
                        AudioManager.Instance.SetGlobalParameterByName("Emotion", 7);
                        AudioManager.Instance.PlayOneShot2D(speak);
                        break;

                    case "Laugh":
                        AudioManager.Instance.SetGlobalParameterByName("Emotion", 8);
                        AudioManager.Instance.PlayOneShot2D(speak);
                        break;

                    case "Goodbye":
                        AudioManager.Instance.SetGlobalParameterByName("Emotion", 9);
                        AudioManager.Instance.PlayOneShot2D(speak);
                        break;


                    case "NoSpeak":
                        break;
                }

                break;

            case "Barbrook":

                AudioManager.Instance.SetGlobalParameterByName("Character", 2);

                switch (emotion)
                {
                    case "Hi":
                        AudioManager.Instance.SetGlobalParameterByName("Emotion", 0);
                        AudioManager.Instance.PlayOneShot2D(speak);
                        break;

                    case "Excited":
                        AudioManager.Instance.SetGlobalParameterByName("Emotion", 1);
                        AudioManager.Instance.PlayOneShot2D(speak);
                        break;

                    case "Question":
                        AudioManager.Instance.SetGlobalParameterByName("Emotion", 2);
                        AudioManager.Instance.PlayOneShot2D(speak);
                        break;

                    case "Frustration":
                        AudioManager.Instance.SetGlobalParameterByName("Emotion", 3);
                        AudioManager.Instance.PlayOneShot2D(speak);
                        break;

                    case "Impressed":
                        AudioManager.Instance.SetGlobalParameterByName("Emotion", 4);
                        AudioManager.Instance.PlayOneShot2D(speak);
                        break;

                    case "Menacing":
                        AudioManager.Instance.SetGlobalParameterByName("Emotion", 5);
                        AudioManager.Instance.PlayOneShot2D(speak);
                        break;

                    case "Content":
                        AudioManager.Instance.SetGlobalParameterByName("Emotion", 6);
                        AudioManager.Instance.PlayOneShot2D(speak);
                        break;

                    case "Pensive":
                        AudioManager.Instance.SetGlobalParameterByName("Emotion", 7);
                        AudioManager.Instance.PlayOneShot2D(speak);
                        break;

                    case "Laugh":
                        AudioManager.Instance.SetGlobalParameterByName("Emotion", 8);
                        AudioManager.Instance.PlayOneShot2D(speak);
                        break;

                    case "Goodbye":
                        AudioManager.Instance.SetGlobalParameterByName("Emotion", 9);
                        AudioManager.Instance.PlayOneShot2D(speak);
                        break;

                    case "IntroPicnic":
                        AudioManager.Instance.SetGlobalParameterByName("PicNic", 0);
                        break;

                    case "StartPicnic":
                        AudioManager.Instance.SetGlobalParameterByName("PicNic", 1);
                        break;

                    case "EnjoyPicnic":
                        AudioManager.Instance.SetGlobalParameterByName("PicNic", 2);
                        break;

                    case "EndPicnic":
                        AudioManager.Instance.SetGlobalParameterByName("PicNic", 3);
                        break;

                    case "NoSpeak":
                        break;
                }

                break;

        }
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

