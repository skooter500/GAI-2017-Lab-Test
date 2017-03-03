# Game Engines 2 Lab Test Summer 2017

The goal of this test is to create the AI for the blue resource gathers depicted in the video below. 

What is happening:

- The yellow boxes represent flowers that contain pollen, a resource that the bees want to collect. Bees are represented by the blue boxes. The bees collect pollen from the flowers and bring it back to the hive. The hive is represented by a purple box.  
- The hive can create a maximum of 10 bees at a rate of 1 every 2 seconds. A bee costs 5 units of pollen to make. The hive starts with 10 units of pollen and so it will be able to make 2 bees initially. 
- As the bees collect more pollen, the hive makes more bees
- Bees have the following behaviour:
    - Bees explore their environment looking for flowers. 
    - To do this, bees pick a spot within range of the flowers to move to. 
    - They slow down as they approach the spot. 
    - If the bee comes within 20 units of a flower whilst it is exploring, it will head towards the flower in order to load up with pollen
    - When the bee arrives at a flower, it will gather pollen from the flower at a rate of 1 unit per second until the pollen is all gathered. When the flower has no more pollen, it gets removed from the scene and the bee heads back to the hive to deposit the pollen. 
    - If a bee doesn't find any flowers, it will pick a new spot to approach. 
    - The bees AI updates 5 times a second.

# Rules
- This is a closed book test. 
- No use of any internet resources apart from the Unity and Visual Studio API reference pages.  

# Instructions
- Use the assignment submission link you will have received by email. This will provide access to a git repository you need to clone with the starter code for the test. 
- You will also need to submit your assignment to this repository by commiting to it
- I strongly suggest that you make regular commits every time you get something working or every 20 minutes at least. 

# Marking Scheme
| Description | Marks |
|-------------|-------|
| Creating new bees from the hive as described above | 20 |
| Bee movement | 25 |
| Bee resource gathering | 35 |
| Improvements to the bee's AI or presentation you make | 20 |

Marks will be awarded for creative, elegent and efficient soultions