# Changelog

API changes / Backward compatible features / Backward compatible bug fixes.
You do not need to update the version for refactors or code improvements that
do not change functionality nor do you need to when you you write tests.


## [1.5.1] - 2026-03-31

### Fixed 

- Restored design setup of parietal minigame view approved by stakeholders


## [1.5.0] - 2026-03-22

### Added

- Added the current implementation of the frontal lobe minigame

## [1.4.1] - 2026-03-22

- Persistent XR rig
- Service.prefab which mounts all our services and managers
## [1.4.0] - 2026-03-22

### Added

- Added unfinished mini game signs to all scenes except Frontal Lobe scene

## [1.3.5] - 2026-03-22

### Added

- Added (and modified pre-existing) tooltips on all brain regions and portals

## [1.3.4] - 2026-03-22

### Fixed

- Fixed overlapping ScaleOnHover system on brain region hover. Now, it only scales one brain region at a time.


## [1.3.3] - 2026-03-22

### Added

- dll files so everyone can use FsCheck without installing it themselves

## [1.3.2] - 2026-03-19

### Fixed

- Adjusted ToolTips To bottom-center of view to reduce clipping.
- Allow only one ToolTip to appear at a time.

### Added

- A new Brain component that accesses the spin animation attached to the big 3D brain model in the Hub
- With the ScaleOnHover System, along with the new Brain component, the 3D brain pauses spin animation and scales brain region on hover

## [1.3.1] - 2026-03-17

### Fixed

- Updated project settings to enable building of the game

## [1.3.0] - 2026-03-17

### Added

- Portals now transport the player to its target scene.
- Below are the listed portals and their target scenes:
  - Hub <---> Frontal Lobe
  - Hub <---> Parietal Lobe
  - Hub <---> Occipital Lobe
  - Hub <---> Temporal Lobe
  - Hub <---> Cerebrum
  - Cerebrum ---> Frontal Lobe
  - Cerebrum ---> Parietal Lobe
  - Cerebrum ---> Occipital Lobe
  - Cerebrum ---> Temporal Lobe

## [1.2.0] - 2026-03-17

### Added

- ToolTips (UI educational display)

## [1.1.1] - 2026-03-08

### Added

- Room MVC

### Fixed

- This section is for bug fixes and patches

## [1.1.0] - 2026-03-05

### Added

- Room prefabs and scenes for 5 brain regions:
  - Cerebrum
  - Frontal Lobe
  - Parietal Lobe
  - Occipital Lobe
  - Temporal Lobe

## [1.0.0] - 2026-02-25

### Added

- Doors (player teleportation and scene changing)
- The hub scene (with the brain display)
- See minor versions which cumulate into this major version bump

## [0.2.0] - 2026-02-25

### Added

- Created the main 3D model of the brain for the hub
- All doors are in main hub, but not implemented for scene change
- XR Interactions were added to simulate headset capabilities
- All interfaces are defined and implemented for their methods
- Added tests for all applicable methods and files

### Changed

- Project settings and packages were either downgraded or removed

### Fixed

- Many comment format errors were fixed
- A lot of LogAssert's not being in the test files

## [0.1.0] - 2026-02-18

### Added

- Added reusable door prefab, teleportation logic

## [0.0.2] - 2026-02-18

### Changed

- Tweaked package installs slightly so that the build is less strict (and less
  fragile)

## [0.0.1] - 2026-02-18

### Fixed

- XRI errors that were triggering from improperly versioned sample code

## [0.0.0] - 2026-02-17

### Added

- The semantic versioning files (changelog.md and VERSION)
- The code is in the ID1 State, you could call it version 1.0.0 but we don't
  really have anything in the main branch, it lives in the Spike prototype
  branches. So we will make ID2 version 1.0.0

## [reference] - YYYY-MM-DD

### Added

- This section is for new functionality added

### Changed

- This is for when functionality changes but there isn't new stuff being added

### Fixed

- This section is for bug fixes and patches
