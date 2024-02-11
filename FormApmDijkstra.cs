using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;


namespace ApmDijkstra
{
    public partial class FormApmDijkstra : Form
    {
        private double totalCost;
        public string SelectedItem1 { get; private set; }
        public string SelectedItem2 { get; private set; }

        private Dictionary<string, Node> _dictNodes;     
        private List<Edge> _edges;                       
        private List<Node> _nodes;
        private Graph dijkstra;                            
        TextBox[] txtBxCosturi;        
        private Dictionary<String, Panel> _panel;
        private Dictionary<String, TextBox> _textBox;
        private XmlTextWriter xmlDijkstraWriter;
        private XmlTextReader xmlDijkstraReader;

        public FormApmDijkstra()
        {
            InitializeComponent();

            txtBxCosturi = new TextBox[24];
            txtBxCosturi[0] = textBoxA_B;             
            txtBxCosturi[1] = textBoxA_F;
            txtBxCosturi[2] = textBoxB_E;
            txtBxCosturi[3] = textBoxB_C;
            txtBxCosturi[4] = textBoxC_D;
            txtBxCosturi[5] = textBoxC_G;
            txtBxCosturi[6] = textBoxD_E;
            txtBxCosturi[7] = textBoxD_H;
            txtBxCosturi[8] = textBoxD_P;
            txtBxCosturi[9] = textBoxE_F;
            txtBxCosturi[10] = textBoxE_O;
            txtBxCosturi[11] = textBoxF_N;
            txtBxCosturi[12] = textBoxN_O;
            txtBxCosturi[13] = textBoxN_M;
            txtBxCosturi[14] = textBoxM_L;
            txtBxCosturi[15] = textBoxO_L;
            txtBxCosturi[16] = textBoxL_K;
            txtBxCosturi[17] = textBoxP_K;
            txtBxCosturi[18] = textBoxK_J;
            txtBxCosturi[19] = textBoxI_J;
            txtBxCosturi[20] = textBoxP_I;
            txtBxCosturi[21] = textBoxH_I;
            txtBxCosturi[22] = textBoxG_H;
            txtBxCosturi[23] = textBoxO_P;

            comboBoxNodeEnd.SelectedIndex = 0;          
            comboBoxNodeStart.SelectedIndex = 0;

            _dictNodes = new Dictionary<string, Node>(); 

            _dictNodes.Add("Peshawar Bacha Khan International Airport", new Node("Peshawar Bacha Khan International Airport"));
            _dictNodes.Add("Islamabad International Airport", new Node("Islamabad International Airport"));
            _dictNodes.Add("Gilgit Airport", new Node("Gilgit Airport"));
            _dictNodes.Add("Faisalabad International Airport", new Node("Faisalabad International Airport"));
            _dictNodes.Add("Multan International Airport", new Node("Multan International Airport"));
            _dictNodes.Add("D.I Khan Airport", new Node("D.I Khan Airport"));
            _dictNodes.Add("Sialkot International Airport", new Node("Sialkot International Airport"));
            _dictNodes.Add("Allama Iqbal International Airport", new Node("Allama Iqbal International Airport"));
            _dictNodes.Add("Shaikh Zayed International Airport", new Node("Shaikh Zayed International Airport"));
            _dictNodes.Add("Hyderabad Airport", new Node("Hyderabad Airport"));
            _dictNodes.Add("Jinnah International Airport", new Node("Jinnah International Airport"));
            _dictNodes.Add("Khuzdar Domestic Airport", new Node("Khuzdar Domestic Airport"));
            _dictNodes.Add("Gwadar International Airport", new Node("Gwadar International Airport"));
            _dictNodes.Add("Quaid-e-Azam International Airport", new Node("Quaid-e-Azam International Airport"));
            _dictNodes.Add("Sibi Airport", new Node("Sibi Airport"));
            _dictNodes.Add("Dera Ghazi Khan International Airport", new Node("Dera Ghazi Khan International Airport"));

            _nodes = new List<Node>();                 
            foreach (Node n in _dictNodes.Values)       
            {
                _nodes.Add(n);
            }

            _textBox = new Dictionary<string, TextBox>();
            _textBox.Add("A_B", textBoxA_B);
            _textBox.Add("A_F", textBoxA_F);
            _textBox.Add("B_E", textBoxB_E);
            _textBox.Add("B_C", textBoxB_C);
            _textBox.Add("C_D", textBoxC_D);
            _textBox.Add("C_G", textBoxC_G);
            _textBox.Add("D_E", textBoxD_E);
            _textBox.Add("D_H", textBoxD_H);
            _textBox.Add("D_P", textBoxD_P);
            _textBox.Add("E_F", textBoxE_F);
            _textBox.Add("E_O", textBoxE_O);
            _textBox.Add("F_N", textBoxF_N);
            _textBox.Add("N_O", textBoxN_O);
            _textBox.Add("N_M", textBoxN_M);
            _textBox.Add("M_L", textBoxM_L);
            _textBox.Add("O_L", textBoxO_L);
            _textBox.Add("L_K", textBoxL_K);
            _textBox.Add("P_K", textBoxP_K);
            _textBox.Add("K_J", textBoxK_J);
            _textBox.Add("I_J", textBoxI_J);
            _textBox.Add("P_I", textBoxP_I);
            _textBox.Add("H_I", textBoxH_I);
            _textBox.Add("G_H", textBoxG_H);
            _textBox.Add("O_P", textBoxO_P);

            _panel = new Dictionary<String, Panel>();

            _panel.Add("Peshawar Bacha Khan International Airport_Islamabad International Airport", panelA_B);
            _panel.Add("Islamabad International Airport_Gilgit Airport", panelB_C);
            _panel.Add("Islamabad International Airport_Multan International Airport", panelB_E);
            _panel.Add("Gilgit Airport_Faisalabad International Airport", panelC_D);
            _panel.Add("Gilgit Airport_Sialkot International Airport", panelC_G);
            _panel.Add("Faisalabad International Airport_Multan International Airport", panelD_E);
            _panel.Add("Faisalabad International Airport_Allama Iqbal International Airport", panelD_H);
            _panel.Add("Faisalabad International Airport_Dera Ghazi Khan International Airport", panelD_P);
            _panel.Add("Multan International Airport_D.I Khan Airport", panelE_F);
            _panel.Add("Multan International Airport_Sibi Airport", panelE_O);
            _panel.Add("Peshawar Bacha Khan International Airport_D.I Khan Airport", panelA_F);
            _panel.Add("D.I Khan Airport_Quaid-e-Azam International Airport", panelF_N);
            _panel.Add("Quaid-e-Azam International Airport_Sibi Airport", panelN_O);
            _panel.Add("Gwadar International Airport_Quaid-e-Azam International Airport", panelM_N);
            _panel.Add("Sibi Airport_Dera Ghazi Khan International Airport", panelO_P);
            _panel.Add("Shaikh Zayed International Airport_Dera Ghazi Khan International Airport", panelI_P);
            _panel.Add("Jinnah International Airport_Dera Ghazi Khan International Airport", panelK_P);
            _panel.Add("Khuzdar Domestic Airport_Gwadar International Airport", panelL_M);
            _panel.Add("Khuzdar Domestic Airport_Sibi Airport", panelL_O);
            _panel.Add("Jinnah International Airport_Khuzdar Domestic Airport", panelK_L);
            _panel.Add("Hyderabad Airport_Jinnah International Airport", panelJ_K);
            _panel.Add("Shaikh Zayed International Airport_Hyderabad Airport", panelI_J);
            _panel.Add("Allama Iqbal International Airport_Shaikh Zayed International Airport", panelH_I);
            _panel.Add("Sialkot International Airport_Allama Iqbal International Airport", panelG_H);
            _panel.Add("Islamabad International Airport_Peshawar Bacha Khan International Airport", panelA_B);
            _panel.Add("Gilgit Airport_Islamabad International Airport", panelB_C);
            _panel.Add("Multan International Airport_Islamabad International Airport", panelB_E);
            _panel.Add("Faisalabad International Airport_Gilgit Airport", panelC_D);
            _panel.Add("Sialkot International Airport_Gilgit Airport", panelC_G);
            _panel.Add("Multan International Airport_Faisalabad International Airport", panelD_E);
            _panel.Add("Allama Iqbal International Airport_Faisalabad International Airport", panelD_H);
            _panel.Add("Dera Ghazi Khan International Airport_Faisalabad International Airport", panelD_P);
            _panel.Add("D.I Khan Airport_Multan International Airport", panelE_F);
            _panel.Add("Sibi Airport_Multan International Airport", panelE_O);
            _panel.Add("D.I Khan Airport_Peshawar Bacha Khan International Airport", panelA_F);
            _panel.Add("Quaid-e-Azam International Airport_D.I Khan Airport", panelF_N);
            _panel.Add("Sibi Airport_Quaid-e-Azam International Airport", panelN_O);
            _panel.Add("Quaid-e-Azam International Airport_Gwadar International Airport", panelM_N);
            _panel.Add("Dera Ghazi Khan International Airport_Sibi Airport", panelO_P);
            _panel.Add("Dera Ghazi Khan International Airport_Shaikh Zayed International Airport", panelI_P);
            _panel.Add("Dera Ghazi Khan International Airport_Jinnah International Airport", panelK_P);
            _panel.Add("Gwadar International Airport_Khuzdar Domestic Airport", panelL_M);
            _panel.Add("Sibi Airport_Khuzdar Domestic Airport", panelL_O);
            _panel.Add("Khuzdar Domestic Airport_Jinnah International Airport", panelK_L);
            _panel.Add("Jinnah International Airport_Hyderabad Airport", panelJ_K);
            _panel.Add("Hyderabad Airport_Shaikh Zayed International Airport", panelI_J);
            _panel.Add("Shaikh Zayed International Airport_Allama Iqbal International Airport", panelH_I);
            _panel.Add("Allama Iqbal International Airport_Sialkot International Airport", panelG_H);


        }

