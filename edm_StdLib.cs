$timeToStart = 40;
$gameState = 0;
$allowAdds = 1;

function onMissionStart()
{
  schedule("startGame();", $timeToStart);
  say("everyone", 0, "Round will start in " @ $timeToStart @ " seconds");
  setHUDTimerAll($timeToStart, -1, "Time to start", 1);
}

function startGame()
{
  $allowAdds = 0;
  $gameState = 1;
  say("everyone", 0, "The round has now started, no more spawns until the next round.");
  countTeams();
}

function endMission()
{
  $gameState = 2;
  schedule("missionEndConditionMet();", 10);
}

function wilzuun::player::onAdd(%player)
{
  %player.startTime = getCurrentTime();
  %player.name = getName(%player);
  %nowDate = getDate();
  %nowTime = getTime();
  %player.IP = getConnection(%player);
  %outputString =  %nowDate @ ", " @ %nowTime @ " -- " @ %player.name @ " joined the game (" @ %player.IP @ ")";
  fileWrite("multiplayer\\serverlog.txt", append, %outputString);
  %player.alive = 0;
}

function wilzuun::player::OnRemove(%player)
{
  %player.name = getName(%player);
  %nowDate = getDate();
  %nowTime = getTime();
  %timeEnd = getCurrentTime();
  %timeIn = timeDifference( %timeEnd, %player.startTime);
  %outputString =  %nowDate @ ", " @ %nowTime @ " -- " @ %player.name @ " left the game (" @ %player.IP @ ") -- " @ %timeIn;

  fileWrite("multiplayer\\serverlog.txt", append, %outputString);
}

function wilzuun::vehicle::onAdd(%vehicleId)
{
  %player = playerManager::vehicleIdToPlayerNum(%vehicleId);

  if($allowAdds == 0)
  {
    say(%player, 0, "Sorry, round in progress.  Please wait until next round to join in");
    schedule("damageObject(" @ %vehicleId @ ", 50000);", 2);
  }
  else setHudTimer($timeToStart, -1, "Time to start", 1, %player);

  %player.alive = 1;
}

function wilzuun::vehicle::onDestroyed( %victimVeh, %destroyerVeh )
{
  %player = playerManager::vehicleIdToPlayerNum(%victimVeh);
  if($gameState == 2) return;

  %message = getFancyDeathMessage(getHUDName(%victimVeh), getHUDName(%destroyerVeh));
  if(%message != "")
  {
    say( 0, 0, %message);
  }
  %player.alive = 0;

  countTeams();
}

function setHudTimerAll(%time, %increment, %string, %channel)
{
  %count = playerManager::getPlayerCount();
  for(%i = 0; %i < %count; %i++)
  {
    %player = playerManager::getPlayerNum(%i);
    setHudTimer(%time, %increment, %string, %channel, %player);
  }
}

function countTeams()
{
  if($gameState != 1) return;

  %teamCount = 0;
  %yellow = 0;
  %blue = 0;
  %red = 0;
  %purple = 0;

  %count = playerManager::getPlayerCount();
  for(%i = 0; %i < %count; %i++)
  {
    %player = playerManager::getPlayerNum(%i);
    if(%player.alive == 1)
    {
      if(getTeam( %player ) == *IDSTR_TEAM_YELLOW)
      {
        if(%yellow == 0)
        {
          %yellow = 1;
          %teamCount = %teamCount + 1;
//        say(0, 0, "Yellow is alive");
        }
      } 
      if(getTeam( %player ) == *IDSTR_TEAM_BLUE)
      {
        if(%blue == 0)
        {
          %blue = 1;
          %teamCount = %teamCount + 2;
//        say(0, 0, "Blue is alive");
        }
      } 
      if(getTeam( %player ) == *IDSTR_TEAM_RED)
      {
        if(%red == 0)
        {
          %red = 1;
          %teamCount = %teamCount + 4;
//        say(0, 0, "Red is alive");
        }
      } 
      if(getTeam( %player ) == *IDSTR_TEAM_PURPLE)
      {
        if(%purple == 0)
        {
          %purple = 1;
          %teamCount = %teamCount + 8;
//        say(0, 0, "Purple is alive");
        }
      } 
    }
  }

  if(%teamCount == 1)
  {
    messageBox(0, "Yellow Team wins the round!");	
    endMission();
  }
  else if(%teamCount == 2)
  {
    messageBox(0, "Blue Team wins the round!");	
    endMission();
  }
  else if(%teamCount == 4)
  {
    messageBox(0, "Red Team wins the round!");	
    endMission();
  }
  else if(%teamCount == 8)
  {
    messageBox(0, "Purple Team wins the round!");	
    endMission();
  }

  else if(%teamCount == 0)
  {
    messageBox(0, "Everyone's dead, game is a draw");	
    endMission();
  }
}

// 1 = Yellow Team
// 2 = Blue Team
// 4 = Red Team
// 8 = Purple Team