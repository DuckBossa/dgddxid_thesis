using UnityEngine;
using System.Collections;

namespace GLOBAL{
    
    public class GAME : MonoBehaviour {
        public const float range = .5f;
        public const float timeBetweenAttacks = .75f;
        public const int max_health = 3;

        public const float invulnerable_timer = 2f;
        public const float player_velocity = 3f;
        public const float player_atkRange = .5f;

        public const int dashes = 3;
        public const float dash_force = 10f;
        public const float dash_timer = 0.25f;
        public const float dash_cooldown = 2f;

        public const int jumps = 1;
        public const float jump_force = 250f;
        public const float jump_anim_loop = 1.05f;
        public const float jump_velocity = 5;

        public const float acid_dot_timer = 2f;
        public const int tile_size = 64;
    }

}


