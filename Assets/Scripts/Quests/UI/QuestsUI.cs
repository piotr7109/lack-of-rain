using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class QuestsUI : MonoBehaviour {
    public GameObject journalUI;
    public Transform questsParent;
    public Transform questPrefab;
    public Transform titleUI;

    private QuestManager questManager;
    private QuestStatus mode;

    void Start() {
        questManager = QuestManager.instance;
        questManager.onJournalChanged += UpdateUI;
        UpdateUI();
        mode = QuestStatus.IN_PROGRESS;
    }

    void Update() {
        if (Input.GetButtonDown("Journal")) {
            if (!journalUI.activeSelf) {
                Show();
            } else {
                Hide();
            }
        }

        if (Input.GetButtonDown("Cancel")) {
            Hide();
        }
    }

    void Show() {
        TimeManager.instance.StopTime();
        UpdateUI();
        journalUI.SetActive(true);
    }

    void Hide() {
        TimeManager.instance.RestoreTime();
        journalUI.SetActive(false);
    }

    public void SwitchMode() {
        if (mode == QuestStatus.IN_PROGRESS) {
            mode = QuestStatus.FINISHED;
            titleUI.GetComponent<Text>().text = "Done";
        } else {
            mode = QuestStatus.IN_PROGRESS;
            titleUI.GetComponent<Text>().text = "Current";
        }

        UpdateUI();
    }

    void UpdateUI() {
        foreach (Transform child in questsParent) {
            Destroy(child.gameObject);
        }

        questManager.quests
            .FindAll(quest => quest.status == mode)
            .ForEach(quest => {
                Transform questInstance = Instantiate(questPrefab, questsParent);
                questInstance.Find("Title").GetComponent<Text>().text = quest.title;
                questInstance.Find("Scroll").Find("Description").GetComponent<Text>().text = quest.description;
            });
    }
}
