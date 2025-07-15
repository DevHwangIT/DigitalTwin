using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//과제용 임시 처리
public class DataManager : MonoSingleton<DataManager>
{
    private const string loadMachinePath = "Machine";
    
    private List<GameObject> machinePrefabs = new List<GameObject>();
    public List<GameObject> MachinePrefabs => machinePrefabs;

    private Machine currentMachine;
    public Machine CurrentMachine
    {
        get { return currentMachine; }
        set { currentMachine = value; }
    }

    private void Awake()
    {
        Machine[] machinePrefabComponents = Resources.LoadAll<Machine>(loadMachinePath);
        foreach (var machineCoponent in machinePrefabComponents)
            machinePrefabs.Add(machineCoponent.gameObject);

        if (currentMachine == null && machinePrefabs.Count != 0)
            currentMachine = Instantiate(machinePrefabs[0]).GetComponent<Machine>();
        
        // Test Rotate For Generate Check
        foreach (var link in currentMachine.Links)
            link.RotateAngle = Random.Range(-15, 15);
    }
}