##--------------------------- Header
// FILE:        scoresStdLib.cs
// AUTHORS:     ^TFW^ Wilzuun
// LAST MOD:    12 Jan 2021
// VERSION:     No Version Number.

##--------------------------- Notes
//  Put this line is player::onAdd(%player)
//      %player.name = getHUDName(%player);

##--------------------------- History
// 12 Jan 2021
//      File Created.

##--------------------------- Settings
// Top number of scores to show.
// Default 10. INTs only.
$topScores = 10;

$divider = "ㅤ";
##--------------------------- Functions

function importScores(%gameType)
{
    // Check for Scores file.
    if(isFile("scores_" @ %gameType @ ".cs"))
    {
        exec("scores_" @ %gameType @ ".cs");
    }
    // It doesn't exist, create it.
    else
    {
        fileWrite("scores_" @ %gameType @ ".cs", overwrite, "");
    }
}

function exportScores(%gameType)
{
    // Save all scores to a file.
    export("$scores::*", "scores_" @ %gameType @ ".cs");
}

function displayScores(%topScores)
{
    // get display amount...
    if (%topScores == "")
    {
        %topScores = $topScores;
    }
    
    // TODO: more functionality here
    %count = 1;
    %highest = 0;
    
    %output = "Top Scores:\n";
    while(%scores::score[%count] != "")
    {
        %score = %scores::score[%count];
        if (%score > %highest)
        {
            %scores[1] = %count;
            
        }
    }
    %output = %output @ $scores::names[%count] @ " - " @ $scores::score[%count] @ "\n";
    
    for(%c = 2; %c < %topScores; %c++)
    {
        %nextHighest = 0;
        while(%scores::score[%count] != "")
        {
            %score = %scores::score[%count];
            if ( (%score < %scores[%c]) && (%score > %nextHighest) )
                %scores[%c] = %c;
        }
        %output = %output @ $scores::names[%scores[%c]] @ " - " @ $scores::score[%scores[%c]] @ "\n";
    }
    
    return %output;
}

function editScore(%player,%newScore)
{
    // Do a check to see if %player has a NAME property.
    if (%player.name != "") %name = %player.name;
    // It doesn't, get their HUD name.
    else %name = getHUDName(%player);
    
    %count = 1;
    // Find the player's %count number.
    // Or find first non-Existing score record.
    while (%count)
    {
        if ($scores::names[%count] == %name) break;
        if ($scores::names[%count] == "") break;
        %count++;
    }
    
    // Found a non-existing score record. Assign it to the %name.
    if ($scores::names[%count] == "")
    {
        $scores::names[%count] = %name;
    }
    
    // Assign new score to this player.
    $scores::score[%count] = %newScore;
}