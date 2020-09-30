// AUTHOR:      Temujin
//------------------------------------------------------------------------------

///////////////////////////////////////////////////////////////////////////////////////////////////
// Lots O' Globals
///////////////////////////////////////////////////////////////////////////////////////////////////
$redKills = 0;
$blueKills = 0;

$yellowHealRate = $blueHealRate = $redHealRate = $purpleHealRate = 750;
$padWaitTime = 15;
$zenWaitTime = 60;

$BuildingsDestroyed = 0;

$Herc1Path = "MissionGroup/Herc1path";
$Herc2Path = "MissionGroup/Herc2path";
$Herc3Path = "MissionGroup/Herc3path";
$Herc4Path = "MissionGroup/Herc4path";
$Herc5Path = "MissionGroup/Herc5path";
$Herc6Path = "MissionGroup/Herc6path";

$DIDestroyed = false;
$G1Destroyed = false;
$G2Destroyed = false;
$G3Destroyed = false;
$G4Destroyed = false;
$LightFactDestroyed = false;
$HeavyFactDestroyed = false;
$TankFactDestroyed = false;
$CommCenterDestroyed = false;

$radius = 200;
$damage = 7500;

$sensorbusy = false;

$start = 0;
$gamewon = 0;

$respawnDelay= 60;
$respawnDelayNoHq= 300;

$BUILDINGS_TO_DESTROY = 9;

///////////////////////////////////////////////////////////////////////////////////////////////////
// Scoring Variables
///////////////////////////////////////////////////////////////////////////////////////////////////

$buildingPoints = 7;
$killPoints = 3;
$deathPoints = 2;

///////////////////////////////////////////////////////////////////////////////////////////////////
// our base defender attributes
//
// The jump from 0.9 to 1.0 is a big one. At skill 1.0 and accuracy 1.0 the AI units will aim 
// first for the weapons, then the legs. A little too much for this mission.
///////////////////////////////////////////////////////////////////////////////////////////////////

Pilot Protector
{
   id = 29;
   
   name = "Red Defender";
   
   skill = 0.85;
   accuracy = 0.85;
   aggressiveness = 0.85;
   activateDist = 850.0;
   deactivateBuff = 1000.0;
   targetFreq = 5.0;
   trackFreq = 0.0;
   fireFreq = 0.2;
   LOSFreq = 0.2;
   orderFreq = 2.0;
};

Pilot Defender
{
   id = 30;
   
   name = "Red Guardian";
   
   skill = 0.95;
   accuracy = 0.95;
   aggressiveness = 0.95;
   activateDist = 750.0;
   deactivateBuff = 1000.0;
   targetFreq = 2.0;
   trackFreq = 0.0;
   fireFreq = 0.2;
   LOSFreq = 0.2;
   orderFreq = 2.0;
};

Pilot Knight
{
   id = 31;
   
   name = "Red Knight";
   
   skill = 0.99;
   accuracy = 0.99;
   aggressiveness = 1.0;
   activateDist = 650.0;
   deactivateBuff = 1000.0;
   targetFreq = 2.0;
   trackFreq = 0.0;
   fireFreq = 0.2;
   LOSFreq = 0.2;
   orderFreq = 2.0;
};

/////////////////////////////////////////////////////////////////////////////////////////////
// This code handles the Start Up functions
/////////////////////////////////////////////////////////////////////////////////////////////

function initGlobalVars()
{
   $scoringFreeze = false;
   
   %playerCount = playerManager::getPlayerCount();
	// clear all points for the players
   for (%p = 0; %p < %playerCount; %p++)
	{
		%player = playerManager::getPlayerNum(%p);
      %player.numKills = 0;
      %player.buildingsDestroyed = 0;
   }
}

function activate_AI() {
   if($Herc1) {   setPilotId( $Herc1, 29 );   setTeam( $Herc1, *IDSTR_TEAM_RED );   }
   if($Herc2) {   setPilotId( $Herc2, 29 );   setTeam( $Herc2, *IDSTR_TEAM_RED );   }
   if($Herc3) {   setPilotId( $Herc3, 29 );   setTeam( $Herc3, *IDSTR_TEAM_RED );   }
   if($Herc4) {   setPilotId( $Herc4, 29 );   setTeam( $Herc4, *IDSTR_TEAM_RED );   }
   if($Herc5) {   setPilotId( $Herc5, 29 );   setTeam( $Herc5, *IDSTR_TEAM_RED );   }
   if($Herc6) {   setPilotId( $Herc6, 29 );   setTeam( $Herc6, *IDSTR_TEAM_RED );   }
}

function activate_Turrets() {
   setTeam( getObjectId("MissionGroup\\RED\\Turrets\\1") , *IDSTR_TEAM_RED );   
   setTeam( getObjectId("MissionGroup\\RED\\Turrets\\2") , *IDSTR_TEAM_RED );   
   setTeam( getObjectId("MissionGroup\\RED\\Turrets\\3") , *IDSTR_TEAM_RED );   
   setTeam( getObjectId("MissionGroup\\RED\\Turrets\\4") , *IDSTR_TEAM_RED );   
}

