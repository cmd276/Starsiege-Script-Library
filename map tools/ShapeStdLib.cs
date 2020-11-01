##--------------------------- Header
// FILE:        ShapeStdLib.cs
// AUTHORS:     ^TFW^ Wilzuun
// LAST MOD:    2 Sep 2020
// VERSION:     1.33.7

##--------------------------- Required Files
exec(wilzuunStdLib);
exec(TrigonometryStdLib);

##--------------------------- On-The-Fly Settings

//  $Shape::zCenter : boolean
//      True  : Use the center of the Shape's terrain height as the base height.
//      False : Use the item's 'drop' point's terrain height as the base height.
//      Using this will allow for the Shape objects to appear at the same height on hilly maps.
$Shape::zCenter     = true;

//  $Shape::Object
//      The object that we're going to be using for the sides.
$Shape::Object        = "pr_emp";

//  $Shape::markCenter : bool
//      Mark the center of the Shape with a marker?
$Shape::markCenter  = true;

//  $Shape::marker : object
//      If markCenter is true, uses the following object as the Shape center marker.
$Shape::marker      = redBeam;

//  $Shape::zRotate : boolean
//  $Shape::xRotate : boolean
//      Should an item be rotated?
//      These are not interconnected.
//      zRotate will make the item face center.
$Shape::xRotate     = false;
$Shape::zRotate     = false;

//  $Shape::zRotMod : int
//  $Shape::xRotMod : int
//      Respective modifiers for rotations. These are in degrees.
//      xRotMod @ 90 will cause the item to stand vertically (as if placed on a wall)
//              @ 270 will result in the same thing as 90. Just the other way.
//              @ 180 will cause items to be upside down.
//      zRotate spins the item.
//      xRotate tilts the item.
$Shape::xRotMod     = 0;
$Shape::zRotMod     = 0;

//  $Shape::underground : bool
//      Force rendering of underground objects.
$Shape::underground = true;

##--------------------------- Concrete Settings
// Please don't edit these, unless you understand the issues you may cause.
$Shape::GroupName   = "TheShape";

##--------------------------- Functions

function Shape::Polygon(%object, %sideCount, %radius, %distance, %xOffset, %yOffset, %zOffset, %rod, %plane)
{
    if (%sideCount < 2) return  echo("sideCount must be more than 1. Less than 2 is not permitted.");
    if (%sideCount > 99) return echo("sideCount must be less than 100. Less than 2 is not permitted.");
    

    %id = shape::getNextId($Shape::groupName);
    %group = newObject($Shape::GroupName @ %id, SimGroup);
    addToSet("MissionGroup", %group);

    // Make the center point, if its wanted.
    if ($Shape::markCenter == true && $Shape::marker != "") 
        shape::AddCenter(%group, %xOffset, %yOffset, %zOffset);

    // The math for each side of the shape. This saves the point on the shape
    for(%side = 0; %side <= %sideCount; %side++)
    {
        %math = (360/%sideCount) * %side + %rod;
        %sides[%side,'x'] = positionX(360, %math, %radius);
        %sides[%side,'y'] = positionY(360, %math, %radius);
    }

    // Fill in each side of the shape with the %object.
    for(%side = 0; %side < %sideCount; %side++)
    {
        // "Source" Point to "Destination" point fill in math.
        %x0 = %sides[%side,'x'];
        %y0 = %sides[%side,'y'];
        %x1 =  %sides[%side+1,'x'];
        %y1 =  %sides[%side+1,'y'];
        %d = sqrt( square(%x1-%x0) + square(%y1-%y0) );
        
        // Fill in the line with objects.
        for(%a = 0; %a <= floor(%d); %a = %a + %distance)
        {
            %t = %a/%d;
            %xN = ((1 - %t) * %x0 + %t * %x1) + %xOffset;
            %yN = ((1 - %t) * %y0 + %t * %y1) + %yOffset;
            %zN = getTerrainHeight(%xN, %yN) + %zOffset;
            
            // based on the plane, change placing.
            if (%plane == 1) { // xz plane
                %xN = %xOffset;
                %zN = getTerrainHeight(%xN, %yN) + ((1 - %t) * %x0 + %t * %x1);
            } else if (%plane == 2) { // yz plane
                %yN = %yOffset;
                %zN = getTerrainHeight(%xN, %yN) + ((1 - %t) * %y0 + %t * %y1);
            } else if (%plane == 3) { // xy plane, z tilted by x.
                %zN = getTerrainHeight(%xN, %yN) + ((1 - %t) * %x0 + %t * %x1);
            } else if (%plane == 4) { // xy plane, z tilted by y.
                %zN = getTerrainHeight(%xN, %yN) + ((1 - %t) * %y0 + %t * %y1);
            } else { // xy plane. (Standard)
                if ($Shape::zCenter)
                    %zN = getTerrainHeight(%xOffset, %yOffset);
            }
            
            %zN = %zN + %zOffset;
            %LocationToBe = getTerrainHeight(%xN, %yN);

            // Spawn items if its wanted.
            if (
                ($Shape::underground == true) ||
               (($Shape::underground == false) && (%zN >= %LocationToBe))
               )
            {
                %newMarker = spawnObject(%object);
                addToSet(%group,%newMarker);
                setPosition(%newMarker, %xN, %yN, %zN, 0, 0);
            }            
        }
    }
    return %id;
}

