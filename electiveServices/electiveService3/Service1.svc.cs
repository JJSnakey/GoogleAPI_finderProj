using System;
using System.Threading.Tasks;
using System.Net.Http;

/*
Joshua Greer 1218576515
CSE 445 Assignment 3 part 2 (individual)
3/3/2024
Elective Service 3
Find Schools service

http://localhost:55640/Service1.svc
local host testing

bugs detailed at bottom
 */

namespace electiveService3
{

    /*THE PLAN================================================================================================

    input parameters: 
    latitude
    longitude
    
    predefined parameters: 
    radius 3200 meters
    api key = AIzaSyD5NdSZgBOdreBiPCsDjoCGNU20Y_eVu-s
    type = secondary_school

    google will not let me pass 200$ / month, so be careful and delete when done
    (graders, I am watching you) 
    ps if you have your own key please use it, you can create for free on google cloud

    example query:
    https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=33.4%2C-111.8&radius=3200&type=secondary_school&key=AIzaSyD5NdSZgBOdreBiPCsDjoCGNU20Y_eVu-s


    ----------------------------------------------------------------------------------------------------------------
    We using the google places api to find high schools within 3200 meters of a given location
    If there is a nearby school, we will return the name of the school
    If there is no nearby schools, we will return "no nearby schools"

    most errors are not finding any schools
     */
    public class Service1 : IService1
    {
        public async Task<string> findSchool(double latitude, double longitude)
        {
            //create http instance
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    //endpoint url
                    string apiURL = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=";
                    string subURL = latitude + "%2C" + longitude + "&radius=3200&type=secondary_school&key=";
                    string apiKey = "AIzaSyD5NdSZgBOdreBiPCsDjoCGNU20Y_eVu-s";

                    string requestURL = apiURL + subURL + apiKey;      //full request url

                    //make get request
                    HttpResponseMessage response = await client.GetAsync(requestURL);

                    //check for success
                    if (response.IsSuccessStatusCode)
                    {
                        //read content as string
                        string jsonData = await response.Content.ReadAsStringAsync();

                        string partial = jsonData.Substring(0, 2068);   //this JSON is huge, we only need a small part of it. This part is the first (closest) record

                        //now that we have partial data, we can parse it for the name of the high school
                        int index = partial.IndexOf("name");
                        if (index == -1)
                        {
                            return "There are no high schools within 2 miles of this location";
                        }
                        else
                        {
                            int j = index + 9; //skip to the name of the high school
                            string schoolName = "";
                            while (partial[j] != ',' && partial[j] != '}' && partial[j] != '\"')
                            {
                                schoolName += partial[j];
                                j++;
                            }

                            return schoolName + " is within two miles of (" + latitude + ", " + longitude + ") !";
                        }

                    }
                    else
                    {
                        return "unsuccessful request (no data)";
                    }

                }
                catch (HttpRequestException ex)
                {
                    return "No high schools found";
                }
            }
        }
    }
}

/*
*Notes *
This API finds anything with the type "secondary_school"


*Test Cases*
input (latitude, longitude)     |  expected output:                
                                | description

33.4, -111.8                    | AMC Mesa Grand 14
                                | this lat/long is Mesa AZ, AMC is the closest

33.4, -111.9                    | Cinemark Mesa 16 
                                | this lat/long is Tempe AZ, Cinemark is closest

40.7, -74.0                     | Regal Union Square
                                | this lat/long is New York City, there are lots of areas tagged as parks

39.07, -75.13                   | Error
                                | this lat/long is in the middle of the ocean, no parks
*/