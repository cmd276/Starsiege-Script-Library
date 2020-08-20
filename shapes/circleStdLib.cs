##--------------------------- Header
// FILE:        circleStdLib.cs
// AUTHORS:     ^TFW^ Wilzuun
// LAST MOD:    10 Dec 2019
// VERSION:     2.0

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
$circle::GroupName = "TheCircle";

##--------------------------- Functions

function circle::Cleanup(%id)
{
    deleteObject($circle::GroupName, %id );
}

function Circle::Create (%object, %radius, %count, %xOffset, %yOffset, %zOffset)
{
    // Get an ID, and create a group. Add that group to the mission items.
    %id = shape::getNextId(%groupName);
    %group = newObject($sphere::GroupName @ %id, SimGroup);
    addToSet("MissionGroup", %group);

    if ($circle::markCenter)
    {
        %item = newObject("CenterMarker",getObjectType($circle::marker), $circle::marker);
        addToSet(%group, %item);
        setPosition(%item, %xOffset, %yOffest, %zOffset);
    }    

    if ($circle::zCenter)
        %zBase = getTerrainHeight(%xOffset, %yOffset);

    %tSin = sin((360/1));
    %tCos = cos((360/1));

    for (%h = 0; %h <= %count; %h++)
    {
        %sCos = cos((360/%count) * %h);
        %sSin = sin((360/%hSpawn) * %h);
        // Sigma Angle.
        // this dictates rotation around the Z axis.
        %sCos = cos((360/%hSpawn) * %h);
        %sSin = sin((360/%hSpawn) * %h);

        // Find out placement of the item...
        %xPos = %radius * %sCos * %tSin;
        %yPos = %radius * %sSin * %tSin;
        %zBase = getTerrainHeight(%xPos, %yPos);
        if ($circle::zCenter)
            %zBase = getTerrainHeight(%xOffset, %yOffset);
        if ($circle::onGround)
            %zBase = getTerrainHeight(%xPos, %yPos);
        %zPos = (%radius * %tCos) + %zBase + %zOffset;
        %zPos = %zPos + %zOffset;

        if ($circle::zRotate)
        {
            %zRot = getAngle(%xOffset, %yOffest, %xPos, %yPos);
            if (%zRot == "") %zRot = 90;
            %zRot = %zRot + $circle::zRotMod;
        }
        else %zRot = 0;

        if ($circle::xRotate) %xRot = $circle::xRotMod;
        else %xRot = 0;

        if (($circle::underground == true) ||
            (($circle::underground == false) && (%zPos >= getTerrainHeight(%xPos, %yPos)))
           )
        {
            
        }
    }

    // End of Function, return ID
    return %id;
}