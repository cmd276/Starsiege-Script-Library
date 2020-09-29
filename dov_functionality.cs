##--------------------------- Header
//  Filename: dov\Functionality.cs
//  AUTHOR: ^TFW^ Wilzuun
//  CO-AUTHOR: Drake
##-----------------------------------------------------------------------------
##-----------------------------------------------------------------------------
//      Please don't edit this file.
##-----------------------------------------------------------------------------
##-----------------------------------------------------------------------------

function getAiCount (%player)
{
    return %player.aiCount;
}

function getMax (%player)
{
    return %player.maxAi;
}

function getSpawn (%player)
{
    return %player.sinceSpawn;
}

function wilzuun::initScoreBoard()
{
   deleteVariables("$ScoreBoard::PlayerColumn*");
   deleteVariables("$ScoreBoard::TeamColumn*");
	   // Player ScoreBoard column headings
	   $ScoreBoard::PlayerColumnHeader1 = "AI Count";
	   $ScoreBoard::PlayerColumnHeader2 = *IDMULT_SCORE_KILLS;
	   $ScoreBoard::PlayerColumnHeader3 = *IDMULT_SCORE_DEATHS;
	   $ScoreBoard::PlayerColumnHeader4 = "Kills since Spawn";
	   $ScoreBoard::PlayerColumnHeader5 = "Max Kills";

	   // Player ScoreBoard column functions
	   $ScoreBoard::PlayerColumnFunction1 = "getAiCount";
	   $ScoreBoard::PlayerColumnFunction2 = "getKills";
	   $ScoreBoard::PlayerColumnFunction3 = "getDeaths";
	   $ScoreBoard::PlayerColumnFunction4 = "getSpawn";
	   $ScoreBoard::PlayerColumnFunction5 = "getMax";
       
   // tell server to process all the scoreboard definitions defined above

   serverInitScoreBoard();
}

function wilzuun::onMissionEnd() // Mission::onEnd
{
    // echo("## ------------ function onMissionEnd()");
    %players = playerManager::getPlayerCount();
    for(%i = 0; %i < %players; %i++)
    {
        %player = playerManager::getPlayerNum(%i);
        deleteObject(%player.group);
    }
}

function wilzuun::onMissionLoad()
{
    // echo("## ------------ function onMissionLoad()");
    %players = playerManager::getPlayerCount();
    for(%i = 0; %i < %players; %i++)
    {
        %player = playerManager::getPlayerNum(%i);
        %player.sinceSpawn = 0;
        %player.aiCount = 0;
        %player.joinAi = 0;
        %player.maxAi = 0;
        %player.group = newObject("group" @ %player, simGroup);
        addToSet("MissionGroup", %player.group);
    }
}

function GetKilled()
{
    // echo("## ------------ function function GetKilled()");
    %playerCount = playerManager::getPlayerCount();
    for(%p = 0; %p <%playerCount; %p++)
    {
        %player = playerManager::getPlayerNum(%p);
        %vehicle = playerManager::playerNumToVehicleId(%player);
        if (%player.joinAi == 1)
        {
            %player.joinAi--;
            setTeam(%vehicle, *IDSTR_TEAM_RED);
            say(%player, 0, "Your grace period is over with... The swarm is after you again");
            schedule("order(" @ %player.group @ ", attack, " @ %vehicle @ ");", waitTime(%player));
        }
        else if (%player.joinAi > 1)
        {
            %player.joinAi--;
        }
    }
    schedule("getKilled();", 1);
}

function wilzuun::onMissionStart()
{
    // echo("## ------------ function onMissionStart()");
    ##  Yellow Team are bots. Make sure they hate red.
    setHostile(*IDSTR_TEAM_YELLOW, *IDSTR_TEAM_RED);
    ## Players should be red. Make sure every one hates them.
    ## Allow them to attack any one.
    setHostile(*IDSTR_TEAM_RED, *IDSTR_TEAM_RED, *IDSTR_TEAM_YELLOW, *IDSTR_TEAM_BLUE, *IDSTR_TEAM_PURPLE);
    setHostile(*IDSTR_TEAM_BLUE, *IDSTR_TEAM_RED);
    setHostile(*IDSTR_TEAM_PURPLE, *IDSTR_TEAM_RED);

    schedule("getKilled();", 1);
}

