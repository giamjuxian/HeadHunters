using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EffectManager : MonoBehaviour
{
    [SerializeField]
    GameObject dandruffDroppingParticles;
    [SerializeField]
    GameObject dandruffParticles;
    [SerializeField]
    GameObject soapBubblesParticle;
    [SerializeField]
    GameObject washWashParticle;

    List<GameObject> dandruffParticlesChildArray;
    ParticleSystem soapBubblesParticleEffect;
    ParticleSystem waterWashingParticleEffect;
    ParticleSystem dandruffParticleEffect;

    float currCountdownValue;
    bool hasDandruff;

    void Start()
    {
        hasDandruff = true;
        soapBubblesParticleEffect = soapBubblesParticle.GetComponent<ParticleSystem>();
        waterWashingParticleEffect = washWashParticle.GetComponent<ParticleSystem>();
        dandruffParticleEffect = dandruffDroppingParticles.GetComponent<ParticleSystem>();
        dandruffParticlesChildArray = new List<GameObject>();
        foreach (Transform tran in dandruffParticles.transform)
        {
            dandruffParticlesChildArray.Add(tran.gameObject);
        }
    }

    public void PlayHairWash()
    {
        CancelInvoke(); // Cancel all existing invokes
        soapBubblesParticleEffect.Play();
        waterWashingParticleEffect.Play();
        StartCoroutine(HideDandruffParticle());
        hasDandruff = true;
        InvokeRepeating("growDandruff", 5.0f, 0.5f);
    }

    public void PlayHairWashWithClear()
    {
        CancelInvoke(); // Cancel all invokes
        soapBubblesParticleEffect.Play();
        waterWashingParticleEffect.Play();
        StartCoroutine(HideDandruffParticle());
        hasDandruff = false;
    }


    public IEnumerator HideDandruffParticle(float countdownValue = 5)
    {
        currCountdownValue = countdownValue;
        while (currCountdownValue > 0)
        {
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
        }
        foreach (GameObject child in dandruffParticlesChildArray)
        {
            child.SetActive(false);
        }
    }

    public void PlayDandruffDropping()
    {
        if (hasDandruff)
        {
            dandruffParticleEffect.Play();
        }
    }

    public void growDandruff()
    {
        if (hasDandruff)
        {
            Debug.Log("Hello");
            int index = Random.Range(0, dandruffParticlesChildArray.Count);
            if (dandruffParticlesChildArray[index].activeSelf == false)
            {
                dandruffParticlesChildArray[index].SetActive(true);
            }
        }
    }

    void Update()
    {

    }
}
