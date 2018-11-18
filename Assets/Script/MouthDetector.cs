using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

public class MouthDetector : MonoBehaviour {

    [SerializeField]
    GameObject butterflies;
    ButterfliesEffect butterfliesEffect;

    bool shapeEnabled = false;
    Dictionary<string, float> currentBlendShapes;

    // Use this for initialization
    void Start () 
    {
        UnityARSessionNativeInterface.ARFaceAnchorAddedEvent += FaceAdded;
        UnityARSessionNativeInterface.ARFaceAnchorUpdatedEvent += FaceUpdated;
        UnityARSessionNativeInterface.ARFaceAnchorRemovedEvent += FaceRemoved;
        butterfliesEffect = butterflies.GetComponent<ButterfliesEffect>();

    }

    void Update()
    {
        bool enableMouthOpen = false;
        if (shapeEnabled)
        {
            if (currentBlendShapes.ContainsKey(ARBlendShapeLocation.JawOpen))
            {
                enableMouthOpen = (currentBlendShapes[ARBlendShapeLocation.JawOpen] > 0.5f);
            }
        }

        if (enableMouthOpen)
        {
            butterfliesEffect.StartButterflyAnimation();

        } else {
            butterfliesEffect.StopButterflyAnimation();
        }
    }

    void FaceAdded (ARFaceAnchor anchorData)
    {
        shapeEnabled = true;
        currentBlendShapes = anchorData.blendShapes;
    }

    void FaceUpdated (ARFaceAnchor anchorData)
    {
        currentBlendShapes = anchorData.blendShapes;
    }

    void FaceRemoved (ARFaceAnchor anchorData)
    {
        shapeEnabled = false;
    }
}
