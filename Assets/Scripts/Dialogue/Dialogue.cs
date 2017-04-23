using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour {

    /*
        Characters that distinguish different parts of the line
        -------------------------------------------------------

        <- -> = Character name. ex: <-CHAR NAME HERE->
        -x- = This is the end of the line.
    */

    public string[] lines;
    public List<char> charArr = new List<char>();
    private string currentLine = string.Empty;

    private float characterDelay = 0.1f;

    public int index = 0;

    public GameObject dialogueContainer;
    public Text dialogueTextZone;
    public Text charNameTextZone;

    private bool done = false;


	// Use this for initialization
	void Start () {
        //Debug.Log(this.transform.root.name);
        //StartDialogue(0);
        CheckIfEndOfLine(charArr);

    }
	
	// Update is called once per frame
	void Update () {
		if(done)
        {
            if(Input.anyKeyDown)
                dialogueContainer.SetActive(false);
        }
    }

    public void StartDialogue(int desiredIndex)
    {
        dialogueContainer.SetActive(true);
        done = false;
        index = desiredIndex;
        currentLine = lines[index];
        StartCoroutine(TypeLine());
    }

    private IEnumerator TypeLine()
    {
        Debug.Log("TypeLine");

        string line = string.Empty;
        string name = string.Empty;

        List<char> chars = new List<char>();
        chars.AddRange(currentLine.ToCharArray());

        int nameChars = ParseName(chars);

        for (int j = 0; j < nameChars; j++)
        {
            if (j > 1 || j < nameChars - 2)
            {
                name += chars[j];
            }
        }

        chars.RemoveRange(0, nameChars);
        

        bool isEndOfLine = CheckIfEndOfLine(chars);
        if (isEndOfLine)
        {
            done = true;
            for (int j = 0; j < 3; j++)
            {
                chars.RemoveAt(chars.Count - 1);
            }
        }
        else
        {
            StartDialogue(index + 1);
        }


        int i = 0;

        while(line.ToString() != currentLine)
        {
            Debug.Log("Typing a char.");
            line += chars[i];
            i++;
            dialogueTextZone.text = line.ToString();
            yield return new WaitForSeconds(characterDelay);
        }
    }

    private int ParseName(List<char> chars)
    {
        //What this essentially will do is return how many characters our name takes up (including the indicators).
        int num = 0;
        bool parseComplete = false;

        while(!parseComplete)
        {
            num++;
            if(chars[num] == '>')
            {
                parseComplete = true;
            }
        }


        return num;
    }

    private bool CheckIfEndOfLine(List<char> chars)
    {
        //<--> is character name.
        //--- is end of line.
        bool finished = false;

        if(chars.Count <= 3)
        {
            finished = false;
        }else if(
            chars[chars.Count - 1] == '-' &&
            chars[chars.Count - 2] == '-' &&
            chars[chars.Count - 3] == '-'
            )
        {
            finished = true;
        }

        Debug.Log(finished);
        return finished;
    }
}
