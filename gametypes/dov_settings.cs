##--------------------------- Header
//  Filename: dov\Settings.cs
//  AUTHOR: ^TFW^ Wilzuun
//  CO-AUTHOR: Drake
//
##--------------------------- Swarm Variables
//  Swarm Variables. This directly affects the number of AI on the map.

//  $fixedSwarm : boolean :
//    If true, Each AI is replaced with a fixed amount of AI.
$fixedSwarm         = true;

//  $swarmStatic : int
//    The value of how many AI to spawn on AI spawn.
//    (if $fixedSwarm == true)
$swarmStatic        = 2;

//  $swarmDynanic : int
//    (if $fixedSwarm == false)
//    playerKills / 10 + 1
//    On an AI death, will calculate how many AI to spawn. The value used here
//      will be used as a base value. If you use 15, the first 14 AI deaths will
//      spawn one AI, 15-29 will spawn 2 AI, 30-44 will spawn 3 AI, and so on.
$swarmDynanic       = 2;

//  $swarmClone : boolean
//      If this is true, will make all swarm AI spawned, clones of the player that "owns" them.
$swarmClone         = false;

##--------------------------- Rewards System
//
//  Rewards System. For those that want to see others fall.
//  This sytem is for PvP play rewards.

//  $pvpReward : boolean
//    true : Turns on giving players incentive to kill other players.
//    false : Players get nothing for killing another player.
$pvpReward          = true;

//  $pvpReload : boolean
//    true : reload a player killer.
$pvpReload          = true;

//  $pvpHeal : boolean
//    true : heal the killer
$pvpHeal            = true;

//  $pvpStopAI : boolean
//    true : Player is made a member of the AI team. AI stop attacking them.
$pvpStopAi          = true;

//  $pvpTimeAi : int
//    if $pvpStopAI == true
//    The amount of time that the AI doesn't attack a player killer, and how long
//      player killer is on the AI team. in seconds!
$pvpTimeAi          = 60;


##--------------------------- Success System
//
//  Success System. Most games should reward a successful player. This isnt like
//    that in the least. We want our players to die. Miserably. With a challenge.
//    Question is how hard do we want to get?

//  $pveReward : boolean
//    true : They get rewards!
$pveReward          = true;

//  $pveArtillery : boolean
//    true : Spawn artillery
$pveArtillery       = true;

//  $pveArtilleryChance : int
//    1 - 100%, 2 - 50%, 3 - 33%, 4 - 25%, 5 - 20%, 10 - 10% ,
//    100 - 1%, 50 - 2%, 33 - 3%, 25 - 4%, 20 - 5%
$pveArtilleryChance = 5;

//  $pveArtWhen : int
//    At how many kills will we start spawning artillery units against the player.
$pveArtWhen         = 10;

//  $pveReload : boolean
//    true : Stop reloading the player after so much time.
$pveReload          = true;

//  $pveReloadWhen : int
//    When do we start neglecting the reload on kill?
$pveReloadWhen      = 20;

//  $pveHeal : boolean
//    true : Stop healing the player after so much time.
$pveHeal            = true;

//  $pveHealWhen : int
//    When do we start neglecting the heal on kill?
$pveHealWhen        = 20;

##--------------------------- Stealth Detection
//
//  Stealth Detection. Alert players when some one leaves / joins with a name
//    like the following:
//    '// ^TFW^ DarkFlare'
//    true is on, false is off.
$stealthDetect      = true;

##--------------------------- Spawn Protection
//
//  Spawn Safety Switch
//  $spawnSafetySwitch : bool
//      Enables preventing AI from instantly attacking players.
$spawnSafetySwitch = true;

//  Spawn Protection. Puts players on Neutral team for X Seconds.
//  $spawnSafetyBase : int
$spawnSafetyBase = 5;

// Spawn Increment. Used as a buffer for more AI.
// floor( player.AICount / spawnSafetyMod) + spawnSafetyBase;
// Example Mod = 5, player.AI = 23;
// A player would get 23/5 = 4.6, Rounded down is 4. 4 + 5 is 9.
// A player would have 9 seconds of being on Neutral Team.

