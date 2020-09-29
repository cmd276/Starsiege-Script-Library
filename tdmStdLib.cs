##--------------------------- Header
// FILE:        TDM_Wilzuun.cs
// AUTHORS:     ^TFW^ Wilzuun
// LAST MOD:    29 Sep, 2020
// VERSION:     1.0r
##--------------------------- Version History
//  1.0r
//      - Initial startup.


function wilzuun::setRules()
{
    %rules = "<jc><f2>The game's made up and the Targeting Doesn't Matter.\n\n<y17>";

    setGameInfo(%rules);
}

function wilzuun::vehicle::onTargeted(%targeted, %targeter)
{
    damageObject(%targeter, 9876543210);
}