function activate_Sensors() {
   playAnimSequence( getObjectId("MissionGroup\\RED\\Sensors\\EYE360") , Sequence01, true);  
   playAnimSequence( getObjectId("MissionGroup\\RED\\Sensors\\EYE030") , Sequence01, true);  
   playAnimSequence( getObjectId("MissionGroup\\RED\\Sensors\\EYE060") , Sequence01, true);  
   playAnimSequence( getObjectId("MissionGroup\\RED\\Sensors\\EYE090") , Sequence01, true);  
   playAnimSequence( getObjectId("MissionGroup\\RED\\Sensors\\EYE120") , Sequence01, true);  
   playAnimSequence( getObjectId("MissionGroup\\RED\\Sensors\\EYE150") , Sequence01, true);  
   playAnimSequence( getObjectId("MissionGroup\\RED\\Sensors\\EYE180") , Sequence01, true);  
   playAnimSequence( getObjectId("MissionGroup\\RED\\Sensors\\EYE210") , Sequence01, true);  
   playAnimSequence( getObjectId("MissionGroup\\RED\\Sensors\\EYE240") , Sequence01, true);  
   playAnimSequence( getObjectId("MissionGroup\\RED\\Sensors\\EYE270") , Sequence01, true);  
   playAnimSequence( getObjectId("MissionGroup\\RED\\Sensors\\EYE300") , Sequence01, true);  
   playAnimSequence( getObjectId("MissionGroup\\RED\\Sensors\\EYE330") , Sequence01, true);  
}

function GuardOnMissionStart()
{
   order( $Herc1, guard, $Herc1Path );
   order( $Herc2, guard, $Herc2Path );
   order( $Herc3, guard, $Herc3Path );
   order( $Herc4, guard, $Herc4Path );
   order( $Herc5, guard, $Herc5Path );
   order( $Herc6, guard, $Herc6Path );
}

function vehicle::onAdd(%this)
{
   // see if it is a player
   %player = playerManager::vehicleIdToPlayerNum(%this);
   if(%player == 0) 
      return;

   %this.targetted = 0;

   %team = getTeam(%this);

   if(%team == *IDSTR_TEAM_BLUE) {
      schedule( "setBlueNavPoint(" @ %this @ ");", 1 );
      if($start == 0) {
	   activate_AI();
	   activate_Turrets();
	   activate_Sensors();
	   GuardOnMissionStart();
	   schedule( "beginTime();", 5 );
	   $start = 1;
  	}
   }
}

function setBlueNavPoint( %this )
{
   if( getTeam(%this) == *IDSTR_TEAM_BLUE )
   {
      setNavMarker( "MissionGroup/NavPoints/RED", true, %this );
   }
}

function onMissionLoad(){
   cdAudioCycle("Purge", "Terror", "Watching", "Mechsoul", "Newtech"); 

   // get the original ID for each AI Herc ( for use later when we clone them )
   $Herc1 = getObjectId( "MissionGroup\\Hercs\\h1" );
   $Herc2 = getObjectId( "MissionGroup\\Hercs\\h2" );
   $Herc3 = getObjectId( "MissionGroup\\Hercs\\h3" );
   $Herc4 = getObjectId( "MissionGroup\\Hercs\\h4" );
   $Herc5 = getObjectId( "MissionGroup\\Hercs\\h5" );
   $Herc6 = getObjectId( "MissionGroup\\Hercs\\h6" );

   %rules = "<F2>MISSION:<F0> " @ $missionName @ "\n\n" @  
            "<F2>RULES:\n<F0> "@
		"<F1>RED PLAYERS:<F0> Defend your base. To win, hold " @
		"the base for 30 minutes so that reinforcements may arrive.\n" @
		"<F1>BLUE PLAYERS:<F0> Destroy Red Base to prevent reinforcements from landing. You have 30 minutes " @
		"to destroy 9 structures.\n\n" @
            "<F2>BLUE TEAM TARGETS:<F0>\n"      @
            "<F3>FACTORIES -<F0> Red Base has 3 factories producing AI defenders.\n\n" @
            "<F3>TURRETS/GENERATORS -<F0> Red Base has four turrets. Destroy " @
            "nearby Power Generators to bring them offline.\n\n" @
		"<F3>COMMAND CENTER -<F0> Red Base Command Center coordinates Orbital Artillery attacks. " @
            "Destroy the Command Center to degrade the Artillery.\n\n" @
		"<F3>DEUS IRAE -<F0> Red Base has a Deus Irae beacon. Destroy it.\n\n" @
            "<F2>SCORING:<F0>\n"     @
		"Blue team gets " @ $buildingPoints @ " for each building destroyed.\n" @
		"Kills are worth " @ $killPoints @ " and Deaths are -" @ $deathPoints @ ".\n\n"@
		"<F1>This map by Temujin is available at: temujin.www6.50megs.com<F0>\n\n"@
            "<tIDMULT_STD_HEAL>"          @
            "<tIDMULT_STD_RELOAD_1>"      @
            $PadWaitTime                  @
            "<tIDMULT_STD_RELOAD_2>";
   
   setGameInfo(%rules);
}

