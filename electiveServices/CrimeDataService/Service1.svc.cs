using System;
using System.Net.Http;
using System.Threading.Tasks;

/*
Joshua Greer 1218576515
from CSE 445 - Assignment 3 part 1
for CSE 445 - Assignment 3 part 2
3/3/2024
Required Service 18 crime data service
MODERATE DIFFICULTY

Implementing this code for assignment 3 part 2
This code was written by me for the first part of the assignment,
imported here to make a proper try it page 


http://localhost:55023/Service1.svc
(local host, run in browser)
ServiceReference6

known bugs at bottom
*/


namespace CrimeDataService
{
    /*THE PLAN ----------------------------------------------------------------------------------------------------------
    1) Use StateAbbr to get the agency from the api
    2) parse the string to find city and get the "ori" number
    3) use the ori number to get crime data from summarized/agencies/{ori}/{offense}?"
    4) then we grab the number of crimes after "actual"


    for example:
    we look up show low, az
    we use https://api.usa.gov/crime/fbi/cde/agency/byStateAbbr/AZ?API_KEY=VabgMzS6bvqtEsdBIfFz17CVOFSvDiWblcyoL2NX
    this should give us a list of all the agencies in arizona
    
    the return format is:
    {
        "ori": "AZ0010100",
        "agency_name": "Show Low",
    ...
    }
    {
    ...
    }
    ...
    we parse the string for show low
    when we find it we walk back to the last ','
    then we walk back over the ' " ' and grab the 9 digit ori number
    we use https://api.usa.gov/crime/fbi/cde/arrest/agency/AZ0010000/all?from=2020&to=2020&API_KEY=iiHnOKfno2Mgkt5AynpvPpUQTEyxE77jo1RU8PIv
    returns in format
    {
    ... [a bunch of categories people get arrested for]
    ...
    "data_year": 2020,
    [crime type]: [number of arrests],
    [crime type]: [number of arrests],
    ...
    ]
    }
    this gives us the number of arrests in different categories, parse to where they good data begins
    take the number string after each ':'
    add the total of each category to get the total number of arrests within the year
    we display this number as crimes per year
     */


    public class Service1 : IService1
    {
        //variables
        string ORIcode = "";


        // we changed it from lattitude/longitude to city/stateAbbr because the given api's don't exist anymore
        public async Task<string> crimedata(String city, String stateAbbr)
        {
            //method variables
            int backtrackLength = 17+9;// + city.Length + 9;
            
            //abrivation check
            if(stateAbbr.Length != 2)
            {
                return "State abbreviation must be 2 characters";
            }
            else
            {
                stateAbbr = stateAbbr.ToUpper();
            }
            
           //create http instance
            using(HttpClient client = new HttpClient())
            {
                try
                {

                    //endpoint url
                    string apiURL = "https://api.usa.gov/crime/fbi/cde/";
                    string subURL = "/agency/byStateAbbr/" + stateAbbr + "?API_KEY=";
                    string key = "VabgMzS6bvqtEsdBIfFz17CVOFSvDiWblcyoL2NX";

                    string requestURL = apiURL + subURL + key;

                    //make get request
                    HttpResponseMessage response = await client.GetAsync(requestURL);

                    //check for success
                    if (response.IsSuccessStatusCode)
                    {
                        //read content as string
                        string jsonData = await response.Content.ReadAsStringAsync();

                        string partial = jsonData.Substring(0,40000);

                        //time to parse the data and get the ori code
                        int index = partial.IndexOf(city);
                        if(index == -1)
                        {
                            return "failed to find city in state";
                        }
                        else
                        {
                            ORIcode = partial.Substring(index - backtrackLength, 9);
                            //we got the ori code and can use it to make our next request===================
                            return ORIcode;
                        }

                    }
                    else
                    {
                        return "failed to fetch data \nmake sure state abbreviation is valid (2 uppercase letters), and that city name is a city in the state\nMay error if city does not have a police department";
                    }

                }
                catch (HttpRequestException ex)
                {
                    return "Error: " + ex.Message;
                }

            }

        }
        //==================================================================================================
        public async Task<string> crimeCount(string ORI)
        {
            int sum = 0;
            
            //create http instance
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    //we are using the most recent year for info (2020) for our API Request
                    string apiURL = "https://api.usa.gov/crime/fbi/cde/";
                    string subURL = "arrest/agency/" + ORI + "/all?from=2020&to=2020&API_KEY=";
                    string key = "VabgMzS6bvqtEsdBIfFz17CVOFSvDiWblcyoL2NX";

                    string requestURL = apiURL + subURL + key;

                    //make get request
                    HttpResponseMessage response = await client.GetAsync(requestURL);
                    
                    //check for success
                    if (response.IsSuccessStatusCode)
                    {
                        
                        //read content as string
                        string jsonData = await response.Content.ReadAsStringAsync();

                        string partial = jsonData.Substring(0, 1355);

                        //we have the data, now we need to pull the numbers and add them up
                        int index = partial.IndexOf("2020");
                        if (index == -1)
                        {
                            return "failed to calculate number of crimes";
                        }
                        else
                        {
                            string modified = partial.Substring(index);
                            //now we want to get all integers that follow ':' characters, and add them to the sum
                            //when we reach the "']' we know we are finished
                            for (int i = 0; i < modified.Length; i++)
                            {
                                if (modified[i] == ':')
                                {
                                    int j = i + 1;
                                    string num = "";
                                    while (modified[j] != ',' && modified[j] != ']')
                                    {
                                        num += modified[j];
                                        j++;
                                    }
                                    sum += int.Parse(num);
                                }
                            }
                        }
                        return "There is an average of " + sum + " crimes annually in this area.";

                    }
                    else
                    {
                        return "too large of database for my free api key, too much crime in the area";
                    }

                }
                catch (HttpRequestException ex)
                {
                    return "Error: " + ex.Message;
                }
            }

        }
        //---------------

    }
    
}

/*
Note*

Some cities have police departments with custom names,
some have databases too large for my public key to access (im limited to 65000 character requests)
some cities have no police department at all
some cities have police departments that are not in the database

These cause errors from time to time, tho the program is pretty robust

Test cases:
Input:                  Output:
Tempe AZ                5516
Mesa AZ                 11063
Show Low AZ             1145
Apache AZ               32
Brighton CO             1290
Jourdanton TX           121
Albany NY               224
 */
