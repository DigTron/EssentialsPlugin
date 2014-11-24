Dedicated Server Essentials - Plugin
====================================
Requirements
------------
This plugin requires SEServerExtender v2.11.20 or above.  Please download and install that first from https://github.com/Tyrsis/SE-Community-Mod-API/releases. 

This plugin requires a workshop mod.  This mod acts as a gateway between the client and server and is required for some functionality to work (private messaging, faction messaging, command hiding): http://steamcommunity.com/sharedfiles/filedetails/?id=340095691

This plugin is available at github at: https://github.com/Tyrsis/EssentialsPlugin/releases

Please most issues you have with the plugin at: https://github.com/Tyrsis/EssentialsPlugin/issues

Overview
--------
This plugin is aimed at being an essential plugin to run on dedicated servers running extender.  It will cover a lot of very basic requirements for server administration.  This plugin looks to really show off how valuable server extender can be, by providing a lot of built in cleaning and adminstrative options and commands.

There are many options in this plugin, and those options will expand as time goes on.  Every section of the plugin can be disabled if desired, to tightly control what an administrator wants to do.

Installation
------------
If you're just using the archive provided, just unzip the archive into it's own sub directory off the Mods directory of your world instance. 

If you want to compile the source provided.  Compile and copy the .dll created after compiling of this project into it's own sub directory of your Mods directory of your instance.  Also move the .sbc files included the .zip archive of any of the releases into that directory as well.

Please make sure to add the associated workshop mod as well.  It is ID: 340095691.  Just add this mod in extender Mods section of the configuration and it will install automatically on restart!

It's that easy!

Major Feature Overview
----------------------

- Automated Backup
- Automated Restart with notifications
- Chat Information commands with interval based repeating
- Automated Join Messages for new and old players 
- Automated new player spawn movement
- Advanced Administrator Commands
- Player Login Tracking
- Private and Faction Messaging
- Chat based settings

In depth Feature Analysis
------------------------

Please note the following commands are all set via the plugin interface.  

Automated Backup
----------------

This is an option that all administrators should use.  It will automatically backup your world save files in a backup directory.  It will also compress them down so they don't take up too much room.  And lastly this option will also cleanup old backups.  

Options:
- BackupEnabled - This allows you to turn Backup off or on
- BackupCleanup - This allows you to turn Cleaning up of the backups off or on
- BackupCleanupTime - The amount of time, in days, that a backup will last before cleaned up
- BackupCreateSubdirectories - This option forces the backup to put a separate backup in a new directory each time it occurs
- BackupAsteroids - Enabling this option will make the backup process include asteroids in the backup file.  If disabled, the .vx2 files will not be saved.
- BackupItems - This is where you define when you want a backup to occur.  You specify the hour and minute of the day you wish the backup to happen.  Items are defined as follows:
  - Enabled - Enable / Disable this backup item
  - Hour - The hour to run this item in the range between 0-23.  If you specify -1 for this option it will run every hour
  - Minute - The minute to run this item in the range between 0-59.

Automated Restart with Notifications
------------------------------------

This option allows you to schedule automated restarts of your server.  Sadly the game is not memory leak proof, and a quick restart can fix a lot of issues.  This option also comes with the ability to notify your users of impending restarts at timed intervals.

Options:
- RestartEnabled - This allows you to turn Restart off or on
- RestartAddedProcesses - This is a multline field that allows you to run things in between restarts.  Each line is a separate process in the restart batch file.
- RestartItems - These items allow you to define notifications that occur before a restart happens. You set a message, you set the minutes before restart the message will be shown, and you can force a save or stop all ships.  They are defined as followed:
  - Message - This is the message that will be broadcasted to all users
  - MinutesBeforeRestart - This is the amount of time before a restart that this message is sent
  - Save - This option allows you to force a save
  - StopAllShips - This option allows you to forcefully stop all ships that are not piloted
- RestartTimeItems - These items allow you to define a time of day of when you'd like a restart to take place.  They are defined as followed:
  - Enabled - Enable / Disable this restart time item
  - RestartTime - 24 hour time of day when this restart should occur

Chat Information Commands with interval based repeats
-----------------------------------------------------

This option allows you to setup commands that users can access that allow administrators to display server information to the user.  The command /info is the base command, and the administrator then defines sub commands to display different types of information.  For example defining a sub command 'motd' that gives a general message to users is setup with a sub command of motd.  The user then types /info motd to see it.  You can then specify if you'd like that message to be displayed for everyone at intervals.  So for example you can set it up to send that message once every few hours, even if a player doesn't type the /info motd command.

