using UnityEngine;

public class AudioProximity : MonoBehaviour
{
    [SerializeField] private EventID treeA;
    [SerializeField] private EventID treeB;
    [SerializeField] private EventID birdA;
    [SerializeField] private EventID birdB;
    [SerializeField] private EventID birdC;
    [SerializeField] private EventID birdD;
    [SerializeField] private EventID frogBig;
    [SerializeField] private EventID frogMedium;
    [SerializeField] private EventID frogTiny;
    [SerializeField] private EventID river;
    [SerializeField] private EventID grassCollision;
    [SerializeField] private EventID entry1;
    [SerializeField] private EventID entry2;
    [SerializeField] private EventID entry3;


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
        Debug.Log("tu entres dans la zooooone de l'oiseau C");
    }

    public void PostBirdD(GameObject obj)
    {
        AudioManager.Instance.Play3DEvent(birdD, obj);
        Debug.Log("tu entres dans la zooooone de l'oiseau D");
    }

    public void PostFrogBig(GameObject obj)
    {
        AudioManager.Instance.Play3DEvent(frogBig, obj);
        Debug.Log("tu entres dans la zooooone de la grosse grenouille");
    }

    public void PostFrogMedium(GameObject obj)
    {
        AudioManager.Instance.Play3DEvent(frogMedium, obj);
        Debug.Log("tu entres dans la zooooone de la medium grenouille");
    }

    public void PostFrogTiny(GameObject obj)
    {
        AudioManager.Instance.Play3DEvent(frogTiny, obj);
        Debug.Log("tu entres dans la zooooone de la tinyyy grenouille");
    }

    public void PostRiver(GameObject obj)
    {
        AudioManager.Instance.Play3DEvent(river, obj);
        Debug.Log("tu entres dans la zooooone de la rivière");
    }

    public void PostGrassCollision(GameObject obj)
    {
        AudioManager.Instance.Play3DEvent(grassCollision, obj);
        Debug.Log("je marche sur l'herbe oui oui");
    }

    public void PostEntry1(GameObject obj)
    {
        AudioManager.Instance.Play3DEvent(entry1, obj);
        Debug.Log("MUSIQUE ENTRY 1");
    }

    public void PostEntry2(GameObject obj)
    {
        AudioManager.Instance.Play3DEvent(entry2, obj);
        Debug.Log("MUSIQUE ENTRY 2");
    }

    public void PostEntry3(GameObject obj)
    {
        AudioManager.Instance.Play3DEvent(entry3, obj);
        Debug.Log("MUSIQUE ENTRY 2");
    }
}
