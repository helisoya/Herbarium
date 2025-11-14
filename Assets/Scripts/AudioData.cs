using UnityEngine;
using AYellowpaper.SerializedCollections;

[CreateAssetMenu(fileName = "AudioData", menuName = "Scriptable Objects/AudioData")]
public class AudioData : ScriptableObject
{
    public SerializedDictionary<EventID, FMODUnity.EventReference> events = new();
    public SerializedDictionary<ParamID, FMODUnity.ParamRef> parameters = new();
    public SerializedDictionary<BankID, FMODUnity.EditorBankRef> banks = new();
    //je ne suis pas sûre du EditorBankRef
}

public enum EventID
{

}

public enum ParamID
{

}

public enum BankID
{

}