using UnityEngine;
using System.Collections;

public class Enums : MonoBehaviour {
    public enum CharacterType { Cancer, Diabetic };
    public enum PlayerState { Idle, MoveUp, MoveDown, MoveLeft, MoveRight, Attack, Heal, Skill };
}
