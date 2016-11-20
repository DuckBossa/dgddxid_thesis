using UnityEngine;
using System.Collections;

namespace GLOBAL{
    
    public class GAME : MonoBehaviour {
        public const int max_health = 15;

        public const float invulnerable_timer = 2f;
        public const float player_velocity = 3f;
        public const float player_atkRange = .25f;

        public const int dashes = 3;
        public const float dash_force = 6.5f;
        public const float dash_timer = 0.25f;
        public const float dash_cooldown = 2f;
        
        public const int jumps = 1;
        public const float jump_force = 250f;
        public const float jump_velocity = 5;

        public const float acid_dot_timer = 2f;
        public const int tile_size = 64;

        public const float loadoutLifetime = 10;
        public const float waveTimeInMins = 2;
        public const float num_waves = 3;
        public const float loadoutIndicatorDecaySpeed = .03f;

		public const int num_swords = 3;
		public const int sword_max_lvl = 3;

        public const float FlyingShigella_aspd = 1f;
        public const float FlyingShigella_mvspd = 1f;
        public const float FlyingShigella_patrolDist = 2f;
        public const float FlyingShigella_projlife = 4f;
        public const float FlyingShigella_projspeed = 2f;

        public const int Shigellang_Dormant_MaxHealth = 15;
        public const float Shigellang_Dormant_TimeBetweenAttacks = 1f;

        public const float Shigellang_mvspd = 1.25f;
        public const float Shigellang_walkdist = 2f;
        public const float Shigellang_LeapCD = 2f;
        public const float Shigellang_TimeIdleRange = 0.75f;
        public const float Shigellang_LeapAttackCD = 5f;
        public const float Shigellang_ProjectileCD = 4f;
        public const float Shigellang_ProjectileLife = 5f;
        public const float Shigellang_ProjectileSpeed = 1.5f;
        public const float Shigellang_RadarPlayer = 10f;
        public const float Shigellang_RadarMap = 30f;
		public const float Shigellang_LeapSpeed = 2.5f;
		public const float Shigellang_JumpForce = 15f;

        public const float Shigellang_Active_MaxHealth = 15;
    }

}


