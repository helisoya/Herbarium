using UnityEngine;

public class SyncAnimations : MonoBehaviour
{
    public Animator animator1;
    public Animator animator2;
    
    [System.Serializable] public struct AnimationLink
    {
        public string stateGO1;
        public string stateGO2;
    }
    
    public AnimationLink[] links;

    private string lastGO1State = "";
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo stateInfo = animator1.GetCurrentAnimatorStateInfo(0);
        string currentGO1State = "";

        foreach (var link in links)
        {
            if (stateInfo.IsName(link.stateGO1))
            {
                currentGO1State = link.stateGO1;

                if (lastGO1State != currentGO1State)
                {
                    animator2.Play(link.stateGO2);
                    lastGO1State = currentGO1State;
                }
                break;
            }
        }
    }
}
