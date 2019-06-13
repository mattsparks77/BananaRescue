using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InteractionController : MonoBehaviour
{
    bool interact_button;
    public AudioSource a;
    public Rigidbody rb;
    public GameObject parent;
    public UnityEngine.UI.InputField mainInputField;
    Text interactText;
    public GameObject finalKey;
    GameManager gm;
    private bool terminalActive = false;
    public bool isRespawning;
    public Text winText;
    public Transform spawnPoint;
    public Transform spawnPoint1;
    public Transform spawnPoint2;
    public ButtonManager bm;
    public SkinnedMeshRenderer smr;
    string t;
    bool picked = false;
    UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter tpc;
    UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl tpuc;
    // Start is called before the first frame update
    void Start()
    {
        terminalActive = false;
        bm = FindObjectOfType<ButtonManager>();
        rb = GetComponentInParent<Rigidbody>();
        tpc = GetComponentInParent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter>();
        tpuc = GetComponentInParent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>();
        parent = GameObject.FindGameObjectWithTag("Player");
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        //iZone = GetComponentInChildren<BoxCollider>();
        a = gameObject.AddComponent<AudioSource>();
        //smr = GetComponentInParent<SkinnedMeshRenderer>();


        interactText = GameObject.FindGameObjectWithTag("InteractUI").GetComponent<Text>();
        interactText.enabled = false;
        //iZone.enabled = true;
    }


    void Update()
    {
        if (t == "100" && !picked)
        {
            finalKey.SetActive(true);
            picked = true;
            winText.gameObject.SetActive(true);
            //gm.sm.PlaySoundByName(a, "UnlockSound", false, .5f, 1.0f);
        }
  
    }
    IEnumerator Respawn(Transform spawn)
    {
        
        isRespawning = true;
        smr.enabled = false;
        tpuc.enabled = false;
        tpc.enabled = false;
        
        gm.inventory.ResetKeys();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        //rb.transform.position = other.gameObject.transform.position;
        yield return new WaitForSeconds(1f);
        smr.enabled = true;
        tpuc.enabled = true;
        tpc.enabled = true;
        parent.transform.position = spawn.transform.position;
        isRespawning = false;
        bm.RandomizeButtons();
        gm.sm.PlaySoundOneShotName(a, "Sliding", false, 0.5f, 1.0f);
    }

    private void OnTriggerStay(Collider other)
    {
        
        if (other.gameObject.tag == "Untagged" || other.gameObject.tag == "Spikes")
        {
            interactText.enabled = false;
        }
        else if (interactText.enabled == false) interactText.enabled = true;
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (other.gameObject.tag == "key")
            {

                gm.sm.PlaySoundByName(a, "pickup", false, .5f, 1.0f);
                gm.sm.PlaySoundOneShotName(a, "UnlockSecret", false, 0.5f, 1.0f);

                gm.inventory.AddKey();
                //other.gameObject.GetComponent<MeshRenderer>().enabled = false;
                other.gameObject.SetActive(false);
                interactText.enabled = false;
            }
            if (other.gameObject.tag == "Button" )
            {
                //gm.inventory.DecrementKeys();
                ButtonObject bo = other.gameObject.GetComponent<ButtonObject>();
                other.gameObject.GetComponent<Animator>().SetBool("isPushed", true);
               
                other.gameObject.GetComponent<ButtonObject>().PushButton();
                
                if (bo.effect == "Explosion")
                {
                    gm.resettableObjects.DelayedReset(0f);
                    gm.sm.PlaySoundByName(a, "RPG", false, .5f, 1.0f);
                    StartCoroutine("Respawn", spawnPoint1);
                }
                //other.gameObject.GetComponent<InteractionObject>().passable = true;
                //gm.sm.PlaySoundByName(a, "door_open", false, .5f, 1.0f);
                interactText.enabled = false;
            }
            if (other.gameObject.tag == "Door" && gm.inventory.hasKey())
            {
                gm.inventory.DecrementKeys();
                
                other.gameObject.GetComponent<Animator>().SetBool("Opening", true);
                other.gameObject.GetComponent<Animator>().SetBool("isOpen", true);
                other.gameObject.GetComponent<InteractionObject>().passable = true;
                gm.sm.PlaySoundByName(a, "door_open", false, .5f, 1.0f);
                //gm.sm.PlaySoundOneShotName(a, "UnlockSecret", false, 0.5f, 1.0f);
                interactText.enabled = false;
            }
            if (other.gameObject.tag == "Terminal" )
            {
                mainInputField.gameObject.SetActive(false);
                if (!terminalActive)
                {
                    StartCoroutine("StallTerminal");
                }
                else {
                    mainInputField.gameObject.SetActive(false);
                    interactText.text = "Press (F) to Interact";
                }
                Debug.Log("Terminal");
                t = mainInputField.text;
            }

        }

        //if (other.gameObject.tag == "DeathBox")
        //{
        //    interactText.enabled = false;
        //    if (!isRespawning) //other.gameObject.GetComponent<ProximityTrigger>().spikesUp && 
        //    {
        //        Debug.Log("Dead");

        //        gm.resettableObjects.DelayedReset(0f);
        //        StartCoroutine("Respawn");

        //    }
        //}
    }

    IEnumerator StallTerminal()
    {
        yield return new WaitForSeconds(.5f);
        interactText.text = "Press (f) to Enter Answer";
        mainInputField.gameObject.SetActive(true);
        terminalActive = true;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DeathBox")
        {
            interactText.enabled = false;
            if (!isRespawning) //other.gameObject.GetComponent<ProximityTrigger>().spikesUp && 
            {

                gm.resettableObjects.DelayedReset(0f);
                StartCoroutine("Respawn", spawnPoint);

            }
        }
        if (other.gameObject.tag == "DeathBox1")
        {
            interactText.enabled = false;
            if (!isRespawning) //other.gameObject.GetComponent<ProximityTrigger>().spikesUp && 
            {

                //gm.resettableObjects.DelayedReset(0f);
                StartCoroutine("Respawn", spawnPoint2);

            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        interactText.enabled = false;
    }
}
