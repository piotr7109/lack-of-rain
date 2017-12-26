using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour {

    #region Singleton

    public static DialogueManager instance;

    void Awake() {
        instance = this;
    }

    #endregion

    public GameObject dialogueUI;

    public Text dialogueText;
    public RectTransform optionsPanel;
    public Button optionPrefab;

    private static Dialogue dialogue;
    private Npc npc;

    public void RunDialogue(string fileName, Npc npc) {
        TimeManager.instance.StopTime();
        this.npc = npc;
        dialogue = DialogueSerialization.LoadDialogue(fileName);
        dialogueUI.SetActive(true);

        RunNode(0, NpcReaction.None);
    }

    void RunNode(int nodeIndex, NpcReaction reaction) {
        CleanOptionsPanel();

        if (nodeIndex == -1) {
            dialogueUI.SetActive(false);
            npc.reaction = reaction;
            dialogue = null;
            TimeManager.instance.RestoreTime();

            return;
        }

        DialogueNode node = dialogue.nodes[nodeIndex];
        dialogueText.text = node.text;

        node.options.ForEach(option => {
            if (CheckIfCanShowOption(option)) {
                Button optButton = Instantiate(optionPrefab, optionsPanel) as Button;
                optButton.GetComponentInChildren<Text>().text = option.text;
                optButton.onClick.AddListener(delegate {
                    RunNode(option.destinationNodeId, option.reaction);

                    int questId = option.getQuestId();

                    if (questId != -1) {
                        if (option.destinationNodeId == -1) {
                            if (option.questStatus == QuestStatus.NONE) {
                                QuestManager.instance.AddQuest(npc.quests[questId]);
                            }
                        }

                        if (option.questStatus == QuestStatus.IN_PROGRESS) {
                            if (npc.quests[questId].IsFinnished()) {
                                QuestManager.instance.FinishQuest(npc.quests[questId]);
                            } else {
                                Debug.Log("GET AWAY AND DO YOUR JOB!");
                            }
                        }
                    }
                });
            }
        });
    }

    private bool CheckIfCanShowOption(DialogueOption option) {
        if (option.excludedReactions.Count > 0) {
            List<NpcReaction> excluded = option.excludedReactions.FindAll(r => r == npc.reaction);
            if (excluded.Count > 0) {
                return false;
            }
        }

        if (option.getQuestId() != -1) {
            int questId = option.getQuestId();
            if (option.questStatus == npc.quests[questId].status) {
                if (option.questStatus == QuestStatus.IN_PROGRESS) {
                    return npc.quests[questId].IsFinnished();
                }

                return true;
            }

            return false;
        }

        if (option.reactionTrigger == NpcReaction.Any) {
            return true;
        }

        if (option.reactionTrigger == npc.reaction) {
            return true;
        }

        return false;
    }

    private void CleanOptionsPanel() {
        foreach (Transform child in optionsPanel) {
            Destroy(child.gameObject);
        }
    }
}