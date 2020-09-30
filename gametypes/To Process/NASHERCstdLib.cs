// NASHERC Racing Standard Library (version 2.0)
// FILENAME: NASHERCstdLib.cs
// SCRIPTED BY: Com. Sentinal [M.I.B.]
//------------------------------------------------------------------------------

$missionName = "Daytona International Speedway"; //Track Name

//Different series allow different vehicles, weapons, components,
//mass limits, and tech mixing options.
//Keep the series name & abbreviation the same if you don't want 
//to change the current restrictions.
//If you want to change the series, merely change the series name
//and abbreviation to one of the following:
//
//1.  "Winston Cup" & "WC"        //Allows Emans, Goads, Talons, & Seekers
//2.  "Busch" & "BSH"             //Allows Knight's Apocs (turbines)
//3.  "Seeker" & "SK"             //Allows Seekers (turbines & rockets)
//4.  "Craftsman Tank" & "TK"     //Allows Bolos, Preds, & Disrupters (turbines)
//5.  "Goad/Talon" & "GT"         //Allows Goads & Talons
//
//The script will automatically recognize the series name you chose
//and will use it's restriction settings in the game.

$Series = "Winston Cup";  //Series name
$SeriesAbbreviation = "WC";  //Series Abbreviation

//The mission creator is used in the download message and in other areas of the script.
//The download message is used if you have any custom message you would 
//like to add when a player joins the game.

$missionCreator = "Com. Sentinal [M.I.B.]";
$downloadMessage = "You can download this & other missions made by " @ $missionCreator @ " in the \"MIB Map Packs\" on the MIB Website at www.starsiegemeninblack.cjb.net.";

//These are the coordinates where you respawn if you die during the race.
//You always start at the last checkpoint you crossed.
//Be sure to fill these out if you're making a custom track.
//------------------------------------------------------------------------------

//Checkpoint #1 coordinates
$checkpoint1x = 1415.19; //Checkpoint #1 x coordinate
$checkpoint1y = 1220.18;  //Checkpoint #1 y coordinate
$checkpoint1z = 76;  //Checkpoint #1 z coordinate
$checkpoint1rot = 80;  //Checkpoint #1 rotation coordinate (degrees)

//Checkpoint #2 coordinates
$checkpoint2x = 683.513;  //Checkpoint #2 x coordinate
$checkpoint2y = 250.762;  //Checkpoint #2 y coordinate
$checkpoint2z = 76;  //Checkpoint #2 z coordinate
$checkpoint2rot = 95;  //Checkpoint #2 rotation coordinate (degrees)

//Making a custom NASHERC RaceTrack
//------------------------------------------------------------------------------
//First, make sure you filled out all of the global variables in your
//map's script. You don't need to change anything in this file.
//
//The following is instructions on how to make a custom race track in the
//Starsiege Mission Editor:
//
//#1 - First you must have a track built that is fully enclosed and has
//a pit road. Make sure that the pit road is placed so that it doesn't
//become a short-cut. I recommend that your pit road contain 2-3 zen pads.
//Make sure that your Zen pad triggers have the script class "ZenAll".
//
//#2 - Next, you must place certain triggers around the track.
//VERY IMPORTANT: Make sure that all of the triggers in the map have
//"Is Sphere" disabled. IF they DO NOT have it disabled, then errors in 
//lap scoring WILL occur. The following is a list of the triggers that 
//you need and where they must be placed in order to work correctly. 
//The name of each trigger refers to the script class that must be used:
//
//    A. - The "finishline" trigger should be placed at the start/finish line.
//    B. - The "checkpoint1" trigger should be placed 1/3 of the way around
//         the track.
//    C. - The "checkpoint2" trigger should be placed 2/3 of the way around
//         the track.
//    D. - The "pitroad" trigger should be placed at the beginning of pit road.
//    E. - The "exitpitroad" trigger should be placed at the end of pit road.
//
//#3 - Next, you must make a folder in Missiongroup called "StartGates".
//Inside that folder, you must put in 2 bridges. Name one of them "StartGate"
//and name the other one "BackWall". Make sure that their script class is
//named this as well as their object names. Rotate the bridges so that they
//become walls. Place the one called "StartGate" right before the
//"finishline" trigger. Make sure that you allow enough space between the
//"startGate" wall and the "finishline" trigger. Then, lower the "StartGate"
//wall into the ground so that a herc can see over it. Next, place the
//"BackWall" wall about 100-150 meters behind the "StartGate" wall. Leave
//the "BackWall" wall high, so that nothing can see over it.
//
//#4 - Next, place 10 deathmatch drop points between the "StartGate" & the
//"Backwall".
//
//#5 - Finally, get the coordinates for the checkpoint triggers & write them
//in their proper location at the top of the script.
//
//Now, your custom NASHERC RaceTrack should work perfect IF you followed
//the directions above for making the track & filled in the global variables
//at the top of your map's script.
//------------------------------------------------------------------------------ 

function setDefaultMissionOptions()
{
   $server::TeamPlay = false;
   $server::AllowDeathmatch = true;
   $server::AllowTeamPlay = false;
 
   // what can the client choose for a team
   $server::AllowTeamRed = true;
   $server::AllowTeamBlue = true;
   $server::AllowTeamYellow = false;
   $server::AllowTeamPurple = false;
 
   // what can the server admin choose for available teams
   $server::disableTeamRed = false;
   $server::disableTeamBlue = false;
   $server::disableTeamYellow = true;
   $server::disableTeamPurple = true;

   $server::MaxPlayers = 10;
   $server::TimeLimit = 0;
   
   if($Series == "Winston Cup")
   {
      $server::AllowMixedTech = "False";
      $server::MassLimit = 0;
   }
   else if($Series == "Busch")
   {
      $server::AllowMixedTech = "True";
      $server::MassLimit = 40;
   }
   else if($Series == "Seeker")
   {
      $server::AllowMixedTech = "True";
      $server::MassLimit = 0;
   }
   else if($Series == "Craftsman Tank")
   {
      $server::AllowMixedTech = "True";
      $server::MassLimit = 45;
   }
   else if($Series == "Goad/Talon")
   {
      $server::AllowMixedTech = "False";
      $server::MassLimit = 0;
   }
   else
   {
      $server::AllowMixedTech = "True";
      $server::MassLimit = 0;
   }
} 

