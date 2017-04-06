using UnityEngine;
using System.Collections;

namespace GLOBAL{
    
    public class GAME : MonoBehaviour {
        public const int max_health = 50;
        public const int healthpickupvalue = 5;
        public const float invulnerable_timer = 2f;
        public const float player_velocity = 3f;
        public const int acidCyclesPerHealthPickup = 1;


        public const int dashes = 3;
        public const float dash_force = 10f;
        public const float dash_timer = 0.25f;
        public const float dash_cooldown = 2f;
        
        public const int jumps = 1;
        public const float jump_force = 250;
        public const float jump_velocity = 5;


		public const float playerdagger_lifetime = 7.5f;
        public const float playerdagger_aspd = 0.25f;
        public const float playerdagger_speed = 4.5f;
        public const float player_ground_jump_time = 0.1f;

		public const float playerclusterbomb_lifetime = 1.5f;
        public const float playerclusterbomb_aspd = 1f;
        public const float playerclusterbomb_speed = 3f;
        public const float playersword_aspd = 1f;


        public const float acid_dot_timer = 2f;
        public const float acid_stay_timer = 12f;
        public const int tile_size = 64;

        public const float loadoutIndicatorDecaySpeed = .03f;
        public const float loadoutLifetime = 10;
        public const float waveTimeInMins = 2;
        public const float num_waves = 3;
		public const int num_weapons = 3;
		public const int weaps_max_lvl = 3;

        public const float WalkingShigella_aspd = 2f;

        public const float FlyingShigella_aspd = 3f;
        public const float FlyingShigella_mvspd = 1f;
        public const float FlyingShigella_patrolDist = 2f;
        public const float FlyingShigella_projlife = 4f;
        public const float FlyingShigella_projspeed = 2f;

        public const int Shigellang_Fighting_MaxHealth = 400;
        public const int Shigellang_Dormant_MaxHealth = 100;
        public const float Shigellang_Dormant_TimeBetweenAttacks = 1f;
        public const float Shigellang_mvspd = 1.25f;
        public const float Shigellang_walkdist = 2f;
        public const float Shigellang_PlayerSpottedMax = 2f;
        public const float Shigellang_LeapCD = 2f;
        public const float Shigellang_TimeIdleRange = 0.75f;
        public const float Shigellang_LeapAttackCD = 5f;
        public const float Shigellang_ProjectileCD = 4f;
        public const float Shigellang_ProjectileLife = 12f;
        public const float Shigellang_ProjectileSpeed = 1.5f;
        public const float Shigellang_RadarPlayer = 12f;
        public const float Shigellang_RadarMap = 18f;
		public const float Shigellang_LeapSpeed = 2.5f;
		public const float Shigellang_JumpForce = 15f;
		public const float Shigellang_DMGTimer = 0.75f;
        public const float Shigellang_AngleProjectile = 15f;

    	
		public const string PLAYER_PREFS_RP = "PlayerResearchPoints";
		public const string PLAYER_PREFS_WEAPLEVEL = "WeaponLVL";



		public static readonly int[,] RP_UPGRADE = {{0,150,200},
													{0,150,200},
													{0,150,200}};

		public static readonly int[,] WEAP_DAMAGE = new int[,]{ {15,30,60},
													     {3,6,12},
                                                         {10,20,30}};

        public const int LevelStomach_WAVECOUNTER = 3;
        public static readonly float[] RESISTANCE_TICK = {0.125f,0.125f, 0.125f};
        public const float resitanceTickTimer = 30f;
        public const float peakResist = 0.85f;

        public static readonly int[] NUM_BACTERIA_WAVE = { 20, 30, 40 };
        public const int PillPickupTimeLimit = 20; // in seconds
        public static string[] messages = new string[] {
            "NEVER self-medicate! Consult with a physician before taking antibiotics.",
            "If you miss your scheduled antibiotic dosage, reset your succeeding intake intervals from the time you took your late dosage.",
            "Always finish taking your prescribed medicines! Not finishing your medication will lead to further complications.",
            "Research for newer and stronger antibiotics is incredibly expensive.",
            "Some of the most common antibiotics are Amoxicillin, Ciprofloxacin, and Penicillin.",
            "Acquiring antibiotics without a prescription is against the law.",
            "Believe it or not, common colds don't require antibiotics. In fact, antibiotics do nothing to cure common colds.",
            "Some antibiotics kill bacteria (bactericidal) while others prevent multiplication (bacteriostatic).",
            "The very first antibiotics came from molds and were used by the Chinese in 1000 BC.",
            "Before the discovery of Teixobactin in 2015, the last antibiotic discovery made was back in 1987.",
            "Many antibiotics come from molds. This is how Alexander Fleming discovered Penicillin!"
        };
    }


    public interface IDamage {
        void TakeDamage(int dmg);
    }
	public interface IPlayerDamage{
		int Damage ();
	}


}


