using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetup : MonoBehaviour
{
    public Transform SpawnLocation;
    public GameObject cam, player;
	// Use this for initialization
	public void StartMe ()
    {
        if (!SpawnLocation) SpawnLocation = GameObject.Find("Start-Point").transform;

        try { cam = FindObjectOfType<ThirdPersonCamera>().transform.parent.gameObject; } catch { }
        if (!cam) cam = (GameObject)Instantiate(Resources.Load("CameraRig"), transform.position, Quaternion.identity);
        try { player = FindObjectOfType<CharacterControllerLogic>().gameObject; } catch { }
        if (!player) player = (GameObject)Instantiate(Resources.Load("Player"), SpawnLocation.position, Quaternion.identity);

        cam.GetComponentInChildren<ThirdPersonCamera>().Activate(player);
        player.transform.rotation = Quaternion.Euler(0, player.transform.rotation.y + 180, 0);
        player.GetComponent<CharacterControllerLogic>().Activate(cam.GetComponentInChildren<ThirdPersonCamera>());
        player.AddComponent<StatSystem>();
        player.GetComponent<StatSystem>().setName(StatSystem.NameSetType.Normal, "Player");
        GameObject NT = new GameObject("nametag");
        NT.transform.parent = player.transform;
        NT.AddComponent<NameTag>();
        NT.GetComponent<NameTag>().stats = player.GetComponent<StatSystem>();
        player.gameObject.AddComponent<WeaponManager>();
        cam.name = "Camera Rig";
        player.name = "Player";
        player.GetComponent<StatSystem>().setHealth(100);
        //Destroy(this.gameObject);
	}
}