        private double converCost(TextBox textBox)
        {
            double result;
            try
            {
                result = Convert.ToDouble(textBox.Text);
            }
            catch
            {
                result = 99999;
                textBox.Text = result.ToString();
            }

            return result;
        }

        private void init()
        {

            textBoxA_B.Text = "106.94";
            textBoxA_F.Text = "249.45";
            textBoxB_E.Text = "324.11";
            textBoxB_C.Text = "277";
            textBoxC_D.Text = "513.38";
            textBoxC_G.Text = "382";
            textBoxD_E.Text = "204.39";
            textBoxD_H.Text = "131.36";
            textBoxD_P.Text = "209.89";
            textBoxE_F.Text = "189.9";
            textBoxE_O.Text = "355";
            textBoxF_N.Text = "413.6";
            textBoxN_O.Text = "96.45";
            textBoxN_M.Text = "569.12";
            textBoxM_L.Text = "520";
            textBoxO_L.Text = "434";
            textBoxL_K.Text = "331";
            textBoxP_K.Text = "504.31";
            textBoxK_J.Text = "94.33";
            textBoxI_J.Text = "512";
            textBoxP_I.Text = "176";
            textBoxH_I.Text = "1527";
            textBoxG_H.Text = "78.35";
            textBoxO_P.Text = "463";


            _edges = new List<Edge>();

            _edges.Add(new Edge(_dictNodes["Peshawar Bacha Khan International Airport"], _dictNodes["Islamabad International Airport"], Convert.ToDouble(textBoxA_B.Text)));
            _edges.Add(new Edge(_dictNodes["Peshawar Bacha Khan International Airport"], _dictNodes["D.I Khan Airport"], Convert.ToDouble(textBoxA_F.Text)));

            _edges.Add(new Edge(_dictNodes["Islamabad International Airport"], _dictNodes["Peshawar Bacha Khan International Airport"], Convert.ToDouble(textBoxA_B.Text)));
            _edges.Add(new Edge(_dictNodes["Islamabad International Airport"], _dictNodes["Gilgit Airport"], Convert.ToDouble(textBoxB_C.Text)));
            _edges.Add(new Edge(_dictNodes["Islamabad International Airport"], _dictNodes["Multan International Airport"], Convert.ToDouble(textBoxB_E.Text)));

            _edges.Add(new Edge(_dictNodes["Gilgit Airport"], _dictNodes["Islamabad International Airport"], Convert.ToDouble(textBoxB_C.Text)));
            _edges.Add(new Edge(_dictNodes["Gilgit Airport"], _dictNodes["Faisalabad International Airport"], Convert.ToDouble(textBoxC_D.Text)));
            _edges.Add(new Edge(_dictNodes["Gilgit Airport"], _dictNodes["Sialkot International Airport"], Convert.ToDouble(textBoxC_G.Text)));

            _edges.Add(new Edge(_dictNodes["Faisalabad International Airport"], _dictNodes["Gilgit Airport"], Convert.ToDouble(textBoxC_D.Text)));
            _edges.Add(new Edge(_dictNodes["Faisalabad International Airport"], _dictNodes["Multan International Airport"], Convert.ToDouble(textBoxD_E.Text)));
            _edges.Add(new Edge(_dictNodes["Faisalabad International Airport"], _dictNodes["Allama Iqbal International Airport"], Convert.ToDouble(textBoxD_H.Text)));
            _edges.Add(new Edge(_dictNodes["Faisalabad International Airport"], _dictNodes["Dera Ghazi Khan International Airport"], Convert.ToDouble(textBoxD_P.Text)));

            _edges.Add(new Edge(_dictNodes["Multan International Airport"], _dictNodes["D.I Khan Airport"], Convert.ToDouble(textBoxE_F.Text)));
            _edges.Add(new Edge(_dictNodes["Multan International Airport"], _dictNodes["Islamabad International Airport"], Convert.ToDouble(textBoxB_E.Text)));
            _edges.Add(new Edge(_dictNodes["Multan International Airport"], _dictNodes["Faisalabad International Airport"], Convert.ToDouble(textBoxD_E.Text)));
            _edges.Add(new Edge(_dictNodes["Multan International Airport"], _dictNodes["Sibi Airport"], Convert.ToDouble(textBoxE_O.Text)));

            _edges.Add(new Edge(_dictNodes["D.I Khan Airport"], _dictNodes["Peshawar Bacha Khan International Airport"], Convert.ToDouble(textBoxA_F.Text)));
            _edges.Add(new Edge(_dictNodes["D.I Khan Airport"], _dictNodes["Multan International Airport"], Convert.ToDouble(textBoxE_F.Text)));
            _edges.Add(new Edge(_dictNodes["D.I Khan Airport"], _dictNodes["Quaid-e-Azam International Airport"], Convert.ToDouble(textBoxF_N.Text)));

            _edges.Add(new Edge(_dictNodes["Quaid-e-Azam International Airport"], _dictNodes["D.I Khan Airport"], Convert.ToDouble(textBoxF_N.Text)));
            _edges.Add(new Edge(_dictNodes["Quaid-e-Azam International Airport"], _dictNodes["Sibi Airport"], Convert.ToDouble(textBoxN_O.Text)));
            _edges.Add(new Edge(_dictNodes["Quaid-e-Azam International Airport"], _dictNodes["Gwadar International Airport"], Convert.ToDouble(textBoxN_M.Text)));

            _edges.Add(new Edge(_dictNodes["Sibi Airport"], _dictNodes["Quaid-e-Azam International Airport"], Convert.ToDouble(textBoxN_O.Text)));
            _edges.Add(new Edge(_dictNodes["Sibi Airport"], _dictNodes["Multan International Airport"], Convert.ToDouble(textBoxE_O.Text)));
            _edges.Add(new Edge(_dictNodes["Sibi Airport"], _dictNodes["Dera Ghazi Khan International Airport"], Convert.ToDouble(textBoxO_P.Text)));
            _edges.Add(new Edge(_dictNodes["Sibi Airport"], _dictNodes["Khuzdar Domestic Airport"], Convert.ToDouble(textBoxO_L.Text)));

            _edges.Add(new Edge(_dictNodes["Dera Ghazi Khan International Airport"], _dictNodes["Sibi Airport"], Convert.ToDouble(textBoxO_P.Text)));
            _edges.Add(new Edge(_dictNodes["Dera Ghazi Khan International Airport"], _dictNodes["Faisalabad International Airport"], Convert.ToDouble(textBoxD_P.Text)));
            _edges.Add(new Edge(_dictNodes["Dera Ghazi Khan International Airport"], _dictNodes["Shaikh Zayed International Airport"], Convert.ToDouble(textBoxP_I.Text)));
            _edges.Add(new Edge(_dictNodes["Dera Ghazi Khan International Airport"], _dictNodes["Jinnah International Airport"], Convert.ToDouble(textBoxP_K.Text)));

            _edges.Add(new Edge(_dictNodes["Gwadar International Airport"], _dictNodes["Quaid-e-Azam International Airport"], Convert.ToDouble(textBoxN_M.Text)));
            _edges.Add(new Edge(_dictNodes["Gwadar International Airport"], _dictNodes["Khuzdar Domestic Airport"], Convert.ToDouble(textBoxM_L.Text)));

            _edges.Add(new Edge(_dictNodes["Khuzdar Domestic Airport"], _dictNodes["Gwadar International Airport"], Convert.ToDouble(textBoxM_L.Text)));
            _edges.Add(new Edge(_dictNodes["Khuzdar Domestic Airport"], _dictNodes["Sibi Airport"], Convert.ToDouble(textBoxO_L.Text)));
            _edges.Add(new Edge(_dictNodes["Khuzdar Domestic Airport"], _dictNodes["Jinnah International Airport"], Convert.ToDouble(textBoxL_K.Text)));

            _edges.Add(new Edge(_dictNodes["Jinnah International Airport"], _dictNodes["Khuzdar Domestic Airport"], Convert.ToDouble(textBoxL_K.Text)));
            _edges.Add(new Edge(_dictNodes["Jinnah International Airport"], _dictNodes["Dera Ghazi Khan International Airport"], Convert.ToDouble(textBoxP_K.Text)));
            _edges.Add(new Edge(_dictNodes["Jinnah International Airport"], _dictNodes["Hyderabad Airport"], Convert.ToDouble(textBoxK_J.Text)));

            _edges.Add(new Edge(_dictNodes["Hyderabad Airport"], _dictNodes["Jinnah International Airport"], Convert.ToDouble(textBoxK_J.Text)));
            _edges.Add(new Edge(_dictNodes["Hyderabad Airport"], _dictNodes["Shaikh Zayed International Airport"], Convert.ToDouble(textBoxI_J.Text)));

            _edges.Add(new Edge(_dictNodes["Shaikh Zayed International Airport"], _dictNodes["Hyderabad Airport"], Convert.ToDouble(textBoxI_J.Text)));
            _edges.Add(new Edge(_dictNodes["Shaikh Zayed International Airport"], _dictNodes["Dera Ghazi Khan International Airport"], Convert.ToDouble(textBoxP_I.Text)));
            _edges.Add(new Edge(_dictNodes["Shaikh Zayed International Airport"], _dictNodes["Allama Iqbal International Airport"], Convert.ToDouble(textBoxH_I.Text)));

            _edges.Add(new Edge(_dictNodes["Allama Iqbal International Airport"], _dictNodes["Shaikh Zayed International Airport"], Convert.ToDouble(textBoxH_I.Text)));
            _edges.Add(new Edge(_dictNodes["Allama Iqbal International Airport"], _dictNodes["Faisalabad International Airport"], Convert.ToDouble(textBoxD_H.Text)));
            _edges.Add(new Edge(_dictNodes["Allama Iqbal International Airport"], _dictNodes["Sialkot International Airport"], Convert.ToDouble(textBoxG_H.Text)));

            _edges.Add(new Edge(_dictNodes["Sialkot International Airport"], _dictNodes["Allama Iqbal International Airport"], Convert.ToDouble(textBoxG_H.Text)));
            _edges.Add(new Edge(_dictNodes["Sialkot International Airport"], _dictNodes["Gilgit Airport"], Convert.ToDouble(textBoxC_G.Text)));

        }

