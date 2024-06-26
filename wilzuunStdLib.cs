##--------------------------- Header
// FILE:        WilzuunStdLib.cs
// AUTHORS:     ^TFW^ Wilzuun
// LAST MOD:    10 Dec 2019
// VERSION:     No Version Number.

##--------------------------- History
// Added math functions abs(), min(), and max()
// Added shape functions. These are more of a MissionGroup management service almost.

##--------------------------- Settings

##--------------------------- Main Classification
//  Turning off either of these will override all options below. Flyers are not
//    included due to them snapping to map locations, and being very hard to
//    predict, and to hit with weapons.
//
//    Examples: tanks = true; hercs = false; Terran = true;
//      Only Terran Tanks will spawn, no Terran hercs will show up.

//  $allow["Tanks"] : boolean
//    true : Enable AI tanks
if ($allow["Tanks"] == "") $allow["Tanks"] = true;

//  $allow["Hercs"] : boolean
//    true : Enable AI Hercs
if ($allow["Hercs"] == "") $allow["Hercs"] = true;

//  $allow["Drone"] : boolean
//    true : Enable AI Drones. (These typically have no weapons, so they're pretty much easy kills.
if ($allow["Drone"] == "") $allow["Drone"] = true;  // This is Broken. Research required to fix.

// Flyers aren't included at all in this project.

##--------------------------- Faction Classification
//
//  These options matter if the above options are turned on.
//    Example: Tanks = true; hercs = false; Terran = true; Knight = false;
//      Only tanks will appear. Only Terran tanks will be spawned. Knight tanks
//      will not appear at all

//  $allow["Terran"]  : boolean
//    true : Will spawn in Terran vehicles.
if ($allow["Terran"] == "") $allow["Terran"] = true;

//  $allow["Knight"]  : boolean
//    true : Will spawn in Knight vehicles.
if ($allow["Knight"] == "") $allow["Knight"] = true;

//  $allow["Pirate"]  : boolean
//    true : Will spawn in Pirate vehicles.
if ($allow["Pirate"] == "") $allow["Pirate"] = true;

//  $allow["Rebel"]  : boolean
//    true : Will spawn in Rebel vehicles.
if ($allow["Rebel"] == "") $allow["Rebel"] = true;

//  $allow["Cybrid"]  : boolean
//    true : Will spawn in Cybrid vehicles.
if ($allow["Cybrid"] == "") $allow["Cybrid"] = true;

//  $allow["Metagan"] : boolean
//    true : Will spawn in Metagan vehicles.
if ($allow["Metagan"] == "") $allow["Metagan"] = true;

//  $allow["Special"] : boolean
//    true : Will spawn in Special vehicles.
if ($allow["Special"] == "") $allow["Special"] = false;

##--------------------------- Specialty Classification
//
//  These last two categories are in respects to groups of vehicles that players
//    may find highly annoying, or undesirable for normal game play. They still 
//    require their Faction Classification to be enabled to show up.

//  $allow["Disruptors"] : boolean
//    true : Will spawn in Disruptor Tanks.
if ($allow["Disruptors"] == "") $allow["Disruptors"] = false;

//  $allow["Artillery"] : boolean
//    true : Will spawn in Artillery Tanks.
if ($allow["Artillery"] == "") $allow["Artillery"] = false;


##--------------------------- Functions
//  abs(%int) : int
//      Returns the absolute value of %int.
function abs(%this)
{
    if (%this == "")
    {
        echo("abs NUMBER");
        echo("Returns the absolute value of a number.");
        return;
    }
    if (%this > -%this)
        return %this;
    else
        return -%this;
}

//  min(%n0, %n1, %n2, %n3, %n4, %n5, %n6, %n7, %n8, %n9)
//      Returns the smallest number in a collection.
function min(%n0, %n1, %n2, %n3, %n4, %n5, %n6, %n7, %n8, %n9)
{
    if (%n0 == "")
    {
        echo("min NUMBER [NUMBER ...]");
        echo("Can take up to ten numbers. Will return smallest number in collection. Requires at least one number.");
        return;
    }
    %min = %n0;
    for (%count = 0; %count < 10; %count = %count+1)
    {
        if (%n[count] != "")
            if (%min > %n[%count])
                %min = %n[%count];
    }
    return %min;
}

