using System;

namespace AStar {
    class MainClass {
        public static void Main (string[] args) {
            while( true ) {
                // Build the map!
                Node a = new Node( "A", -19, 11 );
                Node b = new Node( "B", -13, 13 );
                Node c = new Node( "C", 4, 14 );
                Node d = new Node( "D", -4, 12 );
                Node e = new Node( "E", -8, 3 );
                Node f = new Node( "F", -18, 1 );
                Node g = new Node( "G", -12, -9 );
                Node h = new Node( "H", 12, -9 );
                Node i = new Node( "I", -18, -11 );
                Node j = new Node( "J", -4, -11 );
                Node k = new Node( "K", -12, -14 );
                Node l = new Node( "L", 2, -18 );
                Node m = new Node( "M", 18, -13 );
                Node n = new Node( "N", 4, -9 );
                Node o = new Node( "O", 22, 11 );
                Node p = new Node( "P", 18, 3 );
                a.Add( b, e );
                b.Add( a, d );
                c.Add( d, p, e );
                d.Add( c, e, b );
                e.Add( d, a, c, g, j, n );
                f.Add( g, i );
                g.Add( f, j, e );
                h.Add( n , p );
                i.Add( f , k );
                j.Add( k , e, l, g );
                k.Add( i , j , l );
                l.Add( k , j , m );
                m.Add( l , o , p );
                n.Add( h , e );
                o.Add( m , p );
                p.Add( m , c , o , h );
                Map map = new Map( a );
                string valid = "ABCDEFGHIJKLMNOP";
                Console.WriteLine( "Starting Node [A-P](x to exit):" );
                string start = Console.ReadLine();
                start = start.ToUpper();
                if ( valid.IndexOf( start ) == -1 || start == "" ) {
                    break;
                }
                Console.WriteLine( "Ending Node [A-P](x to exit):" );
                string end = Console.ReadLine();
                end = end.ToUpper();
                if ( valid.IndexOf( end ) == -1 || end == "") {
                    break;
                }
                map.PathFindAStar( end, start );
            }
        }
    }
}
