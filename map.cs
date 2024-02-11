using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET.MapProviders;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsPresentation;

using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace ApmDijkstra
{
    public partial class map : Form
    {

        private Dictionary<string, List<(string, double)>> graph; // Airport graph with distances

        string sourceAirport, destinationAirport;
        public map()
        {
            InitializeComponent();
            InitializeGraph();
        }

        private void InitializeGraph()
        {
            // Initialize the graph with airports and distances
            graph = new Dictionary<string, List<(string, double)>>();

            // Add routes with distances

            AddRoute("Karachi", "Hyderabad", 94.33);
            AddRoute("Karachi", "Dera Ghazi Khan", 504.31);
            AddRoute("Gwadar", "Quetta", 569.12);
            AddRoute("Multan", "Islamabad", 324.11);
            AddRoute("Lahore", "Faisalabad", 131.36);
            AddRoute("Lahore", "Sialkot", 78.35);
            AddRoute("Dera Ghazi Khan", "Faisalabad", 209.89);
            AddRoute("Islamabad", "Peshawar", 106.94);
            AddRoute("Khuzdar", "Karachi", 331);
            AddRoute("Islamabad", "Gilgit", 277);
            AddRoute("Peshawar", "DIKhan", 249.45);
            AddRoute("Gilgit", "Faisalabad", 513.38);
            AddRoute("Gilgit", "Sialkot", 382);
            AddRoute("DIKhan", "Multan", 189.9);
            AddRoute("DIKhan", "Quetta", 413.6);
            AddRoute("Multan", "Sibbi", 355);
            AddRoute("Multan", "Faisalabad", 204.39);
            AddRoute("Quetta", "Sibbi", 96.45);
            AddRoute("Sibbi", "Khuzdar", 434);
            AddRoute("Sibbi", "DGKhan", 463);
            AddRoute("DGKhan", "RahimYK", 176);
            AddRoute("Gawadar", "Khuzdar", 520);
            AddRoute("Hyderabad", "RahimYK", 512);
            AddRoute("RahimYK", "Lahore", 527);

            AddReverseRoutes();
        }
        private void AddRoute(string source, string destination, double distance)
        {
            if (!graph.ContainsKey(source))
                graph[source] = new List<(string, double)>();

            graph[source].Add((destination, distance));

            if (!graph.ContainsKey(destination))
                graph[destination] = new List<(string, double)>();

            graph[destination].Add((source, distance));
        }

        private void AddReverseRoutes()
        {
            foreach (var node in graph.Keys.ToList())
            {
                foreach (var (neighbor, distance) in graph[node])
                {
                    if (!graph[neighbor].Any(n => n.Item1 == node))
                    {
                        graph[neighbor].Add((node, distance));
                    }
                }
            }
        }



        private Dictionary<string, PointLatLng> airportCoordinates = new Dictionary<string, PointLatLng>
        {
        { "Jinnah International Airport", new PointLatLng(24.9000, 67.1681) },
        { "Allama Iqbal International Airport", new PointLatLng(31.5216, 74.4036) },
        { "Islamabad International Airport", new PointLatLng(33.6167, 72.7167) },
        { "Dera Ghazi Khan International Airport", new PointLatLng(29.9608, 70.4858) },
        { "Faisalabad International Airport", new PointLatLng(31.3650, 72.9947) },
        { "Gwadar International Airport", new PointLatLng(25.2322, 62.3272) },
        { "Multan International Airport", new PointLatLng(30.2033, 71.4192) },
        { "Peshawar Bacha Khan International Airport", new PointLatLng(33.9939, 71.5147) },
        { "Quaid-e-Azam International Airport", new PointLatLng(30.2400, 66.9400) },
        { "Shaikh Zayed International Airport", new PointLatLng(28.3840, 70.2797) },
        { "Sialkot International Airport", new PointLatLng(32.5356, 74.3639) },
        { "Hyderabad Airport", new PointLatLng(25.3183, 68.3667) },
        { "Gilgit Airport", new PointLatLng(35.9194, 74.3320) },
        { "D.I Khan Airport", new PointLatLng(31.9098, 70.8878) },
        { "Khuzdar Domestic Airport", new PointLatLng(27.7969, 66.6393) },
        { "Sibi Airport", new PointLatLng(29.5712, 67.8479) }

        };

        private void button1_Click(object sender, EventArgs e)
        {
            gMapControl1.Overlays.Clear();
            GMapOverlay markersOverlay = new GMapOverlay("markers");
            GMapOverlay routesOverlay = new GMapOverlay("routes");

            GMarkerGoogle marker1 = new GMarkerGoogle(new PointLatLng(24.9008, 67.1681), GMarkerGoogleType.blue);
            markersOverlay.Markers.Add(marker1);
            GMarkerGoogle marker2 = new GMarkerGoogle(new PointLatLng(31.5203, 74.4104), GMarkerGoogleType.blue);
            markersOverlay.Markers.Add(marker2);
            GMarkerGoogle marker3 = new GMarkerGoogle(new PointLatLng(33.5565, 72.8341), GMarkerGoogleType.blue);
            markersOverlay.Markers.Add(marker3);
            GMarkerGoogle marker4 = new GMarkerGoogle(new PointLatLng(29.96083, 70.48583), GMarkerGoogleType.blue);
            markersOverlay.Markers.Add(marker4);
            GMarkerGoogle marker5 = new GMarkerGoogle(new PointLatLng(31.3650, 72.9947), GMarkerGoogleType.blue);
            markersOverlay.Markers.Add(marker5);
            GMarkerGoogle marker6 = new GMarkerGoogle(new PointLatLng(25.2322, 62.3272), GMarkerGoogleType.blue);
            markersOverlay.Markers.Add(marker6);
            GMarkerGoogle marker7 = new GMarkerGoogle(new PointLatLng(30.2033, 71.4192), GMarkerGoogleType.blue);
            markersOverlay.Markers.Add(marker7);
            GMarkerGoogle marker8 = new GMarkerGoogle(new PointLatLng(33.9939, 71.5147), GMarkerGoogleType.blue);
            markersOverlay.Markers.Add(marker8);
            GMarkerGoogle marker9 = new GMarkerGoogle(new PointLatLng(30.2400, 66.9400), GMarkerGoogleType.blue);
            markersOverlay.Markers.Add(marker9);
            GMarkerGoogle marker10 = new GMarkerGoogle(new PointLatLng(28.3840, 70.2797), GMarkerGoogleType.blue);
            markersOverlay.Markers.Add(marker10);
            GMarkerGoogle marker11 = new GMarkerGoogle(new PointLatLng(32.5356, 74.3639), GMarkerGoogleType.blue);
            markersOverlay.Markers.Add(marker11);
            GMarkerGoogle marker12 = new GMarkerGoogle(new PointLatLng(25.3183, 68.3667), GMarkerGoogleType.blue);
            markersOverlay.Markers.Add(marker12);
            GMarkerGoogle marker13 = new GMarkerGoogle(new PointLatLng(35.9194, 74.3320), GMarkerGoogleType.blue);
            markersOverlay.Markers.Add(marker13);
            GMarkerGoogle marker14 = new GMarkerGoogle(new PointLatLng(31.9098, 70.8878), GMarkerGoogleType.blue);
            markersOverlay.Markers.Add(marker14);
            GMarkerGoogle marker15 = new GMarkerGoogle(new PointLatLng(27.7969, 66.6393), GMarkerGoogleType.blue);
            markersOverlay.Markers.Add(marker15);
            GMarkerGoogle marker16 = new GMarkerGoogle(new PointLatLng(29.5712, 67.8479), GMarkerGoogleType.blue);
            markersOverlay.Markers.Add(marker16);


            gMapControl1.Overlays.Add(markersOverlay);

            // Route 1: Sibbi to Khuzdar
            List<PointLatLng> routePointsSibbiToKhuzdar = new List<PointLatLng>
{
    new PointLatLng(29.5712, 67.8479), // Sibbi
    new PointLatLng(27.7969, 66.6393)  // Khuzdar
};
            GMap.NET.WindowsForms.GMapRoute routeSibbiToKhuzdar = new GMap.NET.WindowsForms.GMapRoute(routePointsSibbiToKhuzdar, "Sibbi to Khuzdar");
            routeSibbiToKhuzdar.Stroke = new Pen(Color.Cyan, 2); // Set your desired color
            routesOverlay.Routes.Add(routeSibbiToKhuzdar);


            // Route 2: Karachi to Khuzdar
            List<PointLatLng> routePointsKarachiToKhuzdar = new List<PointLatLng>
{
    new PointLatLng(24.9000, 67.1681), // Karachi
    new PointLatLng(27.7969, 66.6393)  // Khuzdar
};
            GMap.NET.WindowsForms.GMapRoute routeKarachiToKhuzdar = new GMap.NET.WindowsForms.GMapRoute(routePointsKarachiToKhuzdar, "Karachi to Khuzdar");
            routeKarachiToKhuzdar.Stroke = new Pen(Color.Cyan, 2); // Set your desired color
            routesOverlay.Routes.Add(routeKarachiToKhuzdar);

            // Route 3: Gwadar to Khuzdar
            List<PointLatLng> routePointsGwadarToKhuzdar = new List<PointLatLng>
{
    new PointLatLng(25.2322, 62.3272), // Gwadar
    new PointLatLng(27.7969, 66.6393)  // Khuzdar
};
            GMap.NET.WindowsForms.GMapRoute routeGwadarToKhuzdar = new GMap.NET.WindowsForms.GMapRoute(routePointsGwadarToKhuzdar, "Gwadar to Khuzdar");
            routeGwadarToKhuzdar.Stroke = new Pen(Color.Blue, 2); // Set your desired color
            routesOverlay.Routes.Add(routeGwadarToKhuzdar);


            // Route 4: DIkhan to Multan
            List<PointLatLng> routePointsDIkhanToMultan = new List<PointLatLng>
{
    new PointLatLng(25.3183, 68.3667), // DIkhan
    new PointLatLng(30.2033, 71.4192)  // Multan
};
            GMap.NET.WindowsForms.GMapRoute routeDIkhanToMultan = new GMap.NET.WindowsForms.GMapRoute(routePointsDIkhanToMultan, "DIKhan to Multan");
            routeDIkhanToMultan.Stroke = new Pen(Color.Orange, 2); // Set your desired color
            routesOverlay.Routes.Add(routeDIkhanToMultan);


            // Route 5: DI to Peshawar
            List<PointLatLng> routePointsDIKhanToPeshawar = new List<PointLatLng>
{
    new PointLatLng(31.9098, 70.8878), // DIKhan
    new PointLatLng(33.9939, 71.5147)  // Peshawar
};
            GMap.NET.WindowsForms.GMapRoute routeDIKhanToPeshawar = new GMap.NET.WindowsForms.GMapRoute(routePointsDIKhanToPeshawar, "DI Khan to Peshawar");
            routeDIKhanToPeshawar.Stroke = new Pen(Color.Yellow, 2); // Set your desired color
            routesOverlay.Routes.Add(routeDIKhanToPeshawar);

           
            // Route 6: Quetta to DIKhan
            List<PointLatLng> routePointsQuettaToDIKhan = new List<PointLatLng>
{
    new PointLatLng(30.2400, 66.9400), // Quetta
    new PointLatLng(31.9098, 70.8878)  // DIKhan
};
            GMap.NET.WindowsForms.GMapRoute routeQuettaToDIKhan = new GMap.NET.WindowsForms.GMapRoute(routePointsQuettaToDIKhan, "Quetta to DI Khan");
            routeQuettaToDIKhan.Stroke = new Pen(Color.Red, 2); // Set your desired color
            routesOverlay.Routes.Add(routeQuettaToDIKhan);


            // Route 7: Quetta to Sibbi
            List<PointLatLng> routePointsQuettaToSibbi = new List<PointLatLng>
{
    new PointLatLng(30.2400, 66.9400), // Quetta
    new PointLatLng(29.5712, 67.8479)  // Sibbi
};
            GMap.NET.WindowsForms.GMapRoute routeQuettaToSibbi = new GMap.NET.WindowsForms.GMapRoute(routePointsQuettaToSibbi, "Quetta to Sibbi");
            routeQuettaToSibbi.Stroke = new Pen(Color.Blue, 2); // Set your desired color
            routesOverlay.Routes.Add(routeQuettaToSibbi);

            // Route 8: Gilgit to Islamabad
            List<PointLatLng> routePointsGilgitToIslamabad = new List<PointLatLng>
{
    new PointLatLng(35.9194, 74.3320), // Gilgit
    new PointLatLng(33.6167, 72.7167)  // Islamabad
};
            GMap.NET.WindowsForms.GMapRoute routeGilgitToIslamabad = new GMap.NET.WindowsForms.GMapRoute(routePointsGilgitToIslamabad, "Gilgit to Islamabad");
            routeGilgitToIslamabad.Stroke = new Pen(Color.Green, 2); // Set your desired color
            routesOverlay.Routes.Add(routeGilgitToIslamabad);

            // Route 9: Faisalabad to Multan
            List<PointLatLng> routePointsFaisalabadToMultan = new List<PointLatLng>
{
    new PointLatLng(31.3650, 72.9947), // Faisalabad
    new PointLatLng(30.2033, 71.4192)  // Multan
};
            GMap.NET.WindowsForms.GMapRoute routeFaisalabadToMultan = new GMap.NET.WindowsForms.GMapRoute(routePointsFaisalabadToMultan, "Faisalabad to Multan");
            routeFaisalabadToMultan.Stroke = new Pen(Color.Purple, 2); // Set your desired color
            routesOverlay.Routes.Add(routeFaisalabadToMultan);


            // Route 10: Dera Ghazi Khan to Sibbi
            List<PointLatLng> routePointsDeraGhaziKhanToSibbi = new List<PointLatLng>
{
    new PointLatLng(29.9608, 70.4858), // Dera Ghazi Khan
    new PointLatLng(29.5712, 67.8479)  // Sibbi
};
            GMap.NET.WindowsForms.GMapRoute routeDeraGhaziKhanToSibbi = new GMap.NET.WindowsForms.GMapRoute(routePointsDeraGhaziKhanToSibbi, "Dera Ghazi Khan to Sibbi");
            routeDeraGhaziKhanToSibbi.Stroke = new Pen(Color.Cyan, 2); // Set your desired color
            routesOverlay.Routes.Add(routeDeraGhaziKhanToSibbi);

            // Route 11: rahim to DGKhan
            List<PointLatLng> routePointsDGKhanToRahimYarKhan = new List<PointLatLng>
{
    new PointLatLng(25.2322, 62.3272), // dg
    new PointLatLng(28.3840, 70.2797)  // Rahim Yar Khan
};
            GMap.NET.WindowsForms.GMapRoute routeDGKhanToRahimYarKhan = new GMap.NET.WindowsForms.GMapRoute(routePointsDGKhanToRahimYarKhan, "Dera Ghazi Khan to Rahim Yar Khan");
            routeDGKhanToRahimYarKhan.Stroke = new Pen(Color.Green, 2); // Set your desired color
            routesOverlay.Routes.Add(routeDGKhanToRahimYarKhan);

            // Route 12: rahim to Hyderabad
            List<PointLatLng> routePointsGwadarToHyderabad = new List<PointLatLng>
{
    new PointLatLng(25.3183, 68.3667), // Hyderabad
    new PointLatLng(28.3840, 70.2797)  // Rahim Yar Khan
};
            GMap.NET.WindowsForms.GMapRoute routeGwadarToHyderabad = new GMap.NET.WindowsForms.GMapRoute(routePointsGwadarToHyderabad, "Gwadar to Hyderabad");
            routeGwadarToHyderabad.Stroke = new Pen(Color.Green, 2); // Set your desired color
            routesOverlay.Routes.Add(routeGwadarToHyderabad);


            // Route 13: Karachi to Dera Ghazi Khan
            List<PointLatLng> routePointsKarachiToDeraGhaziKhan = new List<PointLatLng>
{
    new PointLatLng(24.9000, 67.1681), // Karachi
    new PointLatLng(29.9608, 70.4858)  // Dera Ghazi Khan
};
            GMap.NET.WindowsForms.GMapRoute routeKarachiToDeraGhaziKhan = new GMap.NET.WindowsForms.GMapRoute(routePointsKarachiToDeraGhaziKhan, "Karachi to Dera Ghazi Khan");
            routeKarachiToDeraGhaziKhan.Stroke = new Pen(Color.Green, 2);
            routesOverlay.Routes.Add(routeKarachiToDeraGhaziKhan);


            // Route 14: Gwadar to Quetta
            List<PointLatLng> routePointsGwadarToQuetta = new List<PointLatLng>
{
    new PointLatLng(25.2322, 62.3272), // Gwadar
    new PointLatLng(30.2400, 66.9400)  // Quetta
};
            GMap.NET.WindowsForms.GMapRoute routeGwadarToQuetta = new GMap.NET.WindowsForms.GMapRoute(routePointsGwadarToQuetta, "Gwadar to Quetta");
            routeGwadarToQuetta.Stroke = new Pen(Color.Green, 2); // Set your desired color
            routesOverlay.Routes.Add(routeGwadarToQuetta);


            // Route 15: Dera Ghazi Khan to Faisalabad
            List<PointLatLng> routePointsDeraGhaziKhanToFaisalabad = new List<PointLatLng>
{
    new PointLatLng(29.9608, 70.4858), // Dera Ghazi Khan
    new PointLatLng(31.3650, 72.9947)  // Faisalabad
};
            GMap.NET.WindowsForms.GMapRoute routeDeraGhaziKhanToFaisalabad = new GMap.NET.WindowsForms.GMapRoute(routePointsDeraGhaziKhanToFaisalabad, "Dera Ghazi Khan to Faisalabad");
            routeDeraGhaziKhanToFaisalabad.Stroke = new Pen(Color.Yellow, 2); // Set your desired color
            routesOverlay.Routes.Add(routeDeraGhaziKhanToFaisalabad);



            // Route 16: Lahore to Faisalabad
            List<PointLatLng> routePointsLahoreToFaisalabad = new List<PointLatLng>
{
    new PointLatLng(31.5216, 74.4036), // Lahore
    new PointLatLng(31.3650, 72.9947)  // Faisalabad
};
            GMap.NET.WindowsForms.GMapRoute routeLahoreToFaisalabad = new GMap.NET.WindowsForms.GMapRoute(routePointsLahoreToFaisalabad, "Lahore to Faisalabad");
            routeLahoreToFaisalabad.Stroke = new Pen(Color.Orange, 2); // Set your desired color
            routesOverlay.Routes.Add(routeLahoreToFaisalabad);


            // Route 17: Lahore to RahimYK
            List<PointLatLng> routePointsLahoreToRahimYK = new List<PointLatLng>
{
    new PointLatLng(31.5216, 74.4036), // Lahore
    new PointLatLng(28.3840, 70.2797)  // RahimYK
};
            GMap.NET.WindowsForms.GMapRoute routeLahoreToRahimYK = new GMap.NET.WindowsForms.GMapRoute(routePointsLahoreToRahimYK, "Lahore to Rahim Yar Khan");
            routeLahoreToRahimYK.Stroke = new Pen(Color.Yellow, 2); // Set your desired color
            routesOverlay.Routes.Add(routeLahoreToRahimYK);


            // Route 18: Lahore to Sialkot
            List<PointLatLng> routePointsLahoreToSialkot = new List<PointLatLng>
{
    new PointLatLng(31.5216, 74.4036), // Lahore
    new PointLatLng(32.5356, 74.3639)  // Sialkot
};
            GMap.NET.WindowsForms.GMapRoute routeLahoreToSialkot = new GMap.NET.WindowsForms.GMapRoute(routePointsLahoreToSialkot, "Lahore to Sialkot");
            routeLahoreToSialkot.Stroke = new Pen(Color.Purple, 2); // Set your desired color
            routesOverlay.Routes.Add(routeLahoreToSialkot);


            // Route 19: Faisalabad to Gilgit
            List<PointLatLng> routePointsFaisalabadToGilgit = new List<PointLatLng>
{
    new PointLatLng(31.3650, 72.9947), // Faisalabad
    new PointLatLng(35.9194, 74.3320)  // Gilgit
};
            GMap.NET.WindowsForms.GMapRoute routeFaisalabadToGilgit = new GMap.NET.WindowsForms.GMapRoute(routePointsFaisalabadToGilgit, "Faisalabad to Gilgit");
            routeFaisalabadToGilgit.Stroke = new Pen(Color.Blue, 2); // Set your desired color
            routesOverlay.Routes.Add(routeFaisalabadToGilgit);

            // Route 20: Gilgit to Sialkot
            List<PointLatLng> routePointsGilgitToSialkot = new List<PointLatLng>
{
    new PointLatLng(35.9194, 74.3320), // Gilgit
    new PointLatLng(32.5356, 74.3639)  // Sialkot
};
            GMap.NET.WindowsForms.GMapRoute routeGilgitToSialkot = new GMap.NET.WindowsForms.GMapRoute(routePointsGilgitToSialkot, "Gilgit to Sialkot");
            routeGilgitToSialkot.Stroke = new Pen(Color.Green, 2); // Set your desired color
            routesOverlay.Routes.Add(routeGilgitToSialkot);

            // Route 21: Islamabad to Peshawar
            List<PointLatLng> routePointsIslamabadToPeshawar = new List<PointLatLng>
{
    new PointLatLng(33.6167, 72.7167), // Islamabad
    new PointLatLng(33.9939, 71.5147)  // Peshawar
};
            GMap.NET.WindowsForms.GMapRoute routeIslamabadToPeshawar = new GMap.NET.WindowsForms.GMapRoute(routePointsIslamabadToPeshawar, "Islamabad to Peshawar");
            routeIslamabadToPeshawar.Stroke = new Pen(Color.Red, 2); // Set your desired color
            routesOverlay.Routes.Add(routeIslamabadToPeshawar);

            // Route 22: Multan to Islamabad
            List<PointLatLng> routePointsMultanToIslamabad = new List<PointLatLng>
{
    new PointLatLng(30.2033, 71.4192), // Multan
    new PointLatLng(33.6167, 72.7167)  // Islamabad
};
            GMap.NET.WindowsForms.GMapRoute routeMultanToIslamabad = new GMap.NET.WindowsForms.GMapRoute(routePointsMultanToIslamabad, "Multan to Islamabad");
            routeMultanToIslamabad.Stroke = new Pen(Color.Yellow, 2);
            routesOverlay.Routes.Add(routeMultanToIslamabad);


            // Route 23: Karachi to hyderabad
            List<PointLatLng> routePointsKarachitoHyderabad = new List<PointLatLng>
{
    new PointLatLng(24.9000, 67.1681), // karachi
    new PointLatLng(25.3182, 68.3661)  // hyderabad
};
            GMap.NET.WindowsForms.GMapRoute routeKarachitoHyderabad = new GMap.NET.WindowsForms.GMapRoute(routePointsKarachitoHyderabad, "Multan to Islamabad");
            routeKarachitoHyderabad.Stroke = new Pen(Color.Purple, 2);
            routesOverlay.Routes.Add(routeKarachitoHyderabad);


            // Route 24: Multan to Sibbi
            List<PointLatLng> routePointsMultanToSibbi = new List<PointLatLng>
{
    new PointLatLng(30.2033, 71.4192), // Multan
    new PointLatLng(29.5712, 67.8479)  // Sibbi
};
            GMap.NET.WindowsForms.GMapRoute routeMultanToSibbi = new GMap.NET.WindowsForms.GMapRoute(routePointsMultanToSibbi, "Multan to Sibbi");
            routeMultanToSibbi.Stroke = new Pen(Color.Yellow, 2);
            routesOverlay.Routes.Add(routeMultanToSibbi);


            gMapControl1.Overlays.Add(routesOverlay);
            gMapControl1.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormApmDijkstra f = new FormApmDijkstra();
            f.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            initial i = new initial();
            i.Show();
            this.Hide();
        }

        private void map_Load(object sender, EventArgs e)
        {

            gMapControl1.MapProvider = GMapProviders.GoogleMap;
            double initialLat = 30.3753;
            double initialLng = 69.3451;
            gMapControl1.Position = new PointLatLng(initialLat, initialLng);
            gMapControl1.MinZoom = 1;
            gMapControl1.MaxZoom = 18;
            gMapControl1.Zoom = 6; // Adjust this value to zoom in or out
            gMapControl1.MouseWheelZoomType = MouseWheelZoomType.MousePositionWithoutCenter;
            gMapControl1.MapProvider = GMapProviders.GoogleMap;
            GMaps.Instance.Mode = AccessMode.ServerOnly;
            gMapControl1.Refresh();
        }
    }
}









