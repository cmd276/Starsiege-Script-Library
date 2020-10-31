# Pilot.cs

Includes all the Pilot information for all the projects.

Feel free to add your own pilots to this file as well.

Here is a base line, weak but aggressive AI.
```cs
Pilot pilotWeakAggressive // Pilot name.
{
   id = 16; 
   // Pilot ID, must be unique.
   
   skill = 0.2;                 
   // Skill 0.1 - 1.0
   
   accuracy = 0.2;              
   // Targeting ability 0.1 - 1.0
   
   aggressiveness = 0.8;        
   // How likely they are to go after a person. 0.1 - 1.0
   
   activateDist = 400.0;
   // How far away a person has to be before the AI tries to attack them.
   // No restriction on distance.
   
   deactivateBuff = 200.0;
   // How far away a person... IDK...
   // No actual restriction
   
   targetFreq = 2.0;
   // How often they readjust their target.
   // In seconds. Lower == faster.
   
   trackFreq = 2.3;
   // How often they update on firing position.
   // Unknown time measurement. (MS/SEC/MIN)
   
   fireFreq = 2.0;
   // How often an AI fires their weapons.
   // Time is in seconds.
   
   LOSFreq = 1.2;
   // How often they check their Line Of Sight.
   // Time is in seconds.
   
   orderFreq = 8.0;
   // How often the AI checks for new orders.
};
```