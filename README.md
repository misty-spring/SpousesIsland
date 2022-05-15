**Spouses' Island** is a Stardew Valley mod, which allows the player's spouse to visit the island. Comes with integrated schedules for the vanilla spouses, and offers support for custom schedules (including mod NPCs).

To choose if it's an island day, this mod uses a randomized number- and compares it with the chance(%) set by the user. This number changes every in-game day.

## Contents
* [For players](#for-players)
  * [Install](#install)
  * [Uninstall](#uninstall)
  * [Compatibility](#compatibility)
* [For mod authors](#for-mod-authors)
  * [How to use](#how-to-use)
  * [For more help](#for-more-help)
* [Translations](#translations)
* [Known issues](#known-issues)

## For players
### Install
1. [Install the latest version of SMAPI](https://smapi.io/).
2. Install [this mod from Nexus Mods](https://www.nexusmods.com/stardewvalley/mods/11037).
3. Install [Non Destructive NPCs](https://www.nexusmods.com/stardewvalley/mods/5176) and [Custom NPC Exclusions](https://www.nexusmods.com/stardewvalley/mods/7089).
4. Run the game using SMAPI.
(Optional) if you want to use content packs for this mod, just drop them in 'Mods' and they'll be automatically recognized.

### Uninstall
Remove this mod from the game's 'Mods' folder (or, if placed in a sub-folder, remove it from that one).

### Compatibility
Works with Stardew Valley 1.5.5 on Linux/macOS/Windows. (Requires SMAPI 3.14)

The following mods are confirmed to be compatible:
* Child To NPC
* Free Love
* Stardew Valley Expanded
* Immersive spouses
* ContentPatcher mods

This mod only edits schedules on specific days (and uses unique dialogue keys), it should work with most mods.

## For mod authors
### How to use
To use the framework, you need to add this in the content pack's `manifest.json`:
```js
"ContentPackFor":{
	"UniqueID": "mistyspring.spousesisland", 
	"MinimumVersion": "3.0.0" 
	},
```
...and Spouses' Island will recognize your mod automatically.

Then, you can use [this](https://github.com/misty-spring/SpousesIsland/blob/main/content_template.json) as a template for the content.json. For more information, check [Creating a Content Pack](https://github.com/misty-spring/SpousesIsland/blob/main/creating-content-pack.md).

Lastly, just put those two files inside a folder and name it like you'd do for any other mod.

### For more help

* If you're facing issues with the formatting
The [json parser](https://smapi.io/json) will tell you of any problem within the content pack. When asked for a JSON format, leave it as "None".

## Translations
If you'd like to contribute translating to the mod, there's a few options:
* Patching the mod via ContentPatcher (this allows you to translate dialogue, but commands will stay in english)
* Making a i18n .json file (for a template(empty), see [here](https://github.com/misty-spring/SpousesIsland/blob/main/i18n_template.json). For the default i18n file, see [here](https://github.com/misty-spring/SpousesIsland/blob/main/SpousesIsland/i18n/default.json).)
The second option gives you full access to translation. If you choose that last option: you can upload said translation on nexus, or you can send it to me for direct implementation (credit for translation will be mantained).
## Known issues
Children can't use NPC warps. However, this is a bug on ChildToNPC's side (which i can't do much about).
