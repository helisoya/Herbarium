using UnityEngine;

/// <summary>
/// Represents the Plants index in the Herbarium
/// There can be multiple pages of plant index, but only one manager
/// </summary>
public class HerbariumPlantIndex : HerbariumPage
{
    [Header("Plant Index")]
    [SerializeField] protected HerbariumPlantIndexEntry prefabEntry;
    [SerializeField] protected Transform holderLeft;
    [SerializeField] protected Transform holderRight;

    private const int ENTRY_COUNT = 14;


    public override void OnClose()
    {
        foreach (Transform child in holderLeft) Destroy(child.gameObject);
        foreach (Transform child in holderRight) Destroy(child.gameObject);
    }

    public override void OnOpen()
    {
        // There can be 14 entries per page

        string[] unlockedPages = GameManager.instance.GetPlayerDataHandler().GetHerbariumUnlockedPages();
        int pagesCount = Mathf.CeilToInt((float)unlockedPages.Length / ENTRY_COUNT);

        for (int i = 0; i < ENTRY_COUNT && i + ENTRY_COUNT * localPageIndex < unlockedPages.Length; i++)
        {
            Instantiate<HerbariumPlantIndexEntry>(prefabEntry,
            i < ENTRY_COUNT / 2 ? holderLeft : holderRight
            ).Init(pagesCount + i, gui, GameManager.instance.GetPlantDatabase().GetPlant(unlockedPages[i]).Name);
        }
    }
}
