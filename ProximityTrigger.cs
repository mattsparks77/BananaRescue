using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    Animator anim;
    public bool spikesUp;
    public BoxCollider deathBox;
    public BoxCollider spikeBox;
    public AudioSource a;
    private GameManager gm;
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        a = gameObject.AddComponent<AudioSource>();
        spikesUp = false;
        anim = GetComponent<Animator>();
       // deathBox = GetComponentInChildren<BoxCollider>();
        spikeBox = GetComponent<BoxCollider>();
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            anim.SetBool("steppedOn", true);
            gm.sm.PlaySoundByName(a, "Trap_fortnite", false, 0.5f, 1.0f);
            if (!spikesUp)
                StartCoroutine("SpikesReset");
        }
    }


    IEnumerator SpikesReset()
    {
        spikesUp = true;
        spikeBox.enabled = false;
        deathBox.enabled = true;
        yield return new WaitForSeconds(1.5f);
        anim.SetBool("steppedOn", false);
        anim.SetBool("returnSpikes", true);
        spikeBox.enabled = true;
        deathBox.enabled = false;
        spikesUp = false;
    }
}