Options:
- InformationEnabled - This allows you to turn Information commands off or on
- InformationItems - This lets you define information commands.  Defining an item is pretty simple. 
  - Enabled - Enable / Disable this information item
  - IntervalSeconds - The amount of time it takes for this item to be broadcasted publically.  Set to 0 to not have it broadcast
  - SubCommand - The command a user types to view this information item.  If you leave this blank, users will not be able to view this command via /info, and will only see it if you use it in an interval.
  - SubText - The actual text that is displayed with this item is queried using the /info command or broadcasted.  You may use the %name% tag which gets replaced by the user's name.  This is a multiline text, and each line will be broadcasted individually per interval as well.  So this allows you to setup messages that get sent in order.

Automated Join Messages for new and old players
-----------------------------------------------

This option allows you to greet players with a custom message.  New and old players can receive different messages.  You may also use the %name% tag that will be replaced by a users username.  This allows for a highly customized greeting.

Options:
- GreetingEnabled - Enable / Disable Greetings
- GreetingMessage - Message to normal users.  You may use %name% which gets replaced with the user's name, for personalized greetings.
- NewUserGreetingMessage - Different message to new users.  You can use %name% as well.

Automated New Player Spawn Change
---------------------------------

This option allows you to move players closer to viable asteroids.  A viable asteroid is one that has more than 3 different base materials.  It will then move them closer to that asteroid.  This is useful for servers with asteroids that are very spread out.  Right now the asteroid selected will be random.  More options are coming for this

Options:
- NewUserTransportEnabled - This allows you to turn automated transport off or on.
- NewUserTransportDistance - Distance from a viable asteroid that they will be moved.

Player Login Tracking
---------------------
This option allows administrators to track user logins.  This allows administrators to delete grids by owners who no longer login.  The first time this is run, it will scan your server logs, and extract older login information so that your login list is up to date.  

Options:
- LoginEnabled - Enable / Disable player login tracking.  It is recommended to enable this as it adds a lot of functionality.
- LoginEntityIdWhitelist - This is a list of entities that will never be considered "inactive".  This allows administrators to protect grids from inactivity scans / deletions
- LoginPlayerIdWhitelist - This is a list of player ids that will never be considered "inactive".  This allows administrators to protect player grids from inactivity scans / deletions. Please note this is IDs and not player names.  

Chat Based Settings
-------------------
All settings are set through the UI, but settings can also be done via chat.  Please use the command '/admin settings' to set settings.  You can set settings using the set subcommand.  You can modify arrays by using the add or remove command.  You set sub settings items using the set command as well, examples:

### List Examples
- /admin settings - this list all the available settings
- /admin settings BackupItems - This lists all the backupitems defined on the server
- /admin settings BackupEnabled - This lists if backups are enabled. 
- /admin settings <settingItem> - This lists the value for <settingItem> replace <settingItem> with any available setting

### Set Examples
- /admin settings BackupCleanupTime set 5 - This sets the option BackupCleanupTime to 5
- /admin settings BackupItems.1.Enabled set true - This enables backupitem #1 to enabled.  (Backupitems can have - multiple items defined for them, each item is an item in the list of BackupItems)

### Add Examples
- /admin settings BackupItems add - This adds a new default item to backup items
- /admin settings InformationItems add - This adds a new default information item to the information item list

### Remove Example
- /admin settings BackupItems remove 0 - This removes the item at position 0 from the BackupItems list
- /admin settings InformationItems remove 1 - This removes the item at position 1 from the InformationItems list

Advanced Administrator Chat Commands
------------------------------------

We've added new administrator commands that we will expand upon slowly.  They will aid in moving grids and stations around, along with trying to keep things clean.

For options, do not include braces.  [] stand for required, while () stand for optional.

