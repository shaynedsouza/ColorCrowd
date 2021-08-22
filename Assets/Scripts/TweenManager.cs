using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenManager : MonoBehaviour
{
    public static TweenManager instance;
    public LeanTweenType inType;
    public LeanTweenType outType;


    private void Awake()
    {
        instance = this;
    }

    public void OnOpen()
    {
        transform.localScale = new Vector3(0, 0, 0);
        LeanTween.scale(gameObject, new Vector3(1, 1, 1), 0.25f).setDelay(0.1f).setEase(inType);


    }

    public void OnClose()
    {
        LeanTween.scale(gameObject, new Vector3(0, 0, 0), 0.25f).setEase(outType);
    }
}
