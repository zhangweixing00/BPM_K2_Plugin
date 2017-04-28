using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LogProcess
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "C:\\1.txt";
            StringBuilder content = new StringBuilder();
            using (StreamReader sr = new StreamReader(path, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if (string.IsNullOrEmpty(line))
                    {
                        continue;
                    }
                    if (GoodLine(line))
                    {
                        line = line.Replace(" DEBUG logger [(null)]  - ", ",");
                        line = line.Replace("erpFormCode:", "审批中,");
                        line = line.Replace("Proc:", "流程结束通知,");

                        content.AppendLine(line);
                    }
                }

            }
            File.WriteAllText("C:\\2_process.txt", content.ToString(),Encoding.Unicode);
            using (StreamReader sr = new StreamReader("C:\\2_process.txt", Encoding.Unicode))
            {
                List<ItemInfo> infos = new List<ItemInfo>();
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] lineParams = line.Split(',');
                    if (lineParams.Length == 4)
                    {
                        infos.Add(new ItemInfo()
                        {
                            PTime = lineParams[0],
                            ECode = lineParams[3],
                            CStatus = "审批中"
                        });
                    }
                    else if (lineParams.Length == 5)
                    {
                        infos.Add(new ItemInfo()
                        {
                            PTime = lineParams[0],
                            ECode = lineParams[3],
                            CStatus = lineParams[4]
                        });
                    }
                }
                ItemInfo.Write(infos);
            }
        }
        public class ItemInfo
        {
            public string PTime { get; set; }

            public string ECode { get; set; }

            public string CStatus { get; set; }

            public override string ToString()
            {
                return string.Format("{0}\t{1}\t{2}\r\n", PTime, ECode, CStatus);
            }
            public void Write()
            {
                File.AppendAllText("C:\\3_p.txt", this.ToString());
            }
            public static void Write(List<ItemInfo> infos)
            {
                foreach (var item in infos)
                {
                    item.Write();
                }
            }

        }
        private static bool GoodLine(string line)
        {
            string[] unGoodString = { "df:", "Email--Result:", "Params:", "Result-e:", "在 ERP_", "在 Invoke", "NO:ex:", "Result:False"
                                    ,"header","footer"};

            return unGoodString.Count(x => line.Contains(x)) == 0;
        }
    }
}
