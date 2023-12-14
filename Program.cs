﻿Simulation sim = new(1080, 720, "Physics Simulation");

sim.Lines.AddRange(Line.Bezier(1f, 1f, 8f, 5f, 1.5f, 2.5f, 3.5f, 5f));
sim.Lines.Add(new(8f, 5f, 12f, 5f));
sim.Lines.Add(new(15f, 4f, 9f, 10f));
sim.Lines.AddRange(Line.Bezier(9f, 10f, 6f, 9.5f, 7.5f, 11.5f, 6f, 9.5f));
sim.Lines.Add(new(1f, 6f, 1f, 7f));
sim.Lines.Add(new(1f, 7f, 2f, 6.8f));

sim.Balls.Add(new(3f, 0.3f, 2f, 0.2f));

sim.Run();