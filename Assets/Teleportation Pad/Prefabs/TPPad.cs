using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TPPad : MonoBehaviour {

    GameObject mesh;
	void Update ()
    {
        mesh = transform.GetChild(0).gameObject;
        if (!mesh) return;
        CollectionBin bin = FindObjectOfType<CollectionBin>();
        if (!bin) return;
        mesh.SetActive(bin.keys == 3 && bin.present == 1 ? true : false);	
	}

    public IEnumerator GameRestart()
    {
        StopWatch.Stop();
        // Display Level compleate On Screen
        Debug.Log("Level Compleate");
        Debug.Log(StopWatch.ElapsTime);
        if(FindObjectOfType<CharacterControllerLogic>())
        {
            CharacterControllerLogic player = FindObjectOfType<CharacterControllerLogic>();
            player.Active = false;
            if (player.GetComponent<StatSystem>().Health > 0)
            {
                FindObjectOfType<CBL>().Score += 100;
                FindObjectOfType<CBL>().Level += 1;
                FindObjectOfType<MobSpawner>().MaxMobCount += 1;
                FindObjectOfType<MobSpawner>().MobCount = 0;
            }
        }
        yield return new WaitForSeconds(5);
        // Restart the Game Can create new Level
        FindObjectOfType<CollectionBin>().keys = 0;
        FindObjectOfType<CollectionBin>().present = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
