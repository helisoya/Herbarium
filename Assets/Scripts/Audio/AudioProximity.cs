using UnityEngine;

public class AudioProximity : MonoBehaviour
{
    [SerializeField] private EventID treeA;
    [SerializeField] private EventID treeB;
    [SerializeField] private EventID birdA;
    [SerializeField] private EventID birdB;
    [SerializeField] private EventID birdC;

    public void PostTreeA()
    {
        AudioManager.Instance.Play3DEvent(treeA);
        Debug.Log("tu entres dans la zooooone de l'arbre A");
    }

    public void PostTreeB()
    {
        AudioManager.Instance.Play3DEvent(treeB);
        Debug.Log("tu entres dans la zooooone de l'arbre B");
    }

    public void PostBirdA()
    {
        AudioManager.Instance.Play3DEvent(birdA);
        Debug.Log("tu entres dans la zooooone de l'oiseau A");
    }

    public void PostBirdB()
    {
        AudioManager.Instance.Play3DEvent(birdB);
        Debug.Log("tu entres dans la zooooone de l'oiseau B");
    }

    public void PostBirdC()
    {
        AudioManager.Instance.Play3DEvent(birdC);
        Debug.Log("tu entres dans la zooooone de l'oiseau B");
    }
}