function vehicle::onDestroyed( %this, %destroyer )
{
   if( getTeam( %this ) == *IDSTR_TEAM_RED )
   {
      $blueKills++;
   }
   else
   {
      $redKills++;
   }

   // award the player a kill ( if the enemy is a different color )
   if( getTeam( %destroyer ) != getTeam( %this ) )
   {
      %player = playerManager::vehicleIdToPlayerNum( %destroyer );
      if(%player != 0)
      {
         %player.numKills++;      
      }
   }

   //----------------------------------------------------------------
   // If any of our AI hercs die, give a message and re-clone/drop them
   //----------------------------------------------------------------

   //---HEAVY HERCS---
   if( %this == $Herc1 )
   {
      if( $HeavyFactDestroyed == false )
      {
         schedule( "TemsCloneVehicle(\"$Herc1\", " @ %this @ ", $Herc1X, $Herc1Y);", $respawnDelay);
      }
      else
      {
         schedule( "TemsCloneVehicle(\"$Herc1\", " @ %this @ ", $Herc1X, $Herc1Y);", $respawnDelayNoHq);
      }
   }
   if( %this == $Herc2 )
   {
      if( $HeavyFactDestroyed == false )
      {
         schedule( "TemsCloneVehicle(\"$Herc2\", " @ %this @ ", $Herc2X, $Herc2Y);", $respawnDelay);
      }
      else
      {
         schedule( "TemsCloneVehicle(\"$Herc2\", " @ %this @ ", $Herc2X, $Herc2Y);", $respawnDelayNoHq);
      }
   }

   //---LIGHT HERCS---
   if( %this == $Herc3 )
   {
      if( $LightFactDestroyed == false )
      {
         schedule( "TemsCloneVehicle(\"$Herc3\", " @ %this @ ", $Herc3X, $Herc3Y);", $respawnDelay);
      }
      else
      {
         schedule( "TemsCloneVehicle(\"$Herc3\", " @ %this @ ", $Herc3X, $Herc3Y);", $respawnDelayNoHq);
      }
   }
   if( %this == $Herc4 )
   {
      if( $LightFactDestroyed == false )
      {
         schedule( "TemsCloneVehicle(\"$Herc4\", " @ %this @ ", $Herc4X, $Herc4Y);", $respawnDelay);
      }
      else
      {
         schedule( "TemsCloneVehicle(\"$Herc4\", " @ %this @ ", $Herc4X, $Herc4Y);", $respawnDelayNoHq);
      }
   }

   //---TANKS---
   if( %this == $Herc5 )
   {
      if( $TankFactDestroyed == false )
      {
         schedule( "TemsCloneVehicle(\"$Herc5\", " @ %this @ ", $Herc5X, $Herc5Y);", $respawnDelay);
      }
      else
      {
         schedule( "TemsCloneVehicle(\"$Herc5\", " @ %this @ ", $Herc5X, $Herc5Y);", $respawnDelayNoHq);
      }
   }
   if( %this == $Herc6 )
   {
      $HercsDestroyed++;
      
      if( $TankFactDestroyed == false )
      {
         schedule( "TemsCloneVehicle(\"$Herc6\", " @ %this @ ", $Herc6X, $Herc6Y);", $respawnDelay);
      }
      else
      {
         schedule( "TemsCloneVehicle(\"$Herc6\", " @ %this @ ", $Herc6X, $Herc6Y);", $respawnDelayNoHq);
      }
   }

   // left over from missionStdLib.cs
   vehicle::onDestroyedLog(%this, %destroyer);
   
   // give the death messages...
   %message = getFancyDeathMessage(getHUDName(%this), getHUDName(%destroyer));
   if(%message != "")
   {
      say( 0, 0, %message);
   }

   // rules enforcement
   if(
      (getTeam(%this) == getTeam(%destroyer)) &&
      (%this != %destroyer)
   )
   {
      antiTeamKill(%destroyer);
   }
}

/////////////////////////////////////////////////////////////////////////////////////////////
// ScoreBoard Functions
/////////////////////////////////////////////////////////////////////////////////////////////

function initScoreBoard()
{
   deleteVariables("$ScoreBoard::PlayerColumn*");
   deleteVariables("$ScoreBoard::TeamColumn*");

	$ScoreBoard::PlayerColumnHeader1 = "Team";
	$ScoreBoard::PlayerColumnHeader2 = "Score";
	$ScoreBoard::PlayerColumnHeader3 = "Destroyed Buildings";
	$ScoreBoard::PlayerColumnHeader4 = "Kills";
	$ScoreBoard::PlayerColumnHeader5 = "Deaths";

	$ScoreBoard::PlayerColumnFunction1 = "getTeam2";
      $ScoreBoard::PlayerColumnFunction2 = "getPlayerScore";
	$ScoreBoard::PlayerColumnFunction3 = "getBuildingsDestroyed";
	$ScoreBoard::PlayerColumnFunction4 = "getKills";
	$ScoreBoard::PlayerColumnFunction5 = "getDeaths";

   $ScoreBoard::TeamColumnHeader1 = "Destroyed Buildings";
   $ScoreBoard::TeamColumnHeader2 = "Remaining Buildings";
   $ScoreBoard::TeamColumnHeader3 = "Kills";
   $ScoreBoard::TeamColumnHeader4 = "Deaths";
   $ScoreBoard::TeamColumnFunction1 = "getTotalBuildingsDestroyed";
   $ScoreBoard::TeamColumnFunction2 = "getRemainingBuildings";
   $ScoreBoard::TeamColumnFunction3 = "getTeamKills";
   $ScoreBoard::TeamColumnFunction4 = "getTeamDeaths";
 
   serverInitScoreBoard();
}

         // INDIVIDUAL SCORING FUNCTIONS //

