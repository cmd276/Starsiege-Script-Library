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
exec("pilots.cs");

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
function onMissionPreload() ()
{
    MissionPreLoad();
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

    // // pathway system next!
    // if ($wilzuun::Path == true)
    // {
        // echo("Setting up Dyn::Path items");
        // exec("wilzuun\\dynamic\\path\\settings.cs");
    // }
    
    if($wilzuun::Boost == true)
    {
        exec(BoostStdLib);
    }

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
        echo("Loading Tag Functionality...");
        exec("Tag_StdLib.cs");
    }
    else if ($wilzuun::GameType == "tar")
    {
        echo("Loading tar Functionality...");
        exec("tarStdLib.cs");
    }
    else if ($wilzuun::GameType == "edm")
    {
        echo("Loading EDM Functionality...");
        exec("EDM_StdLib.cs");
    }
    else if ($wilzuun::GameType == "cnh")
    {
        echo("Loading CnH Functionality...");
        exec("CnHStdLib.cs");
    }
    else if ($wilzuun::GameType == "ctf")
    {
        echo("Loading ctf Functionality...");
        exec("ctfStdLib.cs");
    }
    else if ($wilzuun::GameType == "dm")
    {
        echo("Loading dm Functionality...");
        exec("dmStdLib.cs");
    }
    else if ($wilzuun::GameType == "FnR")
    {
        echo("Loading FnR Functionality...");
        exec("FnRStdLib.cs");
    }
    else if ($wilzuun::GameType == "Harvest")
    {
        echo("Loading Harvest Functionality...");
        exec("Harvest_StdLib.cs");
    }
    else if ($wilzuun::GameType == "CvH")
    {
        echo("Loading Harvest Functionality...");
        exec("Harvest_StdLib.cs");
    }
    else 
    {
        // Do nothing... have a blank map... Maybe its traditional DM, or CTF with randomness to it... who knows.
    }
}

