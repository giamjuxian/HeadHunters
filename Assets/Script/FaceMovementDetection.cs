using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;
using UnityEngine.UI;

public class FaceMovementDetection : MonoBehaviour
{
    [SerializeField]
    GameObject anchor;

    [SerializeField]
    GameObject effectManager;

    [SerializeField]
    Text textDebug;

    bool shapeEnabled = false;
    bool headRotated = false;
    Dictionary<string, float> currentBlendShapes;
    EffectManager effectManagerScript;
    Quaternion headRotation;
    Quaternion newheadRotation;
    float angleMoved;

    // Use this for initialization
    void Start()
    {
        UnityARSessionNativeInterface.ARFaceAnchorAddedEvent += FaceAdded;
        UnityARSessionNativeInterface.ARFaceAnchorUpdatedEvent += FaceUpdated;
        UnityARSessionNativeInterface.ARFaceAnchorRemovedEvent += FaceRemoved;
        headRotation = anchor.transform.rotation;
        effectManagerScript = effectManager.GetComponent<EffectManager>();

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

    // Update is called once per frame
    void Update()
    {
        // Detect Face Rotation
        headRotated = false;
        newheadRotation = anchor.transform.rotation;
        angleMoved = Mathf.Acos(Quaternion.Dot(newheadRotation, headRotation));
        angleMoved = Mathf.Abs(angleMoved);
        Debug.Log("Angle Moved " + angleMoved);
        if ((angleMoved * (180 / Mathf.PI)) > 3)
        {
            textDebug.text += "HEAD ROTATED\n";
            headRotated = true;
        }

        if (headRotated)
        {
            effectManagerScript.PlayDandruffDropping();
        }

        headRotation = newheadRotation;
    }
}