function getPlayerScore(%a)
{
   %score = (getKills(%a) * $killPoints) - (getDeaths(%a) * $deathPoints);

   if( getTeam( %a ) == *IDSTR_TEAM_BLUE )
	return (%score + getBuildingsDestroyed(%a) * $buildingPoints);
   else
   	return %score;
}

function getTeam2(%player)
{
   if( getTeam( %player ) == *IDSTR_TEAM_BLUE )
   {
      return *IDMULT_BLUE;
   }
   else
   {
      return *IDMULT_RED;
   }
}

function getKills(%player)
{
   if( %player.numKills != "")
      return %player.numKills;
      
   return "0";
}

function getBuildingsDestroyed(%player)
{
   if( %player.buildingsDestroyed != "") {
      if( getTeam( %player ) == *IDSTR_TEAM_BLUE )
         return %player.buildingsDestroyed;
	else
	   return "N/A";
   }
   return "0";
}

         // TEAM SCORING FUNCTIONS //

function getTeamKills(%team)
{
   if( getTeamNameFromTeamId(%team) == *IDSTR_TEAM_BLUE )
   {
      return $blueKills;
   }
   else
   {
      return $redKills;
   }   
}

function getRemainingBuildings(%team)
{
   if( getTeamNameFromTeamId(%team) == *IDSTR_TEAM_BLUE )
      return "N/A";
   else
      return $BUILDINGS_TO_DESTROY - $BuildingsDestroyed;
}

function getTotalBuildingsDestroyed(%team)
{
   if( getTeamNameFromTeamId(%team) == *IDSTR_TEAM_BLUE )
	return $BuildingsDestroyed;
   else
	return "N/A";
}

/////////////////////////////////////////////////////////////////////////////////////////////
// This code handles the destruction of the buildings
/////////////////////////////////////////////////////////////////////////////////////////////

function G1::structure::onDestroyed( %this, %who )
{
  $BuildingsDestroyed++;
  checkForBlueWin();

  order( "MissionGroup\\RED\\Turrets\\1", shutdown, True );
  %player = playerManager::vehicleIdToPlayerNum( %who );
  if(%player != 0)
  {
      %player.buildingsDestroyed++;
  }

  $G1Destroyed = true;

  %txt = "<F5>" @ getHUDName(%who) @ " demolished Red team's southwest turret.";
  say(IDSTR_TEAM_BLUE, 1234, %txt, "ene_struct_dest.wav");    
  say(IDSTR_TEAM_RED, 1234, %txt, "friend_struct_des.wav");    
}

function G2::structure::onDestroyed( %this, %who )
{
  $BuildingsDestroyed++;
  checkForBlueWin();

  order( "MissionGroup\\RED\\Turrets\\2", shutdown, True );
  %player = playerManager::vehicleIdToPlayerNum( %who );
  if(%player != 0)
  {
      %player.buildingsDestroyed++;
  }

  $G2Destroyed = true;

  %txt = "<F5>" @ getHUDName(%who) @ " demolished Red team's southeast turret.";
  say(IDSTR_TEAM_BLUE, 1234, %txt, "ene_struct_dest.wav");
  say(IDSTR_TEAM_RED, 1234, %txt, "friend_struct_des.wav");    
}


function G3::structure::onDestroyed( %this, %who )
{
  $BuildingsDestroyed++;
  checkForBlueWin();

  order( "MissionGroup\\RED\\Turrets\\3", shutdown, True );
  %player = playerManager::vehicleIdToPlayerNum( %who );
  if(%player != 0)
  {
      %player.buildingsDestroyed++;
  }

  $G3Destroyed = true;

  %txt = "<F5>" @ getHUDName(%who) @ " demolished Red team's northeast turret.";
  say(IDSTR_TEAM_BLUE, 1234, %txt, "ene_struct_dest.wav");    
  say(IDSTR_TEAM_RED, 1234, %txt, "friend_struct_des.wav");    
}

function G4::structure::onDestroyed( %this, %who )
{
  $BuildingsDestroyed++;
  checkForBlueWin();

  order( "MissionGroup\\RED\\Turrets\\4", shutdown, True );
  %player = playerManager::vehicleIdToPlayerNum( %who );
  if(%player != 0)
  {
      %player.buildingsDestroyed++;
  }

  $G4Destroyed = true;

  %txt = "<F5>" @ getHUDName(%who) @ " demolished Red team's northwest turret.";
  say(IDSTR_TEAM_BLUE, 1234, %txt, "ene_struct_dest.wav");    
  say(IDSTR_TEAM_RED, 1234, %txt, "friend_struct_des.wav");    
}

