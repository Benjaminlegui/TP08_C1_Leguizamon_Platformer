# Corrección, Trabajo Práctico N°08, Platformer 2D

- **Alumno/a:** Leguizamon (Benjamin Martin Leguizamon)
- **Entrega:** `TP08_C1_Leguizamon_Platformer`
- **Materia:** Programación con Motores de Videojuegos I
- **Nota TP08:** 8

## 1. Checklist de requisitos

### General

| #  | Requisito | Estado | Evidencia |
|----|-----------|--------|-----------|
| G1 | Repositorio `TP08_C1_Apellido_Titulo` | ✓ | Carpeta `TP08_C1_Leguizamon_Platformer`, `.git/` con remoto `origin/main` |
| G2 | Proyecto de Unity con el mismo nombre | ✓ | La raíz del repo es el proyecto (`Assets/`, `ProjectSettings/`). El `productName` en `ProjectSettings/ProjectSettings.asset:16` es `Stone Ruins Knight`, que es el nombre del build, no del proyecto |
| G3 | Al menos 2 escenas: MainMenu y Gameplay | ✓ | `Assets/Scenes/MainMenu.unity` y `Assets/Scenes/GamePlay.unity` (más `Splash.unity`), las tres en `ProjectSettings/EditorBuildSettings.asset` |
| V1 | Publicado en Itch.io y presentable | ✓ | Publicado. Lo revisé en itch.io el 2026-11-16 |
| V2 | Itch.io jugable en web y descargable para Windows | ✓ | Carga la build web y tiene descarga para Windows |

### Jugador

| #  | Requisito | Estado | Evidencia |
|----|-----------|--------|-----------|
| J1 | Se mueve a derecha e izquierda | ✓ | `Player.cs:105` (`OnMove`), con la velocidad en `Player.cs:72` y `Player.cs:93` |
| J2 | Salta | ✓ | `Player.cs:77-89`, con la altura de `CharacterConfigSo.JumpHeight` en `AirState.cs:18` |
| J3 | Puede dañar enemigos | ✓ | `AttackState.cs:50-61` con `Physics2D.OverlapCircle` y `Health.TakeDamage` |
| J4 | Barra de vida | ✓ | `UIHealthBar.cs` en el HUD de `GamePlay.unity` |
| J5 | Puede curarse | ✓ | `HealState.cs:46-57` consume una carga y llama a `Health.Heal` con `HealAmount` |
| J6 | Todas sus acciones animadas y con sonido | ✓ | Clips en `Assets/Art/Animations/Player/` (Idle, Run, Jump, JumpToFall, Fall, Attack, Heal, Hurt, Death). En `Player.prefab` están asignados `attackSound`, `footstepSound`, `jumpSound`, `landSound`, `hitSound`, `healSound` y `deathSound` |

### Enemigos

| #  | Requisito | Estado | Evidencia |
|----|-----------|--------|-----------|
| E1 | Dañan al jugador | ✓ | `AttackState` reutilizado en `CombatState.cs:44-49`, y daño de contacto en `BaseEnemy.cs:60-71` |
| E2 | Poseen barra de vida | ✓ | Instancia de `Prefabs/UI/UIWorldHealthBar.prefab` como hija del `Skeleton` en `GamePlay.unity` |
| E3 | Feedback visual al morir (partículas + animación) | ✓ | `DeathState` con `Assets/Art/Animations/NPCs/Skeleton/Death.anim`, y `CharacterFeedback.deathEffect` apunta a `Prefabs/Effects/DeathEffect.prefab` en `GamePlay.unity`. Además suelta loot (`LootDropper.cs`) |
| E4 | Todas sus acciones animadas y con sonido | Parcial | Animaciones Idle, Walk, Attack1/2, Hurt y Death. Ataque, golpe y muerte tienen sonido en `GamePlay.unity`, pero en los dos `MoveState` del enemigo el `footstepSound` está vacío en la escena: caminar es silencioso |

### Pickables

