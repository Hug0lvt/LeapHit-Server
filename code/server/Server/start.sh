#! /bin/bash

mcs -out:Server.exe Program.cs
screen -d -m -S leapServer mono Server.exe
