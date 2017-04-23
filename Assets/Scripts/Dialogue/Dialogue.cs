using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour {

    /*
        Characters that distinguish different parts of the line
        -------------------------------------------------------

        <- -> = Character name. ex: <-CHAR NAME HERE->
        -x- = This is the end of the line.
    */

    public static Dialogue _instance;

    public string[] lines;
    //public List<char> charArr = new List<char>();
    private string currentLine = string.Empty;

    private float characterDelay;

    private float normalCharacterDelay = 0.025f;
    private float fastCharacterDelay = 0.005f;
    private float autoAdvanceCharacterDelay = 0.04f;
    private float autoAdvanceDelay = 0.5f;

    public int index = 0;

    public WorldManager world;

    public GameObject dialogueContainer;
    public Text dialogueTextZone;
    public Text charNameTextZone;
    public Text continueText;
    public Image portrait;

    private bool endOfLine = false;
    private bool finishedTyping = false;
    [HideInInspector]
    public bool convoInProgress = false;
    private bool autoAdvance = false;

    void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

	// Use this for initialization
	void Start () {
        characterDelay = normalCharacterDelay;
        //Debug.Log(this.transform.root.name);
        StartDialogue(0, false);
        //CheckIfEndOfLine(charArr);

    }
	
	// Update is called once per frame
	void Update () {
        if (finishedTyping)
        {
            if(!autoAdvance)
                continueText.enabled = true;

            if (Input.anyKeyDown && !autoAdvance)
            {
                if (endOfLine)
                {
                    convoInProgress = false;
                    dialogueContainer.SetActive(false);
                    TimeManager._timeManager.Unpause();

                    if(world.win || world.lose)
                    {
                        SceneManager.LoadScene(0);
                    }
                }
                else
                {
                    StartDialogue(index + 1, false);
                }
            }else if(autoAdvance)
            {
                if (endOfLine)
                {
                    convoInProgress = false;
                    dialogueContainer.SetActive(false);
                }
                else
                {
                    StartDialogue(index + 1, true);
                }
            }
        }
        else
        {
            if(!autoAdvance)
            {
                if (Input.anyKey)
                {
                    characterDelay = fastCharacterDelay;
                }
                else
                {
                    characterDelay = normalCharacterDelay;
                }
            }         
        }
    }

    public void StartDialogue(int desiredIndex, bool autoadvance)
    {
        Debug.LogWarning("Starting line at index" + desiredIndex);
        dialogueContainer.SetActive(true);

        autoAdvance = autoadvance;
        continueText.enabled = false;
        convoInProgress = true;
        endOfLine = false;
        finishedTyping = false;

        index = desiredIndex;
        currentLine = lines[index];
        characterDelay = normalCharacterDelay;

        if(!autoadvance)
        {
            characterDelay = normalCharacterDelay;
            TimeManager._timeManager.Pause();
        }
        else
        {
            characterDelay = autoAdvanceCharacterDelay;
        }

        StartCoroutine(TypeLine());
    }

    private IEnumerator TypeLine()
    {
        Debug.Log("TypeLine");

        string finalLine = string.Empty;
        string line = string.Empty;
        string name = string.Empty;

        List<char> chars = new List<char>();
        chars.AddRange(currentLine.ToCharArray());

        finalLine = currentLine;


        bool isEndOfLine = CheckIfEndOfLine(chars);
        if (isEndOfLine)
        {
            endOfLine = true;
            for (int j = 0; j < 3; j++)
            {
                chars.RemoveAt(chars.Count - 1);
            }
        }

        int nameChars = ParseName(chars);

        for (int j = 0; j < nameChars; j++)
        {
            if (j > 1 && j < nameChars - 1)
            {
                name += chars[j];
            }
        }

        chars.RemoveRange(0, nameChars + 1);
        charNameTextZone.text = name;

        for (int i = 0; i < chars.Count; i++)
        {
            finalLine += chars[i];
        }

        SetPortrait(name.ToLower());


        //int loc = 0;
        for (int i = 0; i < chars.Count; i++)
        {
            //Debug.Log(line.ToString() + " vs " + finalLine);
            //Debug.Log("Typing a char.");
            line += chars[i];
            dialogueTextZone.text = line.ToString();
            yield return new WaitForSeconds(characterDelay);
        }

        if(autoAdvance)
        {
            yield return new WaitForSeconds(autoAdvanceDelay);
        }

        finishedTyping = true;
    }

    private void SetPortrait(string name)
    {
        //Debug.Log("Loading " + name);
        Sprite s = Resources.Load(name, typeof(Sprite)) as Sprite;
        portrait.sprite = s;
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
