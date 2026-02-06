using System.Collections;
using UnityEngine;
using XNode;

/// <summary>
/// Represents a node that can play an animation trigger
/// </summary>
[CreateNodeMenu("Event/Play Animation")]
public class PlayAnimationNode : HerbariumNode
{
    [Input(connectionType = ConnectionType.Multiple)] public bool entry;
    [SerializeField] private string objectID;
    [SerializeField] private string triggerName;
    [Output(connectionType = ConnectionType.Override)] public bool exit;
    

    // Use this for initialization
    protected override void Init()
    {
        base.Init();
    }

    public override IEnumerator Apply()
    {
        GameObject obj = CutsceneManager.instance.GetObject(objectID);
        if (obj && obj.TryGetComponent<Animator>(out Animator animator))
        {
           animator.SetTrigger(triggerName); 
        }

        yield return 0;
    }
}
