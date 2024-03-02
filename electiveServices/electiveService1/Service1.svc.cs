using System;
using System.Threading.Tasks;
using System.Net.Http;

/*
Joshua Greer 1218576515
CSE 445 Assignment 3 part 2 (individual)
3/3/2024
Elective Service 1
Parks within 1600 meters of a given location (1 mile)

http://localhost:52574/Service1.svc
local host testing

bugs detailed at bottom
 */

namespace electiveService1
{
    /*THE PLAN================================================================================================

    input parameters: latitude, longitude
    predefined parameters: radius 1600 meters, api key, parks

    api key = AIzaSyD5NdSZgBOdreBiPCsDjoCGNU20Y_eVu-s
    google will not let me pass 200$ / month, so be careful and delete when done
    (graders, I am watching you) ps if you have your own key please use it, you can create for free on google cloud

    example query:
    https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=33.4%2C-111.8&radius=4800&type=park&key=AIzaSyD5NdSZgBOdreBiPCsDjoCGNU20Y_eVu-s

    ----------------------------------------------------------------------------------------------------------------
    We using the google places api to find parks within 4800 meters of a given location
    (this is 3 miles for you American folk)
    If there is a nearby park, we will return the name of the park
    If there is no nearby park, we will return "no nearby parks"
    most errors are not finding any parks
     */
    public class Service1 : IService1
    {
        public async Task<string> findParks(double latitude, double longitude)
        {
            //create http instance
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    //endpoint url
                    string apiURL = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=";
                    string subURL = latitude + "%2C" + longitude + "&radius=1600&type=park&key=";
                    string apiKey = "AIzaSyD5NdSZgBOdreBiPCsDjoCGNU20Y_eVu-s";

                    string requestURL = apiURL + subURL + apiKey;      //full request url

                    //make get request
                    HttpResponseMessage response = await client.GetAsync(requestURL);

                    //check for success
                    if (response.IsSuccessStatusCode)
                    {
                        //read content as string
                        string jsonData = await response.Content.ReadAsStringAsync();
                        string partial = jsonData.Substring(0, 2108);   //this JSON is huge, we only need a small part of it. This part is the first (closest) record

                        //now that we have partial data, we can parse it for the name of the park
                        int index = partial.IndexOf("name");
                        if (index == -1)
                        {
                            return "There are no parks within a mile of this location";
                        }
                        else
                        {
                            int j = index + 9; //skip to the name of the park
                            string parkName = "";
                            while (partial[j] != ',' && partial[j] != '}' && partial[j] != '\"')
                            {
                                parkName += partial[j];
                                j++;
                            }

                            return parkName + " is within one mile of this location!";
                        }

                    }
                    else
                    {
                        return "unsuccessful request (no data)";
                    }

                }
                catch (HttpRequestException ex)
                {
                    return "No parks found";
                }
            }
        }
    }
}

/*
*Notes*
This API finds anything with the type "park"
sometimes things in the database are tagged park but are not parks


*Test Cases*
input (latitude, longitude)     |  expected output:                
                                | description

33.4, -111.8                    |  "Emerald Volleyball Court"      
                                | this lat/long is Mesa AZ, Emerald volleyball court is a park

33.4, -111.9                    |  "Meyer Park" 
                                | this lat/long is Tempe AZ, Meyer Park is closest

40.7128, -74.0060               |  "Daj Hammarskjold Plaza"
                                | this lat/long is New York City, there are lots of areas tagged as parks

39.07, -75.13                   |  "No parks found"
                                | this lat/long is in the middle of the ocean, no parks

 */