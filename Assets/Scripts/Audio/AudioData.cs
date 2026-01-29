using UnityEngine;
using AYellowpaper.SerializedCollections;

[CreateAssetMenu(fileName = "AudioData", menuName = "Scriptable Objects/AudioData")]
public class AudioData : ScriptableObject
{
    public SerializedDictionary<EventID, FMODUnity.EventReference> events = new();
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
    MusExploration2D, MusExploration3DCresson, MusExploration3DCapillaire, MusExploration3DMenthe,
    StopPlantStress,
    DialogueIn, DialogueOut, DialogueNext, Speak, StartTyping, StopTyping,
    MusForaging, StartForaging, StopForaging, StopForagingGood, StopForagingBad, StopForagingCancel,
    MusNour, MusBarbrook, MusNourPartB, MusBarbrookPicnic,
    MusGoodNight, MusGoodMorning, MusSpawn,
    DryPlant,
}