//  $spawnSafetyMod : int
$spawnSafetyMod = 5;

##--------------------------- Main Classification
//
//  Turning off either of these will override all options below. Flyers are not
//    included due to them snapping to map locations, and being very hard to
//    predict, and to hit with weapons.
//
//    Examples: tanks = true; hercs = false; Terran = true;
//      Only Terran Tanks will spawn, no Terran hercs will show up.

//  $allow["Tanks"] : boolean
//    true : Enable AI tanks
$allow["Tanks"]     = true;

//  $allow["Hercs"] : boolean
//    true : Enable AI Hercs
$allow["Hercs"]     = true;

//  $allow["Drone"] : boolean
//    true : Enable AI Drones. (These typically have no weapons, so they're pretty much easy kills.
$allow["Drone"]     = true; // This is Broken. Research required to fix.

// Flyers aren't included at all in this project.

##--------------------------- Faction Classification
//
//  These options matter if the above options are turned on.
//    Example: Tanks = true; hercs = false; Terran = true; Knight = false;
//      Only tanks will appear. Only Terran tanks will be spawned. Knight tanks
//      will not appear at all

//  $allow["Terran"]  : boolean
//    true : Will spawn in Terran vehicles.
$allow["Terran"]    = true;

//  $allow["Knight"]  : boolean
//    true : Will spawn in Knight vehicles.
$allow["Knight"]    = true;

//  $allow["Pirate"]  : boolean
//    true : Will spawn in Pirate vehicles.
$allow["Pirate"]    = true;

//  $allow["Rebel"]  : boolean
//    true : Will spawn in Rebel vehicles.
$allow["Rebel"]    = true;

//  $allow["Cybrid"]  : boolean
//    true : Will spawn in Cybrid vehicles.
$allow["Cybrid"]    = true;

//  $allow["Metagan"] : boolean
//    true : Will spawn in Metagan vehicles.
$allow["Metagan"]   = true;

//  $allow["Special"] : boolean
//    true : Will spawn in Special vehicles.
$allow["Special"]   = false;

##--------------------------- Specialty Classification
//
//  These last two categories are in respects to groups of vehicles that players
//    may find highly annoying, or undesirable for normal game play. They still 
//    require their Faction Classification to be enabled to show up.

//  $allow["Disruptors"] : boolean
//    true : Will spawn in Disruptor Tanks.
$allow["Disruptors"]= false;

//  $allow["Artillery"] : boolean
//    true : Will spawn in Artillery Tanks.
$allow["Artillery"] = false;

##--------------------------- History
//
//    History:
//    1.0:
//      Copy and pasted desired effects from C&D map sets.
//      Tested, and works as desired. But code looks ugly.
//      Requested that [eC] Drake™ take a look and give opinions.
//
//    1.0r2
//      Revision and complete rewrite by [eC] Drake™.
//
//    2.0
//      Added rewards for killing players.
//      Added a few of the long standing player "rewards"
//
//    2.1
//      Fixed crash issues with improper vehicle drop attempts.
//      Added random chance artillery drops.
//      Moved a few items around, due to how the game loads data.
//
//    3.0
//      Fixed issue with vehicles spawning in on top of players when using clone
//      mode. Added in Clone Mode.
//      Updated history. Refactored death code. 
//      

##--------------------------- Plans
//
//    Road Map / Plans:
//
//    Get basic map up and running. -- Done.
//    Get AI to drop near players. -- Done;
//    Get AI to attack the player.  -- Done;
//    Get AI to attack player after respawn  -- Done;
//    Add more AI on AI death.  -- Done;
//
//    Add Tank / Herc Only Modes.
//    Add Tank & Herc Mode.
//
//    Harsh Progression. Things get harder as time goes on.
//    -> Survive Long enough, and get Artillery against you.
//    -> Survive long enough, reinforcements grow larger.  -- Done;
//    Reward Progression. Killing players grants boons.
//    -> AI no longer hunt you for a brief time period.  -- Done;