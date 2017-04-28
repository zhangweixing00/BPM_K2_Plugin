// Guids.cs
// MUST match guids.h
using System;

namespace star.VSPackage1
{
    static class GuidList
    {
        public const string guidVSPackage1PkgString = "0593694d-8d9e-48ba-b396-0de4335d8c8f";
        public const string guidVSPackage1CmdSetString = "d41d1b2b-4ab3-4117-bc1a-a0ce56766f0b";
        public const string guidToolWindowPersistanceString = "b9bbf268-4732-41b1-a043-0582b304bdbe";
        public const string guidVSPackage1EditorFactoryString = "8a6723db-07ef-47bc-9d93-9de61872e721";

        public static readonly Guid guidVSPackage1CmdSet = new Guid(guidVSPackage1CmdSetString);
        public static readonly Guid guidVSPackage1EditorFactory = new Guid(guidVSPackage1EditorFactoryString);
    };
}