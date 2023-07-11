using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WiggleAnimation : MonoBehaviour
{
    [SerializeField] private RectTransform animatedRectTransform = null;

    // Start is called before the first frame update
    void Start()
    {
        animatedRectTransform.DOScale(0.95f, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
