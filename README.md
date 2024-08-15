# Lottery App

![Platform](https://img.shields.io/badge/platform-Xamarin.Native-blue) ![License](https://img.shields.io/badge/license-MIT-green)

A cross-platform lottery application built using Xamarin Native (iOS and Android). This app allows users to generate and check lottery numbers, providing a simple and intuitive interface for managing lottery draws.

## Table of Contents
- Context
- Features
- Screenshots
- Prerequisites
- End

## Context

Hi Mkodo, thank you for giving me the opportunity to do this take home challenge. I did this throughout the day sporadically whilst on holiday on the 14th of August. I feel that I have met most of the brief.
Please use the master branch.

**Essential** 

- **XML Parsing:** The app parses the .json file provided.
- **Simple UI View:** The app has a simple UI that shows lottery numbers, bonus ball, date and top prize of the draw.
- **Unit Testing:** The app has unit tests covering the shared code.

**Additional** 
- **Detail View:** The app has a detail view where it shows detail of the draw selected.
- **Lottery Tickets:** The app generates random lottery ticket numbers along with a bonus ball.
- **Navigation:** The app has navigation to the detail view.
- **Additional Test:** I have added additional tests to cover the util functions, connectivity and preferences services.
- **Interactive Navigation:** The app has a swipe gesture on each of the lottery cards that navigates once swipe is complete.
- **Local Storage:** The app saves the json into local storage and will show the saved json if there is no connectivity.
- **UI/UX Enhancements:** I have added custom xml views for circles around the numbers.

**Is there anything more I could do?** 
Yes. I had limited time on this due to being on holiday but here's a list of what I would have added.
1. Integration tests - I did add the NUnit project for Android but this was eating into my time to get this to work, as I mentioned I have limited experience but I am hungry to learn. I had to pivot onto other parts of the brief.
2. Unit Tests - I would add more unit tests to improve code coverage of the whole app.
3. Exception Handling - I would like to add more exception handling around the parsing of the data.
4. Error screens - Ontop of exception handling I would also add error screens, for example, no connectivity and no data would have an empty view with "No lottery tickets available".
5. UI - I would improve the UI immensely, padding, margins, fonts. 
6. UI Part 2 - Animation and more custom controls for TextView, styles and resources. I would also update the UI with more branding and add white labelling.
7. Navigation - Better handling of the intents via navigation, pass the model via the intent rather than the individual entities.
8. Navigation Part 2 - Different navigation modes.
9. Add an iOS Project - Xamarin.Native allows for iOS, so I would add an iOS Project.
10. Branding - I would also add much more branding ie: a brand theme, brand logos, colours, buttons etc.

## Features

- **Cross-Platform:** Runs on both iOS and Android using Xamarin Native.
- **Lottery Number Generation:** Randomly generates lottery numbers.
- **Lottery Results Checking:** Allows users to compare their numbers with the drawn numbers.
- **Detailed Draw Information:** Provides information on the draw date, winning numbers, and top prizes.
- **Swipe and Click Navigation:** Users can swipe or click on a lottery draw item to view detailed information.

## Screenshots
Home Screen:
<img width="327" alt="Screenshot 2024-08-15 at 08 22 35" src="https://github.com/user-attachments/assets/1e914009-3f3b-475a-b6e5-6571e8b13d23">

Lottery Detail:
<img width="346" alt="Screenshot 2024-08-15 at 08 22 51" src="https://github.com/user-attachments/assets/c9cfc165-3e31-458c-8657-222a7cba6833">


### Prerequisites

- **Visual Studio 2019/2022** with Xamarin workload installed
- **Xamarin.Android** SDKs
- **.NET Core 3.1** 

## End
Thank you for taking the time to look at my code. I look forward to showing you this app.
