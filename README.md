# Core Foundation

### Motive
We want the Game Architecture to be easily translatable to Business Values. In Games, our Values come in the form of Game Features. Thus our Core Foundation aims to orient the Architecture around Solid Features.

### Brief
In the Core Foundation, we Split the Game into Features and Services. Features can use the help of Services, but Services are Agnostic to Features. 

_Features Using Services Example_
![Feature Service Usage Example](https://github.com/user-attachments/assets/c9a4434f-1e49-4ed4-b5b5-a8d3edda5563)

Each feature consists of the Feature Script for all Logic, The Record for storing Data, and Visuals to host the MonoBehaviours and UnityEngine control code. 
![image](https://github.com/user-attachments/assets/f5502a44-4139-48c3-b783-5a1990aa353f)




And finally each Feature is encapsulated by an Interface that exposes only necessary functionality to the rest of the Game.

This achieves a basic but highly effective MVC architecture within each feature separetely.

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
- Feature Maker


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
![image](https://github.com/user-attachments/assets/bf150afa-71e0-413d-8aba-5a53ceeedda4)


**A Feature Contains**
- Feature Script, The Main Script of the Feature, All the Feature Logic
- Interface Script, Contains the API
- Visual Script (Optional), For MonoBehaviour and Unity Engine
- Record, For Read and Write data
- Config, For Readonly Data this is Common to all Users

![image](https://github.com/user-attachments/assets/af3b91ee-d000-4e5b-9af0-39e96763812b)

## Records

![vinyl-record-icon-symbol-music-260nw-2411383077](https://github.com/user-attachments/assets/fec61b89-cbe9-4b3d-a885-f95dcf0c83d7)

Records are Places for Data.

Data can be of Many Types

Some Data can Change during the Game Lifetime.

Some Data can be Saved between Game Sessions.

Some Data is the Same for All Users.

Many things Are not Data
 - Cancellation Tokens are not data
 - Task Completion Sources are not data
 - Coroutine references are not data
 - MonoBehaviour of other GameObject stuff are not data
 - Callbacks are not Data
 - Events are not Data
 - Delegates are not Data
 - Prefabs, and Textures, and Materials are not data.

Records should only contain Data, and Functionality to Manipulate and Retrieve this Data.

Data ususally is Serializable. And can be Converted from one format to another.

So Records should be Convertible to a Json and Back.

Each Record Type is a Single Instance. Records are [Inject] able into the System as long as they are Bootstrapped
![image](https://github.com/user-attachments/assets/a25cddc9-9ebe-4ebf-9f90-cb33961b3372)

All referense to the Records should point to the same Instance, So changing a Record in one Feature, should be visible to another Feature.

![image](https://github.com/user-attachments/assets/760a4550-af12-4266-943f-8cdf234fe50a)
In this Example the Record of the Grid Feature serves as a hub for Data both for the the Player Feature and the Grid Feature.

While the Grid Feture only changes and sets the Grid Data. The Player Feature can access the Data and Read from it to understand where Movement is Available.

This is why its important that each record exists as a single instance. Which is what happens automatically :)


Each Feature may have its Own Record. But we can also Create Records to contain data regradless of any specific Feature.

Dont forget to Bootstrap your Records
![image](https://github.com/user-attachments/assets/bff9cb01-67ed-45a6-a446-77087e1328c2)

A cool tool that we have is to Spy on the content of your Records during Unity Runtime via the RecordDataSpy Tool
![image](https://github.com/user-attachments/assets/c15de6f3-799a-4c31-9bb1-5a32b5ad7c36)


Most important Note - Never create new Instances of Records, outside the Bootstrap phase, without a good reason.

## [Inject] Attribute

![image](https://github.com/user-attachments/assets/f385283a-31f9-4209-ae21-1d23fcf78c0d)

If you have never seen this type of Attribute, just know its very common.

Our [Inject] Attribute is of Custom Implementation.

We can tag any Properties (not fields) with a getter and a setter with the [Inject] Attribute

And what this does is bring you the IFeature, Record, IService, or IAgent that you are looking for and set your Property with the reference that was set in the Bootstrap.

This works only in Features and Services. We do not support or encourage referencing Features in Visuals or Visuals in Data. There is no reason for it so we dont support it.

All Features can only be "Injected" via their Interface. The Interface has to be registered in the Bootstrap (which happens automatically via the Feature Maker Tool)



External Users of your Feature should never care How the feature achieves its implementation. As long as it works and the API can be called via the Interface that is [Inject]-able

This way the [Inject] Attribute serves us the perfect solution for Dependency Injection without promoting bad practices.

## Configurations