//  max(%n0, %n1, %n2, %n3, %n4, %n5, %n6, %n7, %n8, %n9)
//      Returns the largest number in a collection.
function max(%n0, %n1, %n2, %n3, %n4, %n5, %n6, %n7, %n8, %n9)
{
    if (%n0 == "")
    {
        echo("max NUMBER  [NUMBER ...]");
        echo("Can take up to ten numbers. Will return largest number in collection. Requires at least one number.");
        return;
    }
    %max = %n0;
    for (%count = 0; %count < 10; %count = %count+1)
    {
        if (%n[count] != "")
            if (%max < %n[%count])
                %max = %n[%count];
    }
    return %max;
}

//  startRotation( ObjectID, DelayTimer, RotationInDegrees)
//      %id - ObjectID - Required.
//      %delay - Float - Optional
//      %rotation - Int - Optional
function startRotation (%id, %delay, %rotation)
{
    // If we don't have an ID, tell them how to use this...
    if (%id == "")
    {
        echo("startRotation ObjectID [Delay_Timer] [Rotation_Amount]");
        echo("Starts rotation an Object. Delay_Timer is how fast the rotation is. Rotation_Amount is how much hte ObjectID is rotated at a time.");
        return;
    }
    
    // Turn on Rotation on this object.
    (%id).rotationEnabled = true;
    
    // If delay isnt set, give them a default value. The save it on the object.
    if (%delay == "") 
        %delay = 0.05;
    (%id).rotationDelay = %delay;

    // If rotation isn't set, give a default, then save it on the object.
    if(%rotation == "") 
        %rotation = 1;
    (%id).rotationAmount = %rotation;

    // This is an internal item. Used to keep a running total of the rotation number.
    if ((%id).rotationValue == "") 
        (%id).rotationValue = 0;

    // Set a timer to run the actual loop.
    schedule("rotateObject(" @ %id @ ");",(%id).rotationDelay);
}

// rotateObjecr ( ObjectID )
//      ObjectID - an object ID to be referenced. - REQUIRED.
// This is the actual script to rotate an object. End users shouldn't use it directly.
function rotateObject(%id)
{
    // We need an id... if we dont have it, explain it.
    if (%id == "")
    {
        echo("rotateObject ObjectID");
        echo("Rotates an Object.");
        return;
    }
    
    // If we've been cancelled, do nothing.
    if ((%id).rotationEnabled == true)
    {
        // we need the current location... we're not really changing it.
        %x = getPosition(%id, x);
        %y = getPosition(%id, y);
        %z = getPosition(%id, z);
        // set the rotation. Take the MODULOUS of the value from 360.
        (%id).rotationValue = ((%id).rotationValue + (%id).rotationAmount) % 360;
        // set the position.
        setPosition(%id, %x, %y, %z, (%id).rotationValue);
        // Set a timer to run again.
        schedule("rotateObject(" @ %id @ ");", (%id).rotationDelay);
    }
}

//  stopRotation( ObjectID )
//      ObjectID - Object ID - Required.
function stopRotation(%id)
{
    // we need an id... explain if we dont have it.
    if (%id == "")
    {
        echo("stopRotation ObjectID");
        echo("Stops the rotation of ObjectID.");
        return;
    }
    // Disable the rotation.
    (%id).rotationEnabled = false;
}

//  shape::getNextId(%groupName) : int
//      Starts up a check for a used ID, and returns the first identified unused ID for the %groupName supplied.
//      Example: Currently existing groups for `Circle`: Circle0, Circle2. Running this will return 1. A seecond call would return 3.
function shape::getNextId(%groupName)
{
    // We need a groupName, explain it.
    if (%groupName == "")
    {
        echo("shape::getNextId GroupName");
        echo("Checks for a used ID, and returns the first identified unused ID");
        return;
    }
    // Start at zero, increment by 1, check again.
    %checkId = 0;
    while(shape::__IdUsed(%groupName, %checkId))
    {
        %checkId = %checkId + 1;
    }
    // return the first Available ID.
    return %checkId;
}