function Shape::Polygon2(%object, %sideCount, %radius, %objectCount, %xOffset, %yOffset, %zOffset, %rod, %plane)
{
    if (%sideCount < 2) return  echo("sideCount must be more than 1. Less than 2 is not permitted.");
    if (%sideCount > 99) return echo("sideCount must be less than 100. Less than 2 is not permitted.");
    

    %id = shape::getNextId($Shape::groupName);
    %group = newObject($Shape::GroupName @ %id, SimGroup);
    addToSet("MissionGroup", %group);

    // Make the center point, if its wanted.
    if ($Shape::markCenter == true && $Shape::marker != "") 
        shape::AddCenter(%group, %xOffset, %yOffset, %zOffset);

    // The math for each side of the shape. This saves the point on the shape
    for(%side = 0; %side <= %sideCount; %side++)
    {
        %math = (360/%sideCount) * %side + %rod;
        %sides[%side,'x'] = positionX(360, %math, %radius);
        %sides[%side,'y'] = positionY(360, %math, %radius);
    }

    // Fill in each side of the shape with the %object.
    for(%side = 0; %side < %sideCount; %side++)
    {
        // "Source" Point to "Destination" point fill in math.
        %x0 = %sides[%side,'x'];
        %y0 = %sides[%side,'y'];
        %x1 =  %sides[%side+1,'x'];
        %y1 =  %sides[%side+1,'y'];
        %d = sqrt( square(%x1-%x0) + square(%y1-%y0) );
        %distance = %d/%objectCount;
        
        // Fill in the line with objects.
        for(%a = 0; %a <= floor(%d); %a = %a + %distance)
        {
            %t = %a/%d;
            %xN = ((1 - %t) * %x0 + %t * %x1) + %xOffset;
            %yN = ((1 - %t) * %y0 + %t * %y1) + %yOffset;
            %zN = getTerrainHeight(%xN, %yN) + %zOffset;
            
            // based on the plane, change placing.
            if (%plane == 1) { // xz plane
                %xN = %xOffset;
                %zN = getTerrainHeight(%xN, %yN) + ((1 - %t) * %x0 + %t * %x1);
            } else if (%plane == 2) { // yz plane
                %yN = %yOffset;
                %zN = getTerrainHeight(%xN, %yN) + ((1 - %t) * %y0 + %t * %y1);
            } else if (%plane == 3) { // xy plane, z tilted by x.
                %zN = getTerrainHeight(%xN, %yN) + ((1 - %t) * %x0 + %t * %x1);
            } else if (%plane == 4) { // xy plane, z tilted by y.
                %zN = getTerrainHeight(%xN, %yN) + ((1 - %t) * %y0 + %t * %y1);
            } else { // xy plane. (Standard)
                if ($Shape::zCenter)
                    %zN = getTerrainHeight(%xOffset, %yOffset);
            }
            
            %zN = %zN + %zOffset;
            %LocationToBe = getTerrainHeight(%xN, %yN);

            // Spawn items if its wanted.
            if (
                ($Shape::underground == true) ||
               (($Shape::underground == false) && (%zN >= %LocationToBe))
               )
            {
                %newMarker = spawnObject(%object);
                addToSet(%group,%newMarker);
                setPosition(%newMarker, %xN, %yN, %zN, 0, 0);
            }            
        }
    }
    return %id;
}

