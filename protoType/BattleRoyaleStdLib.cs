##--------------------------- Header
// FILE:        BattleRoyaleStdLib.cs
// AUTHORS:     ^TFW^ Wilzuun
// LAST MOD:    11 Aug, 2020
// VERSION:     1.0r
// NOTES:       Free-For-All, Shrinking Play area. Game is over when one player is left standing.
##--------------------------- Version History
//  1.0r
//      - Initial startup.
##--------------------------- Required Files
exec(ShapeStdLib);


##--------------------------- Settings

// Time until dropping in is no longer allowed. This also will allow players to start killing each other.
// This time is in seconds.
$timeToDrop = 120;

// Safety check. Used to see if we're allowing drop ins. If not, will be used to insta-kill new drop ins.
$allowDrop = true;

// These allow for a random center point to the circle.
$randX = randomInt(-2000,2000);
$randY = randomInt(-2000,2000);
$server::HudMapViewOffsetX = $randX;
$server::HudMapViewOffsetY = $randY;

// How far out from the center point should players be able to spawn?
// Since the defaults for the center point is a 4,000 by 4,000 area, allow players to spawn upto 3800 meters away.
// Please have this number smaller than abs(min) + abs(max) of the randomInt commands above.
$spawnDistance = 3800;

// How often do we redraw the ring? The lower this number, the more often the border ring is updated.
$ringUpdate = 10;

// How big should the starting ring be?
$ringSize = 4000;

// Ring decrease steps...
$ringDecrease = 10;

## -- Please don't play with these settings.
$Shape::zCenter = false;


##--------------------------- Functionality
function wilzuun::initScoreBoard() 
{
    
}

function wilzuun::onMissionEnd() 
{
    
}

function wilzuun::onMissionLoad() 
{
    // Set these to true, so that the new game can get going.
    $server::DropInProgress = "True";
    $allowDrop = true;
    Shape::Circle (%object, %itemCount, %radius, %xOffset, %yOffset, %zOffset, %plane);
    say(0,0, "First player to join will start the timer until round start!");
}

function wilzuun::onMissionStart() 
{
    // Let the players know some one dropped into the game.
    say(0,0,"Welcome to Battle Royale! You have 2 minutes to join the game!");
    // Schedule a point where players are no longer allowed to drop in.
    schedule("EndFreeDrop();", $timeToDrop);
    // Clear out hostile teams. Should prevent players from killing each other before the game starts.
    setHostile(*IDSTR_TEAM_RED, *IDSTR_TEAM_YELLOW, *IDSTR_TEAM_BLUE, *IDSTR_TEAM_PURPLE);
}

function wilzuun::player::onAdd(%player) 
{
    ## Stealth Setting Detection.
    if(($stealthDetect) && (strAlign(1, left, getName(%player)) == "/"))
        say(Everyone, 0, "<F4>" @ getName(%player) @ "<F4> joined the game.");
    say(%player,%player, "Welcome to Battle Royale!");
    if ( ! $allowDrop )
    {
        say(%player, %player, "You have to wait until the next game to start playing.");
    }
    else
    {
        say(%player, %player, "You may drop into the game if you wish.");
    }
}

function wilzuun::player::onRemove(%player) 
{
    
}

function wilzuun::setRules()
{
    
}

function wilzuun::vehicle::onAdd(%vehicleId) 
{
    if ( ! $allowDrop )
    {
        healObject(%vehicleId, -99999);
    }
    else
    {
        // Force EVERYONE to red team.
        setTeam(%vehicleId, *IDSTR_TEAM_RED);
    }
}

function wilzuun::vehicle::onDestroyed(%destroyed, %destroyer) 
{
    %message = getFancyDeathMessage(getHUDName(%destroyed), getHUDName(%destroyer));
    if(%message != "")
        say( 0, 0, %message);
}

function EndFreeDrop ()
{
    // Remove the ability to drop into the game.
    $server::DropInProgress = "False";
    $allowDrop = false;
    setHostile(*IDSTR_TEAM_RED, *IDSTR_TEAM_RED);
}

function testVehiclesOnField()
{
    
}

function setBoundary(%distance)
{
    %x = $randX;
    %y = $randY;
    if ($oldCircle != "")
    {
        shape::Cleanup("TheShape", $oldCircle);
    }
    %itemCount = $ringSize / 100;
    %radius = ($ringSize/2)-5;
    $ringSize = $ringSize - $ringDecrease;
    $oldCircle = Shape::Circle ($Shape::Object, %itemCount, %radius, %x, %y, %getTerrainHeight(%x, %y));
}