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
$dps::origin['-'] = 50;

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
$dps::jitter = 360;

// Enabled: Tells the script to take control of a few items.
$dps::enabled = false;

// Enable Quadrants. Disabled quadrants will prevent way-points from spawning in those directions.
$dps::quad['1'] = true; // N.E
$dps::quad['2'] = true; // N.W
$dps::quad['3'] = true; // S.W
$dps::quad['4'] = true; // S.E

function getDirection()
{
    %count = -1;
    if ($dps::quad['1']) %opt[%count++] = randomInt(0,89);
    if ($dps::quad['2']) %opt[%count++] = randomInt(90,179);
    if ($dps::quad['3']) %opt[%count++] = randomInt(180,269);
    if ($dps::quad['4']) %opt[%count++] = randomInt(270,359);

    return %opt[randomInt(0,%count)];
}

function getNavPointThrow()
{
    %min = $dps::space['distance'] * (1 - $dps::space['variance']);
    %max = $dps::space['distance'] * (1 + $dps::space['variance']);
    return randomInt(%min, %max);
}

function verifyVariables()
{
    if ($dps::space['distance']<0)
        return false;
    
    %mod = $dps::jitter/360;
    if (%mod != floor(%mod))
        return false;

    return true;
}