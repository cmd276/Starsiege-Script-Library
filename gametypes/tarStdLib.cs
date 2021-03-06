##--------------------------- Header
// FILE:        TDM_Wilzuun.cs
// AUTHORS:     ^TFW^ Wilzuun
// LAST MOD:    29 Sep, 2020
// VERSION:     1.0r
// NOTES:       Targeting Game. Target some one and die.
//              This file was created, tested, and modified in a day.
##--------------------------- Version History
//  1.0r
//      - Initial startup.

$wilzuun::GameMode = "tdm";

function wilzuun::setRules()
{
    %rules = "<jc><f2>The game's made up and the Targeting Doesn't Matter.\n\nHere's some advice, don't target things.<y17>";

    setGameInfo(%rules);
    return %rules;
}

function wilzuun::vehicle::onTargeted(%targeted, %targeter)
{
    if($wilzuun::GameMode == "tdm")
    {
        if (getTeam(%targeted) != getTeam(%targeter))
        {
            damageObject(%targeter, 9876543210);
        }
    }
    else
    {
        damageObject(%targeter, 9876543210);
    }
}

function wilzuun::vehicle::onScan(%d, %r)
{
    if (getTeam(%targeted) == getTeam(%targeter))
    {
        healObject(%d, 9876543210);
    }
}

