using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterfliesEffect : MonoBehaviour
{
    Transform[] childObjectTransform;
    List<GameObject> butterflyList;

    // Use this for initialization
    void Awake()
    {
        butterflyList = new List<GameObject>();
        childObjectTransform = GetComponentsInChildren<Transform>();
        foreach (Transform trans in childObjectTransform)
        {
            butterflyList.Add(trans.gameObject);
        }
    }

    public void AnimateRandomButterfly()
    {
        int index = Random.Range(0, butterflyList.Capacity);
        Animation anim = butterflyList[index].GetComponent<Animation>();
        if (anim && !anim.isPlaying)
        {
            anim.Play();
        }
    }

    public void StartButterflyAnimation() {
        InvokeRepeating("AnimateRandomButterfly", 1f, 0.2f);
        this.gameObject.SetActive(true);
    }

    public void StopButterflyAnimation()
    {
        CancelInvoke();
        this.gameObject.SetActive(false);
    }

    void Update()
    {

    }
}
