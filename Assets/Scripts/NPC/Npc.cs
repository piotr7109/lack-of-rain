using System.Collections.Generic;

public class Npc : Interactable {

    public string dialogueFilename;
    public NpcReaction reaction = NpcReaction.None;
    public List<Quest> quests;
    
    private EnemyNpc enemy;

    public override void Start() {
        base.Start();
        SetTooltipSprite(prefabsManager.talkIcon);
        enemy = GetComponent<EnemyNpc>();
    }

    public override void Interact() {
        base.Interact();

        if (dialogueFilename != "" && reaction != NpcReaction.Attack) {
            DialogueManager.instance.RunDialogue(dialogueFilename, this);
        }
    }

    void FixedUpdate() {
        if (enemy != null && reaction == NpcReaction.Attack) {
            SetTooltipSprite(null);
            enemy.enabled = true;
        }
    }
}

public enum NpcReaction { Any, None, Attack };