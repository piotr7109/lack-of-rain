public class Npc : Interactable {

    public string dialogueFilename;
    public NpcReaction reaction = NpcReaction.None;
    private EnemyNpc enemy;

    public override void Start() {
        base.Start();
        enemy = GetComponent<EnemyNpc>();
    }

    public override void Interact() {
        base.Interact();

        if (dialogueFilename != "" && reaction != NpcReaction.Attack) {
            DialogueManager.instance.RunDialogue(dialogueFilename, this);
        }
    }

    void FixedUpdate() {
        if(enemy != null && reaction == NpcReaction.Attack) {
            enemy.enabled = true;
        }
    }
}

public enum NpcReaction { None, Attack };