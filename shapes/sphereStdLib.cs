##--------------------------- Header
// FILE:        sphereStdLib.cs
// AUTHORS:     ^TFW^ Wilzuun
// LAST MOD:    10 Dec 2019
// VERSION:     2.0

##--------------------------- Version History
//  2.1 - 10 Dec 2019
//      Organized the code into sections better suited to public use.
//      Updated code to match what was in practice.

//  2.0 - 29 Apr 2019
//      Started comments.
//      Added in all on-the-fly variables
//      Added in the ID functions (GetNextId() & __IdUsed(%id))

//  1.0 - 28 Oct 2018
//      Started ground works.
//      Finished ground work on 29 Apr 2019.

##--------------------------- Notes


##--------------------------- On-The-Fly Settings
// Feel free to edit this variables in your own scripts as much as you want.
// These are more of a default setup, so one can minimally do something, and see results.

//  $sphere::xOffset : int
//  $sphere::yOffset : int
//  $sphere::zOffset : int
//      Respectively causes an offset from 0,0,0 location. These numbers can be postive, or negative.
//      they can also be floating point numbers as well.
//      Notes on zOffset : This will adjust items based on where the terrain is. So a 0 is *at* terrain height,
//      a positive number will cause the item to float, a negative number will cause it to be underground.
$sphere::xOffset = 0;
$sphere::yOffset = 0;
$sphere::zOffset = 0;

//  $sphere::markCenter : bool
//      Mark the center of the sphere with a marker?
$sphere::markCenter  = false;
//  $sphere::marker : object
//      If markCenter is true, uses the following object as the sphere center marker.
$sphere::marker      = "";

//  $sphere::radius : int
//      User supplied size of the sphere that will be spawned.
//      POSITIVE INTEGERS ONLY!
//  $sphere::spawnVerical : int
//      How many "levels" should be used to make the sphere?
//  $sphere::spawnHorizontal : int 
//      How many objects per "level"
$sphere::radius = 300;
$sphere::spawnVerical = 10;
$sphere::spawnHorizontal = 10;

//  $sphere::zRotate : boolean
//  $sphere::xRotate : boolean
//      Should an item be rotated?
//      These are not interconnected.
//      zRotate will make the item face center.
$sphere::xRotate = false;
$sphere::zRotate = false;

//  $sphere::zRotMod : int
//  $sphere::xRotMod : int
//      Respective modifiers for rotations. These are in degrees.
//      xRotMod @ 90 will cause the item to stand vertically (as if placed on a wall)
//              @ 270 will result in the same thing as 90. Just the other way.
//              @ 180 will cause items to be upside down.
//      zRotate spins the item.
//      xRotate tilts the item.
$sphere::xRotMod = -1;
$sphere::zRotMod = -1;

//  $sphere::underground : bool
//      Force rendering of underground objects.
$sphere::underground = true;

##--------------------------- Concrete Settings
// Please don't edit these, unless you understand the issues you may cause.
$sphere::GroupName = "TheSphere";

##--------------------------- Functions
// Returns an ID of the group. (For user usage later.)
function sphere::Create(%radius, %xOffset, %yOffset, %zOffset, %vSpawn, %hSpawn)
{
    %id = shape::getNextId(%groupName);
    %group = newObject($sphere::GroupName @ %id, SimGroup);
    addToSet("MissionGroup", %group);

    // Let's go ahead and place a center marker.
    if ($sphere::markCenter)
    {
        %item = newObject("CenterMarker",getObjectType($sphere::marker), $sphere::marker);
        addToSet(%group, %item);
        setPosition(%item, %xOffset, %yOffest, %zOffset);
    }    
    // zBase is used so that all items in the sphere are properly aligned.
    %zBase = getTerrainHeight(%xOffset, %yOffset);

    // The sphere is "cut" into "levels" and we need to calculate each "level"
    for (%v = 0; %v <= %vSpawn/2; %v++)
    {
        // This calculation is for everything in the same "level"
        // Theta Angle.
        %tSin = sin((360/%vSpawn) * %v);
        %tCos = cos((360/%vSpawn) * %v);

        // The circle that has to be made per "level"
        for (%h = 0; %h <= %hSpawn; %h++)
        {
            // Sigma Angle.
            // this dictates rotation around the Z axis.
            %sCos = cos((360/%hSpawn) * %h);
            %sSin = sin((360/%hSpawn) * %h);

            // Find out placement of the item...
            %xPos = %radius * %sCos * %tSin;
            %yPos = %radius * %sSin * %tSin;
            %zPos = (%radius * %tCos) + %zBase + %zOffset;

            // get the terrain height of this exact spot...
            // Why? because if we dont spawn thigns underground, we'll know it
            // with this variable.
            %terrain = getTerrainHeight(%xPos, %yPos);

            if ($sphere::zRotate)
            {
                if ($sphere::zRotMod > -1)
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
                    $zRot = $sphere::zRotMod;
                }
            }
            // xRot changes the pitch of the object we're spawning. Its responsible
            // for making a sphere when you use bridges as the objects.
            // only set it on the first object of this "level" as the objects get
            // spawned, their angle changes slightly, causing odd behavoir.
            if ((%h < 1) && $sphere::xRotate)
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

            // verify that we're placing it.
            if ((%zPos >= %terrain) || ($sphere::underground))
            {
                // create teh object...
                %item = newObject("EMP Projectile", StaticInterior, "xbridgehub.dis");
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

function sphere::Cleanup(%id)
{
    deleteObject($sphere::GroupName @ %id );
}