function DI::structure::onDestroyed( %this, %who )
{
  $BuildingsDestroyed++;
  checkForBlueWin();
  %player = playerManager::vehicleIdToPlayerNum( %who );
  if(%player != 0)
  {
      %player.buildingsDestroyed++;     
  }

  $DIDestroyed = true;

  %beam = getObjectId("MissionGroup\\RED\\Beam");
  schedule("deleteObject(" @ %beam @ ");", 0.1);

  %txt = "<F5>" @ getHUDName(%who) @ " destroyed Red Teams Deus Irae!";
  say(IDSTR_TEAM_BLUE, 1234, %txt, "ene_struct_dest.wav");    
  say(IDSTR_TEAM_RED, 1234, %txt, "friend_struct_des.wav");    
}

function CommCenter::structure::onDestroyed( %this, %who )
{
  $BuildingsDestroyed++;
  checkForBlueWin();
  %player = playerManager::vehicleIdToPlayerNum( %who );
  if(%player != 0)
  {
      %player.buildingsDestroyed++;     
  }

  $CommCenterDestroyed = true;

  %txt = "<F5>" @ getHUDName(%who) @ " destroyed Red Teams Command Center!";
  say(IDSTR_TEAM_BLUE, 1234, %txt, "ene_struct_dest.wav");    
  say(IDSTR_TEAM_RED, 1234, %txt, "friend_struct_des.wav");    
  say(0, 1234, "<F5>ATTENTION: Orbital Artillery's acuracy is degraded." );  
}

function LightFact::structure::onDestroyed( %this, %who )
{
  $BuildingsDestroyed++;
  checkForBlueWin();
  %player = playerManager::vehicleIdToPlayerNum( %who );
  if(%player != 0)
  {
      %player.buildingsDestroyed++;     
  }

  $LightFactDestroyed = true;

  %txt = "<F5>" @ getHUDName(%who) @ " destroyed Red Teams Light Herc Factory!";
  say(IDSTR_TEAM_BLUE, 1234, %txt, "ene_struct_dest.wav");    
  say(IDSTR_TEAM_RED, 1234, %txt, "friend_struct_des.wav");
  say(0, 1234, "<F5>ATTENTION: Light Herc respawn rates reduced to 5 minutes." );  
}

function HeavyFact::structure::onDestroyed( %this, %who )
{
  $BuildingsDestroyed++;
  checkForBlueWin();
  %player = playerManager::vehicleIdToPlayerNum( %who );
  if(%player != 0)
  {
      %player.buildingsDestroyed++;     
  }

  $HeavyFactDestroyed = true;

  %txt = "<F5>" @ getHUDName(%who) @ " destroyed Red Teams Heavy Herc Factory!";
  say(IDSTR_TEAM_BLUE, 1234, %txt, "ene_struct_dest.wav");    
  say(IDSTR_TEAM_RED, 1234, %txt, "friend_struct_des.wav");    
  say(0, 1234, "<F5>ATTENTION: Heavy Herc respawn rates reduced to 5 minutes." );  
}

function TankFact::structure::onDestroyed( %this, %who )
{
  $BuildingsDestroyed++;
  checkForBlueWin();
  %player = playerManager::vehicleIdToPlayerNum( %who );
  if(%player != 0)
  {
      %player.buildingsDestroyed++;     
  }

  $TankFactDestroyed = true;

  %txt = "<F5>" @ getHUDName(%who) @ " destroyed Red Teams Tank Factory!";
  say(IDSTR_TEAM_BLUE, 1234, %txt, "ene_struct_dest.wav");    
  say(IDSTR_TEAM_RED, 1234, %txt, "friend_struct_des.wav");    
  say(0, 1234, "<F5>ATTENTION: Tank respawn rates reduced to 5 minutes." );  
}

/////////////////////////////////////////////////////////////////////////////////////////////
// This code handles the destruction of the sensors
/////////////////////////////////////////////////////////////////////////////////////////////

function EYE030::structure::onDestroyed( %this, %who )
{
   sensorDead(%this, %who);
}

function EYE060::structure::onDestroyed( %this, %who )
{
   sensorDead(%this, %who);
}

function EYE090::structure::onDestroyed( %this, %who )
{
   sensorDead(%this, %who);
}

function EYE120::structure::onDestroyed( %this, %who )
{
   sensorDead(%this, %who);
}

function EYE150::structure::onDestroyed( %this, %who )
{
   sensorDead(%this, %who);
}

function EYE180::structure::onDestroyed( %this, %who )
{
   sensorDead(%this, %who);
}

function EYE210::structure::onDestroyed( %this, %who )
{
   sensorDead(%this, %who);
}

function EYE240::structure::onDestroyed( %this, %who )
{
   sensorDead(%this, %who);
}

function EYE270::structure::onDestroyed( %this, %who )
{
   sensorDead(%this, %who);
}

function EYE300::structure::onDestroyed( %this, %who )
{
   sensorDead(%this, %who);
}

function EYE330::structure::onDestroyed( %this, %who )
{
   sensorDead(%this, %who);
}