        private void buttonCalc_Click(object sender, EventArgs e)
        {
            try
            {
                string keystart = (string)comboBoxNodeStart.SelectedItem;      
                string keyend = (string)comboBoxNodeEnd.SelectedItem;          

                init();                                                        // run init()
                dijkstra = new Graph(_edges, _nodes);                       
                dijkstra.calculateDistance(_dictNodes[keystart]);             

                List<Node> path = dijkstra.getPathTo(_dictNodes[keyend]);

                //new code dala hay for calculating cost as well 
                if (path.Count > 0)
                {
                    textBox.AppendText("Source to Destination Airport: ");

                    totalCost = 0; // Initialize the total cost

                    string panelNume;
                    string nodeStart = "";
                    string nodeEnd;

                    foreach (Node n in path)
                    {
                        textBox.AppendText(n.Name + " ");
                        if (nodeStart == "")
                        {
                            nodeStart = n.Name;
                        }
                        else
                        {
                            nodeEnd = n.Name;

                            panelNume = nodeStart + "_" + nodeEnd;
                            _panel[panelNume].BackColor = Color.Red;

                            // Calculate and update the total cost
                            totalCost += _edges.Find(edge =>
                                edge.Origin.Name == nodeStart &&
                                edge.Destination.Name == nodeEnd).Distance;

                            nodeStart = nodeEnd;
                        }
                    }

                    // Display the total cost in the "txtdist" TextBox
                    dist.Text = "Total Distance: " + totalCost.ToString();
                    textBox.AppendText("\r\n");
                }

            }

            catch (Exception ex)
            {
                Console.WriteLine("No shortest path between these two,\n Try another one!");
            }
            finally
            {
                Console.WriteLine("Finally block executed.");
            }

        }

