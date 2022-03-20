# WYSSaveUtils

This Utility aims to create a stable environment for save and setting files of Will You Snail? to be used by other programs.  
Expect bugs for this version.

### Documentation

#### SaveLoader

Create a new instance of SaveLoader with `SaveLoader save = new SaveLoader(0);`  
There is two way to instanciate a SaveLoader object :  
- With an int, this would be the slot number, 0 to 2.  
- With an absolute path to the save file as a string object.

You can access the save values using `save.saveContent.somevalue`


To edit values, considering the SaveLoader object is named `save`,  
if you want to edit the game version, do : `save.saveContent.GameVersion = some_string`.  
And then `save.WriteChanges();` to make the changes appear in the save file.  
But be sure of what you're doing, after writing, there's no going back.

If you want to reload the contents of the savefile, just do `save.Reload();`

When you're finished with what you're doing, call `save.CloseLoader();`


You can get the string value identifiers with the `Fields` object, using the same access value as `saveContent`.  
e.g : `Fields.GameVersion` will return `"Game Version"`

You can change the loader mode from a slot format to a filepath format using `save.ChangeMode(slot or filepath);`.  
But be sure you don't call it before saving the changes you made in the old file, or these will be saved to the new file.

#### SetLoader

Create a new instance of SetLoader with `SetLoader set = new SetLoader();`  
There is two way to instanciate a SetLoader object :  
- With no argument, it just instanciate the setting file in %appdata%/Local/Will_You_Snail
- With an absolute path to the setting file as a string object.

You can access the setting values using `set.setContent.somevalue`

To edit values, considering the SetLoader object is named `set`,  
if you want to edit the Screenshake value, do : `set.setContent.Screenshake = some_float_value`.  
And then `set.WriteChanges();` to make the changes appear in the settings.  
These changes actually are updated at runtime when the game is launched.  
But be sure of what you're doing, after writing, you can't go back.

If you want to reload the contents of the savefile, just do `set.Reload();`. It's already called by `WriteChanges()` though.

When you're finished with what you're doing, call `set.CloseLoader();`

You can get the string value identifiers with the `SetFields` object, using the same access value as `setContent`.  
e.g : `SetFields.VSync` will return a bool.

You can change the loader mode from a filepath format from the appdata format using `set.ChangeMode();` or `set.ChangeMode(filepath);`.  
But be sure you don't call it before saving the changed you made, or these will not be saved.