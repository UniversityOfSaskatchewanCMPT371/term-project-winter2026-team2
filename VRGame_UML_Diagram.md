# VRGame - Structural UML Diagram

```plantuml
@startuml VRGame_Architecture

' Style configuration
skinparam classAttributeIconSize 0
skinparam packageStyle rectangle
skinparam linetype ortho

' ============================================
' DOOR MODULE (MVC Pattern)
' ============================================
package "Door Module" #LightBlue {
    interface IDoorController {
        +TriggerDebounce: bool {get}
        +OnPlayerEnter(player: IPlayerController): void
        +Init(): void
    }
    
    interface IDoorModel {
        +DoorId: int {get, set}
        +TargetDoorId: int {get, set}
        +DestinationSceneId: int {get, set}
        +TeleportOffset: Vector3 {get, set}
        +GetTargetDoor(): IDoorModel
        +GetTeleportPosition(): Vector3
        +GetTeleportRotation(): Quaternion
        +Init(): void
    }
    
    interface IDoorView {
        +Init(): void
        +OnTriggerEnter(other: Collider): void
        +OnTriggerEnterLogic(colliderWrapper: IColliderWrapper): void
    }
    
    class DoorController implements IDoorController {
        -serializableDoorModel: MonoBehaviour
        -doorModel: IDoorModel
        -serializableSceneChangerController: MonoBehaviour
        -sceneChangerController: ISceneChangerController
        -{static} triggerDebounce: bool
        +DoorModel: IDoorModel {set}
        +SceneChangerController: ISceneChangerController {set}
    }
    
    class DoorModel implements IDoorModel {
        -{static} doorLookup: Dictionary<int, IDoorModel>
        -doorId: int
        -targetDoorId: int
        -destinationSceneId: int
        -teleportOffset: Vector3
        +ResetDoorLookup(): void
        -Start(): void
    }
    
    class DoorView implements IDoorView {
        -serializableDoorController: MonoBehaviour
        -doorController: IDoorController
        +DoorController: IDoorController {set}
        -Start(): void
    }
}

' ============================================
' PLAYER MODULE (MVC Pattern)
' ============================================
package "Player Module" #LightGreen {
    interface IPlayerController {
        +Awake(): void
        +teleportPlayerTo(position: Vector3, rotation: Quaternion): void
    }
    
    interface IPlayerModel {
        +getPlayerName: string {get, set}
        +getPlayerId: int {get, set}
        +playerIsAlive: bool {get, set}
        +Initialize(name: string, id: int): void
    }
    
    interface IPlayerView {
    }
    
    class PlayerController implements IPlayerController {
        -model: PlayerModel
        -view: PlayerView
    }
    
    class PlayerModel implements IPlayerModel {
        -playerName: string
        -id: int
        -alive: bool
    }
    
    class PlayerView implements IPlayerView {
    }
}

' ============================================
' SCALE ON HOVER MODULE (MVC Pattern)
' ============================================
package "ScaleOnHover Module" #LightYellow {
    interface IScaleOnHoverController {
        +Start(): void
        +Init(): void
        +retrieveLinkedObjects(): Transform[]
        +retrieveTargetScale(): Vector3[]
        +retrieveScaleSpeed(): float
        +IsHovering(): bool
        +OnHoverEnter(): void
        +OnHoverExit(): void
    }
    
    interface IScaleOnHoverModel {
        +LinkedObjects: Transform[] {get, set}
        +HoverScaleMultiplier: float {get, set}
        +ScaleSpeed: float {get, set}
        +NormalScales: Vector3[] {get}
        +TargetScales: Vector3[] {get, set}
        +IsHovering: bool {get}
        +Initialize(linkedObjects: Transform[], hoverScaleMultiplier: float, scaleSpeed: float): void
        +InitializeScales(): void
        +OnHoverEnter(): void
        +OnHoverExit(): void
        +Awake(): void
    }
    
    interface IScaleOnHoverView {
        +Start(): void
        +SetupXREvents(): void
        +OnXRHoverEnter(args: HoverEnterEventArgs): void
        +OnXRHoverExit(args: HoverExitEventArgs): void
        +Init(): void
        +OnHoverEnter(): void
        +OnHoverExit(): void
        +Update(): void
    }
    
    class ScaleOnHoverController implements IScaleOnHoverController
    class ScaleOnHoverModel implements IScaleOnHoverModel
    class ScaleOnHoverView implements IScaleOnHoverView
}

' ============================================
' SCENE CHANGER SERVICE (Singleton)
' ============================================
package "SceneChanger Service" #LightCoral {
    interface ISceneChangerController {
        +LoadDebounce: bool {get}
        +LoadScene(sceneKey: int): IAsyncOperationWrapper
        +ResetInstance(): void
        +Init(): void
    }
    
    class SceneChangerController implements ISceneChangerController {
        -{static} instance: SceneChangerController
        -sceneManagerWrapper: ISceneManagerWrapper
        -{static} loadDebounce: bool
    }
}

' ============================================
' WRAPPERS (Testability Layer)
' ============================================
package "Wrappers" #Lavender {
    interface IColliderWrapper {
        +GetPlayerFromParent(): IPlayerController
        +CompareGameObjectTag(tag: string): bool
    }
    
    interface ISceneManagerWrapper {
        +LoadSceneAsync(sceneKey: int): IAsyncOperationWrapper
    }
    
    interface IAsyncOperationWrapper {
        +Completed: Action<IAsyncOperationWrapper>
    }
    
    class ColliderWrapper implements IColliderWrapper {
        -collider: Collider
    }
    
    class SceneManagerWrapper implements ISceneManagerWrapper
    
    class AsyncOperationWrapper implements IAsyncOperationWrapper {
        -asyncOperation: AsyncOperation
    }
}

' ============================================
' ENUMS & UTILITIES
' ============================================
package "Enums & Utilities" #LightGray {
    enum SceneEnum {
        Hub = 0
    }
}

' ============================================
' RELATIONSHIPS - Door Module
' ============================================
DoorController --> IDoorModel : uses
DoorController --> ISceneChangerController : uses
DoorView --> IDoorController : uses
DoorView --> IColliderWrapper : uses
DoorController --> IPlayerController : interacts with
DoorModel ..> SceneEnum : validates against

' ============================================
' RELATIONSHIPS - Player Module
' ============================================
PlayerController --> IPlayerModel : manages
PlayerController --> IPlayerView : manages

' ============================================
' RELATIONSHIPS - ScaleOnHover Module
' ============================================
ScaleOnHoverController --> IScaleOnHoverModel : uses
ScaleOnHoverView --> IScaleOnHoverController : uses

' ============================================
' RELATIONSHIPS - SceneChanger Service
' ============================================
SceneChangerController --> ISceneManagerWrapper : uses
SceneChangerController ..> SceneEnum : uses
ISceneManagerWrapper --> IAsyncOperationWrapper : returns

' ============================================
' RELATIONSHIPS - Wrappers
' ============================================
ColliderWrapper --> IPlayerController : retrieves

' ============================================
' CROSS-MODULE DEPENDENCIES
' ============================================
DoorController ..> IAsyncOperationWrapper : uses

' Notes
note right of DoorController
  Singleton pattern with
  static triggerDebounce
end note

note right of SceneChangerController
  Singleton service for
  scene management
end note

note right of DoorModel
  Static doorLookup dictionary
  for all door instances
end note

note bottom of "Wrappers"
  Wrapper classes enable
  unit testing by abstracting
  Unity's non-mockable types
end note

@enduml
```

