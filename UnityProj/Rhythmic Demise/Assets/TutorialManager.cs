using UnityEngine;
using System.Collections;

public class TutorialManager : MonoBehaviour {

    const int POS = 4;
    GameObject arrow;
    public GameObject[] noteLocation;
    public GameObject IconPos;
    bool animate;

    GameObject redNote, blueNote, greenNote, yellowNote;

    void Start()
    {
        animate = false;

        arrow = Resources.Load<GameObject>("Prefabs/GamePlay/Right Arrow Icon");
        print(arrow);

        redNote = Resources.Load<GameObject>("Prefabs/GamePlay/Red Note");
        blueNote = Resources.Load<GameObject>("Prefabs/GamePlay/Blue Note");
        greenNote = Resources.Load<GameObject>("Prefabs/GamePlay/Green Note");
        yellowNote = Resources.Load<GameObject>("Prefabs/GamePlay/Yellow Note");
    }

    void FixedUpdate()
    {
        if (animate) {
            InitAnim();
            Invoke("StopAnimation", 4);
            Invoke("StartAnimation", 5);
            StopAnimation();
        }
    }

    void StartAnimation()
    {
        animate = true;
    }

    void StopAnimation()
    {
        animate = false;
    }
    
    void InitRightNotes()
    {
        //red red green yellow
        noteLocation[0] = Instantiate(redNote, noteLocation[0].transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
        noteLocation[1] = Instantiate(redNote, noteLocation[1].transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
        noteLocation[2] = Instantiate(greenNote, noteLocation[2].transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
        noteLocation[3] = Instantiate(yellowNote, noteLocation[3].transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
    }

    void InitUpNotes()
    {
        //red red yellow yellow
        noteLocation[0] = Instantiate(redNote, noteLocation[0].transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
        noteLocation[1] = Instantiate(redNote, noteLocation[1].transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
        noteLocation[2] = Instantiate(yellowNote, noteLocation[2].transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
        noteLocation[3] = Instantiate(yellowNote, noteLocation[3].transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
    }

    void InitDownNotes()
    {
        //red red green green
        noteLocation[0] = Instantiate(redNote, noteLocation[0].transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
        noteLocation[1] = Instantiate(redNote, noteLocation[1].transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
        noteLocation[2] = Instantiate(greenNote, noteLocation[2].transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
        noteLocation[3] = Instantiate(greenNote, noteLocation[3].transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
    }

    void InitAttackNotes()
    {
        //green green green blue
        noteLocation[0] = Instantiate(greenNote, noteLocation[0].transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
        noteLocation[1] = Instantiate(greenNote, noteLocation[1].transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
        noteLocation[2] = Instantiate(greenNote, noteLocation[2].transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
        noteLocation[3] = Instantiate(blueNote, noteLocation[3].transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
    }

    void InitDefendNotes()
    {
        //yellow yellow yellow red
        noteLocation[0] = Instantiate(yellowNote, noteLocation[0].transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
        noteLocation[1] = Instantiate(yellowNote, noteLocation[1].transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
        noteLocation[2] = Instantiate(yellowNote, noteLocation[2].transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
        noteLocation[3] = Instantiate(redNote, noteLocation[3].transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
    }

    void InitSkillNotes()
    {
        //red blue green yellow
        noteLocation[0] = Instantiate(redNote, noteLocation[0].transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
        noteLocation[1] = Instantiate(blueNote, noteLocation[1].transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
        noteLocation[2] = Instantiate(greenNote, noteLocation[2].transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
        noteLocation[3] = Instantiate(yellowNote, noteLocation[3].transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
    }

    void HideAll()
    {
        for(int i = 0; i < noteLocation.Length; i++)
            noteLocation[i].GetComponent<SpriteRenderer>().enabled = false;
    }

    void PlayMoveRight()
    {
        Instantiate(arrow, IconPos.transform.position, Quaternion.Euler(0f, 0f, 0f));
        InitRightNotes();
        HideAll();
        StartAnimation();
    }

    void PlayMoveUp()
    {
        Instantiate(arrow, IconPos.transform.position, Quaternion.Euler(0f, 0, 90f));
        InitUpNotes();
        HideAll();
        StartAnimation();
    }

    void PlayMoveDown()
    {
        Instantiate(arrow, IconPos.transform.position, Quaternion.Euler(0f, 0, -90f));
        InitDownNotes();
        HideAll();
        StartAnimation();
    }

    void PlayAttack()
    {
        //Instantiate(arrow, IconPos.transform.position, Quaternion.Euler(0f, 0, 0f));    //attack icon
        InitAttackNotes();
        HideAll();
        StartAnimation();
    }

    void PlayDefend()
    {
        //Instantiate(arrow, IconPos.transform.position, Quaternion.Euler(0f, 0, -90f));        //defend icon
        InitDefendNotes();
        HideAll();
        StartAnimation();

    }

    void PlaySkill()
    {
        //Instantiate(arrow, IconPos.transform.position, Quaternion.Euler(0f, 0, -90f));        //skill icon
        InitSkillNotes();
        HideAll();
        StartAnimation();

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

    void DestroyAll()
    {
        for(int i = 0; i < noteLocation.Length; i++)
            Destroy(noteLocation[i]);
    }

    void Blink(GameObject obj)
    {

    }
}
