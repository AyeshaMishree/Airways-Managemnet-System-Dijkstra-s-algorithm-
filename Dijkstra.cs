using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApmDijkstra
{
    class Graph
    {
        private List<Node> _nodes;                       
        private List<Edge> _edges;                        
        private List<Node> _basis;
        private Dictionary<string, double> _dist;               
        private Dictionary<string, Node> _previous;             
 
        // Constructor
        public Graph(List<Edge> edges, List<Node> nodes)
        {
 
            _edges = edges;
            _nodes = nodes;
            _basis = new List<Node>();                     
            _dist = new Dictionary<string, double>();      
            _previous = new Dictionary<string, Node>();
 
            foreach (Node n in _nodes)                     
            {
                _previous.Add(n.Name, null);
                _basis.Add(n);
                _dist.Add(n.Name, double.MaxValue);
            }
        }
 
       
        public void calculateDistance(Node start)           
        {
            _dist[start.Name] = 0;
 
            while (_basis.Count > 0)
            {
                Node u = getNodeWithSmallestDistance();
                if (u == null)
                {
                    _basis.Clear();
                }
                else
                {
                    foreach (Node v in getNeighbors(u))
                    {
                        double alt = _dist[u.Name] +
                                getDistanceBetween(u, v);
                        if (alt < _dist[v.Name])
                        {
                            _dist[v.Name] = alt;
                            _previous[v.Name] = u;
                        }
                    }
                    _basis.Remove(u);
                }
            }
        }
 
     
        public List<Node> getPathTo(Node d)                    
        {
            List<Node> path = new List<Node>();
 
            path.Insert(0, d);
 
            while (_previous[d.Name] != null)
            {
                d = _previous[d.Name];
                path.Insert(0, d);
            }
 
            return path;
        }
 
       
        public Node getNodeWithSmallestDistance()            
        {
            double distance = double.MaxValue;
            Node smallest = null;
 
            foreach (Node n in _basis)
            {
                if (_dist[n.Name] < distance)
                {
                    distance = _dist[n.Name];
                    smallest = n;
                }
            }
 
            return smallest;
        }

        
        public List<Node> getNeighbors(Node n)                  
        {
            List<Node> neighbors = new List<Node>();
 
            foreach (Edge e in _edges)
            {
                if (e.Origin.Equals(n) && _basis.Contains(n))
                {
                    neighbors.Add(e.Destination);
                }
            }
 
            return neighbors;
        }
 
        
        public double getDistanceBetween(Node o, Node d)            
        {
            foreach (Edge e in _edges)
            {
                if (e.Origin.Equals(o) && e.Destination.Equals(d))
                {
                    return e.Distance;
                }
            }
 
            return 0;
        }
    }
}
