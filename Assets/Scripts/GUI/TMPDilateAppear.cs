using TMPro;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class TMPDilateAppear : MonoBehaviour
{
    private TMP_Text tmp;

    private bool isTyping;
    private int max;
    [SerializeField] private float duration = 3f;
    private float startTime;


    private void Awake()
    {
        tmp = GetComponent<TMP_Text>();
    }


    void Update()
    {
        if (isTyping)
        {
            int newValue = (int)Mathf.Lerp(0, max, (Time.time - startTime) / duration);

            tmp.maxVisibleCharacters = newValue;

            if (newValue == max)
            {
                isTyping = false;
                ResetVisibleCharacters();
            }
        }
    }

    public void ResetVisibleCharacters()
    {
        tmp.maxVisibleCharacters = 999999999;
        isTyping = false;
    }

    public void PlayAppear()
    {
        isTyping = true;
        tmp.maxVisibleCharacters = 0;

        tmp.ForceMeshUpdate(false);
        TMP_TextInfo inf = tmp.textInfo;

        max = inf.characterCount;
        startTime = Time.time;
    }

    private void OnDestroy()
    {
        isTyping = false;
    }
}