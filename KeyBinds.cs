using System;
using System.Collections.Generic;
using System.IO;

namespace WYSSaveUtils
{

    /* TODO :
     * Trouver un moyen de faire en sorte que keyContent soit accessible directement sans passer par le nom de la classe mais par KeyBinds directement.
     * 
     */

    public class KeyBinds
    {
        public string WYSFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Will_You_Snail";
        public KeyBindsContent keyContent;
        // when true, it's loaded from a save slot, if false, it's loaded from an absolute path
        internal bool loaderMode = true;
        internal static string Filepath = "";

        internal List<string> lines = new List<string>();
        internal FileStream stream;

        /// <summary>
        /// Load the Keybinds.sav file directly from the save folder in %appdata%\Local\Will_You_Snail
        /// </summary>
        public KeyBinds()
        {
            stream = new FileStream(WYSFolder + "\\Keybinds.sav", FileMode.Open);
            PopulateLines();
            Reload();
        }

        /// <summary>
        /// Load a save file from an absolute path not located in the save folder
        /// </summary>
        /// <param name="filepath">Absolute path containing the setting file name, finished with .set</param>
        public KeyBinds(string filepath)
        {
            Filepath = filepath;
            loaderMode = false;
            stream = new FileStream(Filepath, FileMode.Open);
            PopulateLines();
            Reload();
        }

        /// <summary>
        /// Don't forget to call this method when you're finished with the keybind file to avoid it being used in memory for nothing.
        /// </summary>
        public void CloseLoader() => stream.Close();

        /// <summary>
        /// Change the loader mode from a filepath mode from an appdata mode.
        /// MUST NOT BE CALLED BEFORE SAVING CHANGES.
        /// </summary>
        public void ChangeMode()
        {
            loaderMode = true;
            stream = new FileStream(WYSFolder + "\\Keybinds.sav", FileMode.Open);
            PopulateLines();
            Reload();
        }

        /// <summary>
        /// Changes the SetLoader to be loading a given file from a filepath in a string format.
        /// MUST NOT BE CALLED BEFORE SAVING CHANGES.
        /// </summary>
        /// <param name="filepath">Absolute path containing the setting file name, finished with .sav</param>
        public void ChangeMode(string filepath)
        {
            Filepath = filepath;
            loaderMode = false;
            stream = new FileStream(Filepath, FileMode.Open);
            PopulateLines();
            Reload();
        }

