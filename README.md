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

Our tech stack is typical of VR in Unity (2022.3.62f3). We are using [XR Interaction Tookit](https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@2.6/manual/index.html)
which lets AR and VR experiences be created more readily. There is also the
[XR Device Simulator](https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@2.6/manual/samples-xr-device-simulator.html)
which is being used in testing to drive the VR game from the desktop without a
headset. These packages are so powerful that we do not need much else, but that
which remains can be seen in [manifest.json](./VRGame/Packages/manifest.json)

## Installation

To setup this project for development:

1. clone the repository
2. install [Unity](https://unity.com/download) locally (version 2022.3.62f3)
3. open the project which will install required packages
4. the project is now ready, all dependencies are versioned from the
   [manifest](./VRGame/Packages/manifest.json)

The installation steps for production deployment, to actual headsets in the
field, is not concretely established yet.

## Project Development

Our processes can be seen in the [wiki](https://github.com/UniversityOfSaskatchewanCMPT371/term-project-winter2026-team2/wiki).

## Development Notes

- NSubstitute is being used for mocking. To mock, you need an interface, so
  writing interfaces for all classes is not only good style but mandatory
- We use FsCheck for property testing
- Multilevel logging is being done through Unity's `Debug.Log`, `Debug.LogError`
  , and `Debug.LogWarning`
- GameCI is capturing our multi level logging to GitHub Actions
- Friend assembly is being used to let our compiled test asseblies access
  internal methods on classes. This is our "testhook" method to keep dev
  APIs from being exposed to anything but tests
- We are using `<inheritdoc/>` at the class / method level to inherit docs from
  interfaces to remove duplication
- [Our license](./LICENSE) is MIT
