using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

public class WinkDetector : MonoBehaviour
{


    [SerializeField]
    GameObject sunglasses;
    Animator sunglassesAnimator;

    ButterfliesEffect butterfliesEffect;

    bool shapeEnabled = false;
    Dictionary<string, float> currentBlendShapes;

    // Use this for initialization
    void Start()
    {
        UnityARSessionNativeInterface.ARFaceAnchorAddedEvent += FaceAdded;
        UnityARSessionNativeInterface.ARFaceAnchorUpdatedEvent += FaceUpdated;
        UnityARSessionNativeInterface.ARFaceAnchorRemovedEvent += FaceRemoved;
        sunglassesAnimator = sunglasses.GetComponent<Animator>();

    }

    void Update()
    {
        bool enableRightBlink = false;
        bool enableLeftBlink = false;

        if (shapeEnabled)
        {
            if (currentBlendShapes.ContainsKey(ARBlendShapeLocation.EyeBlinkLeft) && currentBlendShapes.ContainsKey(ARBlendShapeLocation.EyeBlinkRight))
            {
                enableLeftBlink = (currentBlendShapes[ARBlendShapeLocation.EyeBlinkLeft] > 0.5f 
                                   && currentBlendShapes[ARBlendShapeLocation.EyeBlinkRight] <= 0.5f);
                enableRightBlink = (currentBlendShapes[ARBlendShapeLocation.EyeBlinkRight] > 0.5f 
                                    && currentBlendShapes[ARBlendShapeLocation.EyeBlinkLeft] <= 0.5f);
            }
                
        }

        if (enableRightBlink)
        {
            sunglasses.SetActive(true);
            if (!sunglassesAnimator.GetCurrentAnimatorStateInfo(0).IsTag("1"))
            {
                sunglassesAnimator.Play("Sunglasses In");
            }

        }
        else if (enableLeftBlink)
        {
            sunglasses.SetActive(false);
            sunglassesAnimator.Play("Idle");
        }
    }

    void FaceAdded(ARFaceAnchor anchorData)
    {
        shapeEnabled = true;
        currentBlendShapes = anchorData.blendShapes;
    }

    void FaceUpdated(ARFaceAnchor anchorData)
    {
        currentBlendShapes = anchorData.blendShapes;
    }

    void FaceRemoved(ARFaceAnchor anchorData)
    {
        shapeEnabled = false;
    }
}