//Global Variables
//------------------------------------------------------------------------------

$healRate = 100;   
$ammoRate = 3;
$padWaitTime = 0;
$zenWaitTime = 0;
$ThirtySecondsLeft = false;
$racing = false;
$winner = false;
$countdown = false;
$leader1 = true;
$leader2 = true;
$leader3 = true;
$leader4 = true;
$leader5 = true;

function onMissionStart()
{
   $startgate = getObjectId("MissionGroup\\startgates\\startgate");
   $backwall = getObjectId("MissionGroup\\startgates\\backwall");
   $ExtraWall = getObjectId("MissionGroup\\startgates\\extrawall");
   NashercOnMissionStart();
} 

function NashercOnMissionStart()
{
   //Blank function
}

function onMissionLoad()
{
   setGameInfo("<F2>GAME TYPE:<F0>  NASHERC Racing\n\n<F2>TRACK:<F0>  " @ $missionName @ "\n\n<F2>SERIES:<F0>  " @ $Series @ "\n\nWelcome to NASHERC's " @ $missionName @ "! NASHERC's " @ $Series @ " Series (" @ $SeriesAbbreviation @ ") will be racing here today. When at least 2 players have joined the game, a 3-minute timer will begin counting down to the start of the race. During this time, players can setup their racing vehicles for optimum performance. After the race begins, players race around the track, trying to stay in front & destroy any other players they encounter. The first player to complete 5 laps is the winner. If a player's vehicle becomes damaged during the race, he can use the pits to repair/reload. " @ $downloadMessage @ " You can download the official NASHERC Racing skins at www.nashercracing.cjb.net.\n\n<F3>NASHERC Racing script created by Com. Sentinal [M.I.B.].<F0>"); 
   
   %count = playerManager::getPlayerCount();
   for(%i = 0; %i < %count; %i++)
   {
      %player = playerManager::getPlayerNum(%i);
      %player.points = 0;
      %player.lapsled = 0;
      %player.check1 = 0;
      %player.check2 = 0;
      %player.lock = false;
      %player.fastestlap = "None";
      %player.late = false;
      %player.winnings = 0;
      %player.leadalap = 0;
      %player.record = %player.temp;
   }
   
   NashercOnMissionLoad();
}  

function NashercOnMissionLoad()
{
   //Blank function
}

function player::onAdd(%player)
{
   say(%player, 0, "Welcome to NASHERC's " @ $missionName @ "! NASHERC's " @ $Series @ " Series (" @ $SeriesAbbreviation @ ") will be racing here today. Check the game info tab for the rules. " @ $downloadMessage @ " You can download the official NASHERC skins at www.nashercracing.cjb.net.");
   %player.points = 0;
   %player.check1 = 0;
   %player.check2 = 0;
   %player.lapsled = 0;
   %player.lock = false;
   %player.fastestlap = "None";
   %player.winnings = 0;
   %player.leadalap = 0;
   %player.record = 0;
   %player.temp = 0;
   %player.vehtype = "N/A";
   %player.delay = false;
   if($racing == true)
   {
      %player.late = true;
      messageBox(%player, "Welcome to NASHERC's " @ $missionName @ "! There is currently a race in progress. Please wait until the next race to play.");
   }
   else if($racing == false)
   {
      %player.late = false;
   }

   NashercPlayerOnAdd(%player);
} 

function NashercPlayerOnAdd(%player)
{
   //Blank function
}

function vehicle::onAdd(%vehicleId)
{
   %player = playerManager::vehicleIdToPlayerNum(%vehicleId);
   %player.vehtype = getVehicleName(%vehicleId);
   %player.delay = false;
   if($countdown == false)
   {
      if(playerManager::getPlayerCount() == 1)
      {
         messageBox(%player, "At least 2 players must be in the server before the countdown to the race will begin. Please wait until another player joins the server to race.");
      }
      else if(playerManager::getPlayerCount() >= 2)
      {
         $countdown = true;
         $countdownStartTime = getCurrentTime();
         countdown();
         say("Everybody", 10, "3 minutes until the race begins...");
         %txt1 = "\"2 minutes until the race begins...\"";
         %txt2 = "\"1 minute until the race begins...\"";
         %txt3 = "\"30 seconds until the race begins...\"";
         %txt4 = "\"15 seconds until the race begins...\"";
         %txt5 = "\"<jc>10\"";
         %txt6 = "\"<jc>9\"";
         %txt7 = "\"<jc>8\"";
         %txt8 = "\"<jc>7\"";
         %txt9 = "\"<jc>6\"";
         %txt10 = "\"<jc>5\"";
         %txt11 = "\"<jc>4\"";
         %txt12 = "\"<jc>3\"";
         %txt13 = "\"<jc>2\"";
         %txt14 = "\"<jc>1\"";
         schedule("say(0, 10, " @ %txt1 @ ");", 60);
         schedule("say(0, 11, " @ %txt2 @ ");", 120);
         schedule("$ThirtySecondsLeft = true;", 150);
         schedule("say(0, 12, " @ %txt3 @ ");", 150);
         schedule("say(0, 13, " @ %txt4 @ ");", 165);
         schedule("say(0, 14, " @ %txt5 @ ");", 170);
         schedule("say(0, 15, " @ %txt6 @ ");", 171);
         schedule("say(0, 16, " @ %txt7 @ ");", 172);
         schedule("say(0, 17, " @ %txt8 @ ");", 173);
         schedule("say(0, 18, " @ %txt9 @ ");", 174);
         schedule("say(0, 19, " @ %txt10 @ ");", 175);
         schedule("say(0, 20, " @ %txt11 @ ");", 176);
         schedule("say(0, 21, " @ %txt12 @ ");", 177);
         schedule("say(0, 22, " @ %txt13 @ ");", 178);
         schedule("say(0, 23, " @ %txt14 @ ");", 179);
         schedule("startrace();", 180);
      }
   }

   if(($racing == false)&&($countdown == true))
   {
      %player.late = false;
      $timeLeft = 180 - (getCurrentTime() -  $countdownStartTime);
      if($ThirtySecondsLeft == false)
      {
         setHudTimer($timeLeft, -1, "Race begins in:", 1, %player);
         messagebox(%player, "The race has not started yet, please wait until the timer reaches zero to race.");
      }
      else if($ThirtySecondsLeft == true)
      {
         setHudTimer($timeLeft, -1, "Race begins in:", 1, %player);
         say(%player, %player, "Get ready to race!");
      }
   }
   else if($racing == true)
   {
      if(%player.late == true)
      {
         healObject(%vehicleId, -50000); 
         messageBox(%player, "You cannot enter the game while a race is in progress. Please wait until the next race to join.");
      }
      else if(%player.late == false)
      {
         if((%player.check1 == 1)&&(%player.check2 == 0))
         {
            setPosition(%vehicleId, $checkpoint1x, $checkpoint1y, $checkpoint1z, $checkpoint1rot);
            say(%player, %player, "Hurry up " @ getName(%player) @ "! You can still catch up with the pack!");
         }
         else if((%player.check1 == 1)&&(%player.check2 == 1))
         {
            setPosition(%vehicleId, $checkpoint2x, $checkpoint2y, $checkpoint2z, $checkpoint2rot);
            say(%player, %player, "Hurry up " @ getName(%player) @ "! You can still catch up with the pack!");  
         }
         else
         {
            say(%player, %player, "Hurry up " @ getName(%player) @ "! You can still catch up with the pack!");
         }
      }
   }
   
   NashercVehicleOnAdd(%vehicleId);
} 

