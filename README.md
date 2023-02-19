# NSG QTESlider
 A simple and easy to integrate QTE mini game for the unity game engine

 ![](https://s3.gifyu.com/images/NSGQTESlider-Example.gif)

## How to set up
- Download the latest package [release here](https://github.com/RealJamako/NSG_QTESlider/releases)
- Import into your project
- Drag both the **QTE Canvas** & **QTE Manager** prefabs into your projects hierarchy
- On the **QTE Managers** inspector hook up the reference to the QTE Controller that is attached inside of the **QTE Canvas** prefab
- As this uses the old input system you will need to set up the input button in your input manager with the name "QTEAction"

## How to use
- From a script of your choosing call the following method `QTEManager.Instance.StartQTE();`

## Customise
You can customise almost every aspect of the system without touching any code. Listed below are the components that have useful settings visible in the inspector.

1. QTE Manager
    - Start Delay
    - Stop Delay

2. QTE Controller
    - Max Cycle Count
    - QTE Cycle Time

3. QTE Presenter
    - Min Target Size
    - Max Target Size
    - Movement Speed

Beyond customising the feel of the game you are also able to customise the look using the available scriptable objects. These are used to hold the audio clips and visuals for successfully or failing to hit the target which are held in the **QTE Streak Counter** component.

Creating a new scriptable object for this is as easy as right clicking in your project and selecting one of the two SO options under the NSG QTE Slider menu.

## Support
I built this for use in my own game so I will be updating if I find any features worthy to push into this package.

If you find any issues or have a feature feel free to open an issue with the respected label.