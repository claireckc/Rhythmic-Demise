using UnityEngine;
using System.Collections;

public class TutorialManager : MonoBehaviour {

    const int POS = 4;
    GameObject arrow, attack, defend;
    public GameObject[] noteLocation;
    public GameObject IconPos;
    bool firstTowerDead, secondTowerDead;

    GameObject redNote, blueNote, greenNote, yellowNote;
    public Tower tower1, tower2;
    Vector3 scale;

    void Start()
    {
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

    void Hide(int index)
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
   

    void PlayMoveRight()
    {
        SetIcon(arrow, 0f);
        InitRightNotes();
    }

    void PlayMoveUp()
    {
        SetIcon(arrow, 90f);
        InitUpNotes();
    }

    void PlayMoveDown()
    {
        SetIcon(arrow, -90f);
        InitDownNotes();
    }

    void PlayAttack()
    {
        SetIcon(attack, 0f);
        InitAttackNotes();
    }

    void PlayDefend()
    {
        SetIcon(defend, 0f);
        InitDefendNotes();
    }

    void PlaySkill()
    {
        //Instantiate(arrow, IconPos.transform.position, Quaternion.Euler(0f, 0, -90f));        //skill icon
        InitSkillNotes();

    }

    void InitAnim()
    {
        int time = 0;
        //call when notes are set in notePosition
        for (int i = 0; i < noteLocation.Length; i++)
        {
            PlayAnimation(++time, i);
        }
    }

    void PlayAnimation(int time, int index)
    {
        StartCoroutine(PlayAnim(time, index));
    }
    
    IEnumerator PlayAnim(int delay, int index)
    {
        noteLocation[index].GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(delay);
        noteLocation[index].GetComponent<SpriteRenderer>().enabled = true;
    }

    void PlayAnimation(int notePosition)
    {
        switch (notePosition)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
        }
    }

    void Refresh()
    {
        for(int i = 0; i < noteLocation.Length; i++)
            Destroy(noteLocation[i]);
        Destroy(IconPos);
    }

    void Blink(GameObject obj)
    {

    }
}