        private void comboBoxNodeStart_SelectedIndexChanged(object sender, EventArgs e)  
        {                                                                               
            buttonCalc.Enabled = ((comboBoxNodeStart.SelectedIndex > 0) && (comboBoxNodeEnd.SelectedIndex > 0));

        }

        private void comboBoxNodeEnd_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonCalc.Enabled = ((comboBoxNodeStart.SelectedIndex > 0) && (comboBoxNodeEnd.SelectedIndex > 0));

        }

        private void buttonInit_Click(object sender, EventArgs e) 
        {
            init();
            MessageBox.Show("Initialization has been done!");
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            panelA_B.BackColor = Color.Black;
            panelB_C.BackColor = Color.Black;
            panelB_E.BackColor = Color.Black;
            panelC_D.BackColor = Color.Black;
            panelC_G.BackColor = Color.Black;
            panelD_E.BackColor = Color.Black;
            panelD_H.BackColor = Color.Black;
            panelD_P.BackColor = Color.Black;
            panelE_F.BackColor = Color.Black;
            panelE_O.BackColor = Color.Black;
            panelA_F.BackColor = Color.Black;
            panelF_N.BackColor = Color.Black;
            panelN_O.BackColor = Color.Black;
            panelM_N.BackColor = Color.Black;
            panelO_P.BackColor = Color.Black;
            panelI_P.BackColor = Color.Black;
            panelK_P.BackColor = Color.Black;
            panelL_M.BackColor = Color.Black;
            panelL_O.BackColor = Color.Black;
            panelK_L.BackColor = Color.Black;
            panelJ_K.BackColor = Color.Black;
            panelI_J.BackColor = Color.Black;
            panelH_I.BackColor = Color.Black;
            panelG_H.BackColor = Color.Black;
            MessageBox.Show("Paths have been reset!");
        }

