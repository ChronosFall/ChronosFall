using ChronosFall.Scripts.Characters.PlayerControl;
using ChronosFall.Scripts.Characters.PlayerControl.PlayerAttack;
using ChronosFall.Scripts.Systems.Enemies.Data;
using ChronosFall.Scripts.Systems.Player.Data;
using UnityEngine;
using UnityEngine.Serialization;

namespace ChronosFall.Scripts.Systems.Player.Base
{
    // 自動アタッチ
    [RequireComponent(typeof(PlayerAttack))]
    [RequireComponent(typeof(Movement))]
    public class PlayerBase : MonoBehaviour
    {
        
    }
}