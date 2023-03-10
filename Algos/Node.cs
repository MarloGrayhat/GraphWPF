using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mousse.Algos
{
    class Node
    {
        public bool visible = false; // for dfs
        public int v = 0; // (vusuble) 0 - не посещен, 1 - посмотрели и добавили, но не посетили 2 - посетили для BFS 
        public int color = 0;
        public int wieght = int.MaxValue;
        public int parents = -1;
    }
}