        private void buttonSalveaza_Click(object sender, EventArgs e)
        {
            if (saveFileDialogSalveaza.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                xmlDijkstraWriter = new XmlTextWriter(saveFileDialogSalveaza.FileName, new UnicodeEncoding());
                xmlDijkstraWriter.Formatting = Formatting.Indented;
                xmlDijkstraWriter.WriteStartDocument();
                xmlDijkstraWriter.WriteStartElement("ApmDijkstra");
                xmlDijkstraWriter.WriteStartElement("Distante");

                for (int i = 0; i < 24; i++)
                {
                    xmlDijkstraWriter.WriteStartElement("Distance");
                    string nume;
                    nume = txtBxCosturi[i].Name;
                    nume = nume.Replace("textBox", "");
                    xmlDijkstraWriter.WriteAttributeString("name", nume);
                    double valoare;
                    valoare = Convert.ToDouble(txtBxCosturi[i].Text);
                    xmlDijkstraWriter.WriteAttributeString("value", valoare.ToString());
                    xmlDijkstraWriter.WriteEndElement(); 
                }

                xmlDijkstraWriter.WriteEndElement(); 
                xmlDijkstraWriter.WriteEndElement(); 
                xmlDijkstraWriter.Close();
            }
            MessageBox.Show("The data has been saved!");
        }