| #  | Requisito | Estado | Evidencia |
|----|-----------|--------|-----------|
| K1 | Heal | ✓ | `HealthPickup.cs` suma una carga a `HealState`, que cura con `E` |
| K2 | Damage (aumenta daño) | ✓ | `DamagePickup.cs` llama a `Character.ApplyDamageBoost` con los valores de `PickupConfig.asset` (x1.5 durante 10 s) |
| K3 | Invulnerability por x segundos | ✓ | `InvulnerabilityPickup.cs` llama a `Health.SetInvulnerable(config.InvulnerabilityDuration)` (5 s en `PickupConfig.asset`) |
| K4 | Reward: monedas para el shop | Parcial | No hay monedas. Tenés la opción "Keys", que la consigna marca como opcional (`KeyPickup.cs`, `Chest.cs`), pero no reemplaza a las monedas |

### Sonido

| #  | Requisito | Estado | Evidencia |
|----|-----------|--------|-----------|
| S1 | AudioMixer con Master, Music, FX y UI | ✓ | `Assets/Audio/GameAudio.mixer` con los grupos Master, Music, Effects y UI, y 4 parámetros expuestos |
| S2 | Al menos 3 grupos con sliders en Settings | ✓ | `AudioSettingsUI.cs` con `masterSlider`, `musicSlider` y `effectsSlider`, aplicado en `AudioManager.ApplyVolume` |
| S3 | Toda acción del juego se refleja en sonido | ✓ | Música en menú y gameplay, sonidos de UI (`UIMenuButton`, `UIPanel`), pickups, cofre (`Chest.cs:10-11`) y acciones del jugador. La falta de pasos del enemigo está marcada en E4 |

### Datos

| #  | Requisito | Estado | Evidencia |
|----|-----------|--------|-----------|
| D1 | Configuraciones del juego en ScriptableObject | ✓ | `CharacterConfigSo` (`PlayerConfig.asset`, `SkeletonConfig.asset`) y `PickupConfigSo` (`PickupConfig.asset`). Queda hardcodeado `waitTime` en `PatrolState.cs:10` (ver Suggestion) |
| D2 | Datos de partida en PlayerPrefs | ✓ | `RunTimer.cs:6` y `RunTimer.cs:37` guardan el mejor tiempo; `AudioManager.cs:9-11` guarda los volúmenes |

### Extras

| #  | Extra | Estado | Evidencia |
|----|-------|--------|-----------|
| X1 | Doble salto | ✓ | `Player.cs:83-88` con `airJumps`, que se desbloquea con `AirJumpPickup` (`airJumps: 1` en `Pickup_DoubleJump.prefab`) |
| X2 | Triple salto por power up | ✗ | El único power up de salto da `airJumps: 1` |
| X3 | Tienda con monedas | ✗ | No hay monedas ni tienda |
| X4 | Integración PlayFab | ✗ | No hay referencias a PlayFab |
| X5 | Multijugador local | ✗ | Un solo `Player` en la escena |

## 2. Cumplimiento de Pautas de Cursada

| ID | Pauta | Estado | Evidencia |
|----|-------|--------|-----------|
| P1 | Unity 6000.3.21f1 | ✓ | `ProjectSettings/ProjectVersion.txt` dice `6000.3.21f1` |
| P2 | Repo público en GitHub | ✓ | El repositorio es público (revisado el 2026-11-16) |
| P3 | Nombre del repo | ✓ | `TP08_C1_Leguizamon_Platformer` |
| P4 | Código en inglés | ✓ | Clases, métodos, campos, escenas y strings de UI en inglés en los 59 scripts |
| P5 | Un repo por TP, sin branches | ✓ | Solo `main`, 43 commits, sin carpetas de otros TPs |

## 3. Estructura de carpetas

| Carpeta esperada | Encontrada | Observación |
|------------------|------------|-------------|
| `Art/Animations/` | ✓ | `Player/`, `NPCs/Skeleton/` |
| `Art/Audio/` | ✗ | El audio está en `Assets/Audio/` |
| `Art/Fonts/` | ✓ | |
| `Art/Materials/` | ✗ | El material físico está en `Assets/Materials/Physics/` |
| `Art/Sprites/` | ✓ | Background, Icons, NPCs, Player, Tiles, UI |
| `Data/` | ✓ | `NPCs/`, `Pickups/`, `Player/` |
| `Prefabs/` | ✓ | `Characters/`, `Effects/`, `Pickups/`, `UI/` |
| `Scenes/` | ✓ | |
| `Scripts/` | ✓ | Subcarpetas por sistema (`Audio`, `Characters`, `Combat`, `Pool`, `UI`, etc.) |
| `Scripts/Data/` | ✓ | |
| `Scripts/Ui/` | ✓ | Como `Scripts/UI/` |

