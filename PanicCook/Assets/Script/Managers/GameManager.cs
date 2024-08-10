using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum GameState
{
    InitState,      // 料理のリセット、主人公のリセット、客のリセット
    WaitGuest,      // 客が出てきて、オーダーを表示
    PlayerTurn,     // プレイヤの移動、料理の提供
    ScoreState,     // スコアの計算
}

public class GameManager : MonoBehaviour
{
    
}
