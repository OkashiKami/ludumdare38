using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif
public class CBL : MonoBehaviour
{
    public static CBL inst;
    public bool active;
    internal CollectionBin bin;
    public int Score = 0;
    public int Level = 1;
    public float radius = 20;
    

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
            float pos = UnityEngine.Random.Range(-(radius - 10), (radius - 10));
            Vector3 spawnpos = transform.position;
            RaycastHit hit;
            if (Physics.Raycast(new Vector3(transform.position.x + pos, transform.position.y + 2.9f, transform.position.z + pos), -Vector3.up, out hit))
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

#if UNITY_EDITOR
[CustomEditor(typeof(CBL))]
public class CBLEditor: Editor
{
    public override void OnInspectorGUI()
    {
        CBL tar = (CBL)target;
        foreach(Transform t in tar.GetComponentsInChildren<Transform>())
        {
            if (t != tar.transform)
                t.localPosition = Vector3.zero;
        }
       foreach(BoxCollider bc in tar.GetComponentsInChildren<BoxCollider>())
        {
            bc.size = new Vector3(tar.radius, 5, tar.radius);
            bc.center = new Vector3(0, 2.5f, 0);
            bc.isTrigger = true;
        }
        base.OnInspectorGUI();
    }
}
#endif