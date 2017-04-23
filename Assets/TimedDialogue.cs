using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDialogue : MonoBehaviour {

    public float[] dialogueDelays; //In seconds
    public int[] dialogueIndexes; //Starting from 0

    public Dialogue dialogue;

    public CollectAsteroids asteroidManager;
    private bool smashDialogueSaid = false;

    public WorldManager world;
    private bool winDialogueSaid = false;
    private bool loseDialogueSaid = false;

    public float timer = 0f;
    public int index = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (TimeManager._timeManager.paused || dialogue.convoInProgress || winDialogueSaid || loseDialogueSaid)
            return;

        if(index < dialogueIndexes.Length)
        {
            timer += Time.deltaTime;

            if (timer >= dialogueDelays[index])
            {
                dialogue.StartDialogue(dialogueIndexes[index], true);
                timer = 0f;
                index++;
                //Dialogue._instance.StartDialogue(dialogueIndexes[index]);
            }
        }

        if (asteroidManager.size >= 6 && !smashDialogueSaid)
        {
            Debug.Log("Dialogue");
            smashDialogueSaid = true;
            dialogue.StartDialogue(12, true);
        }

        if (world.win && !winDialogueSaid)
        {
            winDialogueSaid = true;
            dialogue.StartDialogue(15, false);
        }

        if(world.lose && !loseDialogueSaid)
        {
            loseDialogueSaid = true;
            dialogue.StartDialogue(19, false);

        }
    }
}
