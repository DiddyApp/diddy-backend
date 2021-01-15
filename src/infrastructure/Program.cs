using Amazon.CDK;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure
{
    sealed class Program
    {
        public static void Main(string[] args)
        {
            var app = new App();
            new LambdasStack(app, "LambdasStack");
            app.Synth();
        }
    }
}
