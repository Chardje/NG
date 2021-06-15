using System;
using System.Collections.Generic;
using System.Text;

namespace NG_V0._0._0
{
    class Object
    {
        Vector center;
        Vector vel;
        Vector spin;
        long mass;
        List<Object> constituents;
        internal Object()
        {

        }
    }
    class Atom : Object
    {
        Atom():base()
        {

        }
    }

    class Vector
    {
        long x;
        long y;
        long z;
    }
}