function Shape::Sphere(%object, %itemCount, %radius, %xOffset, %yOffset, %zOffset, %hSpawn)
{
    %id = shape::getNextId(%groupName);
    %group = newObject($Shape::GroupName @ %id, SimGroup);
    addToSet("MissionGroup", %group);

    // Let's go ahead and place a center marker.
    if ($Shape::markCenter == true && $Shape::marker != "") 
        shape::AddCenter(%group, %xOffset, %yOffset, %zOffset);

    // zBase is used so that all items in the sphere are properly aligned.
    %zBase = getTerrainHeight(%xOffset, %yOffset);

    // The sphere is "cut" into "levels" and we need to calculate each "level"
    for (%v = 0; %v <= %itemCount/2; %v++)
    {
        // This calculation is for everything in the same "level"
        // Theta Angle.
        %tSin = sin((360/%itemCount) * %v);
        %tCos = cos((360/%itemCount) * %v);

        // The circle that has to be made per "level"
        for (%h = 0; %h <= %hSpawn; %h++)
        {
            // Sigma Angle.
            // this dictates rotation around the Z axis.
            %sCos = cos((360/%hSpawn) * %h);
            %sSin = sin((360/%hSpawn) * %h);

            // Find out placement of the item...
            %xPos = (%radius * %sCos * %tSin);
            %yPos = (%radius * %sSin * %tSin);
            %zPos = (%radius * %tCos) + %zBase + %zOffset;

            if ($Shape::zRotate)
            {
                if ($Shape::zRotMod > -1)
                {
                    // zRot makes things rotate around the Z axis... which i always mistake.
                    %zRot = getAngle(%xPos,%yPos, %xOffset, %yOffest);
                    // if zRot == "" make it equal 90. For what ever magical reason, 
                    // 90 degrees always comes up as a blank string.
                    if (%zRot == "") %zRot = 90;
                    // add 90 degrees so that it faces inward, towards the center.
                    %zRot = %zRot + 90;
                }
                else
                {
                    $zRot = $Shape::zRotMod;
                }
            }
            // xRot changes the pitch of the object we're spawning. Its responsible
            // for making a sphere when you use bridges as the objects.
            // only set it on the first object of this "level" as the objects get
            // spawned, their angle changes slightly, causing odd behavoir.
            if ((%h < 1) && $Shape::xRotate)
            {
                // get the angle of object placement, and center of sphere.
                %xRot = getAngle(%xPos,%zPos, %xOffset,(%zBase + %zOffset));
                // for the same reason as zRot, a 90 degree is magically empty string
                // answered... wtf is with that?
                if (%xRot == "") 
                {
                    %xRot = 90;
                }
                // Make them face inward, subtract 90 degrees. 
                // Wanna make them face outward? add 90 instead!
                %xRot = %xRot + -90;
            }
            
            %xPos = %xPos + %xOffset;
            %yPos = %yPos + %yOffset;
            // get the terrain height of this exact spot...
            // Why? because if we dont spawn thigns underground, we'll know it
            // with this variable.
            %terrain = getTerrainHeight(%xPos, %yPos);

            // verify that we're placing it.
            if ((%zPos >= %terrain) || ($Shape::underground))
            {
                // create teh object...
                // %item = newObject("EMP Projectile", StaticInterior, "xbridgehub.dis");
                %item = spawnObject(%object);
                // add it to group...
                addToSet(%group, %item);
                // "spawn" it. since all created items are spawned anyways... just below ground
                // move it to where it can be seen...
                setPosition(%item, %xPos, %yPos, %zPos, %zRot, %xRot);
            }
        }
    }
    return %id;
}

