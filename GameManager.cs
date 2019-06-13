using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Inventory inventory;
    Transform startPoint;
    public SoundManager sm;
    public AudioSource a;
    public GameObject canvas;
    public GameObject keyOb;
    public GameObject r;
    public UnityStandardAssets.Utility.ObjectResetter resettableObjects;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        keyOb = GameObject.FindGameObjectWithTag("KeyUI");
        r = GameObject.FindGameObjectWithTag("ResettableObjects");

        resettableObjects = r.GetComponent<UnityStandardAssets.Utility.ObjectResetter>();
        keyOb.SetActive(false);
        sm = new SoundManager();
        sm.InitSoundManager();
        a = gameObject.AddComponent<AudioSource>();
        sm.PlaySoundByName(a, "BGMDrums", true, 0.5f, 1.0f);
        inventory = gameObject.AddComponent<Inventory>();
        inventory.Initialize(this);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
