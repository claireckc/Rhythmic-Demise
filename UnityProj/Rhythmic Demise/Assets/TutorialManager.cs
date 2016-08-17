using UnityEngine;
using System.Collections;

public class TutorialManager : MonoBehaviour {

    public static TutorialManager TutManager;

    const int POS = 4;
    GameObject arrow, attack, defend;
    public GameObject[] noteLocation;
    public GameObject IconPos;
    bool firstTowerDead, secondTowerDead;

    GameObject redNote, blueNote, greenNote, yellowNote;
    public Tower tower1, tower2;
    Vector3 scale;

    public Enums.TutMove currentPlaying;

    void Start()
    {
        TutManager = new TutorialManager();
        currentPlaying = Enums.TutMove.None;
        firstTowerDead = secondTowerDead = false;
        arrow = Resources.Load<GameObject>("Prefabs/GamePlay/Right Arrow Icon");
        attack = Resources.Load<GameObject>("Prefabs/GamePlay/Attack");
        defend = Resources.Load<GameObject>("Prefabs/GamePlay/Defend");

        redNote = Resources.Load<GameObject>("Prefabs/GamePlay/Red Note");
        blueNote = Resources.Load<GameObject>("Prefabs/GamePlay/Blue Note");
        greenNote = Resources.Load<GameObject>("Prefabs/GamePlay/Green Note");
        yellowNote = Resources.Load<GameObject>("Prefabs/GamePlay/Yellow Note");

        scale = new Vector3(0.5f, 0.5f, 0.5f);

        InitNoteLocation();
    }

    void FixedUpdate()
    {
        if (tower1.IsDead && !firstTowerDead)
        {
            PlayMoveRight();
            firstTowerDead = true;
        }

        if (tower2.IsDead && !secondTowerDead)
        {
            PlayMoveRight();
            secondTowerDead = true;
        }
    }

    void InitNoteLocation()
    {
        noteLocation[0] = GameObject.Find("Note Position/Note 1 Position");
        noteLocation[1] = GameObject.Find("Note Position/Note 2 Position");
        noteLocation[2] = GameObject.Find("Note Position/Note 3 Position");
        noteLocation[3] = GameObject.Find("Note Position/Note 4 Position");
    }

    public void Hide(int index)
    {
        noteLocation[index].GetComponent<SpriteRenderer>().enabled = false;
    }

    void ShowAll()
    {
        for (int i = 0; i < noteLocation.Length; i++)
        {
            noteLocation[i].GetComponent<SpriteRenderer>().enabled = true;
        }
    }
    void InitRightNotes()
    {
        //red red green yellow
        noteLocation[0].GetComponent<SpriteRenderer>().sprite = redNote.GetComponent<SpriteRenderer>().sprite;
        noteLocation[1].GetComponent<SpriteRenderer>().sprite = redNote.GetComponent<SpriteRenderer>().sprite;
        noteLocation[2].GetComponent<SpriteRenderer>().sprite = greenNote.GetComponent<SpriteRenderer>().sprite;
        noteLocation[3].GetComponent<SpriteRenderer>().sprite = yellowNote.GetComponent<SpriteRenderer>().sprite;
    }

    void InitUpNotes()
    {
        //red red yellow yellow
        noteLocation[0].GetComponent<SpriteRenderer>().sprite = redNote.GetComponent<SpriteRenderer>().sprite;
        noteLocation[1].GetComponent<SpriteRenderer>().sprite = redNote.GetComponent<SpriteRenderer>().sprite;
        noteLocation[2].GetComponent<SpriteRenderer>().sprite = yellowNote.GetComponent<SpriteRenderer>().sprite;
        noteLocation[3].GetComponent<SpriteRenderer>().sprite = yellowNote.GetComponent<SpriteRenderer>().sprite;
    }

    void InitDownNotes()
    {
        //red red green green
        noteLocation[0].GetComponent<SpriteRenderer>().sprite = redNote.GetComponent<SpriteRenderer>().sprite;
        noteLocation[1].GetComponent<SpriteRenderer>().sprite = redNote.GetComponent<SpriteRenderer>().sprite;
        noteLocation[2].GetComponent<SpriteRenderer>().sprite = greenNote.GetComponent<SpriteRenderer>().sprite;
        noteLocation[3].GetComponent<SpriteRenderer>().sprite = greenNote.GetComponent<SpriteRenderer>().sprite;
    }

