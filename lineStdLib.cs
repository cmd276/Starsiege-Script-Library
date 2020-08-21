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
// Feel free to edit this variables in your own scripts as much as you want.
// These are more of a default setup, so one can minimally do something, and see results.

//  $line::radius : int
//      User supplied size of the shape that will be spawned.
//      POSITIVE INTEGERS ONLY!
$line::radius      = 30;

$line:spacing      = 2;     // space between objects.
$line::underground = false; // spawn even if underground?

$line::xOffset     = 0;     // x origin point
$line::yOffset     = 0;     // y    "     "
$line::zOffset     = 0;     // z    "     "
$line::zCenter     = true;  // apply same height to all objects?

$line::zRotate     = false; // make it rotate Z?
$line::xRotate     = false; //  "   "    "    X?

$line::zRotMod     = 0;     // Rotation for z
$line::xRotMod     = 90;    //     "     "  x

$line::markCenter  = true;  // Mark center of shape?
$line::marker     = "";     // object name to use.

//  $line::sides
//      How many sides are we going to spawn for this shape?
//      Positive numbers only.
$line::sides       = 4;

//  $line::Object
//      The object that we're going to be using for the sides.
$line::Object      = "xMonument";

##--------------------------- Concrete Settings
// Please don't edit these, unless you understand the issues you may cause.
$line::GroupName = "TheShape";

##--------------------------- Functions
function line::Create(%sides, %spacing, %radius)
{
    if (%sides == "")
        %sides = $line::sides;
    if (%spacing == "")
        %spacing = $line::spacing;
    if (%radius == "")
        %radius = $line::radius;
    
    // will change to a much higher number later.
    %angles = 360;
    // Technically, should equal out to 90 deg.
    %arcAngle = %angle/4;

    // foreach side do this...
    for(%item=0;%item<=%sides;%item++)
    {
        %sideDropX[%item] = positionX(%item, %sides, %radius);
        %sideDropY[%item] = positionY(%item, %sides, %radius);
    }
    
}
