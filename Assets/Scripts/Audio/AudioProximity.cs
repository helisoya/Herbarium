using UnityEngine;

public class AudioProximity : MonoBehaviour
{
    [SerializeField] private EventID treeA;
    [SerializeField] private EventID treeB;
    [SerializeField] private EventID birdA;
    [SerializeField] private EventID birdB;
    [SerializeField] private EventID birdC;

    public void PostTreeA(GameObject obj)
    {
        AudioManager.Instance.Play3DEvent(treeA, obj);
        Debug.Log("tu entres dans la zooooone de l'arbre A");
    }

    public void PostTreeB(GameObject obj)
    {
        AudioManager.Instance.Play3DEvent(treeB, obj);
        Debug.Log("tu entres dans la zooooone de l'arbre B");
    }

    public void PostBirdA(GameObject obj)
    {
        AudioManager.Instance.Play3DEvent(birdA, obj);
        Debug.Log("tu entres dans la zooooone de l'oiseau A");
    }

    public void PostBirdB(GameObject obj)
    {
        AudioManager.Instance.Play3DEvent(birdB, obj);
        Debug.Log("tu entres dans la zooooone de l'oiseau B");
    }

    public void PostBirdC(GameObject obj)
    {
        AudioManager.Instance.Play3DEvent(birdC, obj);
        Debug.Log("tu entres dans la zooooone de l'oiseau B");
    }
}
