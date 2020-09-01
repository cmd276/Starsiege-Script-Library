##--------------------------- Header
// FILE:        lineStdLib.cs
// AUTHORS:     ^TFW^ Wilzuun
// LAST MOD:    Dec 17th 2019
// VERSION:     0.1

##--------------------------- Version History
//  0.1
//      Project started.

##--------------------------- Notes

##--------------------------- On-The-Fly Settings
//  $line::Object
//      The object that we're going to be using for the sides.
$line::Object      = "pr_emp";

##--------------------------- Concrete Settings
// Please don't edit these, unless you understand the issues you may cause.
$line::GroupName = "TheShape";

##--------------------------- Functions
function line::Create(%object, %sideCount, %radius, %distance, %xOffset, %yOffest, %zOffset, %rod, %plane)
{
    if (%sideCount < 2) return echo("sideCount must be more than 1. Less than 2 is not permitted.");

    %id = shape::getNextId($line::groupName);
    %group = newObject($line::GroupName @ %id, SimGroup);
    addToSet("MissionGroup", %group);

    for(%side = 0; %side <= %sideCount; %side++)
    {
        %math = (360/%sideCount) * %side + %rod;
        %sides[%side,'x'] = positionX(360, %math, %radius);
        %sides[%side,'y'] = positionY(360, %math, %radius);
        %sides[%side,'z'] = positionY(360, %math, %radius);
    }

    for(%side = 0; %side < %sideCount; %side++)
    {
        %x0 = %sides[%side,'x'];
        %y0 = %sides[%side,'y'];

        %x1 =  %sides[%side+1,'x'];
        %y1 =  %sides[%side+1,'y'];
        %d = sqrt( square(%x1-%x0) + square(%y1-%y0) );
        for(%a = 0; %a <= floor(%d); %a = %a + (%d/2))
        {
            %obj = spawnObject(%object);
            %t = %a/%d;
            if (%plane == 1) { // xz plane
                %xN = 0;
                %yN = ((1 - %t) * %y0 + %t * %y1);
                %zN = getTerrainHeight(%xN, %yN) + ((1 - %t) * %x0 + %t * %x1);
            } else if (%plane == 2) { // yz plane
                %xN = ((1 - %t) * %x0 + %t * %x1);
                %yN = 0;
                %zN = getTerrainHeight(%xN, %yN) + ((1 - %t) * %y0 + %t * %y1);
            } else if (%plane == 3) { // xy plane, z tilted by x.
                %xN = ((1 - %t) * %x0 + %t * %x1);
                %yN = ((1 - %t) * %y0 + %t * %y1);
                %zN = getTerrainHeight(%xN, %yN) + ((1 - %t) * %x0 + %t * %x1);
            } else if (%plane == 4) { // xy plane, z tilted by y.
                %xN = ((1 - %t) * %x0 + %t * %x1);
                %yN = ((1 - %t) * %y0 + %t * %y1);
                %zN = getTerrainHeight(%xN, %yN) + ((1 - %t) * %y0 + %t * %y1);
            } else { // xy plane. (Standard)
                %xN = ((1 - %t) * %x0 + %t * %x1);
                %yN = ((1 - %t) * %y0 + %t * %y1);
                %zN = getTerrainHeight(%xN, %yN);
            }
            setPosition(%obj, %xN + %xOffset, %yN + %yOffest, %zN + %zOffset, 0, 0);
            addToSet(%group,%obj);
        }
    }
}
// Then the point (xt,yt)=(((1−t)x0+tx1),((1−t)y0+ty1))
// https://math.stackexchange.com/questions/175896/finding-a-point-along-a-line-a-certain-distance-away-from-another-point
