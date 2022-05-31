using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoomCombatTrigger : MonoBehaviour
{
    [SerializeField] UnityEvent OnEnterCombatArea;
    [SerializeField] List<Spawner> spawnPoints;
    BoxCollider box;

    private void Start()
    {
        box = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        box.enabled = false;
        OnEnterCombatArea.Invoke();
        //Close all rooms


        if (spawnPoints.Count > 0)
        {
            foreach (var item in spawnPoints)
            {
                item.NextWave();
            }
        }

    }



}