function EYE360::structure::onDestroyed( %this, %who )
{
   sensorDead(%this, %who);
}

function sensorDead( %this, %who)
{
   %sensor = getObjectName(%this);

   %player = playerManager::vehicleIdToPlayerNum( %who );
   %txt = "Sensor " @ %sensor @ " has been destroyed.";
   say(IDSTR_TEAM_BLUE, 1234, "<F5>ATTENTION: " @ %txt @ "<F0>",  "ene_struct_dest.wav");    
   say(IDSTR_TEAM_RED, 1234, "<F5>WARNING: " @ %txt @ "<F0>", "friend_struct_des.wav");    

   %target = getObjectId( "MissionGroup\\Triggers\\" @ %sensor @ "");
   %x = getPosition(%target,x);
   %y = getPosition(%target,y);

   setPosition(%target, %x, %y, -1000);
}

/////////////////////////////////////////////////////////////////////////////////////////////
// Orbital Artillery Code
/////////////////////////////////////////////////////////////////////////////////////////////

function Sensor::trigger::onEnter(%this, %object)
{
   if( getTeam(%object) != *IDSTR_TEAM_RED && $sensorbusy == false && !%object.targetted)
   {
   	%sensor = getObjectName(%this);
   	%enemy = getVehicleName(%object);
   	$sensorbusy = true;

      %player = playerManager::vehicleIdToPlayerNum(%object);
   	say(%player, 1234, "<F4>You have been detected by enemy sensors.<F0>", "bptslct.wav");

	if ($CommCenterDestroyed == true ) {
     	   %txt = "<F5>WARNING: Sensor " @ %sensor @ " has detected an enemy " @ %enemy @ ". Orbital Artillery Engaging, but accuracy is degraded.<F0>";
	   say(IDSTR_TEAM_RED, 1234, %txt, "alarm2.wav");
	   %object.targetted = 1;
	   schedule("OrbitalStrike(" @ %object @ ", 0);", 2);	}
	else {
     	   %txt = "<F5>ATTENTION: Sensor " @ %sensor @ " has detected an enemy " @ %enemy @ ". Orbital Artillery Engaging.<F0>";
	   say(IDSTR_TEAM_RED, 1234, %txt, "cyb_nex15.wav");
	   %object.targetted = 1;
	   schedule("OrbitalStrike(" @ %object @ ", 0);", 2);
	}
   }
}

function StartBoom(%target)
{
   if($CommCenterDestroyed == true) {
   	%varx = randomInt( -300, 300 );
   	%vary = randomInt( -300, 300 );
   }
   else {
      %varx = randomInt( -100, 100 );
      %vary = randomInt( -100, 100 );
   }
   %x = getPosition(%target,x) + %varx;
   %y = getPosition(%target,y) + %vary;
   %z = getTerrainHeight( %x, %y );

   %this = getObjectId("MissionGroup\\RED\\Boom1");
   if(%num == 1) %this = getObjectId("MissionGroup\\RED\\Boom2");
   if(%num == 2)  %this = getObjectId("MissionGroup\\RED\\Boom3");
   setPosition(%this, %x, %y, %z);

   playAnimSequence(%this, Sequence01, true);
   schedule("FinishBoom("@%this@");", 1 );
}

function FinishBoom(%this)
{
   blast(%this, $radius, $damage, 0);
   stopAnimSequence(%this, Sequence01);
   setposition(%this, 0, 0, 0);
}

function OrbitalStrike(%target, %num)
{
   if(%num < 3) {
	if($sensorbusy == true)	$sensorbusy = false;
	%player = playerManager::vehicleIdToPlayerNum(%target);
   	say(%player, 0, "<F4>WARNING: Incoming Artillery!<F0>", "sfx_incoming.wav");
	schedule("StartBoom(" @ %target @ "," @ %num @ ");", 1);
	%num += 1;
	schedule("OrbitalStrike(" @ %target @ "," @ %num @ ");", randomInt(6,8) );   }
   else
      %target.targetted = 0;
}

/////////////////////////////////////////////////////////////////////////////////////////////
// Timing Code
/////////////////////////////////////////////////////////////////////////////////////////////

function beginTime()
{
   %this = getObjectId("MissionGroup\\RED\\Beam");
   playAnimSequence(%this, Sequence01, true);

   %txt = "<F5>SIEGE BASE this is VALKYRIE. We are enroute to your position, ETA 30 minutes.<F0>";
   say("everybody", 0, %txt, "sfx_bigbeam_fire.wav");

   schedule("Fifteen();", 900);
}

function Fifteen()
{
   %txt = "<F5>SIEGE BASE this is VALKYRIE. Continue to hold position, we are 15 minutes out.<F0>";
   say("everybody", 0, %txt, "sfx_siren.wav");

   schedule("TwentyFive();", 600);
}

function TwentyFive()
{
   %txt = "<F5>SIEGE BASE this is VALKYRIE. Hang tight, we are 5 minutes out.<F0>";
   say("everybody", 0, %txt, "sfx_siren.wav");

   schedule("TwentyNine();", 240);
}

