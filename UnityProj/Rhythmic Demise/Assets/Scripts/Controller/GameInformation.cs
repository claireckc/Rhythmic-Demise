using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;

[System.Serializable]
public class GameInformation : MonoBehaviour {
    public const int RANGENUMBER = 3;
    public static GameInformation gameInfo;    //static is not serialized, this only for loading data
    
    public GameInformation()
    {
        Initialization();
    }

    [System.Serializable]
    public struct TroopSelected
    {
        public Troops troop;
        public int troopCount;
    }

    [System.Serializable]
    public struct PlayerSave
    {
        /*****************************Reource****************************/
        public int totalResource; //to level up(suagr or carbon)
        public int totalEnergy;    //to summon, map progress

        /*****************************Troop information****************************/
        public Enums.JobType leaderType;
        public Enums.SkillName skillSelected;      //skill is checked with leaderType and skillSelected type
        public Enums.CharacterType chosenUnit; //cancer or diabetic unit, initialize all troops characterType
        public TroopSelected[] troopSelected;  //array of 3

        /*****************************Map****************************/
        public MainMap[] mapProgress;  //contains all the map information, 17 main maps


        /*****************************Settings****************************/
        public float globalVol, sfxVol;
        public float buttonAlpha;
        
    }
    
    public PlayerSave playerSave;

    public void Awake()
    {
        //begin loading from loadinformation script
        gameInfo = this;

        if (gameInfo.playerSave.chosenUnit == Enums.CharacterType.None)
            Initialization();
        else
            print(gameInfo.playerSave.chosenUnit);
    }

    public void Initialization()
    {
        //begin initialization
        playerSave.totalEnergy = 5;
        playerSave.totalResource = 0;

        playerSave.leaderType = Enums.JobType.None;
        playerSave.skillSelected = Enums.SkillName.None;

        playerSave.troopSelected = new TroopSelected[Enums.TROOPSELECTED];

        for(int i = 0; i < playerSave.troopSelected.Length; i++)
        {
            playerSave.troopSelected[i].troop = new Troops();
            playerSave.troopSelected[i].troop.SetInfo(Enums.CharacterType.None, Enums.JobType.None, 0, 0);
            playerSave.troopSelected[i].troop.SetStats(0, 0, 0, 0);

            playerSave.troopSelected[i].troop.skills = new Skills[3];
        }

        playerSave.mapProgress = new MainMap[Enums.MAINMAPCOUNT];
        for(int i = 0; i < playerSave.mapProgress.Length; i++)
        {
            playerSave.mapProgress[i] = new MainMap();
            playerSave.mapProgress[i].NewMap(i);


        }
        playerSave.globalVol = playerSave.sfxVol = playerSave.buttonAlpha = 1.0f;
    }

    public void Start()
    {
        //game information is a singleton
        if (gameInfo != null)
            Destroy(gameObject);
        else
        {
            DontDestroyOnLoad(gameObject);
            gameInfo = this;
        }
    }
}
