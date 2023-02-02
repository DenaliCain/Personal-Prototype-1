using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class DifficultyPicker : MonoBehaviour
{
    public string[] AvailableDifficulties;
    public Button DifficultyButtonPrefab;

    public string SelectedDifficulty { get; private set; }
    public System.Action<string> onDifficultyChanged;

    List<Button> m_DifficultyButtons = new List<Button>();
    public void Init()
    {
        foreach (string difficulty in AvailableDifficulties)
        {
            var newButton = Instantiate(DifficultyButtonPrefab, transform);
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = difficulty;
            newButton.onClick.AddListener(() =>
            {
                SelectedDifficulty = difficulty;
                foreach (var button in m_DifficultyButtons)
                {
                    button.interactable = true;
                }

                onDifficultyChanged.Invoke(SelectedDifficulty);
            });
            m_DifficultyButtons.Add(newButton);
        }
    }
    public void SelectDifficulty(string difficulty)
    {
        for(int i = 0; i < AvailableDifficulties.Length; ++i)
        {
            if(AvailableDifficulties[i] == difficulty)
            {
                m_DifficultyButtons[i].onClick.Invoke();
            }
        }
    }
}

