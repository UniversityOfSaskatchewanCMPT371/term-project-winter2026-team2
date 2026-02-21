# Team 2 - VR Brain Adventure Game for Indigenous Youth

#### Team Members

- Anish Bhagat
- Antoinette Rubia
- Charles Gamboa
- Connor Hamelin
- Derek Wassill
- John Thoms
- Joseph Obeng
- Leif Benson
- Logan Fossenier
- Logan Loopkey
- Purity Ochang
- Reynan Jr. Castro

#### Stakeholders

- Dr. Soo Kim, School of Rehabilitation Science (soo.kim@usask.ca), primary
  contact for ongoing guidance during project
- Dr. Stacey Lovo, School of Rehabilitation Science
- Whitecap Dakota First Nation Community

## Overview

This project is a VR learning experience gamified for Indigenous youth in high
school (ages 14 to 18) to introduce key concepts of brain anatomy, basic
neurophysiology, and everyday wellness.

This VR experience will form one component of the larger VR grant project, which
focuses on designing virtual learning tools that support community-based health
promotion for Indigenous youth in Saskatchewan and interest in health science
professions. Players partake in exploration-based learning, where youth interact
with the room environment to explore parts of the brain and complete short
minigames. We intend for these games to teach basic anatomy and to reinforce
health-promotion messages such as stress regulation, sleep basics, emotional
wellness, movement, and brain health.

<small>_overview given by Dr. Soo Kim and adapted by Logan Fossenier_</small>

## Installation

To setup this project for development:

1. clone the repository
2. install Unity locally (version 2022.3)
3. open the project which will install required packages
4. the project is now ready, all dependencies are versioned from the
   [manifest](./VRGame/Packages/manifest.json)

The installation steps for production deployment, to actual headsets in the
field, is not concretely established yet.

## Project Development

Our processes can be seen in the [wiki](https://github.com/UniversityOfSaskatchewanCMPT371/term-project-winter2026-team2/wiki).

#### Project Structure

`./VRGame` is the Unity project. We refrained from putting everything at the top
level so that we can put our spike prototypes beside the main project.

`./VRGame/Assets/MVC` currently contains each of the Model/View/Controller
folders but we're going to flatten that between ID2 and ID3.

`./VRGame/Assets/Tests/{EditMode,PlayMode}/MVC/*` is where our tests live, and
in the final `MVC` layer of the directory the corresponding file hierarchy of the code
being tested is mirrored.
