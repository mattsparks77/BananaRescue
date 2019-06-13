using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public ButtonObject[] buttons;

    public GameManager gm;
    public string effect;
    public Animator anim;
    public GameObject rock;
    public GameObject explosionPrefab;
    Random random = new Random();
    public List<string> FXlist = new List<string>();
    public void ExecuteEffect()
    {
        if (effect == "Explosion")
        {

        }
        else if (effect == "FloorDrop")
        {

        }
        else if (effect == "KeyDrop")
        {

        }
        else if (effect == "PlaySound")
        {

        }
        else if (effect == "HintDrop")
        {

        }
        else if (effect == "RockDrop")
        {
            
        }
    }

    public void RandomizeButtons()
    {
        int i = 0;
        foreach (var v in buttons)
        {

            int r = Random.Range(0, FXlist.Count);
            v.effect = FXlist[r];
            FXlist.RemoveAt(r);
            i++;

        }
        FXlist.AddRange(new List<string>() { "DropRock", "Explosion", "DropKey", "DropHint" });
    }

    void Start()
    {
        FXlist.AddRange(new List<string>() {"DropRock", "Explosion", "DropKey", "DropHint"});
        buttons = FindObjectsOfType<ButtonObject>(); //GameObject.FindGameObjectsWithTag("Button");
        gm = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    Debug.Log("Randomizing Buttons");
        //    RandomizeButtons();
        //}
    }
}