        /// <summary>
        /// Reloads the content of the KeyBindsContent object to be read again by your program.
        /// Is called automatically by the WriteChanges method.
        /// </summary>
        public void Reload()
        {
            if (loaderMode)
            {
                CloseLoader();
                stream = new FileStream(WYSFolder + "\\Keybinds.sav", FileMode.Open);
                FillKeybindContent();
            }
            else
            {
                CloseLoader();
                stream = new FileStream(Filepath, FileMode.Open);
                FillKeybindContent();
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
        /// Called to save the keyContent into Keybinds.sav.
        /// Calls Reload() at the end, no need to call it yourself.
        /// </summary>
        public void WriteChanges()
        {
            CloseLoader();

            StreamWriter writer;
            if (loaderMode)
            {
                File.WriteAllText(WYSFolder + "\\Keybinds.sav", string.Empty);
                Stream stream = File.OpenWrite(WYSFolder + "\\Keybinds.sav");
                writer = new StreamWriter(stream);
            }
            else
            {
                File.WriteAllText(Filepath, string.Empty);
                Stream stream = File.OpenWrite(Filepath);
                writer = new StreamWriter(stream);
            }

            writer.EditKeyBinds(KeyBindsFields.ActionJump, keyContent.ActionJump);
            writer.EditKeyBinds(KeyBindsFields.ActionMoveRight, keyContent.ActionMoveRight);
            writer.EditKeyBinds(KeyBindsFields.ActionMoveLeft, keyContent.ActionMoveLeft);
            writer.EditKeyBinds(KeyBindsFields.ActionRestart, keyContent.ActionRestart);
            writer.EditKeyBinds(KeyBindsFields.ActionSkipVoiceLine, keyContent.ActionSkipVoiceLine);
            writer.EditKeyBinds(KeyBindsFields.ActionNavigateDown, keyContent.ActionNavigateDown);
            writer.EditKeyBinds(KeyBindsFields.ActionNavigateUp, keyContent.ActionNavigateUp);
            writer.EditKeyBinds(KeyBindsFields.ActionNavigateConfirm, keyContent.ActionNavigateConfirm);
            writer.EditKeyBinds(KeyBindsFields.ActionPause, keyContent.ActionPause);

            writer.Close();
            Reload();
        }

        internal void FillKeybindContent()
        {
            StreamReader reader = new StreamReader(stream);
            keyContent = new KeyBindsContent()
            {
                ActionJump = reader.GetKeyBindList(KeyBindsFields.ActionJump),
                ActionMoveRight = reader.GetKeyBindList(KeyBindsFields.ActionMoveRight),
                ActionMoveLeft = reader.GetKeyBindList(KeyBindsFields.ActionMoveLeft),
                ActionRestart = reader.GetKeyBindList(KeyBindsFields.ActionRestart),
                ActionSkipVoiceLine = reader.GetKeyBindList(KeyBindsFields.ActionSkipVoiceLine),
                ActionNavigateDown = reader.GetKeyBindList(KeyBindsFields.ActionNavigateDown),
                ActionNavigateUp = reader.GetKeyBindList(KeyBindsFields.ActionNavigateUp),
                ActionNavigateConfirm = reader.GetKeyBindList(KeyBindsFields.ActionNavigateConfirm),
                ActionPause = reader.GetKeyBindList(KeyBindsFields.ActionPause)
            };
            reader.Close();
        }

        /// <summary>
        /// Helper method to add a key to an option faster and easier.
        /// </summary>
        /// <param name="property">Must be a return of nameof(). e.g : nameof(keys.KeyBindsContent.ActionJump).</param>
        /// <param name="key">A KeyBindsContent.Key object.</param>
        /// <param name="keyType">A KeyBindsContent.KeyType object.</param>
        public void AddKey(string property, KeyBindsContent.Key key, KeyBindsContent.KeyType keyType)
        {
            var prop = keyContent.GetType().GetProperty(property);
            foreach (KeyBindsContent.KeyBind keytest in (List<KeyBindsContent.KeyBind>)prop.GetValue(keyContent))
                if (keytest.Key == key)
                    throw new ArgumentException("This key is already set in this property. Aborting.");

            List<KeyBindsContent.KeyBind> keyBinds;
            keyBinds = (List<KeyBindsContent.KeyBind>)prop.GetValue(keyContent);
            keyBinds.Add(new KeyBindsContent.KeyBind(key, keyType));
            prop.SetValue(keyContent, keyBinds);
        }

        /// <summary>
        /// Helper method to remove a key to an option faster and easier.
        /// </summary>
        /// <param name="property">Must be a return of nameof(). e.g : nameof(keys.KeyBindsContent.ActionJump).</param>
        /// <param name="key">A KeyBindsContent.Key object.</param>
        public void RemoveKey(string property, KeyBindsContent.Key key)
        {
            var prop = keyContent.GetType().GetProperty(property);
            bool found = false;
            KeyBindsContent.KeyBind? keyBind = null;
            foreach (KeyBindsContent.KeyBind keytest in (List<KeyBindsContent.KeyBind>)prop.GetValue(keyContent))
            {
                if (keytest.Key == key)
                {
                    found = true;
                    keyBind = keytest;
                }
            }
            if (!found)
                throw new ArgumentException("This key is not set in this property. Nothing to remove.");

            List<KeyBindsContent.KeyBind> keyBinds = (List<KeyBindsContent.KeyBind>)prop.GetValue(keyContent);
            keyBinds.Remove((KeyBindsContent.KeyBind)keyBind);
            prop.SetValue(keyContent, keyBinds);
        }

        /// <summary>
        /// Helper method to clear a property of all their keybinds.
        /// </summary>
        /// <param name="property">Must be a return of nameof(). e.g : nameof(keys.KeyBindsContent.ActionJump).</param>
        public void ClearKeys(string property)
        {
            var prop = keyContent.GetType().GetProperty(property);
            prop.SetValue(keyContent, new List<KeyBindsContent.KeyBind>()
            {
                new KeyBindsContent.KeyBind(KeyBindsContent.Key.None, KeyBindsContent.KeyType.KEYBOARD)
            });
        }
    }
}
