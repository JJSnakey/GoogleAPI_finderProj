using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
Joshua Greer 1218576515
CSE 445 Assignment 3 part 1 (individual)
2/25/2024
===Try it page===

required services 18 crime data service and 19 natural hazards service
    
    ServiceReference1 - crime data service
http://localhost:55023/Service1.svc
    ServiceReference2 - natural hazards service
http://localhost:56759/Service1.svc

 */

namespace CrimeDataFormsApp
{
    public partial class Form1 : Form
    {
        //variables

        //crime data
        string city = "";
        string stateAbbr = "";
        string ORI = "";

        //natural hazard data
        double latitude = 0;
        double longitude = 0;
        int radiusKm = 0;
        decimal minMag = 0;
        decimal maxMag = 0;

        public Form1()
        {
            InitializeComponent();
            this.Text = "Joshua Greer's Try-It";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //City box
            city = textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //state abbreviation box
            stateAbbr = textBox2.Text;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var client = new ServiceReference6.Service1Client();

            try
            {
                ORI = client.crimedata(city, stateAbbr);
                label5.Text = client.crimeCount(ORI);
            }
            catch (Exception ex)
            {
                label5.Text = "Error: " + ex.Message;
            }
            client.Close(); //good practice to close the client
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //handling these here because they are causing trouble
            latitude = Convert.ToDouble(textBox3.Text);
            longitude = Convert.ToDouble(textBox5.Text);
            radiusKm = Convert.ToInt32(textBox6.Text);
            minMag = Convert.ToDecimal(textBox7.Text);

            var client = new ServiceReference11.Service1Client();

            try
            {
                label7.Text = client.NaturalHazards(latitude, longitude, radiusKm, minMag);
            }
            catch (Exception ex)
            {
                label7.Text = "Error: " + ex.Message;
            }
            client.Close(); //good practice to close the client
            
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged_1(object sender, EventArgs e)
        {
            //latitude input
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            //min magnitude input
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            //radius input

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            //longitude input
            //handled in button2_Click
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
    }
}
