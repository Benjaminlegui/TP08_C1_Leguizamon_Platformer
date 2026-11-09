# TP08 - 2D Platformer

A 2D action platformer inspired by **Hollow Knight**. You play as a knight exploring stone ruins full of skeleton warriors. Find the key hidden in the level and use it to open the chest before you run out of health.

- **Play on itch.io:** _coming soon_
- **Engine:** Unity 6 (6000.3.21f1), Universal Render Pipeline 2D, Input System

## Controls

| Action | Key |
|---|---|
| Move | `A` / `D` |
| Jump (double jump after the power-up) | `Space` |
| Attack | `Q` |
| Heal (uses one potion) | `E` |

## How to play

- Defeat skeletons with your sword. They hurt you with their attacks and on contact.
- Enemies can drop pickups. The first skeleton always drops the **double jump** power-up.
- Collect potions to fill your three heal slots, then press `E` to restore health.
- Find the **key** and bring it to the **chest** to complete the level.
- Your best completion time is saved between sessions.

## Pickups

| Pickup | Effect |
|---|---|
| Red potion | Fills one heal slot. Each slot heals 25 health |
| Sword | Increases attack damage for a few seconds |
| Star | Makes you invulnerable for a few seconds |
| Purple potion | Unlocks the double jump for the rest of the run |
| Key | Opens the chest and completes the level |

## Features

- **Player:** state machine with ground, air, attack, heal, hurt and death states. Health bar, healing, double jump, animations and sounds for every action.
- **Enemies:** patrol and combat states, melee attack, contact damage, health bar, death animation with particles and loot drops.
- **Pickups:** heal, damage boost, invulnerability, double jump and key, spawned from a generic object pool.
- **Audio:** Audio Mixer with Master, Music, Effects and UI groups, volume sliders in Settings and sound effects for every action.
- **Data:** gameplay values in ScriptableObjects (`CharacterConfigSo`, `PickupConfigSo`). Volume and best time saved with PlayerPrefs.
- **Flow:** main menu, settings, credits, gameplay, game over and level complete screens with retry and menu options.

## Project structure

```
Assets/
  Art/          Sprites, animations and fonts
  Audio/        Audio Mixer, music and sound effects
  Data/         ScriptableObject assets (characters and pickups)
  Prefabs/      Player, pickups, effects and UI
  Scenes/       MainMenu and GamePlay
  Scripts/      Characters, combat, pickups, pooling, audio and UI
```

## Credits

**Game:** Benjamin Leguizamon

**Art**

- Knight and Skeleton Warrior - [Xzany](https://xzany.itch.io/)
- Stone Ruins tileset - [Nico Pardo](https://nicopardo.itch.io/stone-ruins)
- Dark Ages UI - [Hypnobius](https://hypnobius.itch.io/dark-ages-ui)
- Pixel Icon Pack - [hayyans](https://hayyans.itch.io/pixel-icon-pack)
- Scrolls - Robert Brooks ([gamedeveloperstudio.com](https://www.gamedeveloperstudio.com))
- Dungeon Font - [vrtxrry](https://vrtxrry.itch.io/dungeonfont)
- Volume slider sprites - author unknown

**Audio**

- RPG Essentials SFX - [Leohpaz](https://leohpaz.itch.io/rpg-essentials-sfx-free)
- Atmospheric Adventures music - [Rosentwig](https://rosentwig.itch.io/atmospheric-adventures-freebie)

All third-party assets are used under the licenses provided by their authors.