Archivos mal ubicados:

- `Assets/Audio/` (mixer, música y SFX) va en `Assets/Art/Audio/`.
- `Assets/Materials/Physics/PlayerMaterial.physicsMaterial2D` va en `Assets/Art/Materials/`.

Carpetas extra: `Assets/Settings/` (URP y `Player.inputactions`), que la genera la plantilla URP. No penaliza.

El `.gitignore` de Unity está, y no versionaste `Library/` ni `Temp/`.

## 4. Qué le falta

- Monedas como recompensa (K4) y, con ellas, la tienda (extra X3).
- Sonido de pasos del enemigo: asigná `footstepSound` en los dos `MoveState` del `Skeleton`.
- Bloquear el input del jugador mientras el juego está pausado (`Player.cs:53`).
- Limitar el evento `Blocked` para que el contacto continuo no repita el sonido en cada paso de física (`Health.cs:35`).
- Mover `Assets/Audio/` y `Assets/Materials/` dentro de `Assets/Art/`.

## 5. Hallazgos de código

### Error (0)

### Bug (2)

- En `Assets/Scripts/Characters/Player/Player.cs:53` el `Update` corre con `Time.timeScale = 0`: en pausa, atacar o curarse entra al estado, reproduce el sonido y deja el estado trabado hasta que despausás.
- En `Assets/Scripts/Combat/Health.cs:35` `Blocked` se invoca en cada `TakeDamage`. Con el daño de contacto de `OnCollisionStay2D`, el jugador invulnerable dispara `blockedSound` en cada paso de física.

### Warning (4)

- `Assets/Scripts/Characters/Enemies/BaseEnemy.cs:68` hacés `GetComponent<Health>()` en cada paso de física dentro de `OnCollisionStay2D`.
- `Assets/Scripts/Characters/Character.cs:44` comparás floats con `==` en `Face`.
- `Assets/Scripts/Characters/Player/States/GroundState.cs:27` comparás floats con `==` en `SelectState`.
- `Assets/Scripts/UI/UIMenuButton.cs:13` llamás a `FindAnyObjectByType<AudioManager>()` en el `Awake` de cada botón (lo mismo en `UIPanel.Awake`).

### Suggestion (6)

- `Assets/Scripts/Characters/Enemies/States/PatrolState.cs:9` `waitTime = 2f` está hardcodeado y no se serializa.
- `Assets/Scripts/Characters/Enemies/States/PatrolState.cs:11` el campo `lastDirection` y el `using System.Collections` no se usan.
- `Assets/Scripts/Characters/Player/States/AirState.cs:77` la variable local `info` no se usa.
- `Assets/Scripts/Characters/Enemies/EnemySensor.cs:7` `wallLayer` está serializado y no se usa.
- `Assets/Scripts/StateMachine/Context.cs:12` las properties están en camelCase (`machine`, `isComplete`, `time`).
- `Assets/Scripts/Characters/Generics/GroundCheck.cs:1` `using System` sin uso.

Esto no lo cuento como hallazgo: el `Debug.LogError` en `AudioManager.cs:111` y el `Debug.LogWarning` en `PoolRegistry.cs:29` solo saltan ante un error de configuración y no llenan la consola.

## 6. Recomendaciones

1. Agregá monedas como pickup de recompensa, guardalas en `PlayerPrefs` y, si podés, sumá una tienda simple (K4 y X3).
2. Asigná `footstepSound` en los `MoveState` del enemigo.
3. En `Player.Update`, salí temprano cuando `Time.timeScale == 0` o desactivá el `PlayerInput` al abrir la pausa.
4. Poné un cooldown al evento `Blocked` en `Health`, o aplicá el daño de contacto con `OnCollisionEnter2D` más un temporizador.
5. Cacheá la `Health` del jugador en `BaseEnemy` y reemplazá los `==` de float por `Mathf.Approximately`.
6. Pasá `waitTime` a `CharacterConfigSo` y borrá `lastDirection`, `wallLayer`, la variable `info` y los `using` que no usás.
7. Mové `Assets/Audio/` y `Assets/Materials/` dentro de `Assets/Art/`.
