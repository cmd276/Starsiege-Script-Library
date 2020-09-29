// FILENAME:	DM_Wilzuun_Map_Setup.cs
//------------------------------------------------------------------------------

$missionName = "DM_Wilzuun_Map_Setup";

exec("multiplayerStdLib.cs");
exec("DMstdLib.cs");

// This map is a working example of how to setup a game for DOV.
// It should also work for setting up any other game from my game libraries.
xec(WilzuunLoader);
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

function structure::onDestroyed(%this, %attackerId)
{
	wilzuun::structure::OnDestroyed(%this, %attackerId);
}

function player::onAdd(%player) 
{
	wilzuun::player::onAdd(%player);
}

function player::onRemove(%player) 
{
	wilzuun::player::onRemove(%player);
}

function setRules ()
{
	// If you wish to add onto the rules, or prepend rules to the game, this is the optimal way to do it.
	%rules = wilzuun::setRules();
	%rules = "This is a line before the rules.\n" @ %rules;
	setGameInfo(%rules); 

}
setRules();

function vehicle::onAdd(%vehicleId)
{
	wilzuun::vehicle::onAdd(%vehicleId);
}

function vehicle::onDestroyed(%destroyed, %destroyer)
{
	wilzuun::vehicle::onDestroyed(%destroyed, %destroyer);
}

function vehicle::onDestroyed(%destroyed, %destroyer)
{
	wilzuun::vehicle::onDestroyed(%destroyed, %destroyer);
}

function initScoreBoard()
{
	wilzuun::initScoreBoard();
}