function wilzuun::player::onAdd(%player)
{
    // echo("## ------------ function player::onAdd()");

    ## Stealth Setting Detection.
    if(($stealthDetect) && (strAlign(1, left, getName(%player)) == "/"))
        say(Everyone, 0, "<F4>" @ getName(%player) @ "<F4> joined the game.");

    ## Set player information.
    ## Max amount of AAI killed before own death.
    %player.maxAi = 0;
    ## Kills since we spawned.
    %player.sinceSpawn = 0;
    ## We have this many AI belonging to this player on the field at this time.
    %player.aiCount = 0;
    ## This player killed another player, and are free to roam.
    %player.joinAi = 0;

    ## Group name: group2049, etc.
    %player.group = newObject("group" @ %player, simGroup);
    addToSet("MissionGroup", %player.group);
}

function wilzuun::player::onRemove(%player)
{
    // echo("## ------------ function player::onRemove()");
    deleteObject(%player.group);
}

function setDefaultMissionOptions()
{
    // echo("## ------------ function setDefaultMissionOptions()");
    $server::TeamPlay = false;
    $server::AllowDeathmatch = true;
    $server::AllowTeamPlay = false;
}

function wilzuun::setRules()
{
    // echo("## ------------ function setRules()");
    %rules = "<jc><f2>Welcome to Dark Overrun!<f0>\n<b0,5:table_head8.bmp><b0,5:table_head8.bmp><jl>\n\n<y17>";
    %rules = %rules @ "The Swarmie Bois are mad at you for betraying them. They've rallied their buddies and are coming after you like the mafia.\n";

    if ($fixedSwarm == true) 
    {
        %rules = %rules @ "The Swarm grows by " @ $swarmStatic @ " every time you kill 1 enemy.\n";
    }
    else 
    {
        %rules = %rules @ "The Swarm grows by (Your Kill Count / " @ $swarmDynanic @ ") every time you kill 1 enemy.\n";
    }
    if ($swarmClone == true)
    {
        %rules = %rules @ "All enemy vehicles are a clone of your vehicle. The pilot rivals that of Bek Storm.\n";
    }
    else
    {
        %rules = %rules @ "All enemies drive random vehicles.\n";
    }
    %rules = %rules @ "\n\n<jc><f2>Player Rewards<f0>\n<b0,5:table_head8.bmp><b0,5:table_head8.bmp><jl>\n\n<y17>";
    if ($pvpReward == true)
    {
        if ($pvpReload == true)
        {
            %rules = %rules @ "There is a reload after every player kill\n";
        }
        if ($pvpHeal == true)
        {
            %rules = %rules @ "There is a heal after every player kill\n";
        }
        if ($pvpStopAi == true)
        {
            %rules = %rules @ "You join the hunters if you kill another player. (You join the hunters for " @ $pvpTimeAi @ " seconds)\n";
        }
    }
    if ($pveReward == true)
    {
        if ($pveReload == true)
        {
            %rules = %rules @ "There is a reload after every AI kill, for the first " @ $pveReloadWhen @ " kills\n";
        }
        if ($pveHeal == true)
        {
            %rules = %rules @ "There is a heal after every AI kil, for the first " @ $pveHealWhen @ " kills\n";
        }
    }

    %rules = %rules @ "\n\n<jc><f2>Player Rewards<f0>\n<b0,5:table_head8.bmp><b0,5:table_head8.bmp><jl>\n\n<y17>These are currently broken. Sorry!";

    setGameInfo(%rules);
    return %rules;
}
setRules();

