using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Reflection;
using System.Collections;

namespace WYSSaveUtils
{
    public class SetContent : IEnumerable
    {
        private List<object> propertyList = new List<object>();

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (var prop in GetType().GetProperties())
            {
                propertyList.Add(GetType().GetProperty(prop.Name).GetValue(this, null));
            }
            return propertyList.GetEnumerator();
        }

        public string GameVersion { get; set; }
        public bool GamepadRumble { get; set; }
        public float VolumeMaster { get; set; }
        public float VolumeVoice { get; set; }
        public float VolumeMusic { get; set; }
        public float VolumeFx { get; set; }
        public int PostProcessing { get; set; }
        public int VisualDetail { get; set; }
        public int Subtitles { get; set; }
        public int WindowMode { get; set; }
        public string TextLanguage { get; set; }
        public string AudioLanguage { get; set; }
        public float Screenshake { get; set; }
        public bool AutoDifficulty { get; set; }
        public float SubtitleSize { get; set; }
        public float GamepadDeadZones { get; set; }
        public bool SpeedrunTimer { get; set; }
        public bool ChapterTimer { get; set; }
        public bool LevelTimer { get; set; }
        public bool VSync { get; set; }
        public bool AIPredictions { get; set; }
        public int TimingMethod { get; set; }
        public int SleepMargin { get; set; }
        public bool ShowFPS { get; set; }
        public int ColorScheme { get; set; }
        public float SquidOpacity { get; set; }
        public bool SquidStayInBack { get; set; }
        public bool SquidFacialExpessions { get; set; }
        public int FontNr { get; set; }
    }

    /// <summary>
    /// Access the field names of the settings file contents.
    /// </summary>
    public static class SetFields
    {
        public static readonly string GameVersion = "Game Version";
        public static readonly string GamepadRumble = "Gamepad Rumble";
        public static readonly string VolumeMaster = "Volume Master";
        public static readonly string VolumeVoice = "Volume Voice";
        public static readonly string VolumeMusic = "Volume Music";
        public static readonly string VolumeFx = "Volume SoundFx";
        public static readonly string PostProcessing = "Post Processing";
        public static readonly string VisualDetail = "Visual Detail";
        public static readonly string Subtitles = "Subtitles";
        public static readonly string WindowMode = "Window Mode";
        public static readonly string TextLanguage = "Text Language";
        public static readonly string AudioLanguage = "Audio Language";
        public static readonly string Screenshake = "Screenshake";
        public static readonly string AutoDifficulty = "Auto Difficulty";
        public static readonly string SubtitleSize = "Subtitle Size";
        public static readonly string GamepadDeadZones = "Gamepad Dead Zones";
        public static readonly string SpeedrunTimer = "Speedrun Timer";
        public static readonly string ChapterTimer = "Chapter Timer";
        public static readonly string LevelTimer = "Level Timer";
        public static readonly string VSync = "V-Sync";
        public static readonly string AIPredictions = "AI Predictions";
        public static readonly string TimingMethod = "Timing Method";
        public static readonly string SleepMargin = "Sleep Margin";
        public static readonly string ShowFPS = "Show FPS";
        public static readonly string ColorScheme = "Color Scheme";
        public static readonly string SquidOpacity = "Squid Opacity";
        public static readonly string SquidStayInBack = "Squid Stay In Back";
        public static readonly string SquidFacialExpessions = "Squid Facial Expessions";
        public static readonly string FontNr = "Font Nr";
    }
}
