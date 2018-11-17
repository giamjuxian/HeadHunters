using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour {
    [SerializeField]
    GameObject soapBubblesEffect;
    [SerializeField]
    GameObject waterWashingEffect;
    [SerializeField]
    GameObject dandruffParticle;

    float currCountdownValue;

    public void playHairWash() {
        ParticleSystem soapBubblesParticle = soapBubblesEffect.GetComponent<ParticleSystem>();
        ParticleSystem waterWashingParticle = waterWashingEffect.GetComponent<ParticleSystem>();

        soapBubblesParticle.Play();
        waterWashingParticle.Play();
        StartCoroutine(HideDandruffParticle());

    }


    public IEnumerator HideDandruffParticle(float countdownValue = 5)
    {
        currCountdownValue = countdownValue;
        while (currCountdownValue > 0)
        {
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
        }
        dandruffParticle.SetActive(false);
    }
}
