using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenCapture
{
    public class Operation
    {
        public OperationType operationType { get; set; }

        public Pen penBefore { get; set; } 
        
        public Pen nowPen { get; set; } 
        
        
        public Font font { get; set; } 
        
        
        public Brush brush { get; set; } 
        
        
        public Point startPos { get; set; } 
        
        
        public Point endPos { get; set; } 
        
        
        public Rectangle region { get; set; } 
        
        
        public string content { get; set; }
    }
}
