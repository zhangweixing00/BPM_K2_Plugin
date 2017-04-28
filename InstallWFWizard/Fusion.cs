using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections;

namespace InstallWFWizard
{
    public class Fusion
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct FUSION_INSTALL_REFERENCE
        {
            public uint cbSize;
            public uint dwFlags;
            public Guid guidScheme; // contains one of the pre-defined guids.
            public string szIdentifier;  // unique identifier for app installing this  assembly.
            public string szNonCannonicalData;  // data is description; relevent to the guid above
        }

        public enum ASM_INSTALL_FLAG
        {
            IASSEMBLYCACHE_INSTALL_FLAG_REFRESH = 1,
            IASSEMBLYCACHE_INSTALL_FLAG_FORCE_REFRESH = 2
        }

        public interface IAssemblyCache
        {
            [PreserveSig]
            int InstallAssembly(
              ASM_INSTALL_FLAG dwFlags,
              [MarshalAs(UnmanagedType.LPWStr)] string pszManifestFilePath,
              IntPtr pvReserved);
        }

        [DllImportAttribute("fusion.dll")]
        public static extern int CreateAssemblyCache(
          out IAssemblyCache ppAsmCache,
          uint dwReserved);
    }

    public class FusionUtil : Fusion, IEnumerable
    {
        public void Install(string fileName)
        {
            IAssemblyCache cache;

            Fusion.CreateAssemblyCache(out cache, 0);

            cache.InstallAssembly(Fusion.ASM_INSTALL_FLAG.IASSEMBLYCACHE_INSTALL_FLAG_REFRESH, fileName, IntPtr.Zero);
        }
    }
}
