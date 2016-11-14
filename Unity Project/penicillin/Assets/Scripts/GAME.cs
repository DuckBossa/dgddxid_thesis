using UnityEngine;
using System.Collections;

namespace GLOBAL{
    
    public class GAME : MonoBehaviour {
        public const float range = .5f;
        public const int max_health = 3;

        public const float invulnerable_timer = 2f;
        public const float player_velocity = 3f;
        public const float player_atkRange = .25f;

        public const int dashes = 3;
        public const float dash_force = 10f;
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
	    
		public const string character_weapons_folder = "Characters/Penny/";


        
        public static readonly string[] character_weapon_swords =  {"basic_sword",
																	"upgraded_sword",
																	"final_sword",
																	"midair_atack_basic",
																	"midair_attack_upgraded",
																	"midair_attack_final"};
		public const int num_swords = 3;
		public const int sword_max_lvl = 3;

        public const float FlyingShigella_aspd = 1f;
        public const float FlyingShigella_mvspd = 1f;
        public const float FlyingShigella_patrolDist = 2f;
        public const float FlyingShigella_projlife = 4f;
        public const float FlyingShigella_projspeed = 2f;
    }

}


