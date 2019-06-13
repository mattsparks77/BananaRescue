using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour
{
    public string iName;
    public bool passable;
    BoxCollider[] bc;
    private MeshRenderer mr;
    Color originalColor;
    // Start is called before the first frame update
    private GameManager gm;
    public Light l;
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        mr = GetComponent<MeshRenderer>();
        originalColor = mr.material.color;
        mr.material.color = Color.gray;
        l = GetComponentInChildren<Light>();
        l.enabled = false;
        passable = false;
        iName = gameObject.name;
        bc = GetComponents<BoxCollider>();

    }

    private void FixedUpdate()
    {
        if (gm.inventory.hasKey())
        {
            mr.material.color = originalColor;
            l.enabled = true;
        }
        else
        {
            mr.material.color = Color.gray;
            l.enabled = false;
        }
    }

    public void SetCollidersInactive()
    {
        foreach (BoxCollider b in bc)
        {
            b.enabled = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (passable)
        {
            SetCollidersInactive();
            passable = !passable;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