function NashercVehicleOnAdd(%vehicleId)
{
   //Blank function
}

function player::onRemove(%player)
{
   if($countdown == true)
   {
      if(($racing == true)&&(%player.late == false))
      {
         %player.late = true;
         %playersRacing = 0;
         %count = playerManager::getPlayerCount();
         for(%i = 0; %i < %count; %i++)
         {
            %player = playerManager::getPlayerNum(%i);
            if(%player.late == false)
            {
               %playersRacing++;
            }
         }
         if(%playersRacing == 1)
         {
            %count = playerManager::getPlayerCount();
            for(%i = 0; %i < %count; %i++)
            {
               %player = playerManager::getPlayerNum(%i);
               if(%player.late == false)
               {
                  %winner = %player;
               }
            }
            messageBox(0, "All other players have forfeited the race. " @ getName(%winner) @ " is the winner!");
            schedule("missionEndConditionMet();", 5);
         }
         else if(%playersRacing < 1)
         {
            messageBox(0, "All of the players that were racing have left the game. The race will now restart.");
            schedule("missionEndConditionMet();", 5);
         }
      }
      else if(playerManager::getPlayerCount() == 2)
      {
         messageBox(0, "All other players have left the server. The race will now restart.");
         schedule("missionEndConditionMet();", 5);
      }
      else if(playerManager::getPlayerCount() <= 1)
      {
         schedule("missionEndConditionMet();", 1);
      }
   }
   NashercPlayerOnRemove(%player);
}

function NashercPlayerOnRemove(%player)
{
   //Blank function
}

function vehicle::onAttacked(%attacked, %attacker)
{
   %player = playerManager::vehicleIdToPlayerNum(%attacker);
   if($racing == false)
   {
      healObject(%attacked, 50000);
      if((%player.delay == false)&&(%attacked != %attacker))
      {
         %player.delay = true;
         say(%player, %player, "You cannot attack a player until after the race begins!");
         schedule("resetPlayerDelay(" @ %player @ ");", 2);
      }
   }
   NashercVehicleOnAttacked(%attacked, %attacker);
}

function NashercVehicleOnAttacked(%attacked, %attacker)
{
   //Blank function
}

function resetPlayerDelay(%player)
{
   %player.delay = false;
}

function vehicle::onDestroyed(%destroyed, %destroyer) 
{ 
   %player = playerManager::vehicleIdToPlayerNum(%destroyed);
   %player2 = playerManager::vehicleIdToPlayerNum(%destroyer);
   if(($racing == true)&&(%player.late == false))
   {  
      if(%destroyed != %destroyer)
      {
         %random = randomInt(1, 3);
         if(%random == 1)
         {
            say("Everybody", 1, getName(%player2) @ " slammed " @ getName(%player) @ " into the wall!");
         }
         else if(%random == 2)
         {
            say("Everybody", 1, getName(%player) @ " was crashed out of the race by " @ getName(%player2) @ "!");
         }
         else if(%random == 3)
         {
            say("Everybody", 1, getName(%player) @ " has crashed!");
         }
      }
      else if(%destroyed == %destroyer)
      {
         say("Everybody", 1, getName(%player) @ " has crashed!");
      }
   }
   else if($racing == false)
   {
      say("Everybody", 1, getName(%player) @ " went back to the garage to work on his racing setup.");
   }
   NashercVehicleOnDestroyed(%destroyed, %destroyer);
}

function NashercVehicleOnDestroyed(%destroyed, %destroyer)
{
   //Blank function
}

function checkpoint1::trigger::OnEnter(%trigger, %vehicleId)
{
   %player = playerManager::vehicleIdToPlayerNum(%vehicleId);
   %player.check1 = 1;
   say(%player, %player, "Checkpoint 1!");
   
   NashercCheckpoint1OnArrived(%trigger, %vehicleId);
} 

function NashercCheckpoint1OnArrived(%trigger, %vehicleId)
{
   //Blank function
}

