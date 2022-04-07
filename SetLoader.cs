using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Help me this is the most non-efficient thing I ever did.

namespace WYSSaveUtils
{
    /// <summary>
    /// Creates a new SetLoader handler. You need to create a new instance of it before doing anything else.
    /// </summary>
    public class SetLoader
    {
        public static string WYSFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Will_You_Snail";
        public SetContent setContent;
        // when true, it's loaded from a save slot, if false, it's loaded from an absolute path
        internal bool loaderMode = true;
        internal static string Filepath = "";

        internal List<string> lines = new List<string>();
        internal FileStream stream;

        /// <summary>
        /// Load a setting file directly from the save folder in %appdata%\Local\Will_You_Snail
        /// </summary>
        public SetLoader()
        {
            stream = new FileStream(WYSFolder + "\\SettoIngs23-2.set", FileMode.Open);
            PopulateLines();
            Reload();
        }

        /// <summary>
        /// Load a save file from an absolute path not located in the save folder
        /// </summary>
        /// <param name="filepath">Absolute path containing the setting file name, finished with .set</param>
        public SetLoader(string filepath)
        {
            Filepath = filepath;
            loaderMode = false;
            stream = new FileStream(Filepath, FileMode.Open);
            PopulateLines();
            Reload();
        }

        /// <summary>
        /// Don't forget to call this method when you're finished with the setting file to avoid it being used in memory for nothing.
        /// </summary>
        public void CloseLoader() => stream.Close();

        /// <summary>
        /// Change the loader mode from a filepath mode from an appdata mode.
        /// MUST NOT BE CALLED BEFORE SAVING CHANGES.
        /// </summary>
        public void ChangeMode()
        {
            loaderMode = true;
            stream = new FileStream(WYSFolder + "\\SettoIngs23-2.set", FileMode.Open);
            PopulateLines();
            Reload();
        }

        /// <summary>
        /// Changes the SetLoader to be loading a given file from a filepath in a string format.
        /// MUST NOT BE CALLED BEFORE SAVING CHANGES.
        /// </summary>
        /// <param name="filepath">Absolute path containing the setting file name, finished with .set</param>
        public void ChangeMode(string filepath)
        {
            Filepath = filepath;
            loaderMode = false;
            stream = new FileStream(Filepath, FileMode.Open);
            PopulateLines();
            Reload();
        }

        /// <summary>
        /// Reloads the content of the setContent object to be read again by your program.
        /// Is called automatically by the WriteChanges method.
        /// </summary>
        public void Reload()
        {
            if (loaderMode)
            {
                CloseLoader();
                stream = new FileStream(WYSFolder + "\\SettoIngs23-2.set", FileMode.Open);
                FillSettingsContent();
            }
            else
            {
                CloseLoader();
                stream = new FileStream(Filepath, FileMode.Open);
                FillSettingsContent();
            }
        }
        
        internal void PopulateLines()
        {
            lines = new List<string>();
            StreamReader reader = new StreamReader(stream);
            while (!reader.EndOfStream)
                lines.Add(reader.ReadLine());
            reader.Close();
        }

