# Covid Tracker
---
 ## Design Document  
 
 ##### Team Composition  
 
-Surabhi Kulkarni – Back-end Developer\
-Daya Nayak – Dev Ops\
-Shweta Shah – Front-end Developer \
-Likhitha Ramaprasad – Back-end Developer\
-Laasya Akunuru – Business Analyst 
 
 Meeting Time: 3 PM to 6 PM on Tuesday and Fridays 
 
 ---
 
 ## Introduction  
 
Coronavirus disease 2019 (COVID-19) dominated 2020 and 2021. This is a look back at how the pandemic evolved and progressed through the year, which closed with the arrival of vaccines, but also continued challenges.

1. Total number of vaccine doses till date 
2. Total number of deaths registered till date
3. State-wise death
4. State-wise vaccination

---  

## Data Feeds  

1. United States COVID 19 Cases and Deaths by State: https://data.cdc.gov/Case-Surveillance/United-States-COVID-19-Cases-and-Deaths-by-State-o/9mfq-cb36/data

2. Covid-19 vaccination trends in the united states national https://catalog.data.gov/dataset/covid-19-vaccination-trends-in-the-united-statesnational/resource/571e619f-4e4d-42ce-b7ff-4fff5c37d205
---  

## Functional Requirements  

#### Requirement 1.1  

Scenario:  The user wants to view the state wise COVID-19 vaccination trends.
Given: A feed of COVID-19 vaccination trends in the United States.
When: User is looking for vaccination trend in a particular state.
Then: Display one result showing vaccination trend for the state entered by the user with attributes such as:
1. State
2. Date
3. Total vaccines administered
4. Total dose #1 administered
5. Total series completed

#### Requirement 1.2  

Scenario:  The user wants to view the state wise COVID-19 cases and death toll.
Given: A feed of COVID-19 cases and deaths in the United States.
When: User is looking for COVID-19 cases and death toll in a particular state.
Then: Display one result showing COVID-19 cases and death toll for the state entered by the user with attributes such as:
1. State
2. Date
3. Total COVID-19 cases reported
4. Total deaths recorded

#### Requirement 1.3  

Scenario:  The user wants to view daily statistics of COVID-19 new cases,new deaths recorded and vaccines administered.
Given: A feed of COVID-19 new cases, new deaths and new vaccination records in the United States.
When: User is looking for COVID-19 cases and death toll in a particular state.
Then: Display one result showing COVID-19 new cases and new deaths recorded and vaccines administered daily for the state entered by the user with attributes such as:
1. State
2. Date
3. New COVID-19 cases reported
4. New deaths recorded
5. Daily vaccines administered
6. Daily dose #1 administered
7. Daily series completed

---