function checkpoint2::trigger::OnEnter(%trigger, %vehicleId)
{
   %player = playerManager::vehicleIdToPlayerNum(%vehicleId);
   if(%player.check1 == 1)
   {
      %player.check2 = 1;
      say(%player, %player, "Checkpoint 2!");
   }

   NashercCheckpoint2OnArrived(%trigger, %vehicleId);
} 

function NashercCheckpoint2OnArrived(%trigger, %vehicleId)
{
   //Blank function
}

function finishline::trigger::OnEnter(%trigger, %vehicleId)
{  
   %player = playerManager::vehicleIdToPlayerNum(%vehicleId);
   if((%player.check1 == 0)&&(%player.check2 == 0)&&(%player.points == 0))
   {
      %player.check1 = 0;
      %player.check2 = 0;
      %player.time1 = getCurrentTime();
      say(%player, %player, "LAP 1!");
      if($leader1==true)
      {
         $leader1 = false;
         say("Everybody", 1, getName(%player) @ " takes the lead on lap 1!");
      }
   }
   else if((%player.check1 == 1)&&(%player.check2 == 1)&&(%player.points == 0))
   {
      %player.check1 = 0;
      %player.check2 = 0;
      %player.laptime1 = timeDifference(getCurrentTime(), %player.time1);
      %player.time2 = getCurrentTime();
      %player.points++;
      say(%player, %player, "LAPTIME: " @ %player.laptime1);
      say(%player, %player, "LAP 2!");
      %player.fastlap = %player.laptime1;
      %player.fastestlap = "Lap 1 - " @ %player.laptime1;
      if($leader2 == true)
      {  
         $leader2 = false;
         %player.lapsled++;
         say("Everybody", 1, "The leader, " @ getName(%player) @ ", is currently on lap 2.");
         if(%player.lapsled==1)
         {
            %player.leadalap = 4;
         }
      }
   }
   else if((%player.check1 == 1)&&(%player.check2 == 1)&&(%player.points == 1))
   {
      %player.check1 = 0;
      %player.check2 = 0;
      %player.laptime2 = timeDifference(getCurrentTime(), %player.time2);
      %player.time3 = getCurrentTime();
      %player.points++;
      say(%player, %player, "LAPTIME: " @ %player.laptime2);
      say(%player, %player, "LAP 3!");
      if($leader3 == true)
      {  
         $leader3 = false;
         %player.lapsled++;
         say("Everybody", 1, "The leader, " @ getName(%player) @ ", is currently on lap 3.");
         if(%player.lapsled == 1)
         {
            %player.leadalap = 4;
         }
      }
      if(%player.laptime2 < %player.fastlap)
      {
         %player.fastlap = %player.laptime2;
         %player.fastestlap = "Lap 2 - " @ %player.laptime2;
      }
   }
   else if((%player.check1 == 1)&&(%player.check2 == 1)&&(%player.points == 2))
   {
      %player.check1 = 0;
      %player.check2 = 0;
      %player.laptime3 = timeDifference(getCurrentTime(), %player.time3);
      %player.time4 = getCurrentTime();
      %player.points++;
      say(%player, %player, "LAPTIME: " @ %player.laptime3);
      say(%player, %player, "LAP 4!");
      if($leader4 == true)
      {  
         $leader4 = false;
         %player.lapsled++;
         say("Everybody", 1, "The leader, " @ getName(%player) @ ", is currently on lap 4.");
         if(%player.lapsled == 1)
         {
            %player.leadalap = 4;
         }
      }
      if(%player.laptime3 < %player.fastlap)
      {
         %player.fastlap = %player.laptime3;
         %player.fastestlap = "Lap 3 - " @ %player.laptime3;
      }
   }
   else if((%player.check1 == 1)&&(%player.check2 == 1)&&(%player.points == 3))
   {
      %player.check1 = 0;
      %player.check2 = 0;
      %player.laptime4 = timeDifference(getCurrentTime(), %player.time4);
      %player.time5 = getCurrentTime();
      %player.points++;
      say(%player, %player, "LAPTIME: " @ %player.laptime4);
      say(%player, %player, "FINAL LAP!");
      if($leader5 == true)
      {  
         $leader5 = false;
         %player.lapsled++;
         say("Everybody", 1, "The leader, " @ getName(%player) @ ", is on the final lap!");
         if(%player.lapsled == 1)
         {
            %player.leadalap = 4;
         }
      }
      if(%player.laptime4 < %player.fastlap)
      {
         %player.fastlap = %player.laptime4;
         %player.fastestlap = "Lap 4 - " @ %player.laptime4;
      }
   }
   else if((%player.check1 == 1)&&(%player.check2 == 1)&&(%player.points == 4)&&($winner == false))
   {
      $winner = true;
      %player.winnings = 10;
      %player.check1 = 0;
      %player.check2 = 0;
      %player.laptime5 = timeDifference(getCurrentTime(), %player.time5);
      %player.points++;
      %player.lapsled++;
      if(%player.lapsled == 1)
      {
         %player.leadalap = 4;
      }
      say(%player, %player, "LAPTIME: " @ %player.laptime5);
      if(%player.laptime5 < %player.fastlap)
      {
         %player.fastlap = %player.laptime5;
         %player.fastestlap = "Lap 5 - " @ %player.laptime5;
      }
      say("Everybody", 1, getName(%player) @ " has won the race!");
      setFlybyCamera(%vehicleId, -15, 0, 15);
      messagebox("Everybody", getName(%player) @ " has won the race!");
      %count = playerManager::getPlayerCount();
      for(%i = 0; %i < %count; %i++)
      {
         %playerNumber = playerManager::getPlayerNum(%i);
         %playerNumber.temp = getStandings(%playerNumber);
      }
      schedule("missionEndConditionMet();",10);

      NashercOnFinishRace();
   }

   NashercFinishLineOnArrived(%trigger, %vehicleId);
} 

