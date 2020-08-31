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
$line::Object      = "xMonument";

##--------------------------- Concrete Settings
// Please don't edit these, unless you understand the issues you may cause.
$line::GroupName = "TheShape";

##--------------------------- Functions
function line::Create(%sideCount, %radius)
{
    // foreach side do this...
    for(%side=0;%side<=%sideCount;%side++)
    {
        %sides[%side,'x'] = positionX(%side, %sideCount, %radius);
        %sides[%side,'y'] = positionY(%side, %sideCount, %radius);
    }

    for(%side = 0; %side < %sideCount-1; %side++)
    {
        %x0 = %sides[%side,'x'];
        %y0 = %sides[%side,'y'];

        %x1 =  %sides[%side+1,'x'];
        %y1 =  %sides[%side+1,'y'];

        line::fill(%x0, %y0, %x1, %y1);
    }
}
/// hen the point (xt,yt)=(((1−t)x0+tx1),((1−t)y0+ty1))

// https://math.stackexchange.com/questions/175896/finding-a-point-along-a-line-a-certain-distance-away-from-another-point
function line::Fill (%x0, %y0, %x1, %y1) {
    %d = sqrt( square(%x1-%x0) + square(%y1-%y0) );
    for(%a = 0; %a <= floor(%d); %a++)
    {
        %obj = spawnObject($line::Object);
        %t = %a/%d;
        %xN = ((1 - %t) * %x0 + %t * %x1);
        %yN = ((1 - %t) * %y0 + %t * %y1);
        setPosition(%obj, %xN, %yN, getTerrainHeight(%xN, %yN), 0, 0);
    }
}
