using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

public class dotweentest : MonoBehaviour
{

    [SerializeField]
    private Volume GlobalVolumeProfile;

    private ColorAdjustments COA;
    private Tween contrastTween;
    private Tween saturationTween;

    // Start is called before the first frame update
    void Start()
    {
        GlobalVolumeProfile.profile.TryGet(out COA);
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.Y))
        {
            COA.contrast.value = 60f;
            COA.saturation.value = -100f;
            ValueTest();
        }
    }

    void ValueTest()
    {
        contrastTween.Kill();
        contrastTween = DOTween.To(() => COA.contrast.value, (x) => COA.contrast.value = x, 0f, 1f).SetDelay(1f);
        saturationTween.Kill();
        saturationTween = DOTween.To(() => COA.saturation.value, (x) => COA.saturation.value = x, 0f, 8f).SetDelay(1f);
    }
}