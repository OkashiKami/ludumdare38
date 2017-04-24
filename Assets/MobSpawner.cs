using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    public static MobSpawner inst;
    public bool active;
    public enum MobType { Goblin }
    public MobType mob;
    public int MaxMobCount = 0, MobCount = 0;
    public bool CanCreate = false;
    float radius = 55;
    public void StartMe()
    {
        if (!inst) { inst = this; DontDestroyOnLoad(this.gameObject); }
        else if (inst) { if(inst != this) { Destroy(this.gameObject); } }

        if (MaxMobCount <= 0) CanCreate = false;
        else if (MaxMobCount > 0) CanCreate = true;
        active = true;
    }

    void OnLevelWasLoaded(int level)
    {
        if (active)
        {
            if (MaxMobCount <= 0) CanCreate = false;
            else if (MaxMobCount > 0) CanCreate = true;
        }
    }

    void Update () {
        if (active)
        {
            if (CanCreate)
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
                GameObject obj = (GameObject)Instantiate(Resources.Load(mob.ToString()), spawnpos, Quaternion.identity);
                obj.name = "Goblin " + MobCount;
                MobCount += 1;
                if (MobCount >= MaxMobCount)
                {
                    CanCreate = false;
                }
            }
        }
	}
}
