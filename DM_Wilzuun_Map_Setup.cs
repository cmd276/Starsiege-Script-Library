// FILENAME:	DM_Wilzuun_Map_Setup.cs
//------------------------------------------------------------------------------

## NOTES:
// Please do not name any of your structures "wilzuun" as this could potentially 
// break the game scripts included in the library.

$missionName = "DM_Wilzuun_Map_Setup";

exec("multiplayerStdLib.cs");
exec("DMstdLib.cs");

// This map is a working example of how to setup a game for DOV.
// It should also work for setting up any other game from my game libraries.
exec(WilzuunLoader);
$wilzuun::GameType = "dov";

function wilzuun::OverRide()
{
	$swarmClone = true;
}

function setDefaultMissionOptions()
{
	wilzuun::setDefaultMissionOptions();
}

function onMissionPreload() 
{
	MIssion::onPreload();
}

function onMissionLoad()
{
	wilzuun::onMissionLoad();
}

function onMissionStart()
{
	wilzuun::onMissionStart();
}

function onMissionEnd() 
{
	wilzuun::onMissionEnd();
}

function setRules ()
{
	// If you wish to add onto the rules, or prepend rules to the game, this is 
	// the optimal way to do it.
	%rules = wilzuun::setRules();
	%rules = "This is a line before the rules.\n" @ %rules;
	setGameInfo(%rules); 

}
setRules();

function initScoreBoard()
{
	wilzuun::initScoreBoard();
}

## -------------------------------------------------------------------- Vehicles

function vehicle::onAdd(%vehicleId)
{
	wilzuun::vehicle::onAdd(%vehicleId);
}

function vehicle::onDestroyed(%destroyed, %destroyer)
{
	wilzuun::vehicle::onDestroyed(%destroyed, %destroyer);
	vehicle::onDestroyedLog();
}

function vehicle::onAction()
{
	wilzuun::vehicle::onAction();
}
function vehicle::onAttacked(%targeted, %targeter)
{
	wilzuun::vehicle::onAttacked(%targeted, %targeter);
}
function vehicle::onMessage(%targeted, %targeter)
{
	wilzuun::vehicle::onMessage(%targeted, %targeter);
}
function vehicle::onScan(%targeted, %targeter)
{
	wilzuun::vehicle::onScan(%targeted, %targeter);
}
function vehicle::onTargeted(%targeted, %targeter)
{
	wilzuun::vehicle::onTargeted(%targeted, %targeter);
}
function vehicle::salvage()
{
	wilzuun::vehicle::salvage();
}

## ------------------------------------------------------------------ structures

function structure::onAdd()
{
	wilzuun::structure::onAdd();
}
function structure::onAttacked()
{
	wilzuun::structure::onAttacked();
}
function structure::onDestroyed(%this, %attackerId)
{
	wilzuun::structure::OnDestroyed(%this, %attackerId);
}

function structure::onDisabled()
{
	wilzuun::structure::onDisabled();
}
function structure::onScan()
{
	wilzuun::structure::onScan();
}

## --------------------------------------------------------------------- players

function player::onAdd(%player) 
{
	wilzuun::player::onAdd(%player);
	player::onAddLog();
}

function player::onRemove(%player) 
{
	wilzuun::player::onRemove(%player);
	player::onRemoveLog();
}