function wilzuun::vehicle::onAdd(%vehicleId)
{
    // echo("## ------------ function vehicle::onAdd()");
    %player = playerManager::vehicleIdToPlayerNum(%vehicleId);
    ## Anti-AI check.
    if (%player == 0) return;
    
    %timeToWait = waitTime(%player);


    if (%player.aiCount <= 0)
    {
        for(%i=0; %i < swarmVolume(%player); %i++)
            spawnSwarm(%player);

        schedule("order(" @ %player.group @ ", attack, " @ %vehicleId @ ");", 3);
    }
    else
    {
        schedule("order(" @ %player.group @ ", attack, " @ %vehicleId @ ");", %timeToWait);
    }

    schedule("setTeam(" @ %vehicleId @ ",*IDSTR_TEAM_RED);",%timeToWait);
    setTeam(%vehicleId, *IDSTR_TEAM_NUETRAL);
}

function wilzuun::vehicle::onDestroyed(%destroyed, %destroyer)
{
    // echo("## ------------ function vehicle::onDestroyed()");
    %message = getFancyDeathMessage(getHUDName(%destroyed), getHUDName(%destroyer));
    if(%message != "")
        say( 0, 0, %message);
    vehicle::onDestroyedLog(%destroyed, %destroyer);

    %dead = playerManager::vehicleIdToPlayerNum(%destroyed);
    %living = playerManager::vehicleIdToPlayerNum(%destroyer);

    ## if an AI Dies, schedule its deletion.
    // AI killed AI... oops?
    if ((%living == 0) && (%dead == 0))
    {
        %destroyed.owner.aiCount--;
        schedule("deleteObject("@ %destroyed @ ");",1);
    }
    // AI kills Player.
    else if ((%living == 0) && (%dead != 0))
    {
        playerDied(%dead);
    }
    // Player Kills Player
    else if ((%living != 0) && (%dead != 0) && (%dead != %living))
    {
        playerLived(%living,true);
        playerDied(%dead);
    }

    // Player Kills AI
    else if ((%living != 0) && (%dead == 0))
    {
        if (getTeam(%destroyed) == getTeam(%destroyer))
        {
            %living.joinAi = 1;
        }

        %destroyed.owner.aiCount--;
        schedule("deleteObject("@ %destroyed @ ");",1);
    
        %countToSpawn = swarmVolume(%living);
        for(%i = 0; %i < %countToSpawn; %i++)
        {
            // echo("Spawning things");
            spawnSwarm(%living);
        }
        playerLived(%living);
    }
  
    // We dont know any more... Or any thing...
    else 
    {
        // uh oh...
        echo("UNKNOWN DEATH SET");
    }
}

function playerLived(%player, %pvp) 
{
    // echo("## ------------ function playerLived()");

    %destroyer = playerManager::playerNumtoVehicleId(%player);
    // Do we reload them still?
    if (($pveReward) && ($pveReload) && ($pveReloadWhen >= %player.sinceSpawn))
        reloadObject(%destroyer, 99999);
    
    // Do we still heal them?
    if (($pveReward) && ($pveHeal) && ($pveHealWhen >= %player.sinceSpawn))
        healObject(%destroyer,99999);

    if (%pvp)
        if (($pvpReward)&&($pvpStopAi)) // how about joining the swarm, instead of being eaten?
        {
            %player.joinAi = %player.joinAi + $pvpTimeAi; // Extend, or give time.
            setTeam(%destroyer, *IDSTR_TEAM_YELLOW);
            schedule("order(" @ %player.group @ ", guard, " @ %destroyer @ ");", waitTime(%player));
            say(%player, 0, "The swarm is willing to overlook your rogue likeness for a period...");
        }
    %player.sinceSpawn++;
}

function playerDied(%player)
{
    // echo("## ------------ function playerDied()");
    if (%player.sinceSpawn > %player.maxAi)
    {
        %player.maxAi = %player.sinceSpawn;
        say(%player, 0, "New Personal Best! " @ %player.sinceSpawn @ " kills before death");
        // storeScore(%player, %player.sinceSpawn);
    }
    %player.sinceSpawn = 0;
}

