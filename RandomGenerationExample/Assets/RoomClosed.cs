using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomClosed : MonoBehaviour
{
    public List<char> closingNeeded = new List<char>();
    private float rand;
    private bool insideExistingRoom = false;
    void Start(){
        rand  = Random.Range(0.00f, 0.05f);
    }
    void Update()
    {
        if (insideExistingRoom)
        {
            Destroy(this.gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.CompareTag("DataPoint"))
        {
            insideExistingRoom = true;
        }
        else if (hit.CompareTag("ClosedPoint"))
        {
            StartCoroutine(absorbSpawners(hit, rand));
        }
    }
    IEnumerator absorbSpawners(Collider2D hit, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (hit.CompareTag("ClosedPoint"))
        {
            foreach (char closePoint in hit.GetComponent<RoomClosed>().closingNeeded)
            {
                this.closingNeeded.Add(closePoint);
            }
            //activeSpawners.spawnerList.Remove(hit.gameObject);
        }
        Destroy(hit.gameObject);
    }
}
