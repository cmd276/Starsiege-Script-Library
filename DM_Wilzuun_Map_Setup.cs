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
	vehicle::onDestroyedLog(%destroyed, %destroyer);
}

function vehicle::onAction()
{
	wilzuun::vehicle::onAction();
}

function vehicle::onAttacked(%attacked, %attacker)
{
	wilzuun::vehicle::onAttacked(%attacked, %attacker);
}

function vehicle::onMessage(%targeted, %targeter)
{
	wilzuun::vehicle::onMessage(%targeted, %targeter);
}

function vehicle::onScan(%scanned, %scanner)
{
	wilzuun::vehicle::onScan(%scanned, %scanner);
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

function structure::onAdd(%structure)
{
	wilzuun::structure::onAdd(%structure);
}

function structure::onAttacked(%attacked, %attacker)
{
	wilzuun::structure::onAttacked(%attacked, %attacker);
}

function structure::onDestroyed(%destroyed, %destroyer)
{
	wilzuun::structure::OnDestroyed(%destroyed, %destroyer);
}

function structure::onDisabled(%structure)
{
	wilzuun::structure::onDisabled(%structure);
}

function structure::onScan(%scanned, %scanner)
{
	wilzuun::structure::onScan(%scanned, %scanner);
}

## --------------------------------------------------------------------- players

function player::onAdd(%player) 
{
	wilzuun::player::onAdd(%player);
	player::onAddLog(%player);
}

function player::onRemove(%player) 
{
	wilzuun::player::onRemove(%player);
	player::onRemoveLog(%player);
}
