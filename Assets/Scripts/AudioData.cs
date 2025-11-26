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
    Amb, TreeTypeA, TreeTypeB, River, BirdTypeA, BirdTypeB
}

public enum ParamID
{

}

public enum BankID
{
    Master,
}