function NashercFinishLineOnArrived(%trigger, %vehicleId)
{
   //Blank function
}

function NashercOnFinishRace()
{
   //Blank function
}

function pitroad::trigger::OnEnter(%trigger, %vehicleId)
{
   %pitter = playerManager::vehicleIdToPlayerNum(%vehicleId);
   %pitter.lock = true;
   %pitter.pit = getCurrentTime();
   say("Everybody", 1, getName(%pitter) @ " is pitting.");
   say(%pitter, %pitter, "Entering pit road.");
   
   NashercPitRoadOnEnter(%trigger, %vehicleId);
}

function NashercPitRoadOnEnter(%trigger, %vehicleId)
{
   //Blank function
}

function exitpitroad::trigger::OnEnter(%trigger, %vehicleId)
{
   %pitter = playerManager::vehicleIdToPlayerNum(%vehicleId);
   if(%pitter.lock == true)
   {
      %pitter.pittime = timeDifference(getCurrentTime(), %pitter.pit);
      say("Everybody", 1, getName(%pitter) @ "'s pitstop lasted " @ %pitter.pittime @ ". Now he's back out on the track!");
      say(%pitter, %pitter, "Leaving pit road.");
      %pitter.lock = false; 
   }

   NashercPitRoadOnLeave(%trigger, %vehicleId);
}

function NashercPitRoadOnLeave(%trigger, %vehicleId)
{
   //Blank function
}

function startRace()
{
   $racing = true;
   setPosition($startgate, 900, 900, -500);
   setPosition($ExtraWall, 900, 900, -500);  //this is used only if you need an extra wall to move at the start of the race
   schedule("setPosition(" @ $backwall @ ", 900, 900, -500);", 7);
   say("Everybody", 24, "GREEN FLAG! GO GO GO!", "sfx_fog.WAV");

   NashercOnStartRace();
}

function NashercOnStartRace()
{
   //Blank function
}

function countdown()
{
   %count = playerManager::getPlayerCount();
   for(%i = 0; %i < %count; %i++)
   {
      %everybody = playerManager::getPlayerNum(%i);
      setHudTimer(180, -1, "Race begins in:", 1, %everybody);
   }
   
   NashercOnStartCountdown();
} 

function NashercOnStartCountdown()
{
   //Blank function
}

function onMissionEnd()
{
   flushConsoleScheduler();
}

function getPlayerScore(%player)
{
   return(%player.points);
}

function getLapsLed(%player)
{
   return(%player.lapsled);
}

function getFastestLap(%player)
{
   return(%player.fastestlap);
}

function getStandings(%player)
{
   return((getKills(%player)) + (%player.lapsled) + (%player.leadalap) + (%player.points) + (%player.winnings) + (%player.record));
}

function getVehicle(%player)
{  
   return(%player.vehtype);
}

function initScoreBoard()
{
   deleteVariables("$ScoreBoard::PlayerColumn*");
   deleteVariables("$ScoreBoard::TeamColumn*");

   if($server::TeamPlay == "True")	
   {
	   // Player ScoreBoard column headings
	   $ScoreBoard::PlayerColumnHeader1 = *IDMULT_SCORE_TEAM;
	   $ScoreBoard::PlayerColumnHeader2 = *IDMULT_SCORE_SQUAD;
	   $ScoreBoard::PlayerColumnHeader3 = "Laps Completed";
	   $ScoreBoard::PlayerColumnHeader4 = *IDMULT_SCORE_KILLS;
	   $ScoreBoard::PlayerColumnHeader5 = *IDMULT_SCORE_DEATHS;

	   // Player ScoreBoard column functions
	   $ScoreBoard::PlayerColumnFunction1 = "getTeam";
	   $ScoreBoard::PlayerColumnFunction2 = "getSquad";
	   $ScoreBoard::PlayerColumnFunction3 = "getPlayerScore";
	   $ScoreBoard::PlayerColumnFunction4 = "getKills";
	   $ScoreBoard::PlayerColumnFunction5 = "getDeaths";
   }
   else
   {
       // Player ScoreBoard column headings
	   $ScoreBoard::PlayerColumnHeader1 = *IDMULT_SCORE_SQUAD;
         $ScoreBoard::PlayerColumnHeader2 = "Laps Completed";
	   $ScoreBoard::PlayerColumnHeader3 = "Laps Led";
         $ScoreBoard::PlayerColumnHeader4 = "Fastest Lap";
         $ScoreBoard::PlayerColumnHeader5 = "Point Standings";
	   $ScoreBoard::PlayerColumnHeader6 = *IDMULT_SCORE_KILLS;
	   $ScoreBoard::PlayerColumnHeader7 = *IDMULT_SCORE_DEATHS;
         $ScoreBoard::PlayerColumnHeader8 = "Vehicle";
         
	   // Player ScoreBoard column functions
	   $ScoreBoard::PlayerColumnFunction1 = "getSquad";
         $ScoreBoard::PlayerColumnFunction2 = "getPlayerScore";
	   $ScoreBoard::PlayerColumnFunction3 = "getLapsLed";
         $ScoreBoard::PlayerColumnFunction4 = "getFastestLap";
         $ScoreBoard::PlayerColumnFunction5 = "getStandings";
	   $ScoreBoard::PlayerColumnFunction6 = "getKills";
	   $ScoreBoard::PlayerColumnFunction7 = "getDeaths";
         $ScoreBoard::PlayerColumnFunction8 = "getVehicle";
   }

   // Team ScoreBoard column headings
   $ScoreBoard::TeamColumnHeader1 = "Laps Completed";
   $ScoreBoard::TeamColumnHeader2 = *IDMULT_SCORE_PLAYERS;
   $ScoreBoard::TeamColumnHeader3 = *IDMULT_SCORE_KILLS;
   $ScoreBoard::TeamColumnHeader4 = *IDMULT_SCORE_DEATHS;

   // Team ScoreBoard column functions
   $ScoreBoard::TeamColumnFunction1 = "getTeamScore";
   $ScoreBoard::TeamColumnFunction2 = "getNumberOfPlayersOnTeam";
   $ScoreBoard::TeamColumnFunction3 = "getTeamKills";
   $ScoreBoard::TeamColumnFunction4 = "getTeamDeaths";

   // tell server to process all the scoreboard definitions defined above
   serverInitScoreBoard();
}