## Architecture Overview

### Design Patterns Used

1. **MVC (Model-View-Controller)**
   - Door Module: Complete MVC implementation
   - Player Module: Complete MVC implementation
   - ScaleOnHover Module: Complete MVC implementation

2. **Singleton Pattern**
   - SceneChangerController: Persistent singleton service
   - DoorModel: Static lookup table for all doors

3. **Wrapper Pattern**
   - ColliderWrapper, SceneManagerWrapper, AsyncOperationWrapper
   - Purpose: Enable unit testing by wrapping Unity's non-mockable types

### Module Descriptions

#### Door Module
Handles door interactions and scene transitions. When a player enters a door, it:
- Validates the destination scene
- Loads the target scene asynchronously
- Teleports the player to the target door's position

#### Player Module
Manages player state and behavior including:
- Player identification (name, ID)
- Player status (alive/dead)
- Teleportation functionality

#### ScaleOnHover Module
Provides interactive scaling effects for VR objects:
- Detects XR ray interactor hover events
- Smoothly scales linked objects on hover
- Returns to normal scale on hover exit

#### SceneChanger Service
Singleton service managing scene loading:
- Prevents multiple simultaneous scene loads
- Wraps Unity's SceneManager for testability
- Uses async operations for smooth transitions

#### Wrappers
Abstraction layer for Unity types to enable unit testing:
- ColliderWrapper: Wraps Unity Collider
- SceneManagerWrapper: Wraps Unity SceneManager
- AsyncOperationWrapper: Wraps Unity AsyncOperation

### Key Architectural Features

1. **Interface-Driven Design**: All modules use interfaces for loose coupling
2. **Testability**: Wrapper pattern enables comprehensive unit testing
3. **Separation of Concerns**: Clear MVC separation in each module
4. **Unity Integration**: SerializeField wrappers for Unity Inspector compatibility
5. **Type Safety**: Enum-based scene identification
