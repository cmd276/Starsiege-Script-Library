# WilzuunLoader.cs

This script activates during the MissionOnPreload command.

It'll load all the needed functions and resources that each individual script needs.

If a game has settings, it will load the default settings, then call an override function, allowing map makers, and dedicated server hosters to modify how the game type settings are done.
Then after the override is done, it will load all the functions for the game type.