function Shape::Circle (%object, %itemCount, %radius, %xOffset, %yOffset, %zOffset, %plane)
{
    // Get a new ID, make a group, add the group to the game.
    %id = shape::getNextId($Shape::groupName);
    %group = newObject($Shape::GroupName @ %id, SimGroup);
    addToSet("MissionGroup", %group);

    if ($Shape::markCenter == true && $Shape::marker != "") 
        shape::AddCenter(%group, %xOffset, %yOffset, %zOffset);
    
    // For each item within the count, find its position, and place it.
    for(%item=0;%item<=%itemCount;%item++)
    {
        // We're going to ass XY plane first.
        %xPos = positionX(%itemCount, %item, %radius) + %xOffset;
        %yPos = positionY(%itemCount, %item, %radius) + %yOffset;
        
        if ($Shape::zCenter == true)
            %zPos = getTerrainHeight(%xOffset, %yOffset);
        else
            %zPos = getTerrainHeight(%xPos, %yPos);
        
        // Now, check for a plane change, and redo the math.
        if ((%plane == "yz") || (%plane == "zy"))
        {
            %yPos = %yOffset;
            %zPos = getTerrainHeight(%xPos, %yPos) + positionY(%itemCount, %item, %radius);
        }
        if ((%plane == "xz") || (%plane == "zx"))
        {
            %xPos = %xOffset;
            %zPos = getTerrainHeight(%xPos, %yPos) + positionX(%itemCount, %item, %radius);
        }
        %zPos = %zPos + %zOffset;

        // Are we spinning the objet?
        if ($Shape::zRotate)
        {
            %zRot = getAngle(%xOffset, %yOffset, %xPos, %yPos);
            if (%zRot == "") %zRot = 90; // its some weird bug, dont ask me.
            %zRot = %zRot + $Shape::zRotMod;
        }
        else
            %zRot = 0;
        
        // This determines the rotation or "tilt" of the object.
        if ($Shape::xRotate)
            %xRot = $Shape::xRotMod;
        else
            %xRot = 0;

        // Spawn the %object if we want it.
        if ((%zPos >= %terrain) || ($Shape::underground))
        {
            %newMarker = spawnObject(%object);
            addToSet(%group,%newMarker);
            setPosition(%newMarker, %xPos, %yPos, %zPos, %zRot, %xRot);
        }
    }
    
    return %id;
}

