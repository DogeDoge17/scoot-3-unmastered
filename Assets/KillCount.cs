using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KillCount : MonoBehaviour
{

    public float yMarker;

    public int people;
    public int peopleLeft;

    public List<NPC> nPCs = new List<NPC>();

    public List<int> alreadyDead = new List<int>();

    void Awake()
    {
        nPCs = FindObjectsOfType<NPC>().ToList();

    }

    // Use this for initialization
    void Start()
    {
        people = nPCs.Count;
        peopleLeft = nPCs.Count;
    }

    // Update is called once per frame
    void Update()
    {
        if (peopleLeft > 0)
            for (int i = 0; i < nPCs.Count; i++)
            {
                if (nPCs[i].transform.position.y <= yMarker && alreadyDead.IndexOf(i) == -1)
                {
                    alreadyDead.Add(i);
                    peopleLeft--;
                }
            }
    }
}
