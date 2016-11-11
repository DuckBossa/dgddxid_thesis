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
        public const float waveTimeInMins = 3;
        public const float num_waves = 4;
	    
		public const string character_weapons_folder = "Characters/Penny/";

        public static readonly string[] character_weapon_swords =  { "penny_sword_slash",
                                                              "penny_sword_slash_EVIL LOOKIGN SWORD HOSHIT",
                                                              "penny_sword_slash_BLUE",
                                                              "penny_sword_slash_GREEN",
                                                              "penny_sword_slash_PINK",
                                                             "penny_sword_slash_YELLOW"};

        /*
        public const string character_weapons_sword_1 = "penny_sword_slash";
        public const string character_weapons_sword_2 = "penny_sword_slash_EVIL LOOKIGN SWORD HOSHIT";
        public const string character_weapons_sword_3 = "penny_sword_slash_BLUE";
        public const string character_weapons_sword_4 = "penny_sword_slash_GREEN";
        public const string character_weapons_sword_5 = "penny_sword_slash_PINK";
        public const string character_weapons_sword_6 = "penny_sword_slash_YELLOW";
        */


        public const float FlyingShigella_aspd = 1f;
        public const float FlyingShigella_mvspd = 1f;
        public const float FlyingShigella_patrolDist = 2f;
        public const float FlyingShigella_projlife = 4f;
        public const float FlyingShigella_projspeed = 2f;
    }

}