function Shape::Star(%object, %sideCount, %radius, %distance, %xOffset, %yOffset, %zOffset, %rod, %plane)
{
    // enforce limitations.
    if (%sideCount < 4) return  echo("sideCount must be more than 4.");
    if (%sideCount > 11) return echo("sideCount must be less than 12.");

    %id = shape::getNextId($Shape::groupName);
    %group = newObject($Shape::GroupName @ %id, SimGroup);
    addToSet("MissionGroup", %group);

    // Make the center point, if its wanted.
    if ($Shape::markCenter == true && $Shape::marker != "") 
        shape::AddCenter(%group, %xOffset, %yOffset, %zOffset);

    // The math for each side of the shape. This saves the point on the shape
    for(%side = 0; %side <= %sideCount; %side++)
    {
        %math = (360/%sideCount) * %side + %rod;
        %sides[%side,'x'] = positionX(360, %math, %radius);
        %sides[%side,'y'] = positionY(360, %math, %radius);
        
        echo("x,y : " @ %sides[%side,'x'] @ "," @ %sides[%side,'y']);
        echo("------ " @ %side);
    }

    // Fill in each side of the shape with the %object.
    for(%side = 0; %side < %sideCount; %side++)
    {
        // "Source" Point to "Destination" point fill in math.
        %x0 = %sides[%side,'x'];
        %y0 = %sides[%side,'y'];
        %mod = (%side + floor(%sideCount/2)) % %sideCount;
        
        if (floor(%sideCount/2) == (%sideCount/2))
        {
            %mod = (%side + (floor(%sideCount/2)-1)) % %sideCount;
        }
        %x1 =  %sides[%mod,'x'];
        %y1 =  %sides[%mod,'y'];
        
        %d = sqrt( square(%x1-%x0) + square(%y1-%y0) );
        
        // Fill in the line with objects.
        for(%a = 0; %a <= floor(%d); %a = %a + %distance)
        {
            %t = %a/%d;
            %xN = ((1 - %t) * %x0 + %t * %x1) + %xOffset;
            %yN = ((1 - %t) * %y0 + %t * %y1) + %yOffset;
            %zN = getTerrainHeight(%xN, %yN) + %zOffset;
            
            // based on the plane, change placing.
            if (%plane == 1) { // xz plane
                %xN = %xOffset;
                %zN = getTerrainHeight(%xN, %yN) + ((1 - %t) * %x0 + %t * %x1);
            } else if (%plane == 2) { // yz plane
                %yN = %yOffset;
                %zN = getTerrainHeight(%xN, %yN) + ((1 - %t) * %y0 + %t * %y1);
            } else if (%plane == 3) { // xy plane, z tilted by x.
                %zN = getTerrainHeight(%xN, %yN) + ((1 - %t) * %x0 + %t * %x1);
            } else if (%plane == 4) { // xy plane, z tilted by y.
                %zN = getTerrainHeight(%xN, %yN) + ((1 - %t) * %y0 + %t * %y1);
            } else { // xy plane. (Standard)
                if ($Shape::zCenter)
                    %zN = getTerrainHeight(%xOffset, %yOffset);
            }
                        
            %zN = %zN + %zOffset;
            %LocationToBe = getTerrainHeight(%xN, %yN);

            // Spawn items if its wanted.
            if (
                ($Shape::underground == true) ||
               (($Shape::underground == false) && (%zN >= %LocationToBe))
               )
            {
                %newMarker = spawnObject(%object);
                addToSet(%group,%newMarker);
                setPosition(%newMarker, %xN, %yN, %zN, 0, 0);
            }
        }
    }
    return %id;
}