function spawnSwarm(%player)
{
    echo("## ------------ function spawnSwarm()");
    ## If AI, quit.
    if (%player == 0) return;
    ## Get the players vehicle ID for later use.
    %vehicleId = playerManager::playerNumToVehicleId(%player);
    echo("----------------------------------------------------"@%vehicleId);

    %x1 = getPosition(%vehicleId, x) - 3000;
    %x2 = getPosition(%vehicleId, x) + 3000;
    %y1 = getPosition(%vehicleId, y) - 3000;
    %y2 = getPosition(%vehicleId, y) + 3000;
        
    if ($swarmClone == true)
    {
        echo("Clone Swarm Detected.");
        storeObject(%vehicleId, %vehicleId @ "_test.veh");
        %newSpawn = loadObject("Swarm Member", %vehicleId @ "_test.veh");
    }
    else
    {
        %newSpawn = loadObject("Swarm Member", getSwarmMember());
    }

    %newSpawn.owner = %player;
    setPilotId(%newSpawn, 29);
    randomTransport(%newSpawn, %x1, %y1, %x2, %y2);
    setTeam(%newSpawn, *IDSTR_TEAM_YELLOW);
    addToSet("MissionGroup\\group" @ %player, %newSpawn);
    order(%newSpawn, speed, high);
    schedule("order(" @ %player.group @ ", attack, " @ %vehicleId @ ");", waitTime(%player));
    
    %player.aiCount++;
    // messageBox(0,"Swarm Drop");
}

Pilot Harabec
{
   id = 29;
   
   name = "Swarmie Boi";
   skill = 1.0;
   accuracy = 1.0;
   aggressiveness = 0.9;
   activateDist = 1500.0;
   deactivateBuff = 2000.0;
   targetFreq = 5.0;
   trackFreq = 1.0;
   fireFreq = 0.2;
   LOSFreq = 0.1;
   orderFreq = 0.2;    
};

function swarmVolume(%player)
{
    // echo("## ------------ function swarmVolume()");
    if ($fixedSwarm == true)
        return $swarmStatic;
    else if ($fixedSwarm == false)
        return floor(%player.sinceSpawn / $swarmDynanic) + 1;
}

function isItOn (%v)
{
    if (%v == true)
        return "on";
    else
        return "off";
}

function waitTime (%player) 
{
    if ($spawnSafetySwitch) 
    {
        return floor($spawnSafetyBase + (player.aiCount / $spawnSafetyMod));
    }
    else return 0.5;
}

