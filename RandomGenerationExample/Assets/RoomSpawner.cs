using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public List<char> openingNeeded = new List<char>();
    public List<char> closingNeeded = new List<char>();
    private List<Collider2D> collidingList = new List<Collider2D>();
    private RoomTemplates templates;
    private int rand1;
    private float rand2;
    private bool spawned = false;
    //public bool chosen = false;
    public bool absorbed = false;
    private Coroutine waiting;
    private ActiveSpawners activeSpawners;
    private List<GameObject> roomOptions = new List<GameObject>();

    //called on start
    void Awake()
    {
        activeSpawners = GameObject.Find("StartRoom").GetComponent<ActiveSpawners>();
        templates = GameObject.FindGameObjectWithTag("Templates").GetComponent<RoomTemplates>();
        rand2 = Random.Range(0.00f, 0.10f);
        StartCoroutine(nonAbsorbCheck());
        activeSpawners.spawnerList.Add(this.gameObject);
    }
    void Update()
    {
        if (spawned)
        {
            activeSpawners.spawnerList.Remove(this.gameObject);
            //activeSpawners.spawning = false;
            Destroy(this.gameObject);
        }
    }
    public void Spawn()
    {
        private bool missingOpening;
        List<GameObject> tempRoomOptions;
        private bool missingClosing;
        if(!spawned)
        {
            foreach (GameObject room in templates.allRooms)
            {
                missingOpening = false;
                foreach (char opening in openingNeeded)
                {
                    if (room.name.IndexOf(opening) == -1)
                        missingOpening = true;
                }
                if (!missingOpening)
                    roomOptions.Add(room);
            }
            tempRoomOptions = new List<GameObject>(roomOptions);
            foreach (GameObject room in tempRoomOptions)
            {
                bool missingClosing = false;
                foreach (char closing in closingNeeded)
                {
                    if (room.name.IndexOf(closing) != -1)
                        missingClosing = true;
                }
                if (missingClosing)
                    roomOptions.Remove(room);
            }
            rand1 = Random.Range(0, roomOptions.Count);
            Instantiate(roomOptions[rand1], transform.position, roomOptions[rand1].transform.rotation);
            spawned = true;
        }
    }
    void OnTriggerEnter2D(Collider2D hit)
    {
        //if (chosen)
        //{
        if (hit.CompareTag("DataPoint"))
        {
            spawned = true;
        }
        else if (hit.CompareTag("SpawnPoint") || hit.CompareTag("ClosedPoint"))
        {
            StartCoroutine(absorbSpawners(hit, rand2));
        }
        //}
    }
    IEnumerator nonAbsorbCheck()
    {
        yield return new WaitForSeconds(0.4f);
        absorbed = true;
    }
    IEnumerator absorbSpawners(Collider2D hit, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (hit.CompareTag("SpawnPoint"))
        {
            this.openingNeeded.AddRange(hit.GetComponent<RoomSpawner>().openingNeeded);
            activeSpawners.spawnerList.Remove(hit.gameObject);
        }
        else if (hit.CompareTag("ClosedPoint"))
        {
            foreach (char closePoint in hit.GetComponent<RoomClosed>().closingNeeded)
            {
                closingNeeded.Add(closePoint);
            }
            //activeSpawners.spawnerList.Remove(hit.gameObject);
        }
        Destroy(hit.gameObject);
        //StopCoroutine(waiting);
        //waiting = StartCoroutine(nonAbsorbCheck());
    }
}