function Shape::Star2(%object, %sideCount, %radius, %objectCount, %xOffset, %yOffset, %zOffset, %rod, %plane)
{
    // enforce limitations.
    if (%sideCount < 4) return  echo("sideCount must be more than 4.");
    if (%sideCount > 11) return echo("sideCount must be less than 12.");

    %id = shape::getNextId($Shape::groupName);
    %group = newObject($Shape::GroupName @ %id, SimGroup);
    addToSet("MissionGroup", %group);

    // Make the center point, if its wanted.
    if ($Shape::markCenter == true && $Shape::marker != "") 
        shape::AddCenter(%group, %xOffset, %yOffset, %zOffset);

    // The math for each side of the shape. This saves the point on the shape
    for(%side = 0; %side <= %sideCount; %side++)
    {
        %math = (360/%sideCount) * %side + %rod;
        %sides[%side,'x'] = positionX(360, %math, %radius);
        %sides[%side,'y'] = positionY(360, %math, %radius);
        
        echo("x,y : " @ %sides[%side,'x'] @ "," @ %sides[%side,'y']);
        echo("------ " @ %side);
    }

    // Fill in each side of the shape with the %object.
    for(%side = 0; %side < %sideCount; %side++)
    {
        // "Source" Point to "Destination" point fill in math.
        %x0 = %sides[%side,'x'];
        %y0 = %sides[%side,'y'];
        %mod = (%side + floor(%sideCount/2)) % %sideCount;
        
        if (floor(%sideCount/2) == (%sideCount/2))
        {
            %mod = (%side + (floor(%sideCount/2)-1)) % %sideCount;
        }
        %x1 =  %sides[%mod,'x'];
        %y1 =  %sides[%mod,'y'];
        
        %d = sqrt( square(%x1-%x0) + square(%y1-%y0) );
        %distance = %d/%objectCount;
        
        // Fill in the line with objects.
        for(%a = 0; %a <= floor(%d); %a = %a + %distance)
        {
            %t = %a/%d;
            %xN = ((1 - %t) * %x0 + %t * %x1) + %xOffset;
            %yN = ((1 - %t) * %y0 + %t * %y1) + %yOffset;
            %zN = getTerrainHeight(%xN, %yN) + %zOffset;
            
            // based on the plane, change placing.
            if (%plane == 1) { // xz plane
                %xN = %xOffset;
                %zN = getTerrainHeight(%xN, %yN) + ((1 - %t) * %x0 + %t * %x1);
            } else if (%plane == 2) { // yz plane
                %yN = %yOffset;
                %zN = getTerrainHeight(%xN, %yN) + ((1 - %t) * %y0 + %t * %y1);
            } else if (%plane == 3) { // xy plane, z tilted by x.
                %zN = getTerrainHeight(%xN, %yN) + ((1 - %t) * %x0 + %t * %x1);
            } else if (%plane == 4) { // xy plane, z tilted by y.
                %zN = getTerrainHeight(%xN, %yN) + ((1 - %t) * %y0 + %t * %y1);
            } else { // xy plane. (Standard)
                if ($Shape::zCenter)
                    %zN = getTerrainHeight(%xOffset, %yOffset);
            }
                        
            %zN = %zN + %zOffset;
            %LocationToBe = getTerrainHeight(%xN, %yN);

            // Spawn items if its wanted.
            if (
                ($Shape::underground == true) ||
               (($Shape::underground == false) && (%zN >= %LocationToBe))
               )
            {
                %newMarker = spawnObject(%object);
                addToSet(%group,%newMarker);
                setPosition(%newMarker, %xN, %yN, %zN, 0, 0);
            }
        }
    }
    return %id;
}

//  shape::getNextId(%groupName) : int
//      Starts up a check for a used ID, and returns the first identified unused ID for the %groupName supplied.
//      Example: Currently existing groups for `Circle`: Circle0, Circle2. Running this will return 1. A seecond call would return 3.
function shape::getNextId(%groupName)
{
    %checkId = 0;
    while(shape::__IdUsed(%groupName, %checkId))
    {
        %checkId = %checkId + 1;
    }
    return %checkId;
}

//  shape::__IdUsed(%groupName, %id) : boolean
//      Checks each item at the MissionGroup base level for a group called %groupName followed by a a number designation of %id.
//      Example: Existing `Circle` groups: Circle0, Circle1, Circle2
//          Supplying 1 as an ID, would give you `true`, using 3 as an ID would result in a `false`
function shape::__IdUsed(%groupName, %id)
{
    %group = getObjectId("MissionGroup");
    %item = getNextObject(%group, 0);
    for (%count = 0; %item != 0; %count = %count+1)
    {
        if (getObjectName(%item) == %groupName @ %id)
        {
            return true;
        }
        %item = getNextObject(%group, %item);
    }
    return false;
}

//  shape::Cleanup(%groupName, %id) : void
//      Find ands deletes an object grouping for Shapes. Technically, could be re-written to delete any group given to it.
//      Will delete *ALL* groups with the same Group name and ID. Make sure to give your groups different IDs.
function shape::Cleanup(%groupName, %id)
{
    if (%groupName == "" || %id == "")
        return echo("shape::Cleanup <GroupName> <id>");

    if (shape::__IdUsed(%groupName, %id))
        shape::delete(%groupName @ %id );
    else
        echo("Group ID " @ %id @ " for the groupName of " @ %groupName @ " not found.");
}

//  shape::Delete ( %groupName ) : void unlessfailure then boolean
//      Will delete all groups in the MissionGroup that have the exact name given.
function shape::delete(%groupName)
{
    %group = getObjectId("MissionGroup");
    %item = getNextObject(%group, 0);
    for (%count = 0; %item != 0; %count = %count+1)
    {
        if (getObjectName(%item) == %groupName)
        {
            deleteObject(%item);
        }
        %item = getNextObject(%group, %item);
    }
    return false;
}

