##--------------------------- Header
// FILE:        circleStdLib.cs
// AUTHORS:     ^TFW^ Wilzuun
// LAST MOD:    10 Dec 2019
// VERSION:     2.0

##--------------------------- Notes

##--------------------------- On-The-Fly Settings
//  $circle::zCenter : boolean
//      True  : Use the center of the circle's terrain height as the base height.
//      False : Use the item's 'drop' point's terrain height as the base height.
//      Using this will allow for the circle objects to appear at the same height on hilly maps.
$circle::zCenter     = true;

//  $circle::onGround : bool
//      Force circle to spawn at terrain height.
//      WARNING: OVERRIDES $circle::zCenter
$circle::onGround    = true;

//  $circle::zRotate : boolean
//  $circle::xRotate : boolean
//      Should an item be rotated?
//      These are not interconnected.
//      zRotate will make the item face center.
$circle::zRotate     = false;
$circle::xRotate     = false;

//  $circle::zRotMod : int
//  $circle::xRotMod : int
//      Respective modifiers for rotations. These are in degrees.
//      xRotMod @ 90 will cause the item to stand vertically (as if placed on a wall)
//              @ 270 will result in the same thing as 90.
//              @ 180 will cause items to be upside down.
//      zRotate spins the item.
//      xRotate tilts the item.
$circle::zRotMod     = 0;
$circle::xRotMod     = 90;

//  $circle::markCenter : bool
//      Mark the center of the circle with a marker?
$circle::markCenter  = true;
//$circle::marker     = "";

//  $circle::plane : str
//      Which plane are we building on?
//          "XY" : Parallel with / On the ground
//          "YZ" : Faces East / West. Appears as a line from North/South views.
//          "XZ" : Faces North/South. Appears as a line from East / West views.
$circle::plane       = "xy";

//  $circle::underground : bool
//      Force rendering of underground objects.
$circle::underground = false;

##--------------------------- Concrete Settings
// Please don't edit these, unless you understand the issues you may cause.
$circle::GroupName = "Circle";

##--------------------------- Functions

function circle::Cleanup(%id)
{
    deleteObject($circle::GroupName, %id );
}


function Circle::Spawn (%object, %itemCount, %radius, %xOffset, %yOffset, %zOffset, %plane)
{
    // Get a new ID, make a group, add the group to the game.
    %id = shape::getNextId($circle::groupName);
    %group = newObject($circle::GroupName @ %id, SimGroup);
    addToSet("MissionGroup", %group);

    if ($circle::markCenter == true)
    {
        // $circle::marker = loadObject(%object, "object_circleStdLib");
        %newMarker = spawnObject(%object);
        addToSet(%group,%newMarker);
        %xPos = %xOffset;
        %yPos = %yOffset;
        %zPos = getTerrainHeight(%xPos, %yPos) + %zOffset;
        setPosition($circle::marker, %xPos, %yPos, %zPos, 0, 0);
    }
    // For each item within the count, find its position, and place it.
    for(%item=0;%item<=%itemCount;%item++)
    {
        // We're going to ass XY plane first.
        %xPos = positionX(%itemCount, %item, %radius) + %xOffset;
        %yPos = positionY(%itemCount, %item, %radius) + %yOffset;
        %zPos = getTerrainHeight(%xPos, %yPos);
        if ($circle::zCenter == true)
            %zPos = getTerrainHeight(%xOffset, %yOffset);
        
        // Now, check for a plane change, and redo the math.
        if ((%plane == "yz") || (%plane == "zy"))
        {
            %xPos = positionX(%itemCount, %item, %radius) + %xOffset;
            %yPos = %yOffset;
            %zPos = getTerrainHeight(%xPos, %yPos) + positionY(%itemCount, %item, %radius);
        }
        if ((%plane == "xz") || (%plane == "zx"))
        {
            %xPos = %xOffset;
            %yPos = positionY(%itemCount, %item, %radius) + %yOffset;
            %zPos = getTerrainHeight(%xPos, %yPos) + positionX(%itemCount, %item, %radius);
        }
        %zPos = %zPos + %zOffset;

        // Are we spinning the objet?
        if ($circle::zRotate)
        {
            %zRot = getAngle(%xOffset, %yOffset, %xPos, %yPos);
            if (%zRot == "") %zRot = 90; // its some weird bug, dont ask me.
            %zRot = %zRot + $circle::zRotMod;
        }
        else
            %zRot = 0;
        
        // This determines the rotation or "tilt" of the object.
        if ($circle::xRotate)
            %xRot = $circle::xRotMod;
        else
            %xRot = 0;

        // Spawn the %object if we want it.
        if (($circle::underground == true) ||
            (($circle::underground == false) && (%zPos >= getTerrainHeight(%xPos, %yPos)))
           )
        {
            %newMarker = spawnObject(%object);
            addToSet(%group,%newMarker);
            setPosition(%newMarker, %xPos, %yPos, %zPos, %zRot, %xRot);
        }
    }
    
    return %id;
}

##--------------------------- Version History
//  --  Version History
//
//  3.0
//      Added in options for XZ, YZ plane circles.
//      Added functions:
//          setPlane()
//
//  2.4
//      Better documentation of variable names and usage.
//      Added in feature to make all items spawn at the same height.
//      Added in Center of circle marker option
//
//  2.3
//      -What did I do?-
//
//  2.2
//      Organized functions and variables to be in alphabetical order.
//
//  2.1
//      Added few safe gaurds to make sure objects actually spawn.
//      Fixed seemingly random unit from existing by deleting it. (SetObject now deletes the object handed to it)
//
//  2.0
//      Created namespace `Circle`
//      Rewrote great majority of code to fit into new namespace.
//      Expanded functions.
//          Created Cleanup()
//          Created Init()
//          Created SetCount()
//          Created SetLocation()
//          Created SetMode()
//          Created SetObject()
//          Created SetSize()
//          Renamed `makeCircle()` to `SpawnCircle()`
//
//  1.5
//      Added Mode types, and correlating formulas
//
//  1.0
//      Basic circle creation.
//      Added Offsets.
//      Added custome objects.
//

