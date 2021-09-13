## ----------------------------------------------------------------------------
##-----------------------------------------------------------------------------
//  Dynamic Pathway System  (DPS)
//  Author: ^TFW^ Wilzuun
//  Started: 14th Dec, 2018
##-----------------------------------------------------------------------------
##-----------------------------------------------------------------------------
// %newMarker = newObject("newMarker", SimMarker);

//Origin points. Players will be put within a vicinity of this area.
$dps::origin['x'] = 0;
$dps::origin['y'] = 0; 
$dps::origin['r'] = 50;

//How many way-points are we making?
$dps::links = 10; // Default will be Zero.

// How far are the way-points from each other?
// Variance allows for a touch of randomness to the distance.
// Variance is in decimal form and is a percentage of distance.
// Examples: Distance of 1500, variance of 0.10 would result in way-points being between 1350 and 1650 meters away from each other.
$dps::space['distance'] = 1200;
$dps::space['variance'] = 0.15;

// Jitter: Used for random math angles. 
// If you modify it, make sure its in multiples of 360.
// Recommend leaving it at 360.
$dps::jitter = 360;

// Enabled: Tells the script to take control of a few items.
$dps::enabled = false;

// Enable Quadrants. Disabled quadrants will prevent way-points from spawning in those directions.
$dps::quad['1'] = true; // N.E
$dps::quad['2'] = true; // N.W
$dps::quad['3'] = true; // S.W
$dps::quad['4'] = true; // S.E

// The parameters used here, are for quick set, overrides. 
// 
function dps::getDirection(%quad1, %quad2, %quad3, %quad4)
{
    %count = -1;
    if (%quad1 == 1 || %quad1 == true) %opt[%count++] = randomInt(0,89);
    else if (%quad1 == "" && $dps::quad['1'] == true) %opt[%count++] = randomInt(0,89);

    if (%quad2 == 1 || %quad2 == true) %opt[%count++] = randomInt(90,179);
    else if (%quad2 == "" && $dps::quad['2'] == true) %opt[%count++] = randomInt(90,179);

    if (%quad3 == 1 || %quad3 == true) %opt[%count++] = randomInt(180,269);
    else if (%quad3 == "" && $dps::quad['3'] == true) %opt[%count++] = randomInt(180,269);

    if (%quad4 == 1 || %quad4 == true) %opt[%count++] = randomInt(270,359);
    else if (%quad4 == "" && $dps::quad['4'] == true) %opt[%count++] = randomInt(270,359);

    return %opt[randomInt(0,%count)];
}

function dps::getThrow()
{
    %min = $dps::space['distance'] * (1 - $dps::space['variance']);
    %max = $dps::space['distance'] * (1 + $dps::space['variance']);
    return randomInt(%min, %max);
}

function dps::verifyVariables()
{
    if ($dps::space['distance'] < 0)
        $dps::space['distance'] = abs($dps::space['distance']);
    %mod = $dps::jitter/360;
    if (%mod != floor(%mod))
        return false;
    return true;
}

function makeNewWaypoint()
{
    // newObject(navigationMarker, ESNavMarker);
    %distance = randomInt(($dps::space['distance'] * (1 - $dps::space['variance'])), ($dps::space['distance'] * (1 - $dps::space['variance'])));
    echo(%distance);
}