//Series Restrictions
//----------------------------------------------------------------------
function setDefaultMissionItems()
{  
   //Winston Cup Series Restrictions
   if($Series == "Winston Cup")
   {
      allowVehicle(  all, FALSE  );
      allowVehicle(  13, TRUE  );
      allowVehicle(  20, TRUE  );
      allowVehicle(  21, TRUE  );
      allowVehicle(  30, TRUE  );   
   
      allowWeapon(  all, TRUE  );
      allowWeapon(  106, FALSE  );
      allowWeapon(  107, FALSE  );
      allowWeapon(  109, FALSE  );
      allowWeapon(  110, FALSE  );
      allowWeapon(  112, FALSE  );
      allowWeapon(  113, FALSE  );
      allowWeapon(  115, FALSE  );
      allowWeapon(  120, FALSE  );
      allowWeapon(  121, FALSE  );
      allowWeapon(  124, FALSE  );
      allowWeapon(  125, FALSE  );
      allowWeapon(  126, FALSE  );
      allowWeapon(  127, FALSE  );
      allowWeapon(  128, FALSE  );
      allowWeapon(  129, FALSE  );
      allowWeapon(  130, FALSE  );
      allowWeapon(  131, FALSE  );
      allowWeapon(  132, FALSE  );
      allowWeapon(  133, FALSE  );
      allowWeapon(  134, FALSE  );
      allowWeapon(  135, FALSE  );
      allowWeapon(  136, FALSE  );
      allowWeapon(  142, FALSE  );
      allowWeapon(  147, FALSE  );
      allowWeapon(  150, FALSE  );

      allowComponent(  all, TRUE  );
      allowComponent(  102, FALSE  );
      allowComponent(  106, FALSE  );
      allowComponent(  107, FALSE  );
      allowComponent(  108, FALSE  );
      allowComponent(  109, FALSE  );
      allowComponent(  110, FALSE  );
      allowComponent(  111, FALSE  );
      allowComponent(  112, FALSE  );
      allowComponent(  113, FALSE  );
      allowComponent(  114, FALSE  );
      allowComponent(  115, FALSE  );
      allowComponent(  129, FALSE  );
      allowComponent(  130, FALSE  );
      allowComponent(  131, FALSE  );
      allowComponent(  133, FALSE  );
      allowComponent(  134, FALSE  );
      allowComponent(  135, FALSE  );
      allowComponent(  136, FALSE  );
      allowComponent(  137, FALSE  );
      allowComponent(  138, FALSE  );
      allowComponent(  139, FALSE  );
      allowComponent(  140, FALSE  );
      allowComponent(  141, FALSE  );
      allowComponent(  142, FALSE  );
      allowComponent(  143, FALSE  );
      allowComponent(  225, FALSE  );
      allowComponent(  227, FALSE  );
      allowComponent(  228, FALSE  );
      allowComponent(  230, FALSE  );
      allowComponent(  305, FALSE  );
      allowComponent(  307, FALSE  );
      allowComponent(  329, FALSE  );
      allowComponent(  330, FALSE  );
      allowComponent(  331, FALSE  );
      allowComponent(  332, FALSE  );
      allowComponent(  333, FALSE  );
      allowComponent(  426, FALSE  );
      allowComponent(  427, FALSE  );
      allowComponent(  428, FALSE  );
      allowComponent(  429, FALSE  );
      allowComponent(  430, FALSE  );
      allowComponent(  431, FALSE  );
      allowComponent(  433, FALSE  );
      allowComponent(  434, FALSE  );
      allowComponent(  809, FALSE  );
      allowComponent(  810, FALSE  );
      allowComponent(  811, FALSE  );
      allowComponent(  812, FALSE  );
      allowComponent(  813, FALSE  );
      allowComponent(  830, FALSE  );
      allowComponent(  860, FALSE  );
      allowComponent(  875, FALSE  );
      allowComponent(  880, FALSE  );
      allowComponent(  885, FALSE  );
      allowComponent(  900, FALSE  );
      allowComponent(  910, FALSE  );
      allowComponent(  912, FALSE  );
   }
   //Busch Series Restrictions
   else if($Series == "Busch")
   {
      allowVehicle( all, FALSE );
      allowVehicle( 10, TRUE );
   
      allowWeapon( all, TRUE  );
      allowWeapon( 106, FALSE  );
      allowWeapon( 107, FALSE  );
      allowWeapon( 108, FALSE  );
      allowWeapon( 109, FALSE  );
      allowWeapon( 110, FALSE  );
      allowWeapon( 112, FALSE  );
      allowWeapon( 113, FALSE  );
      allowWeapon( 119, FALSE  );
      allowWeapon( 120, FALSE  );
      allowWeapon( 121, FALSE  );
      allowWeapon( 124, FALSE  );
      allowWeapon( 125, FALSE  );
      allowWeapon( 126, FALSE  );
      allowWeapon( 127, FALSE  );
      allowWeapon( 128, FALSE  );
      allowWeapon( 129, FALSE  );
      allowWeapon( 130, FALSE  );
      allowWeapon( 131, FALSE  );
      allowWeapon( 132, FALSE  );
      allowWeapon( 133, FALSE  );
      allowWeapon( 134, FALSE  );
      allowWeapon( 135, FALSE  );
      allowWeapon( 136, FALSE  );
      allowWeapon( 142, FALSE  );
      allowWeapon( 147, FALSE  );
      allowWeapon( 150, FALSE  );

      allowComponent( all, TRUE  );
      allowComponent( 100, FALSE  );
      allowComponent( 101, FALSE  );
      allowComponent( 102, FALSE  );
      allowComponent( 103, FALSE  );
      allowComponent( 104, FALSE  );
      allowComponent( 105, FALSE  );
      allowComponent( 111, FALSE  );
      allowComponent( 112, FALSE  );
      allowComponent( 113, FALSE  );
      allowComponent( 114, FALSE  );
      allowComponent( 115, FALSE  );
      allowComponent( 128, FALSE  );
      allowComponent( 129, FALSE  );
      allowComponent( 130, FALSE  );
      allowComponent( 131, FALSE  );
      allowComponent( 132, FALSE  );
      allowComponent( 133, FALSE  );
      allowComponent( 134, FALSE  );
      allowComponent( 135, FALSE  );
      allowComponent( 136, FALSE  );
      allowComponent( 137, FALSE  );
      allowComponent( 138, FALSE  );
      allowComponent( 139, FALSE  );
      allowComponent( 140, FALSE  );
      allowComponent( 141, FALSE  );
      allowComponent( 142, FALSE  );
      allowComponent( 143, FALSE  );
      allowComponent( 305, FALSE  );
      allowComponent( 307, FALSE  );
      allowComponent( 331, FALSE  );
      allowComponent( 333, FALSE  );
      allowComponent( 800, FALSE  );
      allowComponent( 801, FALSE  );
      allowComponent( 802, FALSE  );
      allowComponent( 810, FALSE  );
      allowComponent( 811, FALSE  );
      allowComponent( 812, FALSE  );
      allowComponent( 813, FALSE  );
      allowComponent( 825, FALSE  );
      allowComponent( 860, FALSE  );
      allowComponent( 875, FALSE  );
      allowComponent( 880, FALSE  );
      allowComponent( 900, FALSE  );
      allowComponent( 910, FALSE  );
      allowComponent( 912, FALSE  );
   }
   //Seeker Series Restrictions
   else if($Series == "Seeker")
   {
      allowVehicle(  all, FALSE  );
      allowVehicle(  20, TRUE  );

      allowWeapon(  all, FALSE  );
      allowWeapon(  101, TRUE  );
      allowWeapon(  105, TRUE  );
      allowWeapon(  116, TRUE  );
      allowWeapon(  118, TRUE  );     
      allowWeapon(  103, TRUE  );     
      allowWeapon(  114, TRUE  );
      allowWeapon(  104, TRUE  );
      
      allowComponent(  all, FALSE  );
      allowComponent(  125, TRUE  );
      allowComponent(  226, TRUE  );
      allowComponent(  300, TRUE  );
      allowComponent(  301, TRUE  );
      allowComponent(  302, TRUE  );
      allowComponent(  303, TRUE  );   
      allowComponent(  431, TRUE  );
      allowComponent(  805, TRUE  );
      allowComponent(  806, TRUE  );
      allowComponent(  807, TRUE  );
      allowComponent(  820, TRUE  );
      allowComponent(  830, TRUE  );
      allowComponent(  840, TRUE  );
      allowComponent(  845, TRUE  );
      allowComponent(  850, TRUE  );
      allowComponent(  865, TRUE  );
      allowComponent(  870, TRUE  );
      allowComponent(  880, TRUE  );
      allowComponent(  885, TRUE  );
      allowComponent(  890, TRUE  );
      allowComponent(  914, TRUE  );
      allowComponent(  225, TRUE  );
      allowComponent(  200, TRUE  );
      allowComponent(  201, TRUE  );
      allowComponent(  202, TRUE  );
      allowComponent(  203, TRUE  );
      allowComponent(  204, TRUE  );
      allowComponent(  205, TRUE  );
      allowComponent(  226, TRUE  );
      allowComponent(  227, TRUE  );
      allowComponent(  228, TRUE  );
      allowComponent(  229, TRUE  );
      allowComponent(  230, TRUE  );
      allowComponent(  400, TRUE  );
      allowComponent(  401, TRUE  );
      allowComponent(  408, TRUE  );
      allowComponent(  409, TRUE  );
      allowComponent(  410, TRUE  );
      allowComponent(  411, TRUE  );
      allowComponent(  412, TRUE  );
      allowComponent(  413, TRUE  );
      allowComponent(  414, TRUE  );
      allowComponent(  426, TRUE  );
      allowComponent(  427, TRUE  );
      allowComponent(  428, TRUE  );
      allowComponent(  429, TRUE  );
      allowComponent(  430, TRUE  );
      allowComponent(  432, TRUE  );
      allowComponent(  433, TRUE  );
      allowComponent(  434, TRUE  );
      allowComponent(  128, TRUE  );
      allowComponent(  129, TRUE  );
      allowComponent(  926, TRUE  );
      allowComponent(  927, TRUE  );
      allowComponent(  928, TRUE  );
      allowComponent(  929, TRUE  );
      allowComponent(  930, TRUE  );
   }
   //Craftsman Tank Series Restrictions
   else if($Series == "Craftsman Tank")
   {
      allowVehicle(  all, FALSE  );
      allowVehicle(  17, TRUE  );
      allowVehicle(  25, TRUE  );
      allowVehicle(  41, TRUE  );
      
      allowWeapon(  all, FALSE  );
      allowWeapon(  3, FALSE  );
      allowWeapon(  101, TRUE  );
      allowWeapon(  102, TRUE  );
      allowWeapon(  103, TRUE  );
      allowWeapon(  104, TRUE  );
      allowWeapon(  105, TRUE  );
      allowWeapon(  111, TRUE  );
      allowWeapon(  114, TRUE  );
      allowWeapon(  115, TRUE  );
      allowWeapon(  116, TRUE  );
      allowWeapon(  117, TRUE  );
      allowWeapon(  118, TRUE  );
      allowWeapon(  121, TRUE  );
      allowWeapon(  111, TRUE  );
            
      allowComponent(  all, TRUE  );
      allowComponent(  809, FALSE );
      allowComponent(  810, FALSE );
      allowComponent(  811, FALSE );
      allowComponent(  812, FALSE );
      allowComponent(  813, FALSE );
      allowComponent(  820, FALSE );
      allowComponent(  840, FALSE );
      allowComponent(  845, FALSE );
      allowComponent(  850, FALSE );
      allowComponent(  860, FALSE );
      allowComponent(  875, FALSE );
      allowComponent(  880, FALSE );
      allowComponent(  900, FALSE );
      allowComponent(  910, FALSE );
   }
   //Goad/Talon Series Restrictions
   else if($Series == "Goad/Talon")
   {
      allowVehicle( all, FALSE  );
      allowVehicle( 13, TRUE  );
      allowVehicle( 21, TRUE  );
     
      allowWeapon( all, TRUE  );
      allowWeapon( 106, FALSE  );
      allowWeapon( 107, FALSE  );
      allowWeapon( 108, FALSE  );
      allowWeapon( 109, FALSE  );
      allowWeapon( 110, FALSE  );
      allowWeapon( 112, FALSE  );
      allowWeapon( 113, FALSE  );
      allowWeapon( 115, FALSE  );
      allowWeapon( 119, FALSE  );
      allowWeapon( 120, FALSE  );
      allowWeapon( 121, FALSE  );
      allowWeapon( 124, FALSE  );
      allowWeapon( 125, FALSE  );
      allowWeapon( 126, FALSE  );
      allowWeapon( 127, FALSE  );
      allowWeapon( 128, FALSE  );
      allowWeapon( 129, FALSE  );
      allowWeapon( 130, FALSE  );
      allowWeapon( 131, FALSE  );
      allowWeapon( 132, FALSE  );
      allowWeapon( 133, FALSE  );
      allowWeapon( 134, FALSE  );
      allowWeapon( 135, FALSE  );
      allowWeapon( 136, FALSE  );
      allowWeapon( 142, FALSE  );
      allowWeapon( 147, FALSE  );
      allowWeapon( 150, FALSE  );
   
      allowComponent( all, TRUE  );
      allowComponent( 102, FALSE  );
      allowComponent( 103, FALSE  );
      allowComponent( 104, FALSE  );
      allowComponent( 105, FALSE  );
      allowComponent( 106, FALSE  );
      allowComponent( 107, FALSE  );
      allowComponent( 108, FALSE  );
      allowComponent( 109, FALSE  );
      allowComponent( 110, FALSE  );
      allowComponent( 111, FALSE  );
      allowComponent( 112, FALSE  );
      allowComponent( 113, FALSE  );
      allowComponent( 114, FALSE  );
      allowComponent( 115, FALSE  );
      allowComponent( 128, FALSE  );
      allowComponent( 129, FALSE  );
      allowComponent( 130, FALSE  );
      allowComponent( 131, FALSE  );
      allowComponent( 133, FALSE  );
      allowComponent( 134, FALSE  );
      allowComponent( 135, FALSE  );
      allowComponent( 136, FALSE  );
      allowComponent( 137, FALSE  );
      allowComponent( 138, FALSE  );
      allowComponent( 139, FALSE  );
      allowComponent( 140, FALSE  );
      allowComponent( 141, FALSE  );
      allowComponent( 142, FALSE  );
      allowComponent( 143, FALSE  );
      allowComponent( 225, FALSE  );
      allowComponent( 227, FALSE  );
      allowComponent( 228, FALSE  );
      allowComponent( 230, FALSE  );
      allowComponent( 305, FALSE  );
      allowComponent( 307, FALSE  );
      allowComponent( 329, FALSE  );
      allowComponent( 330, FALSE  );
      allowComponent( 331, FALSE  );
      allowComponent( 332, FALSE  );
      allowComponent( 333, FALSE  );
      allowComponent( 426, FALSE  );
      allowComponent( 427, FALSE  );
      allowComponent( 428, FALSE  );
      allowComponent( 429, FALSE  );
      allowComponent( 430, FALSE  );
      allowComponent( 431, FALSE  );
      allowComponent( 433, FALSE  );
      allowComponent( 434, FALSE  );
      allowComponent( 809, FALSE  );
      allowComponent( 810, FALSE  );
      allowComponent( 811, FALSE  );
      allowComponent( 812, FALSE  );
      allowComponent( 813, FALSE  );
      allowComponent( 831, FALSE  );
      allowComponent( 860, FALSE  );
      allowComponent( 875, FALSE  );
      allowComponent( 880, FALSE  );
      allowComponent( 885, FALSE  );
      allowComponent( 900, FALSE  );
      allowComponent( 910, FALSE  );
      allowComponent( 912, FALSE  );
   }
   //Custom Series Restrictions
   //Unrestricted except for unfair weapons
   //Ready to edit
   else
   {
      allowVehicle( all, TRUE );
      
      allowWeapon( all, TRUE );
      allowWeapon( 3, FALSE );    //Disrupter Weapon  
      allowWeapon( 110, FALSE );  //Plasma Cannon    
      allowWeapon( 124, FALSE );  //Missiles
      allowWeapon( 125, FALSE );
      allowWeapon( 126, FALSE );
      allowWeapon( 127, FALSE );
      allowWeapon( 128, FALSE );

      allowWeapon( 129, FALSE );
      allowWeapon( 130, FALSE );
      allowWeapon( 131, FALSE );  //Arachnitrons
      allowWeapon( 132, FALSE );
      allowWeapon( 133, FALSE );
      allowWeapon( 134, FALSE );  //Proximity Mines
      allowWeapon( 135, FALSE );
      allowWeapon( 136, FALSE );
      allowWeapon( 142, FALSE );  //Radiation Gun
      allowWeapon( 147, FALSE );  //More Missiles
      allowWeapon( 150, FALSE );  //Smart Gun
      
      allowComponent( all, TRUE );
   }

   NashercSetExtraAllowedItems();
}

function NashercSetExtraAllowedItems()
{
   //Blank function
}
