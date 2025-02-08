# Nex-Level-Tech-Challenge
# Description
This is a VR game for the XR Unity Developer internship at NEX Level Gaming. 
The game tasks you to go through obstacles and defeat enemies to win. With your controllers, you are able to shoot a laser at targets or enemies to damage them. Damaging them will open doors and defeating the last enemy will mean you win.
The enemies can also damage you and if your HP, shown by the green squares on your HUD, is depleted to 0, you lose. The enemies create yellow zones on the floor that damage you if you are standing in them or if you walk into them. Enemies can use simple attack patterns such as a simple rectangle across the room or create large damage patterns that are telegraphed by indicators shown above the enemy.

# Playing the game

The game uses the default VR movement system. So you can use the left controller stick for regular locomotion (forward, back, left, right) and the right controller stick for teleporting movement. Using the trigger buttons on either controller you can shoot a purple laser. There is a green laser to help with aiming. This laser has a small cooldown indicated by the bar under your health filling up. This laser can be used to activate targets or damage enemies which will let you proceed through rooms and win the game.

When you enter new rooms, you will find inactive enemies (grey spheres). Shooting them with a laser will activate them and cause them to start attacking. Attack these enemies while also dodging their attacks to defeat them which is indicated by them turning black. Defeat all enemies to win. 

# Notes
This build is a windows build rather than a WebXR build as the WebXR package had compatibility issues that I did not have time to debug and fix.
