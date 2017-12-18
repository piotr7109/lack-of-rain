
public class Npc : Interactable {

    public string dialogueFilename;

    public Npc() {
        Interact();
    }

    public override void Interact() {
        base.Interact();

        if (dialogueFilename != null) {
            DialogueManager.instance.RunDialogue(dialogueFilename, gameObject);
        }
    }

}