        private void buttonIncarca_Click(object sender, EventArgs e)
        {
            if (openFileDialogIncarca.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                xmlDijkstraReader = new XmlTextReader(openFileDialogIncarca.FileName);
                XmlNodeType type;
                while (xmlDijkstraReader.Read())
                {
                    type = xmlDijkstraReader.NodeType;
                    if (type == XmlNodeType.Element)
                    {
                        if (xmlDijkstraReader.Name == "Distanta")
                        {
                            if (xmlDijkstraReader.AttributeCount > 0)
                            {
                                bool numeCitit = false;
                                bool valoareaCitita = false;

                                string numeTextBoxKey = "";
                                string valoareTextBox = "";
                                while (xmlDijkstraReader.MoveToNextAttribute())
                                {
                                    string attributeName = xmlDijkstraReader.Name;

                                    if (attributeName == "nume")
                                    {
                                        numeTextBoxKey = xmlDijkstraReader.Value;
                                        numeCitit = true;
                                    }
                                    else if (attributeName == "valoare")
                                    {
                                        valoareTextBox = xmlDijkstraReader.Value;
                                        valoareaCitita = true;
                                    }

                                    if (numeCitit && valoareaCitita)
                                    {
                                        numeCitit = false;
                                        valoareaCitita = false;

                                        if (_textBox.ContainsKey(numeTextBoxKey))
                                            _textBox[numeTextBoxKey].Text = valoareTextBox;
                                    }
                                }
                            }
                        }
                    }
                } // while
                xmlDijkstraReader.Close();
            }
            MessageBox.Show("The data has been loaded!");
        }

        private void details_Click(object sender, EventArgs e)
        {
            SelectedItem1 = comboBoxNodeStart.SelectedItem?.ToString();
            SelectedItem2 = comboBoxNodeEnd.SelectedItem?.ToString();
            info i = new info(SelectedItem1, SelectedItem2);
            i.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            map m = new map();
            m.Show();
            this.Hide();
        }
    }
}

