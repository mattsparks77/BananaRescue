using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonObject : MonoBehaviour
{
    public string effect;
    Animator anim;
    private Rigidbody rbRock;
    private Rigidbody rbKey;
    private Rigidbody rbCode;
    GameObject rock;
    GameObject key;
    GameObject code;

    public GameObject explosionPrefab;

    public ButtonManager bm;
    public AudioSource a;

    public void FreezePosition()
    {
        if (effect == "DropRock")
        {
            rbRock.constraints = RigidbodyConstraints.FreezeAll;
        }
        else if (effect == "DropKey")
        {
            rbKey.constraints = RigidbodyConstraints.FreezeAll;
        }
        else if (effect == "DropHint")
        {
            rbCode.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    public void UnFreezePosition()
    {
        if (effect == "DropRock")
        {
            rbRock.constraints = RigidbodyConstraints.None;
        }
        else if (effect == "DropKey")
        {
            rbKey.constraints = RigidbodyConstraints.None;
        }
        else if (effect == "DropHint")
        {
            rbCode.constraints = RigidbodyConstraints.None;
        }
    }

   
    public void PushButton()
    {
        if (effect == "DropRock")
        {
            UnFreezePosition();
            bm.gm.sm.PlaySoundByName(a, "RockRoll", false, 0.5f, 1.0f);
            
        }
        
        else if (effect == "DropKey")
        {
            UnFreezePosition();
            bm.gm.sm.PlaySoundByName(a, "UnlockSecret", false, 0.5f, 1.0f);
        }
        else if (effect == "DropHint")
        {
            UnFreezePosition();
            bm.gm.sm.PlaySoundByName(a, "Hint", false, 0.5f, 1.0f);
        }
        else if (effect == "Explosion")
        {
            GameObject g = Instantiate(explosionPrefab, transform);
            Destroy(g, 3f);
        }
    
    }

    void Start()
    {
        a = gameObject.AddComponent<AudioSource>();
        bm = FindObjectOfType<ButtonManager>();
    
        rock = GameObject.Find("Boulder");  //rock = GameObject.FindGameObjectWithTag("Boulder");
        rbRock = rock.GetComponent<Rigidbody>();
        
       

        key = GameObject.Find("FloatingKey"); // FindGameObjectWithTag("Boulder");
        rbKey = key.GetComponent<Rigidbody>();
        
       

        code = GameObject.Find("100"); // FindGameObjectWithTag("Boulder");
        rbCode = code.GetComponent<Rigidbody>();
        FreezePosition();

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
