##--------------------------- Header
// FILE:        WilzuunLoader.cs
// AUTHORS:     ^TFW^ Wilzuun
// LAST MOD:    10 Dec 2019
// VERSION:     1.0

##--------------------------- IMPORTANT NOTES
// IF YOU USE DOV, HAVE AN EMPTY .CS FILE!
// DOV WILL NOT WORK BECAUSE OF THE VOLUME OF AREAS IT NEEDS TO TRIGGER AT.

// Specifically, this file will attempt to load all modules that a map maker wants.

// DYNCITY CAN ERASE A LOT OF THINGS! BEWARE!
// The DynCity script(s) are meant to make dynamic cities on an empty map.
// It will not make trigger areas. Like healing or ammo pads. Or map boundaries.

##--------------------------- Install Instructions
// Place this file in the Starsiege Multiplayer directory.

##--------------------------- Version History
//  1.0 - 09 Dec 2019
//      Started ground works.
//  1.1 - 25 July 2020
//      Fixed the DOV loading scripts section

##--------------------------- Required libraries. 
exec("TrigonometryStdLib.cs");

##--------------------------- Example Usage. 
//  TO use DOV:
## -- Start DOV Items.
// $wilzuun::dov = true; // Tell the system we want DOV.
// function DOVOverRide () // We want to change these specific settings... 
// {
//      $swarmClone = true; // Make all swam members a clone of hte player.
// }
// exec("WilzuunLoader.cs"); // Go ahead and run the Loader.
## -- End DOV Items.

// I will add an examples File later for each item.

##--------------------------- Loading function. 
// Please don't edit below this line.
// onMissionPreload executes before everything in your map file, and is a vastly under used function.
// I utilize it for all of my projects.
function Mission::OnPreLoad() ()
{
    // First, due to complete map destruction.
    // if ($wilzuun::City == true)
    // {
        // echo("Setting up Dyn::City items... ");
        // if ($dyn::City::ClearAll == true)
        // {
            // %group = getGroupId("MissionGroup");
            // %item = getNextObject(%group, 0);
            // for(%count = 0; %item != 0; %count++)
            // {
                // // Don't remove these two groups, they're also required.
                // if ((getObjectName(%item) != "Volumes") && (getObjectName(%item) != "world")) {}
                // // delete everything else... we dont need it, we dont want it.
                // else
                // {
                    // echo("Deleting Item \"" @ %item @ "\"");
                    // deleteObject(%item);
                // }
                // %item = getNextObject(%group, %item);
            // }
        // }
        // echo("Loading DYN::CITY settings...");
        // exec("wilzuun\\dynamic\\city\\settings.cs");
    // }
    
    // // Shapes next, due to, less intrusive?
    // if ($wilzuun::Sphere == true) 
    // {
        // echo("Loading SPHERE items");
        // exec("wilzuun\\shapes\\sphereStdLib.cs");
    // }
    // if ($wilzuun::Cube == true) 
    // {
        // echo("Loading CUBE items");
        // exec("wilzuun\\shapes\\cubeStdLib.cs");
    // }
    // if ($wilzuun::Circle == true) 
    // {
        // echo("Loading CIRCLE items");
        // exec("wilzuun\\shapes\\circleStdLib.cs");
    // }
    // if ($wilzuun::Line == true) 
    // {
        // echo("Loading LINE items");
        // exec("wilzuun\\shapes\\lineStdLib.cs");
    // }
    // // pathway system next!
    // if ($wilzuun::Path == true)
    // {
        // echo("Setting up Dyn::Path items");
        // exec("wilzuun\\dynamic\\path\\settings.cs");
    // }
    
    // Load GameType... First one found, is game type that is going to run.
    if ($wilzuun::GameType == "dov")
    {
        echo("Loading DOV Settings...");
        exec("dov_settings.cs");
        echo("Overriding DOV Settings...");
        wilzuun::OverRide();
        echo("Loading DOV Functionality...");
        exec("dov_functionality.cs");
    }

    else if ($wilzuun::GameType == "tag")
    {
        exec("wilzuun\\gameTypes\\tag\\TagStdLib.cs");
    }
    else if ($wilzuun::GameType == "edm")
    {
        exec("wilzuun\\gameTypes\\edm\\EDMStdLib.cs");
    }
    else 
    {
        // Do nothing... have a blank map... Maybe its traditional DM, or CTF with randomness to it... who knows.
    }
}
