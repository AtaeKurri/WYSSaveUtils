# WYSSaveUtils

This Utility is in its first version, expect bugs.

### Documentation

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

You can change the leader mode from a slot format to a filepath format using `save.ChangeMode(slot or filepath);`.  
But be sure you don't call it before saving the changes you made in the old file, or these will be saved to the new file.