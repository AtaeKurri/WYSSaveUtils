using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace WYSSaveUtils
{
    /// <summary>
    /// Creates a new SaveLoader handler. You need to create a new instance of it before doing anything else.
    /// </summary>
    public class SaveLoader
    {
        public static string saveFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Will_You_Snail";
        public SaveContent saveContent;
        // when true, it's loaded from a save slot, if false, it's loaded from an absolute path
        internal bool loaderMode = true;
        internal static int Slot = 0;
        internal static string Filepath = "";
        internal List<string> lines = new List<string>();

        public string[] SavePrefixes = {"", "S2-", "S3-"};
        internal FileStream stream;

        /// <summary>
        /// Load a save file directly from the save folder in %appdata%\Local\Will_You_Snail
        /// </summary>
        /// <param name="slot">Slot number of a save file between 0 and 2.</param>
        public SaveLoader(int slot)
        {
            Slot = slot;
            stream = new FileStream(saveFolder + "\\" + SavePrefixes[Slot] + "SaavoGame23-2.sav", FileMode.Open);
            PopulateLines();
            Reload();
        }

        /// <summary>
        /// Load a save file from an absolute path not located in the save folder
        /// </summary>
        /// <param name="filepath">Absolute path containing the save file name, finished with .sav</param>
        public SaveLoader(string filepath)
        {
            Filepath = filepath;
            loaderMode = false;
            stream = new FileStream(Filepath, FileMode.Open);
            PopulateLines();
            Reload();
        }

        /// <summary>
        /// Don't forget to call this method when you're finished with the save file to avoid it being used in memory for nothing.
        /// </summary>
        public void CloseLoader() => stream.Close();

        /// <summary>
        /// Changes the SaveLoader to be loading a direct savefile number with a slot id from 0 to 2.
        /// MUST NOT BE CALLED BEFORE SAVING CHANGES.
        /// </summary>
        /// <param name="slot">Slot number of a save file between 0 and 2.</param>
        public void ChangeMode(int slot)
        {
            Slot = slot;
            loaderMode = true;
            stream = new FileStream(saveFolder + "\\" + SavePrefixes[Slot] + "SaavoGame23-2.sav", FileMode.Open);
            PopulateLines();
            Reload();
        }

        /// <summary>
        /// Changes the SaveLoader to be loading a given file from a filepath in a string format.
        /// MUST NOT BE CALLED BEFORE SAVING CHANGES.
        /// </summary>
        /// <param name="filepath">Absolute path containing the save file name, finished with .sav</param>
        public void ChangeMode(string filepath)
        {
            Filepath = filepath;
            loaderMode = false;
            stream = new FileStream(Filepath, FileMode.Open);
            PopulateLines();
            Reload();
        }

        /// <summary>
        /// Reloads the content of the saveContent object to be read again by your program.
        /// Is called automatically by the EditValue method.
        /// </summary>
        public void Reload()
        {
            if (loaderMode)
            {
                CloseLoader();
                stream = new FileStream(saveFolder + "\\" + SavePrefixes[Slot] + "SaavoGame23-2.sav", FileMode.Open);
                FillSaveContent();
            }
            else
            {
                CloseLoader();
                stream = new FileStream(Filepath, FileMode.Open);
                FillSaveContent();
            }
        }

        /// <summary>
        /// Called to save the saveContent to the save file.
        /// Calls Reload() at the end, no need to call it yourself.
        /// </summary>
        public void WriteChanges()
        {
            CloseLoader();

            StreamWriter writer;
            if (loaderMode)
            {
                File.WriteAllText(saveFolder + "\\" + SavePrefixes[Slot] + "SaavoGame23-2.sav", string.Empty);
                Stream stream = File.OpenWrite(saveFolder + "\\" + SavePrefixes[Slot] + "SaavoGame23-2.sav");
                writer = new StreamWriter(stream);
            }
            else
            {
                File.WriteAllText(Filepath, string.Empty);
                Stream stream = File.OpenWrite(Filepath);
                writer = new StreamWriter(stream);
            }

            writer.WriteLine("This save file has been modified by WYSSaveUtils.");
            writer.WriteLine("It is to be considered cheating when using it in a speedrun or a competition of any kind.");
            writer.WriteLine("");

            writer.EditSingle(Fields.GameVersion, saveContent.GameVersion);
            writer.EditSingle(Fields.CurrentRoom, saveContent.CurrentRoom);
            writer.EditSingle(Fields.GlobalDeaths, saveContent.GlobalDeaths);
            writer.EditSingle(Fields.CurrentDifficulty, (int)saveContent.CurrentDifficulty);
            writer.EditList(Fields.PlayedVoiceLines, saveContent.PlayedVoiceLines);
            writer.EditSingle(Fields.LockedDifficulty, saveContent.LockedDifficulty);
            writer.EditList(Fields.DeathsPerLevel, saveContent.DeathsPerLevel);
            writer.EditList(Fields.TimePerLevel, saveContent.TimePerLevel);
            writer.EditList(Fields.UnlockedLevels, saveContent.UnlockedLevels);
            writer.EditList(Fields.LevelsBeatenOnDifficulty, saveContent.LevelsBeatenOnDifficulty);
            writer.EditList(Fields.LevelsBeatenOnDifficultyUnderwater, saveContent.LevelsBeatenOnDifficultyUnderwater);
            writer.EditList(Fields.PlaytimePerLevelAutoDiff, saveContent.PlaytimePerLevelAutoDiff);
            writer.EditList(Fields.DeathPerLevelAutoDiff, saveContent.DeathPerLevelAutoDiff);
            writer.EditSingle(Fields.AutodiffLevelsTillIncrease, saveContent.AutodiffLevelsTillIncrease);
            writer.EditSingle(Fields.TimerGame, saveContent.TimerGame);
            writer.EditSingle(Fields.TimerChapter, saveContent.TimerChapter);
            writer.EditSingle(Fields.CurrentChapter, saveContent.CurrentChapter);
            writer.EditSingle(Fields.TimerLevel, saveContent.TimerLevel);
            writer.EditSingle(Fields.ExplorationMode, saveContent.ExplorationMode);
            writer.EditList(Fields.CollectedExplorationPoints, saveContent.CollectedExplorationPoints);
            writer.EditSingle(Fields.GameSpeed, saveContent.GameSpeed);
            writer.EditSingle(Fields.TrainingMode, saveContent.TrainingMode);
            writer.EditList(Fields.UnlockedDialogs, saveContent.UnlockedDialogs);
            writer.EditSingle(Fields.Hat, (int)saveContent.Hat);
            writer.EditSingle(Fields.HeartFixed, saveContent.HeartFixed);
            writer.EditSingle(Fields.FinalCreditReached, saveContent.FinalCreditReached);
            writer.EditSingle(Fields.PumpInverted, saveContent.PumpInverted);
            writer.EditSingle(Fields.SpeedRunLegit, saveContent.SpeedRunLegit);
            writer.EditSingle(Fields.AngerGameLevel, saveContent.AngerGameLevel);
            writer.EditSingle(Fields.AngerGameXP, saveContent.AngerGameXP);
            writer.EditSingle(Fields.AutoDifficulty, saveContent.AutoDifficulty);
            writer.EditSingle(Fields.FixedJumpheight, saveContent.FixedJumpheight);

            writer.Close();
            Reload();
        }

        internal void PopulateLines()
        {
            lines = new List<string>();
            StreamReader reader = new StreamReader(stream);
            while (!reader.EndOfStream)
                lines.Add(reader.ReadLine());
            reader.Close();
        }

        internal void FillSaveContent()
        {
            StreamReader reader = new StreamReader(stream);
            saveContent = new SaveContent
            {
                GameVersion = reader.GetString(Fields.GameVersion),
                CurrentRoom = reader.GetInt(Fields.CurrentRoom),
                GlobalDeaths = reader.GetInt(Fields.GlobalDeaths),
                CurrentDifficulty = (SaveContent.Difficulty)reader.GetInt(Fields.CurrentDifficulty),
                PlayedVoiceLines = reader.GetStringList(Fields.PlayedVoiceLines),
                LockedDifficulty = reader.GetBool(Fields.LockedDifficulty),
                DeathsPerLevel = reader.GetIntList(Fields.DeathsPerLevel),
                TimePerLevel = reader.GetIntList(Fields.TimePerLevel),
                UnlockedLevels = reader.GetIntList(Fields.UnlockedLevels),
                LevelsBeatenOnDifficulty = reader.GetIntList(Fields.LevelsBeatenOnDifficulty),
                LevelsBeatenOnDifficultyUnderwater = reader.GetIntList(Fields.LevelsBeatenOnDifficultyUnderwater),
                PlaytimePerLevelAutoDiff = reader.GetIntList(Fields.PlaytimePerLevelAutoDiff),
                DeathPerLevelAutoDiff = reader.GetIntList(Fields.DeathPerLevelAutoDiff),
                AutodiffLevelsTillIncrease = reader.GetInt(Fields.AutodiffLevelsTillIncrease),
                TimerGame = reader.GetFloat(Fields.TimerGame),
                TimerChapter = reader.GetFloat(Fields.TimerChapter),
                CurrentChapter = reader.GetInt(Fields.CurrentChapter),
                TimerLevel = reader.GetFloat(Fields.TimerLevel),
                ExplorationMode = reader.GetBool(Fields.ExplorationMode),
                CollectedExplorationPoints = reader.GetStringList(Fields.CollectedExplorationPoints),
                GameSpeed = reader.GetFloat(Fields.GameSpeed),
                TrainingMode = reader.GetBool(Fields.TrainingMode),
                UnlockedDialogs = reader.GetStringList(Fields.UnlockedDialogs),
                Hat = (SaveContent.Hats)reader.GetInt(Fields.Hat),
                HeartFixed = reader.GetBool(Fields.HeartFixed),
                FinalCreditReached = reader.GetBool(Fields.FinalCreditReached),
                PumpInverted = reader.GetBool(Fields.PumpInverted),
                SpeedRunLegit = reader.GetBool(Fields.SpeedRunLegit),
                AngerGameLevel = reader.GetInt(Fields.AngerGameLevel),
                AngerGameXP = reader.GetFloat(Fields.AngerGameXP),
                AutoDifficulty = reader.GetBool(Fields.AutoDifficulty),
                FixedJumpheight = reader.GetBool(Fields.FixedJumpheight)
            };
            reader.Close();
        }
    }
}
