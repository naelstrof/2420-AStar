using System;
using System.Collections;

namespace AStar {
    class Node {
        public string name;
        public Vector2 p;
        public ArrayList neighbors = new ArrayList();
        public double tec; // Total estimated cost
        public double guess; // Heristic
        public double csf; // Cost so far
        public Node cf; // Came from
        public Node( string n, double x, double y ) {
            p = new Vector2( x, y );
            name = n;
        }
        public void Add( params Node[] n ) {
            for ( int i = 0; i<n.Length; i++ ) {
                neighbors.Add( n[i] );
            }
        }
    }
    class Map {
        Hashtable nodes = new Hashtable();
        public Map( Node node ) {
            // Flatten the structure to easily query
            // nodes by name.
            ArrayList open = new ArrayList();
            ArrayList closed = new ArrayList();
            open.Add( node );
            while( open.Count > 0 ) {
                Traverse( open, closed );
            }
            foreach( Node element in closed ) {
                nodes.Add( element.name, element );
            }
        }
        public static void Traverse( ArrayList open, ArrayList closed ) {
            Node c = (Node)open[0];
            foreach( Node element in c.neighbors ) {
                if( !closed.Contains( element ) && !open.Contains(element) ) {
                    open.Add( element );
                }
            }
            open.Remove( c );
            closed.Add( c );
        }
        public void PathFindAStar( string sa, string sb ) {
            Node start = (Node)nodes[sa];
            Node end = (Node)nodes[sb];
            ArrayList open = new ArrayList();
            ArrayList closed = new ArrayList();
            start.guess = start.p.Distance( end.p );
            start.csf = 0;
            start.tec = start.guess;
            start.cf = null;
            open.Add (start);
            while ( BestBet( open, closed, end ) == null ) { }
            Console.WriteLine( "Shortest path from " + sb + " to " + sa + ":" );
            Console.Write( "\t" );
            Node runner = end;
            while( runner != null ) {
                Console.Write( runner.name + ", " );
                runner = runner.cf;
            }
            Console.WriteLine("");
        }
        public static Node BestBet( ArrayList open, ArrayList closed, Node destination ) {
            Node least = (Node)open[0];
            foreach( Node runner in open ) {
                if ( runner.tec < least.tec ) {
                    least = runner;
                }
            }
            return Process( open, closed, least, destination );
        }
        public static Node Process( ArrayList open, ArrayList closed, Node c, Node d ) {
            foreach ( Node runner in c.neighbors ) {
                if ( runner == c.cf ) {
                    continue;
                }
                if ( runner.cf != null ) {
                    double tec = runner.p.Distance( d.p ) + c.csf + c.p.Distance( runner.p );
                    if ( tec > runner.tec ) {
                        continue;
                    }
                }
                runner.cf = c;
                if ( runner == d ) {
                    //Console.WriteLine( "Found " + d.name + " from " + c.name + "!" );
                    return d;
                }
                runner.guess = runner.p.Distance( d.p );
                runner.csf = c.csf + c.p.Distance( runner.p );
                runner.tec = runner.guess + runner.csf;
                //Console.Write( runner.name );
                //Console.Write( ", cf: " + runner.cf.name );
                //Console.WriteLine( ", tec: " + runner.tec );
                if ( !open.Contains( runner ) ) {
                    open.Add( runner );
                }
            }
            //Console.WriteLine( "Removed " + c.name );
            closed.Add( c );
            open.Remove( c );
            return null;
        }
    }
}
