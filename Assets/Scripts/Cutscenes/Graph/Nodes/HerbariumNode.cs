using System.Collections;
using UnityEngine;
using XNode;

/// <summary>
/// Represents a node in a cutscene graph
/// </summary>
[CreateNodeMenu("")]
public class HerbariumNode : Node
{
    /// <summary>
    /// Applies the node value
    /// </summary>
    /// <returns>The next port</returns>
    public virtual IEnumerator Apply(){
        yield return 0;
    }

    public override object GetValue(NodePort port)
    {
        return 0;
    }
}
