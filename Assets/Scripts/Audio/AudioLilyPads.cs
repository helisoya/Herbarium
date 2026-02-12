using UnityEngine;

public class AudioLilyPads : MonoBehaviour
{
    [SerializeField] EventID lilypadEnter;
    [SerializeField] EventID lilypadExit;

    public void OnLilypadEnter()
    {
        AudioManager.Instance.PlayOneShot3D(lilypadEnter, gameObject);
    }

    public void OnLilypadExit()
    {
        AudioManager.Instance.PlayOneShot3D(lilypadExit, gameObject);
    }
    public void OnLilyPadSectionEnter()
    {
        AudioManager.Instance.SetGlobalParameterByName("Material", 1);
    }

    public void OnLilyPadSectionExit()
    {
        AudioManager.Instance.SetGlobalParameterByName("Material", 0);
    }

}
