# WilzuunStdLib.cs

Contains a number of settings, and functions used in a lot of the scripts in the project.

A lot of the settings are `TRUE`/`FALSE` options. A brief description is here. (To be expanded on later.)
```cs

// These allow (or disallow) these vehicle types.
$allow["Tanks"]     = true;
$allow["Hercs"]     = true;

// These allow (or disallow) These specific factions.
// Special Vehicles includes: Prometheus, Bek's Apoc, Caanon's Basi, Predator, and Bek's Super Predator.
$allow["Terran"]    = true;
$allow["Knight"]    = true;
$allow["Pirate"]    = true;
$allow["Rebel"]     = true;
$allow["Cybrid"]    = true;
$allow["Metagan"]   = true;
$allow["Special"]   = false; 

// These are special allowances for Disruptor tanks and Artillery tanks. Most people find them unfair, or highly undesired to face against.
$allow["Disruptors"]= false;
$allow["Artillery"] = false;

// A simple absolute function. Returns the INT or FLOAT you give it and returns the absolute value.
abs(%thisNumber);

// This will return the lowest number given to it.
min(%n0, %n1, %n2, %n3, %n4, %n5, %n6, %n7, %n8, %n9);

// This will return the highest valued number given to it.
max(%n0, %n1, %n2, %n3, %n4, %n5, %n6, %n7, %n8, %n9);

// This is for an internal usage, but I'm sure an external usage could be found.
// This gets the next available ID for a Shape's spawn.
// %groupName is a required variable.
// returns the int of the next available ID.
shape::getNextId(%groupName);

// This is for internal use.
// Used to verify if an ID is already in use.
// Both variables are required.
// returns TRUE if used, FALSE if not used.
shape::__IdUsed(%groupName, %id);

// Internal use.
// Deletes the selected group, and ID from the MissionGroup.
// Both variables are required.
// Returns nothing.
shape::Cleanup(%groupName, %id);

// Internal/External usage.
// Spawns an object. An object is required.
// The name variable is optional.
// Returns the newly spawned object's ID.
spawnObject(%obj, %name);

// Determines whether the object exists in the game, and returns its building type.
// If the object does not exist, will return false.
// %objName is required.
getObjectType(%objName);

// The following two functions both require trigonometryStdLib.cs
// All variables are required.
// Both return a floating point reference to their respective function name.
positionX(%totalNumberOfItems, %numberOfCurrentItem, %radiusOfShape);
positionY(%totalNumberOfItems, %numberOfCurrentItem, %radiusOfShape);

// This last function is a 'On The Fly' AI loader. Allowing you to edit the allowed vehicle to spawn
wilzuun::loadVehicle(%name, %hercs, %tanks, %terran, %knight, %pirate, %rebel, %cybrid, %metagan, %special, %disruptors, %artillery);