function getSwarmMember()
{
    %swarmCount = 0;
    %swarmMembers[-1] = true;

    if ($allow["Hercs"] == true)
    {
        if ($allow["Terran"] == true)
        {
            %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.1.swarm"; // Terran Apocalypse
            %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.2.swarm"; // Terran Minotaur
            %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.3.swarm"; // Terran Gorgon
            %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.4.swarm"; // Terran Talon
            %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.5.swarm"; // Terran Basilisk
        }
        if ($allow["Knight"] == true)
        {
            %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.10.swarm"; // Knight's Apocalypse
            %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.11.swarm"; // Knight's Minotaur
            %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.12.swarm"; // Knight's Gorgon
            %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.13.swarm"; // Knight's Talon
            %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.14.swarm"; // Knight's Basilisk
        }
        if ($allow["Cybrid"] == true)
        {
            %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.20.swarm"; // Cybrid Seeker
            %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.21.swarm"; // Cybrid Goad
            %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.22.swarm"; // Cybrid Shepherd
            %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.23.swarm"; // Cybrid Adjudicator
            %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.24.swarm"; // Cybrid Executioner
            %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.27.swarm"; // Platinum Adjudicator (SP version, not selectable)
            %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.28.swarm"; // Platinum Executioner (SP version, not selectable)
            %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.55.swarm"; // Platinum Adjudicator 2
            %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.56.swarm"; // Platinum Executioner 2
        }
        if ($allow["Metagen"] == true)
        {
            %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.35.swarm"; // Metagen Seeker
            %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.36.swarm"; // Metagen Goad
            %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.37.swarm"; // Metagen Shepherd
            %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.38.swarm"; // Metagen Adjudicator
            %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.39.swarm"; // Metagen Executioner
        }
        if ($allow["Rebel"] == true)
        {
            %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.30.swarm"; // Rebel Emancipator
            %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.33.swarm"; // Rebel Olympian
        }
        if ($allow["Special"] == true)
        {
            %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.29.swarm"; // Prometheus
            %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.40.swarm"; // Harabec's Apocalypse
            %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.42.swarm"; // Caanan's Basilisk
        }
        if ($allow["Pirate"] == true)
        {
            %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.50.swarm"; // Pirate's Apocalypse
            %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.52.swarm"; // Pirate's Emancipator
        }
    }

    if ($allow["Tanks"] == true)
    {
        if ($allow["Terran"] == true)
        {
            %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.6.swarm"; // Paladin Tank
            %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.7.swarm"; // Myrmidon Tank
            if ($allow["Disruptors"] == true)
            {
                %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.8.swarm"; // Disruptor Tank
            }
            if ($allow["Artillery"] == true)
            {
                %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.133.swarm"; // Nike Artillery
                %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.134.swarm"; // Supressor Tank
            }
        }
        if ($allow["Knight"] == true)
        {
            %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.15.swarm"; // Knight's Paladin
            %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.16.swarm"; // Knight's Myrmidon
            if ($allow["Disruptors"] == true)
            {
                %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.17.swarm"; // Knight's Disruptor
            }
        }
        if ($allow["Cybrid"] == true)
        {
            %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.25.swarm"; // Bolo Tank
            %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.26.swarm"; // Recluse Tank
            if ($allow["Artillery"] == true)
            {
                %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.90.swarm"; // Cybrid Artillery
            }
        }
        if ($allow["Rebel"] == true)
        {
            %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.31.swarm"; // Avenger Tank
            %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.32.swarm"; // Dreadnought Tank
            %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.72.swarm";  // Rebel Thumper
            %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.138.swarm"; // Rebel bike
            if ($allow["Artillery"] == true)
            {
                %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.137.swarm"; // Rebel Artillery
                %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.150.swarm"; // SUAV Bus
            }
        }
        if ($allow["Special"] == true)
        {
            %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.41.swarm"; // Harabec's Predator
            %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.45.swarm"; // Harabec's Super Predator
        }
        if ($allow["Pirate"] == true)
        {
            %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.51.swarm"; // Pirate's Dreadlock
        }
    }

    if ($allow["Drone"] == "Not Happening" && true == false)
    {
        if ($allow["Terran"] == true)
        {
            %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.60.swarm";  // Terran Empty Cargo
            %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.61.swarm";  // Terran Ammo Cargo
            %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.62.swarm";  // Terran Big Ammo Cargo
            %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.63.swarm";  // Terran Big Personnel Cargo
            %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.64.swarm";  // Terran Fuel Cargo
            %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.65.swarm";  // Terran Minotaur Cargo
            %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.71.swarm";  // Terran Utility Truck
            %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.73.swarm";  // Terran Starefield
        }
            if ($allow["Rebel"] == true)
        {
            %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.66.swarm";  // Rebel Empty Cargo
            %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.67.swarm";  // Rebel Ammo Cargo
            %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.68.swarm";  // Rebel Big Cargo Transport
            %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.69.swarm";  // Rebel Bix Box Cargo Transport
            %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.70.swarm";  // Rebel Box Cargo Transport
        }
        if ($allow["Cybrid"] == true)
        {
            %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.94.swarm";  // Cybrid Omnicrawler
            %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.95.swarm";  // Cybrid Protector
            %swarmMembers[%swarmCount++] = "wilzuun\\gameTypes\\dov\\vehicles\\veh.96.swarm";  // Cybrid Jamma
        }
    }

    %rand = randomInt(1,%swarmCount);
    
    return %swarmMembers[%rand];
}

