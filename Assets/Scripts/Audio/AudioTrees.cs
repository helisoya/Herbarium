using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class AudioTrees : MonoBehaviour
{
    [SerializeField] EventReference tree;

    EventInstance treeInstance;
    public void OnProximityTree()
    {
        treeInstance = RuntimeManager.CreateInstance(tree);
        treeInstance.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
        treeInstance.start();
    }

    public void OnExitTree()
    {
        treeInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        treeInstance.release();
    }
}
