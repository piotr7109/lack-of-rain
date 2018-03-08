using System.Collections.Generic;

public class Npc : Interactable {

    public string dialogueFilename;
    public NpcReaction reaction = NpcReaction.None;
    public List<Quest> quests;

    private EnemyAI enemy;

    public override void Start() {
        base.Start();
        CloneQuests();
        SetTooltipSprite(prefabsManager.talkIcon);
        enemy = GetComponent<EnemyAI>();

        if (reaction != NpcReaction.Attack) {
            enemy.enabled = false;
        }
    }

    void CloneQuests() {
        for(int i = 0; i < quests.Count; i++) {
            quests[i] = Instantiate(quests[i]);
        }
    }

    public override void Interact() {
        base.Interact();

        if (dialogueFilename != "" && reaction != NpcReaction.Attack) {
            DialogueManager.instance.RunDialogue(dialogueFilename, this);
        }
    }

    void FixedUpdate() {
        if (enemy != null && reaction == NpcReaction.Attack && !enemy.enabled) {
            SetTooltipSprite(null);
            enemy.enabled = true;
        }
    }
}

public enum NpcReaction { Any, None, Attack };