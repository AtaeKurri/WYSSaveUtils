using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Reflection;

namespace WYSSaveUtils
{
    public class SaveContent
    {
        public enum Difficulty
        {
            NONE = -1,
            INFINITELY_EASY = 0,
            EXTREMELY_EASY = 1,
            VERY_EASY = 2,
            EASY = 3
        }

        public enum Hats
        {
            NONE = -1,
            TOPHAT = 0,
            SNAIL = 1,
            UNICORN = 2,
            MOUNT = 3,
            CHRISTMAS = 4,
            SQUID = 5,
            SHIT = 6,
            HEART = 7
        }
        
        public int save_id { get; set; }
        public string GameVersion { get; set; }
        public int CurrentRoom { get; set; }
        public int GlobalDeaths { get; set; }
        public Difficulty CurrentDifficulty { get; set; }
        public List<string> PlayedVoiceLines { get; set; }
        public bool LockedDifficulty { get; set; }
        public List<int> DeathsPerLevel { get; set; }
        public List<int> TimePerLevel { get; set; }
        public List<int> UnlockedLevels { get; set; }
        public List<int> LevelsBeatenOnDifficulty { get; set; }
        public List<int> LevelsBeatenOnDifficultyUnderwater { get; set; }
        public List<int> PlaytimePerLevelAutoDiff { get; set; }
        public List<int> DeathPerLevelAutoDiff { get; set; }
        public int AutodiffLevelsTillIncrease { get; set; }
        public float TimerGame { get; set; }
        public float TimerChapter { get; set; }
        public int CurrentChapter { get; set; }
        public float TimerLevel { get; set; }
        public bool ExplorationMode { get; set; }
        public List<string> CollectedExplorationPoints { get; set; }
        public float GameSpeed { get; set; }
        public bool TrainingMode { get; set; }
        public List<string> UnlockedDialogs { get; set; }
        public Hats Hat { get; set; }
        public bool HeartFixed { get; set; }
        public bool FinalCreditReached { get; set; }
        public bool PumpInverted { get; set; }
        public bool SpeedRunLegit { get; set; }
        public int AngerGameLevel { get; set; }
        public float AngerGameXP { get; set; }
        public bool AutoDifficulty { get; set; }
        public bool FixedJumpheight { get; set; }
    }

    /// <summary>
    /// Access the field names of the savefile contents.
    /// </summary>
    public static class Fields
    {
        public static readonly string GameID = null;
        public static readonly string GameVersion = "Game Version";
        public static readonly string CurrentRoom = "Room";
        public static readonly string GlobalDeaths = "Deaths";
        public static readonly string CurrentDifficulty = "Difficulty";
        public static readonly string PlayedVoiceLines = "Played Voice Lines";
        public static readonly string LockedDifficulty = "Difficulty Settings Unlocked";
        public static readonly string DeathsPerLevel = "Level Data: Deaths";
        public static readonly string TimePerLevel = "Level Data: Playtime";
        public static readonly string UnlockedLevels = "Level Data: Unlocked";
        public static readonly string LevelsBeatenOnDifficulty = "Level Data: Beaten On Difficulty";
        public static readonly string LevelsBeatenOnDifficultyUnderwater = "Level Data: Beaten On Difficulty Underwater";
        public static readonly string PlaytimePerLevelAutoDiff = "Level Data: Autodiff-Playtime";
        public static readonly string DeathPerLevelAutoDiff = "Level Data: Autodiff-Deaths";
        public static readonly string AutodiffLevelsTillIncrease = "Autodiff Levels Till Increase";
        public static readonly string TimerGame = "Timer Game";
        public static readonly string TimerChapter = "Timer Chapter";
        public static readonly string CurrentChapter = "Current Chapter";
        public static readonly string TimerLevel = "Timer Level";
        public static readonly string ExplorationMode = "Exploration Mode";
        public static readonly string CollectedExplorationPoints = "Collected Exploration Points";
        public static readonly string GameSpeed = "Game Speed";
        public static readonly string TrainingMode = "Training Mode";
        public static readonly string UnlockedDialogs = "Unlocked Dialogs";
        public static readonly string Hat = "Hat";
        public static readonly string HeartFixed = "Heart Fixed";
        public static readonly string FinalCreditReached = "Final Credits Reached";
        public static readonly string PumpInverted = "Pump Inverted";
        public static readonly string SpeedRunLegit = "Speedrun Is Still Legit";
        public static readonly string AngerGameLevel = "Anger Game Level";
        public static readonly string AngerGameXP = "Anger Game XP";
        public static readonly string AutoDifficulty = "Auto Difficulty";
        public static readonly string FixedJumpheight = "Fixed Jump Height";
    }
}
