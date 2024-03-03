using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

/*
Joshua Greer 1218576515
CSE 445 Assignment 3 part 1 (individual)
3/3/2024

===Try it page===
Run these in local host to test the services:

findParks service (elective service 1)
http://localhost:52574/Service1.svc 
ServiceReference1

findTheaters service (elective service 2)
http://localhost:52840/Service1.svc
ServiceReference2

find high schools service (elective service 3)
http://localhost:55640/Service1.svc
ServiceReference3

find bus stops service (elective service 4)
http://localhost:55641/Service1.svc
ServiceReference4

 */

namespace TryItPage
{
    public partial class Form1 : Form
    {
        double latitude = 0;
        double longitude = 0;
        public Form1()
        {
            InitializeComponent();
            this.Text = "Joshua Greer's Try-It Assignment 3 part 2";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //activate elective service 1
            
            //handling these here because can trouble
            latitude = Convert.ToDouble(textBox1.Text);
            longitude = Convert.ToDouble(textBox2.Text);

            var client = new ServiceReference1.Service1Client();

            try
            {
                label3.Text = client.findParks(latitude, longitude);
            }
            catch (Exception ex)
            {
                label3.Text = "No parks nearby or invalid latitude/longitude";
            }
            client.Close(); //good practice to close the client
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //activate elective service 2

            //handling these here because can trouble
            latitude = Convert.ToDouble(textBox1.Text);
            longitude = Convert.ToDouble(textBox2.Text);

            var client = new ServiceReference2.Service1Client();

            try
            {
                label4.Text = client.findTheaters(latitude, longitude);
            }
            catch (Exception ex)
            {
                label4.Text = "No Theaters nearby or invalid latitude/longitude";
            }
            client.Close(); //good practice to close the client

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //activate elective service 3

            //handling these here because can trouble
            latitude = Convert.ToDouble(textBox1.Text);
            longitude = Convert.ToDouble(textBox2.Text);

            var client = new ServiceReference3.Service1Client();

            try
            {
                label6.Text = client.findSchool(latitude, longitude);
            }
            catch (Exception ex)
            {
                label6.Text = "No high schools nearby or invalid latitude/longitude";
            }
            client.Close(); //good practice to close the client

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //activate elective service 4

            //handling these here because can trouble
            latitude = Convert.ToDouble(textBox1.Text);
            longitude = Convert.ToDouble(textBox2.Text);

            var client = new ServiceReference4.Service1Client();

            try
            {
                label11.Text = client.findBusStation(latitude, longitude);
            }
            catch (Exception ex)
            {
                label11.Text = "No bus stations nearby or invalid latitude/longitude";
            }
            client.Close(); //good practice to close the client

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //latitudePark
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //longitudePark
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click_1(object sender, EventArgs e)
        {

        }
    }
}