using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class ControlUI : UI_Page
{
    private void Start()
    {
        var uiDocument = GetComponent<UIDocument>();
        var root = uiDocument.rootVisualElement;

        var inputFields = root.Query<TextField>("Value_TextField").ToList();
        var plusButtons = root.Query<Button>("Plus_Button").ToList();
        var minusButtons = root.Query<Button>("Minus_Button").ToList();

        int count = Mathf.Min(inputFields.Count, plusButtons.Count, minusButtons.Count);

        for (int i = 0; i < count; i++)
        {
            int index = i;

            var inputField = inputFields[index];
            var plusButton = plusButtons[index];
            var minusButton = minusButtons[index];

            inputField.value = DataManager.Instance.CurrentMachine.Links[index].RotateAngle.ToString();
            inputField.RegisterValueChangedCallback(evt =>
            {
                if (int.TryParse(evt.newValue, out int value))
                {
                    DataManager.Instance.CurrentMachine.Links[index].RotateAngle = value;
                }
                else
                {
                    inputField.value = DataManager.Instance.CurrentMachine.Links[index].RotateAngle.ToString();
                }
            });

            plusButton.clicked += () =>
            {
                DataManager.Instance.CurrentMachine.Links[index].RotateAngle++;
                inputField.value = DataManager.Instance.CurrentMachine.Links[index].RotateAngle.ToString();
            };

            minusButton.clicked += () =>
            {
                DataManager.Instance.CurrentMachine.Links[index].RotateAngle--;
                inputField.value = DataManager.Instance.CurrentMachine.Links[index].RotateAngle.ToString();
            };

            DataManager.Instance.CurrentMachine.Links[index].OnChangeRotateAngle += (value) =>
            {
                if (value >= Link.MaxValue)
                    plusButtons[index].SetEnabled(false);
                else
                    plusButtons[index].SetEnabled(true);

                if (value <= Link.MinValue)
                    minusButtons[index].SetEnabled(false);
                else
                    minusButtons[index].SetEnabled(true);
            };
        }

        UI_Manager.Instance.Get<InspectorUI>().OnGenerateNewMachineObject += (obj) =>
        {
            Initialize();
        };
    }

    public void Initialize()
    {
        var uiDocument = GetComponent<UIDocument>();
        var root = uiDocument.rootVisualElement;
        var inputFields = root.Query<TextField>("Value_TextField").ToList();
        
        for (int i = 0; i < inputFields.Count; i++)
        {
            var inputField = inputFields[i];
            inputField.value = DataManager.Instance.CurrentMachine.Links[i].RotateAngle.ToString();
        }
    }
}
