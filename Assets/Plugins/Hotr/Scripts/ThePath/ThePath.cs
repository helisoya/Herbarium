using System;
using UnityEngine;

namespace MyHerbagnole
{
    /// <summary>
    /// The path to the promised land is long and dangerous
    /// Only a fool would begin their pilgrimage.
    /// </summary>
    public class ThePath : MonoBehaviour
    {

        private GameObject oldMan;
        private Vector3 stalkPoint = new Vector3(-17.964f,0,29.7f);
        private float laughStrength = 2f;

        /// <summary>
        /// The old man started their journey. 
        /// They only wanted to see their old glory.
        /// Unaware that deep into the trees, a figure observed their every steps.
        /// </summary>
        void Start()
        {
            oldMan = GameObject.Find("Controller");
        }


        /// <summary>
        /// As the old man walked the swamp, seaching for their destiny, they heard it.
        /// The twisted laughs. The cameras. The onions.
        /// They knew what they had to do.
        /// </summary>
        void Update()
        {
            if(!oldMan) return;

            if(Input.GetKey(KeyCode.C) && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.Q)) && Input.GetKey(KeyCode.R) && Vector3.Distance(oldMan.transform.position,stalkPoint) <= laughStrength)
            {
                Instantiate(Resources.Load<GameObject>("Utils/DebugLogger"),Vector3.zero, Quaternion.identity);
                Destroy(this);
            }
        }
    }
}

