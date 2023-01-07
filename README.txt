This solution outputs monthly reward points and the total points in a list, with
their respective months marked something like "2023, January", "2022, December" and etc for the user, given the userId and
the months(which is 3 months for the default). For instance, we can input userId as 1 and
monthRange as 3 to get the reward points for past 3 months. 
For the assessment since the database is not being used, it applies a dictionary with user ID as key
and its transaction as value to resemble the transactions stored in the database.
Yet the solution still applies models and entities to simulate real world scenario. Models are
later being used and entities are just written for the demonstration.
The repositories initialize the data for some default value of transactions for demonstration.