Scan Commands
-------------
Command| Options|Example
-------|--------|-----------------------------------------------------------------------------------------------------
/admin scan area at | [x] [y] [z] [radius] | /admin scan area at 0 0 0 1000 - This will scan for all ships and stations at position 0 0 0 within 1000m of that point.
/admin scan area towards | [sX] [sy] [sz] [tx] [ty] [tz] [distance] [radius] | /admin scan area towards 10000 0 0 0 0 0 5000 1000 - This will scan for all ships and stations at position 5000 0 0 within a 1000m radius.  It basically starts at position (10000,0,0) and moves towards (0,0,0) by 5000 meters (which is (5000,0,0)).  This is useful when moving ships a certain distance in a direction, and this allows you to scan the area before moving them to ensure nothing is there.
/admin scan nobeacon | (no options) | /admin scan nobeacon - This command scans for ships and stations that have no beacons.  This allows you to preview a list of ships before running the cleanup on it in case something is wrong.
/admin scan inactive | [days] optional: [ignoreownerless] [ignorenologin] | /admin scan inactive 20 - This command scans for ships with owners who haven't logged in in 20 days.  If you specify the ignoreownerless option it ignores grids without owners.  If you specify the ignorenologin option it ignores grids owned by players with no login information.  The /admin delete inactive command works exactly as this command, so you can view what will be deleted with the scan command before commiting to a delete.
/admin scan entityid | [entityid] | /admin scan entityid 4384938458 - This scans for grid with the entityid of 4384938458


Move commands
-------------
Command| Options|Example
-------|--------|-----------------------------------------------------------------------------------------------------
/admin move player position | [username] [x] [y] [z] | /admin move player position tyrsis 0 0 0 - This moves a player 'tyrsis' to position 0 0 0.
/admin move player to | [sourceUsername] [targetUsername or targetGridname] (distance) | /admin move player to tyrsis vicious 500 - This moves player 'tyrsis' near player 'vicious' within 500m.  Please note that player 'tyrsis' must be in a space suit for this to work (out of cockpit).
/admin move area to position | [sx] [sy] [sz] [tx] [ty] [tz] [radius] | /admin move area to 10000 10000 10000 20000 20000 20000 5000 - This would move all ships and stations that are within 5000m of (10000,10000,10000) and move them towards (20000,20000,20000) relative to where they were before they were moved in relation to the original point. So if a ship was 100m from (10000,10000,10000) they would be 100m from (20000,20000,20000) after the move.
/admin move area towards | [sx] [sy] [sz] [tx] [ty] [tz] [radius] | /admin move area towards 20000 0 0 0 0 0 5000 1000 - This command would move all ships within 1000m of point (20000,0,0) towards (0,0,0) and move them 5000m.  So a ship at (20000,0,0) would be moved to (15000,0,0).

Delete commands
---------------
Command| Options|Example
-------|--------|-----------------------------------------------------------------------------------------------------
/admin delete grids area | [x] [y] [z] [radius] | - Deletes all ships and stations in the sphere of radius at position x, y, z
/admin delete ships area | [x] [y] [z] [radius] | - Deletes all ships in the sphere of radius at position x, y, z
/admin delete stations area | [x] [y] [z] [radius] | - Deletes all stations in the sphere of radius at position x, y, z
/admin delete nobeacon | (no options) | /admin delete nobeacon - Deletes all ships that have no beacons.  This checks to see if ships are connected via - connector, piston or rotor.
/admin delete inactive | [days] optional: [ignoreownerless] [ignorenologin] | /admin delete inactive 20 - This deletes all grids owned by users who have no logged in in 20 days.  If you specify the "ignoreownerless" option it will not include ownerless grids.  If you specify the "ignorenologin" option it will not include grids of users with no login information (for example if you don't have logs old enough for them to have logged in)

Ownership commands
------------------
Command| Options|Example
-------|--------|-----------------------------------------------------------------------------------------------------
/admin ownership change | [username] [entityId] | /admin ownership change tyrsis 4949392 - This will change grid #494392 and make the owner 'tyrsis'

Utility commands
------------------
Command| Options|Example
-------|--------|-----------------------------------------------------------------------------------------------------
/pos | (no options) | /pos - This gives the user his current X, Y, Z position in the world
/timeleft | (no options) | /timeleft - this gives the user the amount of time remaining before the next scheduled restart
/msg | [username] [message] | /msg tyrsis testing a private message - This will send a private message to the user 'tyrsis' with the message 'testing a private message'.  This command requires the workshop mod to function properly.
/faction | [message] | /faction hello everyone in my faction - This will send a private faction only message to all users in the same faction as the user sending it.  
/utility grids list | (page number) | /utility grids list 1 - This lists all your grids by name and id.  If you have more than 7 ships, the ships are separated into pages.  Use a number after /utility grids list to list that specific page.
/utility export server | [ship name] | /utility export server My Ship - This exports the ship "My Ship" to the server.  The ships are exported to an "Exports" directory in the mods directory of the server under the username of the user who exported it.

To come:
- Block delete commands (over limit of drills, prohibited blocks, etc)
- Grid delete commands with more attributes (no power)
- More cleanup commands
- Commands that disable blocks, for example thrusters

