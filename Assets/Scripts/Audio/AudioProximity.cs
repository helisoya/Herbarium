using FMODUnity;
using UnityEngine;

public class AudioProximity : MonoBehaviour
{
    [SerializeField] private EventID treeA;
    [SerializeField] private EventID treeB;
    [SerializeField] private EventID bioLightA;
    [SerializeField] private EventID insectA;
    [SerializeField] private EventID insectB;
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
    //[SerializeField] private EventID musSpawn;


    public void PostTreeA(GameObject obj)
    {
        AudioManager.Instance.PlayOneShot3D(treeA, obj);
        Debug.Log("tu entres dans la zooooone de l'arbre A");
    }

    public void PostTreeB(GameObject obj)
    {
        AudioManager.Instance.PlayOneShot3D(treeB, obj);
        Debug.Log("tu entres dans la zooooone de l'arbre B");
    }

    public void PostBioLightA(GameObject obj)
    {
        AudioManager.Instance.PlayOneShot3D(bioLightA, obj);
        Debug.Log("tu entres dans la zooooone du nénuphar lumineux");
    }

    public void PostInsectA(GameObject obj)
    {
        AudioManager.Instance.PlayOneShot3D(insectA, obj);
        Debug.Log("tu entres dans la zooooone des moucherons");
    }

    public void PostInsectB(GameObject obj)
    {
        AudioManager.Instance.PlayOneShot3D(insectB, obj);
        Debug.Log("tu entres dans la zooooone des lucioles");
    }

    public void PostBirdA(GameObject obj)
    {
        AudioManager.Instance.PlayOneShot3D(birdA, obj);
        Debug.Log("tu entres dans la zooooone de l'oiseau A");
    }

    public void PostBirdB(GameObject obj)
    {
        AudioManager.Instance.PlayOneShot3D(birdB, obj);
        Debug.Log("tu entres dans la zooooone de l'oiseau B");
    }

    public void PostBirdC(GameObject obj)
    {
        AudioManager.Instance.PlayOneShot3D(birdC, obj);
        Debug.Log("tu entres dans la zooooone de l'oiseau C");
    }

    public void PostBirdD(GameObject obj)
    {
        AudioManager.Instance.PlayOneShot3D(birdD, obj);
        Debug.Log("tu entres dans la zooooone de l'oiseau D");
    }

    public void PostFrogBig(GameObject obj)
    {
        AudioManager.Instance.PlayOneShot3D(frogBig, obj);
        Debug.Log("tu entres dans la zooooone de la grosse grenouille");
    }

    public void PostFrogMedium(GameObject obj)
    {
        AudioManager.Instance.PlayOneShot3D(frogMedium, obj);
        Debug.Log("tu entres dans la zooooone de la medium grenouille");
    }

    public void PostFrogTiny(GameObject obj)
    {
        AudioManager.Instance.PlayOneShot3D(frogTiny, obj);
        Debug.Log("tu entres dans la zooooone de la tinyyy grenouille");
    }

    public void PostRiver(GameObject obj)
    {
        AudioManager.Instance.PlayOneShot3D(river, obj);
        Debug.Log("tu entres dans la zooooone de la rivière");
    }

    public void PostGrassCollision(GameObject obj)
    {
        AudioManager.Instance.PlayOneShot3D(grassCollision, obj);
    }

    public void PostEntry1(GameObject obj)
    {
        AudioManager.Instance.PlayOneShot3D(entry1, obj);
    }

    public void PostEntry2(GameObject obj)
    {
        AudioManager.Instance.PlayOneShot3D(entry2, obj);
    }

    public void PostEntry3(GameObject obj)
    {
        AudioManager.Instance.PlayOneShot3D(entry3, obj);
    }

    public void PostMusSpawn()
    {
        MusicManager.Instance.PlayMusSpawn();
    }

    public void StopMusSpawn()
    {
        MusicManager.Instance.StopMusSpawn();
    }

    public void PostMusExploration()
    {
        MusicManager.Instance.PlayMusExploration();
    }

    public void StopMusExploration()
    {
        MusicManager.Instance.FadeMusExploration(0);
    }
}
