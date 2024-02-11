using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApmDijkstra
{
    public partial class info : Form
    {
        private string selectedItem1;
        private string selectedItem2;
        public info(string selectedItem1, string selectedItem2)
        {
            InitializeComponent();
            this.selectedItem1 = selectedItem1;
            this.selectedItem2 = selectedItem2;
        }

        private void info_Load(object sender, EventArgs e)
        {
            // Set values based on the selected item
            switch (selectedItem1)
            {
                case "Jinnah International Airport ":
                    SetTextBoxValues1("Jinnah International Airport", "Karachi", "Pakistan", "KHI", "OPKC", "24.9000", "67.1681");
                    break;

                case "Allama Iqbal International Airport":
                    SetTextBoxValues1("Allama Iqbal International Airport", "Lahore", "Pakistan", "LHE", "OPLA", "31.5216", "74.4036");
                    break;

                case "Islamabad International Airport":
                    SetTextBoxValues1("Islamabad International Airport", "Islamabad", "Pakistan", "ISB", "OPIS", "33.6167", "72.7167");
                    break;

                case "Dera Ghazi Khan International Airport":
                    SetTextBoxValues1("Dera Ghazi Khan International Airport", "Dera Ghazi Khan", "Pakistan", "DEA", "OPDG", "29.9608", "70.4858");
                    break;

                case "Faisalabad International Airport":
                    SetTextBoxValues1("Faisalabad International Airport", "Faisalabad", "Pakistan", "LYP", "OPFA", "31.3650", "72.9947");
                    break;

                case "Gwadar International Airport":
                    SetTextBoxValues1("Gwadar International Airport", "Gwadar", "Pakistan", "GWD", "OPGD", "25.2322", "62.3272");
                    break;

                case "Multan International Airport":
                    SetTextBoxValues1("Multan International Airport", "Multan", "Pakistan", "MUX", "OPMT", "30.2033", "71.4192");
                    break;

                case "Peshawar Bacha Khan International Airport":
                    SetTextBoxValues1("Peshawar Bacha Khan International Airport", "Peshawar", "Pakistan", "PEW", "OPPS", "33.9939", "71.5147");
                    break;

                case "Quaid-e-Azam International Airport":
                    SetTextBoxValues1("Quaid-e-Azam International Airport", "Quetta", "Pakistan", "UET", "OPQT", "30.2400", "66.9400");
                    break;

                case "Shaikh Zayed International Airport":
                    SetTextBoxValues1("Shaikh Zayed International Airport", "Rahim Yar Khan", "Pakistan", "RYK", "OPRK", "28.3840", "70.2797");
                    break;

                case "Sialkot International Airport":
                    SetTextBoxValues1("Sialkot International Airport", "Sialkot", "Pakistan", "SKT", "OPST", "32.5356", "74.3639");
                    break;

                case "Hyderabad Airport":
                    SetTextBoxValues1("Hyderabad Airport", "Hyderabad", "Pakistan", "HDD", "OPKD", "25.3183", "68.3667");
                    break;

                case "D.I Khan Airport":
                    SetTextBoxValues1("D.I Khan Aiport", "Dera Ismail Khan", "Pakistan", "DIK", "OPDT", "31.9098", "70.8878");
                    break;

                case "Khuzdar Domestic Airport":
                    SetTextBoxValues1("Khuzdar Airport", "Khuzdar", "Pakistan", "KDD", "OPKH", "27.7969", "66.6393");
                    break;

                case "Sibi Airport":
                    SetTextBoxValues1("Sibi Airport", "Sibi", "Pakistan", "SBQ", "OPSB", "29.5712", "67.8479");
                    break;

                case "Gilgit Airport":
                    SetTextBoxValues1("Gilgit Airport", "Gilgit", "Pakistan", "GIL", "OPGT", "35.9194", "74.3320");
                    break;

                default:
                    // Handle other cases or set default values
                    SetTextBoxValues1("", "", "", "", "", "", "");
                    break;
            }






            switch (selectedItem2)
            {
                case "Jinnah International Airport ":
                    SetTextBoxValues2("Jinnah International Airport", "Karachi", "Pakistan", "KHI", "OPKC", "24.9000", "67.1681");
                    break;

                case "Allama Iqbal International Airport":
                    SetTextBoxValues2("Allama Iqbal International Airport", "Lahore", "Pakistan", "LHE", "OPLA", "31.5216", "74.4036");
                    break;

                case "Islamabad International Airport":
                    SetTextBoxValues2("Islamabad International Airport", "Islamabad", "Pakistan", "ISB", "OPIS", "33.6167", "72.7167");
                    break;

                case "Dera Ghazi Khan International Airport":
                    SetTextBoxValues2("Dera Ghazi Khan International Airport", "Dera Ghazi Khan", "Pakistan", "DEA", "OPDG", "29.9608", "70.4858");
                    break;

                case "Faisalabad International Airport":
                    SetTextBoxValues2("Faisalabad International Airport", "Faisalabad", "Pakistan", "LYP", "OPFA", "31.3650", "72.9947");
                    break;

                case "Gwadar International Airport":
                    SetTextBoxValues2("Gwadar International Airport", "Gwadar", "Pakistan", "GWD", "OPGD", "25.2322", "62.3272");
                    break;

                case "Multan International Airport":
                    SetTextBoxValues2("Multan International Airport", "Multan", "Pakistan", "MUX", "OPMT", "30.2033", "71.4192");
                    break;

                case "Peshawar Bacha Khan International Airport":
                    SetTextBoxValues2("Peshawar Bacha Khan International Airport", "Peshawar", "Pakistan", "PEW", "OPPS", "33.9939", "71.5147");
                    break;

                case "Quaid-e-Azam International Airport":
                    SetTextBoxValues2("Quaid-e-Azam International Airport", "Quetta", "Pakistan", "UET", "OPQT", "30.2400", "66.9400");
                    break;

                case "Shaikh Zayed International Airport":
                    SetTextBoxValues2("Shaikh Zayed International Airport", "Rahim Yar Khan", "Pakistan", "RYK", "OPRK", "28.3840", "70.2797");
                    break;

                case "Sialkot International Airport":
                    SetTextBoxValues2("Sialkot International Airport", "Sialkot", "Pakistan", "SKT", "OPST", "32.5356", "74.3639");
                    break;

                case "Hyderabad Airport":
                    SetTextBoxValues2("Hyderabad Airport", "Hyderabad", "Pakistan", "HDD", "OPKD", "25.3183", "68.3667");
                    break;

                case "D.I Khan Airport":
                    SetTextBoxValues2("D.I Khan Aiport", "Dera Ismail Khan", "Pakistan", "DIK", "OPDT", "31.9098", "70.8878");
                    break;

                case "Khuzdar Domestic Airport":
                    SetTextBoxValues2("Khuzdar Airport", "Khuzdar", "Pakistan", "KDD", "OPKH", "27.7969", "66.6393");
                    break;

                case "Sibi Airport":
                    SetTextBoxValues2("Sibi Airport", "Sibi", "Pakistan", "SBQ", "OPSB", "29.5712", "67.8479");
                    break;

                case "Gilgit Airport":
                    SetTextBoxValues2("Gilgit Airport", "Gilgit", "Pakistan", "GIL", "OPGT", "35.9194", "74.3320");
                    break;

                default:
                    // Handle other cases or set default values
                    SetTextBoxValues2("", "", "", "", "", "", "");
                    break;
            }
        }


        private void SetTextBoxValues1(string value1, string value2, string value3, string value4, string value5, string value6, string value7)
        {
            // Set the values in the textboxes
            textBox1.Text = value1;
            textBox2.Text = value2;
            textBox3.Text = value3;
            textBox4.Text = value4;
            textBox5.Text = value5;
            textBox6.Text = value6;
            textBox7.Text = value7;
        }

        private void SetTextBoxValues2(string value1, string value2, string value3, string value4, string value5, string value6, string value7)
        {
            // Set the values in the textboxes
            textBox8.Text = value1;
            textBox9.Text = value2;
            textBox10.Text = value3;
            textBox11.Text = value4;
            textBox12.Text = value5;
            textBox13.Text = value6;
            textBox14.Text = value7;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormApmDijkstra d = new FormApmDijkstra();
            d.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