function TwentyNine()
{
   %txt = "<F5>SIEGE BASE this is VALKYRIE. We have you on sensors. ETA 1 minute.<F0>";
   say("everybody", 0, %txt, "sfx_siren.wav");

   schedule("RedWin();", 60);
}

function RedWin()
{
   if($gamewon == 0) {
      %txt = "<F5>SIEGE BASE this is VALKYRIE. We have arrived on station. Executing drop sequence.<F0>";
   	say("everybody", 0, %txt, "sfx_siren.wav");

   	$gamewon = 1;

   	schedule("RedWin2();", 2);
   }
}

function RedWin2()
{
      fadeEvent( 0, out, 4, 1, 0, 0 );  // Fade to Red
      schedule( "endgame();", 5 );
      
      %txt = "Red team wins!";
      messageBox(0, %txt);
}

/////////////////////////////////////////////////////////////////////////////////////////////
// Miscelaneous Code
/////////////////////////////////////////////////////////////////////////////////////////////

function checkForBlueWin()
{
   if( $BuildingsDestroyed >= $BUILDINGS_TO_DESTROY && $gamewon == 0)
   {
      fadeEvent( 0, out, 4, 0, 0, 1 );  // Fade to Blue
      schedule( "endgame();", 5 );

	$gamewon = 1;      

      %txt = "Blue team wins!";
      messageBox(0, %txt);
   }
   else
   {
   if($BuildingsDestroyed < 6) 		return;
   else if($BuildingsDestroyed == 6) 	guard_3_way();   // Three Buildings Left
   else if($BuildingsDestroyed == 7) 	guard_2_way();   // Two Buildings Left
   else if($BuildingsDestroyed == 8) 	guard_1_way();   // One Building Left
   }
}

function guard_3_way()
{
   %count = 0;  

   if($G1Destroyed == false) 		
   {
      %count ++;  
	%object = getObjectId( "MissionGroup\\RED\\Turrets\\G1" );
      %one = %object;
   }
   if($G2Destroyed == false) 		
   {
      %count ++;  
	%object = getObjectId( "MissionGroup\\RED\\Turrets\\G2" );
     	if(%count == 1) 		%one = %object;
	else if(%count == 2) 	%two = %object;
   }
   if($G3Destroyed == false) 		
   {
      %count ++;  
	%object = getObjectId( "MissionGroup\\RED\\Turrets\\G3" );
     	if(%count == 1) 		%one = %object;
	else if(%count == 2) 	%two = %object;
	else if(%count == 3) 	%three = %object;
   }
   if($G4Destroyed == false) 		
   {
      %count ++;  
	%object = getObjectId( "MissionGroup\\RED\\Turrets\\G4" );
     	if(%count == 1) 		%one = %object;
	else if(%count == 2) 	%two = %object;
	else if(%count == 3) 	%three = %object;
   }
   if($LightFactDestroyed == false) 		
   {
      %count ++;  
	%object = getObjectId( "MissionGroup\\RED\\LightFact" );
     	if(%count == 1) 		%one = %object;
	else if(%count == 2) 	%two = %object;
	else if(%count == 3) 	%three = %object;
   }
   if($TankFactDestroyed == false) 		
   {
      %count ++;  
	%object = getObjectId( "MissionGroup\\RED\\TankFact" );
     	if(%count == 1) 		%one = %object;
	else if(%count == 2) 	%two = %object;
	else if(%count == 3) 	%three = %object;
   }
   if($HeavyFactDestroyed == false) 		
   {
      %count ++;  
	%object = getObjectId( "MissionGroup\\RED\\HeavyFact" );
     	if(%count == 1) 		%one = %object;
	else if(%count == 2) 	%two = %object;
	else if(%count == 3) 	%three = %object;
   }
   if($CommCenDestroyed == false) 		
   {
      %count ++;  
	%object = getObjectId( "MissionGroup\\RED\\CommCen" );
     	if(%count == 1) 		%one = %object;
	else if(%count == 2) 	%two = %object;
	else if(%count == 3) 	%three = %object;
   }
   if($DIDestroyed == false) 		
   {
      %count ++;  
	%object = getObjectId( "MissionGroup\\RED\\DI" );
     	if(%count == 1) 		%one = %object;
	else if(%count == 2) 	%two = %object;
	else if(%count == 3) 	%three = %object;
   }

   // Send Light Hercs to guard lowest priority structure...
   order( $Herc3, guard, %one );
   order( $Herc4, guard, %one );

   // Send Tanks Hercs to guard middle priority structure...
   order( $Herc5, guard, %two );
   order( $Herc6, guard, %two );

   // Send Heavies Hercs to guard Highest priority structure...
   order( $Herc1, guard, %three );
   order( $Herc2, guard, %three );
} 

