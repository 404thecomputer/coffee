using UnityEngine;
using TMPro;
using System;
using System.Collections;

public class GameManager : MonoBehaviour
{
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static GameManager Instance { get; private set; }


    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] GameObject dialoguePanel;

    public static event Action OnDialogueStarted;
    public static event Action OnDialogueEnded;
    bool skipLineTriggered;

    public void StartDialogue(string[] dialogue, int startPosition, string name, int stopPosition)
    {
        Debug.Log(name);
        Debug.Log(dialogue);
        Debug.Log(startPosition);
        Debug.Log(nameText);
        nameText.text = name + "...";

        dialoguePanel.SetActive(true);

        StopAllCoroutines();

        StartCoroutine(RunDialogue(dialogue, startPosition, stopPosition));

    }

    IEnumerator RunDialogue(string[] dialogue, int startPosition, int stopPosition)
    {
        skipLineTriggered = false;
        OnDialogueStarted?.Invoke();

        for (int i = startPosition; i < dialogue.Length; i++)
        {
            if (i == stopPosition)
            {
                break;
            }
            //dialogueText.text = dialogue[i];
            dialogueText.text = null;
            StartCoroutine(TypeTextUncapped(dialogue[i]));

            while (skipLineTriggered == false)
            {
                // Wait for the current line to be skipped
                yield return null;
            }
            skipLineTriggered = false;
        }

        OnDialogueEnded?.Invoke();
        dialoguePanel.SetActive(false);
    }

    public void SkipLine()
    {
        skipLineTriggered = true;
    }

    public void ShowDialogue(string dialogue, string name)
    {
        nameText.text = name + "...";
        StartCoroutine(TypeTextUncapped(dialogue));
        dialoguePanel.SetActive(true);
    }

    public void EndDialogue()
    {
        nameText.text = null;
        dialogueText.text = null;
        dialoguePanel.SetActive(false);
    }

    float charactersPerSecond = 90;

    IEnumerator TypeTextUncapped(string line)
    {
        float timer = 0;
        float interval = 1 / charactersPerSecond;
        string textBuffer = null;
        char[] chars = line.ToCharArray();
        int i = 0;

        while (i < chars.Length)
        {
            if (timer < Time.deltaTime)
            {
                textBuffer += chars[i];
                dialogueText.text = textBuffer;
                timer += interval;
                i++;
            }
            else
            {
                timer -= Time.deltaTime;
                yield return null;
            }
        }
    }
    public GameObject coffee;
    public GameObject[] enemies;

    public void resetEnemyLocations()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            EnemyBehavior s = enemies[i].GetComponent<EnemyBehavior>();
            s.resetLocation();
        }
    }
    public void activateEnemies()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            EnemyBehavior s = enemies[i].GetComponent<EnemyBehavior>();
            s.activate();
        }
    }

    public void resetArena()
    {
        resetEnemyLocations();
        Vector3 pos = new Vector3(0, 31);

        Instantiate(coffee, pos, Quaternion.identity);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
