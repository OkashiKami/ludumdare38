using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectionBin : MonoBehaviour
{
    public int keys;
    private int wantkeys = 3;
    public int present;
    private int wantpresent = 1;
    public void AddKey(Key key)
    {
        if (key) keys += 1;
        FindObjectOfType<UIManager>().ActivateKey(keys - 1);
        Destroy(key.gameObject);
        if (keys >= wantkeys)
        {
            if (!GetComponent<BoxCollider>())
                gameObject.AddComponent<BoxCollider>();
            if (GetComponent<BoxCollider>())
            {
                GetComponent<BoxCollider>().center = new Vector3(0, 2.5f, 0);
                GetComponent<BoxCollider>().size = new Vector3(FindObjectOfType<CBL>().radius, 5, FindObjectOfType<CBL>().radius);
                GetComponent<BoxCollider>().isTrigger = true;
            }
            A:
            float pos = UnityEngine.Random.Range(-FindObjectOfType<CBL>().radius, FindObjectOfType<CBL>().radius);
            Vector3 spawnpos = transform.position;
            RaycastHit hit;
            if (Physics.Raycast(new Vector3(spawnpos.x + pos, spawnpos.y + 16, spawnpos.z + pos), -Vector3.up, out hit))
            {
                spawnpos = hit.point;
            }
            else goto A;
            Instantiate(Resources.Load("Present"), spawnpos , Quaternion.identity);
            Destroy(gameObject.GetComponent<SphereCollider>());
        } 
    }
    public void AddPresent(Present present)
    {
        if (present) this.present += 1;
        FindObjectOfType<UIManager>().ActivateKey(3);
        Destroy(present.gameObject);
    }
}
