# Time Tracker

### Introduction
A few days ago I was reading a book about issues in the workplace. One of the problems hightlighted was interruptions during flow (a state in which you kind of isolate from the external environment and focus solely on your task at hand).

Being able to quantify the amount of distractions you get in your work may help you and the others around you to improve the whole environment and be more productive. Being in the flow is the best way to increase your productiveness, but not only do interruptions distract you from the task at hand (taking away useful time to work), they also require you to spend even more time after each one of them to get back into "flow mode". Since such interruptions may be hard to track and report, I started thinking of a way to better identify them and keep a trace of each.

### Developing Time Tracker
In approaching the problem I identified two main points that my solution should adhere to:
* Easily track start and end times of an interruption, as well as a reason for it.
* Easily activate it without being a distraction by itself.

Since I spend my work time in front of a computer screen, the way I decided to go, then, was to develop an application that I could start with a globally bound shortcut. Such an application should record start time when activated, and then patiently wait for my input (and potential notes) to record end time and close.

Here is what I came up with:

![The application main window](https://cibboy.github.io/TimeTracker/app.png)

At the start of my work session I run my application, which unobtrusively remains in the tray icon area. Whenever an interruption occurs I press Win + Alt + I, and the application interface pops up, registering the start time, as can be seen in the title bar. At this point I can normally use my workstation, if needed, or I can even lock it if I have to temporarily leave. When I return to my work I select the reason of the interruption, add a few notes if needed and press ok. The total interruption time is then recorded into a csv file that can be easily handled with any scripting language (see below).

Recurring distractions even more easily, since the application remembers the last reason and proposes it when it pops up.

### Usage
As I said, use is pretty straightforward: run the application (which remains in the tray icon area) and press Alt + Win + I to pop up a new interruption window. When done, fill the form (in its most basic usage it takes only a couple of seconds off your work which, remember, has already been interrupted) and off you go!

For the sake of simplicity, quitting the application is just a matter of double clicking the tray icon.

Customization of the reason list and output file is done through simple text files.