        /// <summary>
        /// Called to save the setContent to the setting file.
        /// Calls Reload() at the end, no need to call it yourself.
        /// </summary>
        public void WriteChanges()
        {
            CloseLoader();

            StreamWriter writer;
            if (loaderMode)
            {
                File.WriteAllText(WYSFolder + "\\SettoIngs23-2.set", string.Empty);
                Stream stream = File.OpenWrite(WYSFolder + "\\SettoIngs23-2.set");
                writer = new StreamWriter(stream);
            }
            else
            {
                File.WriteAllText(Filepath, string.Empty);
                Stream stream = File.OpenWrite(Filepath);
                writer = new StreamWriter(stream);
            }

            writer.WriteLine("This setting file has been modified by WYSSaveUtils.");
            writer.WriteLine("Make sure you know what you're doing before you change anything here.");
            writer.WriteLine("");
            writer.WriteLine("!! Edit at your own risk !!");
            writer.WriteLine("");

            writer.EditSingle(SetFields.GameVersion, setContent.GameVersion);
            writer.EditSingle(SetFields.GamepadRumble, setContent.GamepadRumble);
            writer.EditSingle(SetFields.VolumeMaster, setContent.VolumeMaster);
            writer.EditSingle(SetFields.VolumeVoice, setContent.VolumeVoice);
            writer.EditSingle(SetFields.VolumeMusic, setContent.VolumeMusic);
            writer.EditSingle(SetFields.VolumeFx, setContent.VolumeFx);
            writer.EditSingle(SetFields.PostProcessing, setContent.PostProcessing);
            writer.EditSingle(SetFields.VisualDetail, setContent.VisualDetail);
            writer.EditSingle(SetFields.Subtitles, setContent.Subtitles);
            writer.EditSingle(SetFields.WindowMode, setContent.WindowMode);
            writer.EditSingle(SetFields.TextLanguage, setContent.TextLanguage);
            writer.EditSingle(SetFields.AudioLanguage, setContent.AudioLanguage);
            writer.EditSingle(SetFields.Screenshake, setContent.Screenshake);
            writer.EditSingle(SetFields.AutoDifficulty, setContent.AutoDifficulty);
            writer.EditSingle(SetFields.SubtitleSize, setContent.SubtitleSize);
            writer.EditSingle(SetFields.GamepadDeadZones, setContent.GamepadDeadZones);
            writer.EditSingle(SetFields.SpeedrunTimer, setContent.SpeedrunTimer);
            writer.EditSingle(SetFields.ChapterTimer, setContent.ChapterTimer);
            writer.EditSingle(SetFields.LevelTimer, setContent.LevelTimer);
            writer.EditSingle(SetFields.VSync, setContent.VSync);
            writer.EditSingle(SetFields.AIPredictions, setContent.AIPredictions);
            writer.EditSingle(SetFields.TimingMethod, setContent.TimingMethod);
            writer.EditSingle(SetFields.SleepMargin, setContent.SleepMargin);
            writer.EditSingle(SetFields.ShowFPS, setContent.ShowFPS);
            writer.EditSingle(SetFields.ColorScheme, setContent.ColorScheme);
            writer.EditSingle(SetFields.SquidOpacity, setContent.SquidOpacity);
            writer.EditSingle(SetFields.SquidStayInBack, setContent.SquidStayInBack);
            writer.EditSingle(SetFields.SquidFacialExpessions, setContent.SquidFacialExpessions);
            writer.EditSingle(SetFields.FontNr, setContent.FontNr);

            writer.Close();
            Reload();
        }

        internal void FillSettingsContent()
        {
            StreamReader reader = new StreamReader(stream);
            setContent = new SetContent
            {
                GameVersion = reader.GetString(SetFields.GameVersion),
                GamepadRumble = reader.GetBool(SetFields.GamepadRumble),
                VolumeMaster = reader.GetFloat(SetFields.VolumeMaster),
                VolumeVoice = reader.GetFloat(SetFields.VolumeVoice),
                VolumeMusic = reader.GetFloat(SetFields.VolumeMusic),
                VolumeFx = reader.GetFloat(SetFields.VolumeFx),
                PostProcessing = reader.GetInt(SetFields.PostProcessing),
                VisualDetail = reader.GetInt(SetFields.VisualDetail),
                Subtitles = reader.GetInt(SetFields.Subtitles),
                WindowMode = reader.GetInt(SetFields.WindowMode),
                TextLanguage = reader.GetString(SetFields.TextLanguage),
                AudioLanguage = reader.GetString(SetFields.AudioLanguage),
                Screenshake = reader.GetFloat(SetFields.Screenshake),
                AutoDifficulty = reader.GetBool(SetFields.AutoDifficulty),
                SubtitleSize = reader.GetFloat(SetFields.SubtitleSize),
                GamepadDeadZones = reader.GetFloat(SetFields.GamepadDeadZones),
                SpeedrunTimer = reader.GetBool(SetFields.SpeedrunTimer),
                ChapterTimer = reader.GetBool(SetFields.ChapterTimer),
                LevelTimer = reader.GetBool(SetFields.LevelTimer),
                VSync = reader.GetBool(SetFields.VSync),
                AIPredictions = reader.GetBool(SetFields.AIPredictions),
                TimingMethod = reader.GetInt(SetFields.TimingMethod),
                SleepMargin = reader.GetInt(SetFields.SleepMargin),
                ShowFPS = reader.GetBool(SetFields.ShowFPS),
                ColorScheme = reader.GetInt(SetFields.ColorScheme),
                SquidOpacity = reader.GetFloat(SetFields.SquidOpacity),
                SquidStayInBack = reader.GetBool(SetFields.SquidStayInBack),
                SquidFacialExpessions = reader.GetBool(SetFields.SquidFacialExpessions),
                FontNr = reader.GetInt(SetFields.FontNr)
            };
            reader.Close();
        }
    }
}
