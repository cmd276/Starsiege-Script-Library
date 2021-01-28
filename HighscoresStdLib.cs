//    FILENAME:    HighscoresStdLib.cs
//
//    AUTHOR:    Drake
//-------------------------------------------------------------------------------------------------

//-------------------------------------------------------------------------------------------------
//    AUTHOR NOTES:
//        - Starting file 07/28/20 to be standard library on handling score information.
//
//-------------------------------------------------------------------------------------------------

// Libraries
//-------------------------------------------------------------------------------------------------
exec("stringManager.cs");

// Score Storage File: hs_{GAMETYPE}.cs
// Global Variable Init
//-------------------------------------------------------------------------------------------------

$hs::gameType = ""; // Change per game lib.

// Top 5, Top 10, etc. Score file will store this. 10 by default
$hs::scoreCount = 10;

// Import Scores
//-------------------------------------------------------------------------------------------------

function hs::import(%gameType) {
    if(!isFile("hs_" @ %gameType @ ".cs")) {
        echo("ERROR: hs_" @ %gameType @ ".cs doesn't exist.");
        return;
    }
    exec("hs_" @ %gameType @ ".cs");
    
    eval("stringM::explode(\"" @ $hs::nameExport @ "\", \"||\", \"names\");");    
    eval("stringM::explode(\"" @ $hs::scoreExport @ "\", \"||\", \"scores\");");
}

// Export Scores
//-------------------------------------------------------------------------------------------------

function hs::export(%gameType) {
    $hs::nameExport = "$hs::nameExport = \"" @ $names0; 
    $hs::scoreExport = "$hs::scoreExport = \"" @ $scores0;
    %count = 1;
    while(%count != $hs::scoreCount) {
        $hs::nameExport = strCat($hs::nameExport, "||" @ $names[%count]);
        $hs::scoreExport = strCat($hs::scoreExport, "||" @ $scores[%count]);
        %count++;
    }
    $hs::nameExport = strCat($hs::nameExport, "\";");
    $hs::scoreExport = strCat($hs::scoreExport, "\";");
    
    fileWrite("hs_" @ $hs::gameType @ ".cs", overwrite, $hs::scoreExport @ "\n" @ $hs::nameExport);
}