using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBL : MonoBehaviour
{
    public static CBL inst;
    public bool active;
    public CollectionBin bin;
    public int Score = 0;
    public int Level = 1;
    public float radius = 40;
    

    void Start () {
        if (!inst) { inst = this;  DontDestroyOnLoad(this.gameObject); }
        else { if(inst != this) { Destroy(this.gameObject); } }


	}

    public void begin()
    {
        active = true;
        GetComponentInChildren<MobSpawner>().StartMe();
        GetComponentInChildren<PlayerSetup>().StartMe();
        StopWatch.Start();
    }

    float keysCreated;
    private void CreateKeys(int v)
    {
        if (keysCreated < v)
        {
            if (!GetComponent<BoxCollider>())
                gameObject.AddComponent<BoxCollider>();
            if (GetComponent<BoxCollider>())
            {
                GetComponent<BoxCollider>().center = new Vector3(0, 2.5f, 0);
                GetComponent<BoxCollider>().size = new Vector3(radius, 5, radius);
                GetComponent<BoxCollider>().isTrigger = true;
            }
            A:
            float pos = UnityEngine.Random.Range(-radius, radius);
            Vector3 spawnpos = transform.position;
            RaycastHit hit;
            if (Physics.Raycast(new Vector3(spawnpos.x + pos, spawnpos.y + 2.9f, spawnpos.z + pos), -Vector3.up, out hit))
            {
                spawnpos = hit.point + new Vector3(0, 0.5f, 0);
            }
            else goto A;
            GameObject obj = (GameObject)Instantiate(Resources.Load("Star"), spawnpos, Quaternion.identity);
            obj.name = "Kay " + keysCreated;
            keysCreated += 1;
        }
    }

    void Update()
    {
        if (!active) return;
        if (!bin) bin = GetComponentInChildren<CollectionBin>();
        if(keysCreated < 3) CreateKeys(3);

      
        if(StopWatch.isRunning) StopWatch.execute();
    }
    
    void OnLevelWasLoaded(int level)
    {
        if (!active) return; 
        keysCreated = 0;
        if (!GetComponent<BoxCollider>())
            gameObject.AddComponent<BoxCollider>();
        if (GetComponent<BoxCollider>())
        {
            GetComponent<BoxCollider>().center = new Vector3(0, 2.5f, 0);
            GetComponent<BoxCollider>().size = new Vector3(radius, 5, radius);
            GetComponent<BoxCollider>().isTrigger = true;
        }
        A:
        float pos = UnityEngine.Random.Range(-radius, radius);
        Vector3 spawnpos = transform.position;
        RaycastHit hit;
        if (Physics.Raycast(new Vector3(spawnpos.x + pos, spawnpos.y + 2.9f, spawnpos.z + pos), -Vector3.up, out hit, 100))
        {
            spawnpos = hit.point;
        }
        else goto A;
        if (bin)
        {
            //bin.transform.position = spawnpos;
        }
    }
}
