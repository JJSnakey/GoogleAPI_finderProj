using System;
using System.Threading.Tasks;
using System.Net.Http;

/*
Joshua Greer 1218576515
CSE 445 Assignment 3 part 2 (individual)
3/3/2024
Elective Service 2
Movie theaters within 6400 meters of a given location (4 miles)

http://localhost:52574/Service1.svc
local host testing

bugs detailed at bottom
 */

namespace electiveService2
{
    /*THE PLAN================================================================================================

     */
    public class Service1 : IService1
    {
        public async Task<string> findTheaters(double latitude, double longitude)
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

*Test Cases*

 */
