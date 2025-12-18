using UnityEngine;

[CreateAssetMenu(fileName = "Quests", menuName = "Herbarium/Quests")]
public class PlayerQuests : ScriptableObject
{
    [SerializeField] public Quest[] quests;
}