//  shape::__IdUsed(%groupName, %id) : boolean
//      Checks each item at the MissionGroup base level for a group called %groupName followed by a number designation of %id.
//      Example: Existing `Circle` groups: Circle0, Circle1, Circle2
//          Supplying 1 as an ID, would give you `true`, using 3 as an ID would result in a `false`
function shape::__IdUsed(%groupName, %id)
{
    // we need a groupName and a group ID.
    if (%groupName == "" || %id == "")
    {
        echo("shape::__IdUsed GroupName Number");
        echo("Checks each item at the MissionGroup base level for a group called GroupName followed by a number designation of NUMBER.");
        return;
    }

    // Grab all the root items in MissionGroup.
    %group = getObjectId("MissionGroup");
    // Get the first item.
    %item = getNextObject(%group, 0);
    // Check each item.
    for (%count = 0; %item != 0; %count = %count+1)
    {
        // If the object is equal to our groupName and ID, return true.
        if (getObjectName(%item) == %groupName @ %id)
        {
            return true;
        }
        // set the next item to be checked.
        %item = getNextObject(%group, %item);
    }
    // we never found it... report false.
    return false;
}

// shape::Cleanup(%groupName, %id)
//      GROUPNAME - STRING - REQUIRED
//      ID - INT - REQUIRED
function shape::Cleanup(%groupName, %id)
{
    // if no groupName or ID, explain we need them.
    if (%obj == "" || %id == "")
    {
        echo("shape::Cleanup GroupName Number");
        echo("Deletes GroupName with an id of NUMBER");
        return;
    }

    // if it exists, delete it. Otherwise, report it couldn't be found.
    if (shape::__IdUsed(%groupName, %id))
        deleteObject(%groupName @ %id );
    else
        echo("Group ID " @ %id @ " for the groupName of " @ %groupName @ " not found.");
}

//  spawnObject ( %obj, %name)
//      %OBJ - Object File name with out the extension. - Required.
//      %name - String, an optional name to give the Object when it gets spawned. - Optional
//      returns the Object's ID.
function spawnObject(%obj, %name)
{
    // check for %obj being set. Otherwise reject.
    if (%obj == "")
    {
        echo("spawnObject Object [Name]");
        echo("Finds, creates, and spawns an object into the map. Doesn't require DTS or DIS clarification. Doesn't work on vehicles or turrets. NAME is optional.");
        return;
    }
    // A name isn't required, and if they opt out of it, make the %name equal to the %obj value.
    if (%name == "")
        %name = %obj;
    

    %type = getObjectType(%obj);
    // Check to see if it exists as a StaticShape.
    if (%type == StaticShape)
    {
        %file = %obj @ ".dts";
    }
    // Not found? See if its a StaticInterior Shape.
    else if (%type == StaticInterior)
    {
        %file = %obj @ ".dis";
    }
    // We still didnt find it... oh well, must not exist.
    else
    {
        return echo("Item '" @ %obj @ "' not found. Reminder, this function will not work on Vehicles or Turrets.");
    }
    // Create the object. Add the object to the MissionGroup, so if you're in ME, it can be muddled with.
    %obj = newObject(%name, %type, %file);
    addToSet("MissionGroup", %obj);
    // Return object so that script caller can modify it themselves. (Like, location, rotation, etc.)
    return %obj;
}

function getObjectType(%objName)
{
    // if they omit the %objName, inform them they need it.
    if (%objName == "")
    {
        echo("getObjectType ObjectName");
        echo("Takes a file name without the extension. If it exists, will return its type. If unfound will report doesnt exist.");
        return false;
    }
    // Static Shape file extension.
    if (GetPathOf(%objName @ ".dts") != "")
    {
        return StaticShape;
    }
    // Static Interior file extension.
    else if (GetPathOf(%objName @ ".dis") != "")
    {
        return StaticInterior;
    }
    // not found... uhoh. Return false.
    else
    {
        echo("Item '" @ %objName @ "' not found. Reminder, this function will not work on Vehicles or Turrets.");
        return false;
    }
}

// These two functions were stolen from the internet, and adapted to fit starsiege.

function positionX(%numItems, %thisNum, %radius)
{
    %alpha = 360/%numItems; // angle between the elements
    %angle = %alpha * %thisNum; // angle for N element
    %x = %radius * cos(%angle); // X coordinates
    return %x;
}

function positionY(%numItems, %thisNum, %radius)
{
    %alpha = 360/%numItems; // angle between the elements
    %angle = %alpha * %thisNum; // angle for N element
    %y = %radius * sin(%angle); // Y coordinates
    return %y;
}


