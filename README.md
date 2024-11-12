# Core Foundation

### Motive
We want the Game Architecture to be easily translatable to Business Values. In Games, our Values come in the form of Game Features. Thus our Core Foundation aims to orient the Architecture around Solid Features.

### Brief
In the Core Foundation, we Split the Game into Features and Services. Features can use the help of Services, but Services are Agnostic to Features. Each Feature is also Split into Three Arch Types. The Feature itself is the host of the Logical Code, or Feature Design. The Visual part will collaborate with the UnityEngine and properly manage the Unity Components on a discrete GameObject. And lastly the Records will serve as Data Hubs for the Feature. We can also Add to the Feature Configurations and Register to Agents.

_Features Using Services Example_
![Feature Service Usage Example](https://github.com/user-attachments/assets/c9a4434f-1e49-4ed4-b5b5-a8d3edda5563)

### Overview
- Empty Scene
- Bootstrapping
- Recommended Assembly + Folder Setup
- Features
- Records
- Visuals
- [Inject] Attribution
- Configurations
- Code Generation
- Factories
- Services
- Agents
- Sequence Flow


## Empty Scene
Our Main Scene should look something like This

![image](https://github.com/user-attachments/assets/7c7bb840-ca26-434a-ba72-3dc581d2289b)

This way All Our Features have a Managed Lifecycle.
Also we set a Convention that is healthy for the Project long term regarding Visual Creation.

Otherwise, Managing a Game where All Features are Present in the Scene leads to -> Merge Conflicts, Dilemas and Race Condition.

The Principle for Empty Main Scene will work well with the the Core Foundation



## Bootstrapping

- A Central Script That Initiates the Game and all the Foundation. The Boostrapping is the process of Setting up all the Features, Services, Agents, Factories, And other custom things in the Same place so that they System can perform properly [Inject] Attribution and Agent Management. 

- In order to Bootstrap we want to create a script that Inherits from `GameInfra` A Good convention is to call this Script `GameBootstrap.cs`. This Script should Sit in the Main Scene on an Empty Activated Object. The Scene Otherwise should be Empty.

- After we made a script inherit from `GameInfra` We want to To Implement all the Abstract Functions, and add all the Code Generation Marks like so

```
    public class GameBoostrap : GameInfra
    {
        protected override void AddServices()
        {
            //<New Service>
        }

        protected override void AddFeatures()
        {
            //<New Feature>
        }

        protected override void AddFactories()
        {
            
        }

        protected override void AddAgents()
        {
            //<New Agent>
        }

        protected override void AddRecords()
        {
            //<New Record>
        }

        protected override void BootstrapCustoms()
        {

        }

        protected override void StartGame()
        {
            
        }
    }
```

The Marks look like so `//<New Something>` And are used later by Code Generation to Automatically incept Bootstrapping to Features.

## Recommended Assembly + Folder Setup

Assembly Defitions form the Compilation Procedure in Unity. The Core Foundation Works Best and is Battle Tested with the Following Assembly Definition Setup

Create a `Scripts` Folder, We denote Folder with (F) and Assembly Definition File with (A), and Script with (S)

```
Plugins (F)
- Plugins (A)
Scripts (F)
- Game (A)
- Agents (F)
  - Agents (A)
- Core (F)
  - Editor (F)
    - CoreEditor (A)
  - Core (A)
- Factories (F)
- Features (F)
- Services (F)
  - Services (A)
- System (F)
  - GameBootstrap.cs (S)
```


Having the Same Folder Structure each time you open a Game will boost integration speed, and resolve lots of Dilemas and Decision Making that might arrise.

Each Assembly Needs to correctly Referense the Assemblies that it Depends On, I recommend to seu it Up like So

Game Assembly

![image](https://github.com/user-attachments/assets/c3f0a6ce-1074-4137-82de-0cef7b3a9661)

Service Assembly

![image](https://github.com/user-attachments/assets/82fb3e32-a245-4d93-9024-14aaa4460fcb)
Might also require Plugins Reference

Agents Assembly

![image](https://github.com/user-attachments/assets/ab955e84-ea2d-4155-8b6c-da330fe1c2b2)

Core Editor

![image](https://github.com/user-attachments/assets/dd60006f-cb7c-4a1d-9df8-38e48dc87c41)
Should be marked as Editor Only obviously

## Features

A good feature always starts with a Good Conversation
ק
![cartoon](https://github.com/user-attachments/assets/da37324b-ae0b-4231-afd0-5a2f42af15f3)

Once you have a good idea for a Feature that you want, Head on over the the Feature Maker to create it

**A Feature Contains**
- Feature Script, The Main Script of the Feature, All the Feature Logic
- Interface Script, Contains the API
- Visual Script (Optional), For MonoBehaviour and Unity Engine
- Record, For Read and Write data
- Config, For Readonly Data this is Common to all Users

![image](https://github.com/user-attachments/assets/af3b91ee-d000-4e5b-9af0-39e96763812b)