//
function shape::AddCenter(%group, %xOffset, %yOffset, %zOffset) 
{
    %newMarker = spawnObject($Shape::marker);
    addToSet(%group,%newMarker);
    %zPos = getTerrainHeight(%xOffset, %yOffset) + %zOffset;
    setPosition(%newMarker, %xOffset, %yOffset, %zPos, 0, 0);
}

function shape::AddItem(%group, %object, %x, %y, %z, %xr, %zr)
{
    %newMarker = spawnObject(%object);
    addToSet(%group,%newMarker);
    setPosition(%newMarker, %x, %y, %z, %xr, %zr);
}

//
function Shape::PolygonPrism(%object, %sideCount, %radius, %distance, %xOffset, %yOffset, %zOffset, %rod, %plane)
{
    if (%sideCount < 2) return  echo("sideCount must be more than 1. Less than 2 is not permitted.");
    if (%sideCount > 99) return echo("sideCount must be less than 100. Less than 2 is not permitted.");
    

    %id = shape::getNextId($Shape::groupName);
    %group = newObject($Shape::GroupName @ %id, SimGroup);
    addToSet("MissionGroup", %group);

    // Make the center point, if its wanted.
    if ($Shape::markCenter == true && $Shape::marker != "") 
        shape::AddCenter(%group, %xOffset, %yOffset, %zOffset);

    // The math for each side of the shape. This saves the point on the shape
    for(%side = 0; %side <= %sideCount; %side++)
    {
        %math = (360/%sideCount) * %side + %rod;
        %sides[%side,'x'] = positionX(360, %math, %radius);
        %sides[%side,'y'] = positionY(360, %math, %radius);
    }

    // Fill in each side of the shape with the %object.
    for(%side = 0; %side < %sideCount; %side++)
    {
        // "Source" Point to "Destination" point fill in math.
        %x0 = %sides[%side,'x'];
        %y0 = %sides[%side,'y'];
        %x1 =  %sides[%side+1,'x'];
        %y1 =  %sides[%side+1,'y'];
        %d = sqrt( square(%x1-%x0) + square(%y1-%y0) );
        
        // Fill in the line with objects.
        for(%a = 0; %a <= floor(%d); %a = %a + %distance)
        {
            %t = %a/%d;
            %xN = ((1 - %t) * %x0 + %t * %x1) + %xOffset;
            %yN = ((1 - %t) * %y0 + %t * %y1) + %yOffset;
            %zN = getTerrainHeight(%xN, %yN) + %zOffset;
            
            // based on the plane, change placing.
            if (%plane == 1) { // xz plane
                %xN = %xOffset;
                %zN = getTerrainHeight(%xN, %yN) + ((1 - %t) * %x0 + %t * %x1);
            } else if (%plane == 2) { // yz plane
                %yN = %yOffset;
                %zN = getTerrainHeight(%xN, %yN) + ((1 - %t) * %y0 + %t * %y1);
            } else if (%plane == 3) { // xy plane, z tilted by x.
                %zN = getTerrainHeight(%xN, %yN) + ((1 - %t) * %x0 + %t * %x1);
            } else if (%plane == 4) { // xy plane, z tilted by y.
                %zN = getTerrainHeight(%xN, %yN) + ((1 - %t) * %y0 + %t * %y1);
            } else { // xy plane. (Standard)
                if ($Shape::zCenter)
                    %zN = getTerrainHeight(%xOffset, %yOffset);
            }
            
            %zN = %zN + %zOffset;
            %LocationToBe = getTerrainHeight(%xN, %yN);

            // Spawn items if its wanted.
            if (
                ($Shape::underground == true) ||
               (($Shape::underground == false) && (%zN >= %LocationToBe))
               )
            {
                %newMarker = spawnObject(%object);
                addToSet(%group,%newMarker);
                setPosition(%newMarker, %xN, %yN, %zN, 0, 0);
            }            
        }
    }
    return %id;
}