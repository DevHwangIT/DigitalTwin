using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class InspectorUI : UI_Page
{
    public Action<Machine> OnGenerateNewMachineObject;

    private void Start()
    {
        var uiDocument = GetComponent<UIDocument>();
        var root = uiDocument.rootVisualElement;
        
        var dropdown = root.Q<DropdownField>("SelectMachineDropBox");
        var machinePrefabs = DataManager.Instance.MachinePrefabs;
        var machineNames = machinePrefabs.Select(prefab => prefab.name).ToList();
        
        dropdown.choices = machineNames;
        if (machineNames.Count > 0)
            dropdown.value = machineNames[0];
        
        dropdown.RegisterValueChangedCallback(evt =>
        {
            Debug.Log("[Notice] Change Robot Object.");

            if (DataManager.Instance.CurrentMachine.gameObject != null)
            {
                Debug.Log("[Notice] Deleted Old Robot Object - " + DataManager.Instance.CurrentMachine.gameObject.name);
                Destroy(DataManager.Instance.CurrentMachine.gameObject);
            }

            var selectedPrefab = machinePrefabs
                .FirstOrDefault(prefab => prefab.name == evt.newValue);

            if (selectedPrefab != null)
            {
                DataManager.Instance.CurrentMachine = Instantiate(selectedPrefab).GetComponent<Machine>();
                
                // Test Rotate For Generate Check
                foreach (var link in DataManager.Instance.CurrentMachine.Links)
                    link.RotateAngle = Random.Range(-15, 15);
                
                OnGenerateNewMachineObject?.Invoke(DataManager.Instance.CurrentMachine);
            }
        });
    }
}
