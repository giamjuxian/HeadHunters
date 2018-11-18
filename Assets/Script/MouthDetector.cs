using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

public class MouthDetector : MonoBehaviour {

    [SerializeField]
    GameObject butterflies;
    [SerializeField]
    GameObject cherryBlossomsEffect;
    [SerializeField]
    GameObject cherryTree;
    Animator cherryTreeAnimator;

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
        cherryTreeAnimator = cherryTree.GetComponent<Animator>();

    }

    void Update()
    {
        bool enableMouthSmile = false;
        if (shapeEnabled)
        {
            if (currentBlendShapes.ContainsKey(ARBlendShapeLocation.MouthSmileLeft) &&
                currentBlendShapes.ContainsKey(ARBlendShapeLocation.MouthSmileRight))
            {
                enableMouthSmile = (currentBlendShapes[ARBlendShapeLocation.MouthSmileLeft] > 0.5f &&
                                  currentBlendShapes[ARBlendShapeLocation.MouthSmileRight] > 0.5f);
            }
        }

        if (enableMouthSmile)
        {
            cherryBlossomsEffect.SetActive(true);
            cherryTree.SetActive(true);
            if (!cherryTreeAnimator.GetCurrentAnimatorStateInfo(0).IsTag("1")) {
                cherryTreeAnimator.Play("Cherry Blooming");
            }

            butterfliesEffect.StartButterflyAnimation();

        } else {
            cherryBlossomsEffect.SetActive(false);
            cherryTree.SetActive(false);
            cherryTreeAnimator.Play("Idle");
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
