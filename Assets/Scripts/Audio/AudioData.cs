using UnityEngine;
using AYellowpaper.SerializedCollections;

[CreateAssetMenu(fileName = "AudioData", menuName = "Scriptable Objects/AudioData")]
public class AudioData : ScriptableObject
{
    public SerializedDictionary<EventID, FMODUnity.EventReference> events = new();
    public SerializedDictionary<ParamID, FMODUnity.ParamRef> parameters = new();
    public SerializedDictionary<BankID, FMODUnity.EditorBankRef> banks = new();
}

public enum EventID
{
    Amb, TreeA, TreeB, River, BirdA, BirdB, BirdC, BirdD, BushA, BushB, BushC, BioLightA, BioLightB,
    Enviro2DMusic, Entry1Music3D, Entry2Music3D, Entry3Music3D,
    CollisionPlant, CollisionTree, CollisionGrass, CollisionRock, CollisionNpc,
    FrogBig, FrogMedium, FrogTiny,
}

public enum ParamID
{
    HerbariumEntries, 
}

public enum BankID
{
    Master,
}