// Modified to allow on the fly changes. left blank, will use the above settings.
function wilzuun::loadVehicle(%name, %hercs, %tanks, %ter, %kni, %pir, %reb, %cyb, %met, %spe, %dis, %art) 
{
    if (%name == "")
        %name = "Swarmie Boi";
    if (%hercs == "")
        %hercs = $allow["Hercs"];
    if (%tanks == "")
        %tanks = $allow["tanks"];
    if (%ter == "")
        %ter = $allow["Terran"];
    if (%kni == "")
        %kni = $allow["Knight"];
    if (%pir == "")
        %pir = $allow["Pirate"];
    if (%reb == "")
        %reg = $allow["Rebel"];
    if (%cyb == "")
        %cyb = $allow["Cybrid"];
    if (%met == "")
        %met = $allow["Metagen"];
    if (%spe == "")
        %spe = $allow["Special"];
    if (%dis == "")
        %dis = $allow["Disruptors"];
    if (%art == "")
        %art = $allow["Artillery"];

    %swarmCount = 0;
    %swarmMembers[-1] = true;

    if ($allow["Hercs"] == true)
    {
        if ($allow["Terran"] == true)
        {
            %swarmMembers[%swarmCount++] = "1"; // Terran Apocalypse
            %swarmMembers[%swarmCount++] = "2"; // Terran Minotaur
            %swarmMembers[%swarmCount++] = "3"; // Terran Gorgon
            %swarmMembers[%swarmCount++] = "4"; // Terran Talon
            %swarmMembers[%swarmCount++] = "5"; // Terran Basilisk
        }
        if ($allow["Knight"] == true)
        {
            %swarmMembers[%swarmCount++] = "10"; // Knight's Apocalypse
            %swarmMembers[%swarmCount++] = "11"; // Knight's Minotaur
            %swarmMembers[%swarmCount++] = "12"; // Knight's Gorgon
            %swarmMembers[%swarmCount++] = "13"; // Knight's Talon
            %swarmMembers[%swarmCount++] = "14"; // Knight's Basilisk
        }
        if ($allow["Cybrid"] == true)
        {
            %swarmMembers[%swarmCount++] = "20"; // Cybrid Seeker
            %swarmMembers[%swarmCount++] = "21"; // Cybrid Goad
            %swarmMembers[%swarmCount++] = "22"; // Cybrid Shepherd
            %swarmMembers[%swarmCount++] = "23"; // Cybrid Adjudicator
            %swarmMembers[%swarmCount++] = "24"; // Cybrid Executioner
            %swarmMembers[%swarmCount++] = "27"; // Platinum Adjudicator (SP version, not selectable)
            %swarmMembers[%swarmCount++] = "28"; // Platinum Executioner (SP version, not selectable)
            %swarmMembers[%swarmCount++] = "55"; // Platinum Adjudicator 2
            %swarmMembers[%swarmCount++] = "56"; // Platinum Executioner 2
        }
        if ($allow["Metagen"] == true)
        {
            %swarmMembers[%swarmCount++] = "35"; // Metagen Seeker
            %swarmMembers[%swarmCount++] = "36"; // Metagen Goad
            %swarmMembers[%swarmCount++] = "37"; // Metagen Shepherd
            %swarmMembers[%swarmCount++] = "38"; // Metagen Adjudicator
            %swarmMembers[%swarmCount++] = "39"; // Metagen Executioner
        }
        if ($allow["Rebel"] == true)
        {
            %swarmMembers[%swarmCount++] = "30"; // Rebel Emancipator
            %swarmMembers[%swarmCount++] = "33"; // Rebel Olympian
        }
        if ($allow["Special"] == true)
        {
            %swarmMembers[%swarmCount++] = "29"; // Prometheus
            %swarmMembers[%swarmCount++] = "40"; // Harabec's Apocalypse
            %swarmMembers[%swarmCount++] = "42"; // Caanan's Basilisk
        }
        if ($allow["Pirate"] == true)
        {
            %swarmMembers[%swarmCount++] = "50"; // Pirate's Apocalypse
            %swarmMembers[%swarmCount++] = "52"; // Pirate's Emancipator
        }
    }

    if ($allow["Tanks"] == true)
    {
        if ($allow["Terran"] == true)
        {
            %swarmMembers[%swarmCount++] = "6"; // Paladin Tank
            %swarmMembers[%swarmCount++] = "7"; // Myrmidon Tank
            if ($allow["Disruptors"] == true)
            {
                %swarmMembers[%swarmCount++] = "8"; // Disruptor Tank
            }
            if ($allow["Artillery"] == true)
            {
                %swarmMembers[%swarmCount++] = "133"; // Nike Artillery
                %swarmMembers[%swarmCount++] = "134"; // Supressor Tank
            }
        }
        if ($allow["Knight"] == true)
        {
            %swarmMembers[%swarmCount++] = "15"; // Knight's Paladin
            %swarmMembers[%swarmCount++] = "16"; // Knight's Myrmidon
            if ($allow["Disruptors"] == true)
            {
                %swarmMembers[%swarmCount++] = "17"; // Knight's Disruptor
            }
        }
        if ($allow["Cybrid"] == true)
        {
            %swarmMembers[%swarmCount++] = "25"; // Bolo Tank
            %swarmMembers[%swarmCount++] = "26"; // Recluse Tank
            if ($allow["Artillery"] == true)
            {
                %swarmMembers[%swarmCount++] = "90"; // Cybrid Artillery
            }
        }
        if ($allow["Rebel"] == true)
        {
            %swarmMembers[%swarmCount++] = "31"; // Avenger Tank
            %swarmMembers[%swarmCount++] = "32"; // Dreadnought Tank
            %swarmMembers[%swarmCount++] = "72";  // Rebel Thumper
            %swarmMembers[%swarmCount++] = "138"; // Rebel bike
            if ($allow["Artillery"] == true)
            {
                %swarmMembers[%swarmCount++] = "137"; // Rebel Artillery
                %swarmMembers[%swarmCount++] = "150"; // SUAV Bus
            }
        }
        if ($allow["Special"] == true)
        {
            %swarmMembers[%swarmCount++] = "41"; // Harabec's Predator
            %swarmMembers[%swarmCount++] = "45"; // Harabec's Super Predator
        }
        if ($allow["Pirate"] == true)
        {
            %swarmMembers[%swarmCount++] = "51"; // Pirate's Dreadlock
        }
    }

    // The following block is actually busted when it comes to spawning the vehicles. Please don't edit this unless you're up for the challenge of fixing it.
    // You will end up crashing a lot if you decide to fix this code section to allow drones to spawn.
    if ($allow["Drone"] == "Not Happening" && true == false)
    {
        if ($allow["Terran"] == true)
        {
            %swarmMembers[%swarmCount++] = "60";  // Terran Empty Cargo
            %swarmMembers[%swarmCount++] = "61";  // Terran Ammo Cargo
            %swarmMembers[%swarmCount++] = "62";  // Terran Big Ammo Cargo
            %swarmMembers[%swarmCount++] = "63";  // Terran Big Personnel Cargo
            %swarmMembers[%swarmCount++] = "64";  // Terran Fuel Cargo
            %swarmMembers[%swarmCount++] = "65";  // Terran Minotaur Cargo
            %swarmMembers[%swarmCount++] = "71";  // Terran Utility Truck
            %swarmMembers[%swarmCount++] = "73";  // Terran Starefield
        }
            if ($allow["Rebel"] == true)
        {
            %swarmMembers[%swarmCount++] = "66";  // Rebel Empty Cargo
            %swarmMembers[%swarmCount++] = "67";  // Rebel Ammo Cargo
            %swarmMembers[%swarmCount++] = "68";  // Rebel Big Cargo Transport
            %swarmMembers[%swarmCount++] = "69";  // Rebel Bix Box Cargo Transport
            %swarmMembers[%swarmCount++] = "70";  // Rebel Box Cargo Transport
        }
        if ($allow["Cybrid"] == true)
        {
            %swarmMembers[%swarmCount++] = "94";  // Cybrid Omnicrawler
            %swarmMembers[%swarmCount++] = "95";  // Cybrid Protector
            %swarmMembers[%swarmCount++] = "96";  // Cybrid Jamma
        }
    }

    %rand = randomInt(1,%swarmCount);
    return loadobject(%name, "vehId_" @ %swarmMembers[%rand] @ ".veh");
}