    void InitAttackNotes()
    {
        //green green green blue
        noteLocation[0].GetComponent<SpriteRenderer>().sprite = greenNote.GetComponent<SpriteRenderer>().sprite;
        noteLocation[1].GetComponent<SpriteRenderer>().sprite = greenNote.GetComponent<SpriteRenderer>().sprite;
        noteLocation[2].GetComponent<SpriteRenderer>().sprite = greenNote.GetComponent<SpriteRenderer>().sprite;
        noteLocation[3].GetComponent<SpriteRenderer>().sprite = blueNote.GetComponent<SpriteRenderer>().sprite;
    }

    void InitDefendNotes()
    {
        //yellow yellow yellow red
        noteLocation[0].GetComponent<SpriteRenderer>().sprite = yellowNote.GetComponent<SpriteRenderer>().sprite;
        noteLocation[1].GetComponent<SpriteRenderer>().sprite = yellowNote.GetComponent<SpriteRenderer>().sprite;
        noteLocation[2].GetComponent<SpriteRenderer>().sprite = yellowNote.GetComponent<SpriteRenderer>().sprite;
        noteLocation[3].GetComponent<SpriteRenderer>().sprite = redNote.GetComponent<SpriteRenderer>().sprite;
    }

    void InitSkillNotes()
    {
        //red blue green yellow
        noteLocation[0].GetComponent<SpriteRenderer>().sprite = redNote.GetComponent<SpriteRenderer>().sprite;
        noteLocation[1].GetComponent<SpriteRenderer>().sprite = blueNote.GetComponent<SpriteRenderer>().sprite;
        noteLocation[2].GetComponent<SpriteRenderer>().sprite = greenNote.GetComponent<SpriteRenderer>().sprite;
        noteLocation[3].GetComponent<SpriteRenderer>().sprite = yellowNote.GetComponent<SpriteRenderer>().sprite;
    }

    void SetIcon(GameObject icon, float rotZ)
    {
        IconPos.GetComponent<SpriteRenderer>().sprite = icon.GetComponent<SpriteRenderer>().sprite;
        IconPos.GetComponent<Animator>().runtimeAnimatorController = icon.GetComponent<Animator>().runtimeAnimatorController;

        if (icon == arrow)
            IconPos.transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
        else if (icon == defend)
            IconPos.transform.localScale = scale;
        else
            IconPos.transform.localScale += scale - new Vector3(0.25f, 0.25f, 0.25f);
        
    }

    public void PlayMoveRight()
    {
        SetIcon(arrow, 0f);
        InitRightNotes();
        TutManager.currentPlaying = Enums.TutMove.Right;
        //currentPlaying = Enums.TutMove.Right;
    }

   public void PlayMoveUp()
    {
        SetIcon(arrow, 90f);
        InitUpNotes();
        TutManager.currentPlaying = Enums.TutMove.Up;
    }

    public void PlayMoveDown()
    {
        SetIcon(arrow, -90f);
        InitDownNotes();
        TutManager.currentPlaying = Enums.TutMove.Down;
    }

    public void PlayAttack()
    {
        SetIcon(attack, 0f);
        InitAttackNotes();
        TutManager.currentPlaying = Enums.TutMove.Attack;
    }

    public void PlayDefend()
    {
        SetIcon(defend, 0f);
        InitDefendNotes();
        TutManager.currentPlaying = Enums.TutMove.Defend;
    }

    public void PlaySkill()
    {
        //Instantiate(arrow, IconPos.transform.position, Quaternion.Euler(0f, 0, -90f));        //skill icon
        InitSkillNotes();
        TutManager.currentPlaying = Enums.TutMove.Skill;
    }
    
    void Refresh()
    {
        for(int i = 0; i < noteLocation.Length; i++)
            Destroy(noteLocation[i]);
        Destroy(IconPos);
    }
}
