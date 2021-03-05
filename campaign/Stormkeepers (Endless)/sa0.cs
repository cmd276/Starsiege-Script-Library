## -------------------------------------------------------------------- Header
//  Filename:   SA0.cs
//  AUTHOR:     ^TFW^ Wilzuun
//  NOTES:      This is the first Mission to the Order of the Stormkeepers campaign.

## -------------------------------------------------------------------- Install Instructions
// The the entirety of the folder this file is in (even the directory) and place it in your Starsiege
// install directory under C:/Path/To/Starsiege/Campaign

## -------------------------------------------------------------------- IMPORTANT NOTES
// There are no important notes as of yet.

// These are not so much End User Notes. They're for the map Developer (Wilzuun)
# "MissionGroup\\NavGroup\\NavAlpha"
# "MissionGroup\\NavGroup\\NavBravo"
# "MissionGroup\\MagnetGroup\\FightZone"
# 

## -------------------------------------------------------------------- Version History
// 1.0 - 14th Feb 2021
//      Started project.
## -------------------------------------------------------------------- Required Files.
exec(otskCampaignData);
exec(otskCampaignFunctions);

## -------------------------------------------------------------------- Mission Brief
MissionBriefInfo missionData
{
   title                = $otsk::missionTitle;
   planet               = *IDSTR_PLANET_MARS;
   campaign             = "Cybrid Counteraction";
   dateOnMissionEnd     = $otsk::dateOnMissionEnd;
   shortDesc            = $otsk::shortDesc;
   longDescRichText     = $otsk::longDescRichText;
   nextMission          = "sa0";
   successDescRichText  = $otsk::successDescRichText;
   failDescRichText     = $otsk::failDescRichText;
   location             = $otsk::location;
};

## -------------------------------------------------------------------- Mission Objectives
MissionBriefObjective missionObjective1
{
	isPrimary 	= true;
	status 		= *IDSTR_OBJ_ACTIVE;
	shortTxt	= "Combat Training";
	longTxt		= "Defeat the Combat Instructor. We're not looking to kill him, "
                  @ "that isn't the aim to this test. ";
};

MissionBriefObjective missionObjective2
{
	isPrimary 	= true;
	status 		= *IDSTR_OBJ_ACTIVE;
	shortTxt	= "Convoy Test";
	longTxt		= "Protect the convoy as it gets to base. This specific run normally goes smoothly, rarely ever an attack on this route. But all things, consider this your first official mission.";
};

## -------------------------------------------------------------------- onMission
function onMissionStart()
{
    dbecho(3, "TRIGGER: onMissionStart();");
    cdAudioCycle(ss3, cyberntx, ss4);
    if (missionData.dateOnMissionEnd > 28299400)
    {
        %rand = randomInt(1,4);
    }
    else
    {
        %rand = randomInt(1,3);
    }
    
    if(%rand == 1)
    {
        $enemyType = "Knighst";
        $enemyName = "Imperal Police";
    }
    else if(%rand == 2) 
    {
        $enemyType = "Rebels";
        $enemyName = "Martian Rebellion";
    }
    else if(%rand == 3)
    {
        $enemyType = "None";
    }
    else if(%rand == 4)
    {
        $enemyType = "Cybrids";
        $enemyName = "Cybrid";
    }
    
}

## -------------------------------------------------------------------- Player
function player::onAdd( %this )
{
    dbecho(3, "TRIGGER: player::onAdd(" @ %this @ ");");
    // Setting this lets us do a player check easily where ever we need it without
    // needed to convert a vehicle ID to a player number.
	$ThePlayer = %this;
}

## -------------------------------------------------------------------- Vehicle
function Trainer::vehicle::onAttacked(%this, %that)
{
    dbecho(3, "Trainer::vehicle::onAttacked(" @ %this @ ", "@ %that @ ");");
    healObject(%this,5000);
    if (%that == $playerNum.id) 
    {
        $TrainerAttacks++;
        if($TrainerAttacks == 10)
        {
            order(%this, shutdown, true);
            missionObjective1.status = *IDSTR_OBJ_COMPLETED;
            say(%$playerNum, 0, "We're done. That convoy test is about to begin, get to your next nav point.");
            // activate next NavPoint
        }
    }
}

function Ambush::vehicle::onDestroyed(%this, %that)
{
    dbecho(3, "Ambush::vehicle::onDestroyed(" @ %this @ ", "@ %that @ ");");
    missionObjective2.status = *IDSTR_OBJ_COMPLETED;
    // activate next NavPoint
}

function vehicle::onAdd(%this)
{
    dbecho(3, "TRIGGER: vehicle::onAdd(" @ %this @ ");");
    if(%this == playerManager::PlayerNumToVehicleId($playerNum))
        $playerNum.id = %this;
}

## -------------------------------------------------------------------- Structure

## -------------------------------------------------------------------- Trigger
function StorkeeperBase::trigger::onEnter(%this, %that)
{
    dbecho(3, "StorkeeperBase::trigger::onEnter(" @ %this @ ", "@ %that @ ");");
    if (missionObjective2.status == *IDSTR_OBJ_COMPLETED && missionObjective1.status == *IDSTR_OBJ_COMPLETED)
    {
        win();
    }
}

## -------------------------------------------------------------------- Server

## -------------------------------------------------------------------- Custom Fn
