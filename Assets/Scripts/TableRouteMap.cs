using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableRouteMap
{
    public static IDictionary<string, float[,]> TableRouteDictionary = new Dictionary<string, float[,]>()
    {
        { "Table1Route1", new[,] { { -5.29f, -6.22f }, {-5.29f, -5.21f }, { -7.67f, -5.21f }, {-7.67f, -4.02f} } },
        { "Table1Route2", new[,] { { -5.29f, -6.22f }, {-5.29f, -5.21f }, { -6.56f, -5.21f }, {-6.56f, -4.02f} } },
        { "Table1Route3", new[,] { { -5.29f, -6.22f }, {-5.29f, -1.77f }, { -6.56f, -1.77f }, {-6.56f, -2.50f} } },
        { "Table1Route4", new[,] { { -5.29f, -6.22f }, {-5.29f, -1.77f }, { -7.67f, -1.77f }, {-7.67f, -2.50f} } },
        { "Table2Route1", new[,] { { -5.29f, -6.22f }, {-5.29f, -5.21f }, { -2.99f, -5.21f }, {-2.99f, -4.02f} } },
        { "Table2Route2", new[,] { { -5.29f, -6.22f }, {-5.29f, -5.21f }, { -4.11f, -5.21f }, {-4.11f, -4.02f} } },
        { "Table2Route3", new[,] { { -5.29f, -6.22f }, {-5.29f, -1.77f }, { -4.11f, -1.77f }, {-4.11f, -2.50f} } },
        { "Table2Route4", new[,] { { -5.29f, -6.22f }, {-5.29f, -1.77f }, { -2.99f, -1.77f }, {-2.98f, -2.50f} } },
        { "Table3Route1", new[,] { { -5.29f, -6.22f }, {-5.29f, -0.35f }, { -7.67f, -0.35f }, {-7.67f, 0.49f} } },
        { "Table3Route2", new[,] { { -5.29f, -6.22f }, {-5.29f, -0.35f }, { -6.56f, -0.35f }, {-6.56f, 0.49f} } },
        { "Table3Route3", new[,] { { -5.29f, -6.22f }, {-5.29f, 2.79f }, { -6.56f, 2.79f }, {-6.56f, 2.15f} } },
        { "Table3Route4", new[,] { { -5.29f, -6.22f }, {-5.29f, 2.79f }, { -7.67f, 2.79f }, {-7.67f, 2.15f} } },
        { "Table4Route1", new[,] { { -5.29f, -6.22f }, {-5.29f, -0.35f }, { -2.99f, -0.35f }, {-2.99f, 0.49f} } },
        { "Table4Route2", new[,] { { -5.29f, -6.22f }, {-5.29f, -0.35f }, { -4.11f, -0.35f }, {-4.11f, 0.49f} } },
        { "Table4Route3", new[,] { { -5.29f, -6.22f }, {-5.29f, 2.79f }, { -2.99f, 2.79f }, {-2.99f, 2.15f} } },
        { "Table4Route4", new[,] { { -5.29f, -6.22f }, {-5.29f, 2.79f }, { -4.11f, 2.79f }, {-4.11f, 2.15f} } }
    };

}
