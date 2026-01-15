using UnityEngine;
using AYellowpaper.SerializedCollections;

[CreateAssetMenu(fileName = "AudioData", menuName = "Scriptable Objects/AudioData")]
public class AudioData : ScriptableObject
{
    public SerializedDictionary<EventID, FMODUnity.EventReference> events = new();
    public SerializedDictionary<ParamID, FMODUnity.ParamRef> parameters = new();
}

public enum EventID
{
    Amb, TreeA, TreeB, River, BirdA, BirdB, BirdC, BirdD, BushA, BushB, BushC, BioLightA, BioLightB,
    Enviro2DMusic, Entry1Music3D, Entry2Music3D, Entry3Music3D,
    CollisionPlant, CollisionTree, CollisionGrass, CollisionRock, CollisionNpc,
    FrogBig, FrogMedium, FrogTiny, InsectA, InsectB, InsectC,
    BackpackOpen, BackpackClose, BackpackHover, BackpackClick, BackpackBack, InventoryOpen, InventoryClose, InventoryHover, InventoryClick,
    HerbariumOpen, HerbariumClose, PageTurnPrevious, PageTurnNext, PageHover, PlantsIndex, QuestsIndex, HerbariumAmb, LinkHover, PinQuest,
    HintHover, HintClick, HintReveal, HintBack,
    PageCresson, PageAquaMint, PageMurailles,
    PinQuestOff, HintButtonHover, PlantsIndexHover, QuestsIndexHover,
    ToolCut, ToolCutGood, ToolCutBad, ToolPickUp, ToolDrop, PlantPickUp, PlantDrop,
    ToolImpact, PlantImpact, PlantStress, InBag,
}

public enum ParamID
{
    HerbariumEntries, 
}