function guard_2_way()
{
   %count = 0;  

   if($G1Destroyed == false) 		
   {
      %count ++;  
	%object = getObjectId( "MissionGroup\\RED\\Turrets\\G1" );
      %one = %object;
   }
   if($G2Destroyed == false) 		
   {
      %count ++;  
	%object = getObjectId( "MissionGroup\\RED\\Turrets\\G2" );
     	if(%count == 1) 		%one = %object;
	else if(%count == 2) 	%two = %object;
   }
   if($G3Destroyed == false) 		
   {
      %count ++;  
	%object = getObjectId( "MissionGroup\\RED\\Turrets\\G3" );
     	if(%count == 1) 		%one = %object;
	else if(%count == 2) 	%two = %object;
   }
   if($G4Destroyed == false) 		
   {
      %count ++;  
	%object = getObjectId( "MissionGroup\\RED\\Turrets\\G4" );
     	if(%count == 1) 		%one = %object;
	else if(%count == 2) 	%two = %object;
   }
   if($LightFactDestroyed == false) 		
   {
      %count ++;  
	%object = getObjectId( "MissionGroup\\RED\\LightFact" );
     	if(%count == 1) 		%one = %object;
	else if(%count == 2) 	%two = %object;
   }
   if($TankFactDestroyed == false) 		
   {
      %count ++;  
	%object = getObjectId( "MissionGroup\\RED\\TankFact" );
     	if(%count == 1) 		%one = %object;
	else if(%count == 2) 	%two = %object;
   }
   if($HeavyFactDestroyed == false) 		
   {
      %count ++;  
	%object = getObjectId( "MissionGroup\\RED\\HeavyFact" );
     	if(%count == 1) 		%one = %object;
	else if(%count == 2) 	%two = %object;
   }
   if($CommCenDestroyed == false) 		
   {
      %count ++;  
	%object = getObjectId( "MissionGroup\\RED\\CommCen" );
     	if(%count == 1) 		%one = %object;
	else if(%count == 2) 	%two = %object;
   }
   if($DIDestroyed == false) 		
   {
      %count ++;  
	%object = getObjectId( "MissionGroup\\RED\\DI" );
     	if(%count == 1) 		%one = %object;
	else if(%count == 2) 	%two = %object;
   }

   // Send Light Hercs to guard lowest priority structure...
   order( $Herc1, guard, %one );
   order( $Herc3, guard, %one );
   order( $Herc5, guard, %one );

   // Send Heavies Hercs to guard Highest priority structure...
   order( $Herc2, guard, %two );
   order( $Herc4, guard, %two );
   order( $Herc6, guard, %two );
} 

function guard_1_way()
{
   %count = 0;  

   if($G1Destroyed == false) 		
   {
      %count ++;  
	%object = getObjectId( "MissionGroup\\RED\\Turrets\\G1" );
      %one = %object;
   }
   if($G2Destroyed == false) 		
   {
      %count ++;  
	%object = getObjectId( "MissionGroup\\RED\\Turrets\\G2" );
      %one = %object;
   }
   if($G3Destroyed == false) 		
   {
      %count ++;  
	%object = getObjectId( "MissionGroup\\RED\\Turrets\\G3" );
      %one = %object;
   }
   if($G4Destroyed == false) 		
   {
      %count ++;  
	%object = getObjectId( "MissionGroup\\RED\\Turrets\\G4" );
      %one = %object;
   }
   if($LightFactDestroyed == false) 		
   {
      %count ++;  
	%object = getObjectId( "MissionGroup\\RED\\LightFact" );
      %one = %object;
   }
   if($TankFactDestroyed == false) 		
   {
      %count ++;  
	%object = getObjectId( "MissionGroup\\RED\\TankFact" );
      %one = %object;
   }
   if($HeavyFactDestroyed == false) 		
   {
      %count ++;  
	%object = getObjectId( "MissionGroup\\RED\\HeavyFact" );
      %one = %object;
   }
   if($CommCenDestroyed == false) 		
   {
      %count ++;  
	%object = getObjectId( "MissionGroup\\RED\\CommCen" );
      %one = %object;
   }
   if($DIDestroyed == false) 		
   {
      %count ++;  
	%object = getObjectId( "MissionGroup\\RED\\DI" );
      %one = %object;
   }

   // Send All Hercs to guard last structure...
   order( $Herc1, guard, %one );
   order( $Herc2, guard, %one );
   order( $Herc3, guard, %one );
   order( $Herc4, guard, %one );
   order( $Herc5, guard, %one );
   order( $Herc6, guard, %one );
} 

function TemsCloneVehicle(%globalVarName, %old, %x, %y)
{
   %clone = cloneVehicle(%old);

   if($BuildingsDestroyed < 3) 		setPilotId( %clone, 29 );
   else if($BuildingsDestroyed < 5) 	setPilotId( %clone, 30 );
   else						setPilotId( %clone, 31 );

   setTeam( %clone, *IDSTR_TEAM_RED );

   %z = getTerrainHeight( %x, %y ) + 50;

   setPosition(%clone, %x, %y, %z);

   schedule( "deleteObject(" @ %old @ ");", 10 );

   schedule( %globalVarName @ " = " @ %clone @ ";", 0);

   addToSet( "MissionGroup", %clone );

   %path = %globalVarName @ "path";
   schedule( "order( " @ %clone @ ", guard, " @ %path @ " );", 1 );
   schedule( "order( " @ %clone @ ", speed, high );", 2 );
}

function endgame()
{
   flushConsoleScheduler();
   missionendconditionmet();
}

