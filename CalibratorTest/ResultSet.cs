using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Reflection;

namespace FakeColourFinder
{
    [Serializable()]
    public class Result
    {
        public double Y { get; set; }
        public double R { get; set; }
        public double G { get; set; }
        public double B { get; set; }

        public double RGBY
        {
            set
            {
                R = value;
                G = value;
                B = value;
                Y = value;
            }
        }
    }

    [Serializable()]
    public class ResultItem
    {
        // For serialisation.
        public ResultItem()
        {
        }

        public ResultItem(int graylevel)
        {
            Target = new Result();
            Actual = new Result();
            GrayLevel = graylevel;
        }

        public Result Target { get; set; }

        public Result Actual { get; set; }

        public int GrayLevel { get; set; }

        public double YDiff
        {
            get
            {
                return PercentDifferent(100, Target.Y, Actual.Y);
            }
        }

        public double RDiff
        {
            get
            {
                return PercentDifferent(255, Target.R, Actual.R);
            }
        }

        public double GDiff
        {
            get
            {
                return PercentDifferent(255, Target.G, Actual.G);
            }
        }

        public double BDiff
        {
            get
            {
                return PercentDifferent(255, Target.B, Actual.B);
            }
        }


        private double PercentDifferent(double range, double reference, double value)
        {
            return 100 * ((value - reference) / range);
        }
    }

    [Serializable()]
    public class ResultSet
    {
        public static bool Save(ResultSet toSave, out string error)
        {
            error = "";

            if (!CheckForResultDirectory())
            {
                error = string.Format("The results directory ({0}) does not exist and could not be created.", GetResultDirectoryPath());
                return false;
            }

            string fileName = Path.Combine(GetResultDirectoryPath(), DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".xml");

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ResultSet));
                StreamWriter output = new StreamWriter(fileName);
                serializer.Serialize(output, toSave);
                output.Close();
                return true;
            }
            catch (Exception ex)
            {
                error = string.Format("An error occurred whilst writing to the file '{0}': {1}", fileName, ex.Message);
                return false;
            }
        }

        public static bool Load(string path, out ResultSet loaded, out string error)
        {
            error = "";
            loaded = null;

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ResultSet));
                StreamReader reader = new StreamReader(path);
                loaded = serializer.Deserialize(reader) as ResultSet;
                reader.Close();
                return true;
            }
            catch (Exception ex)
            {
                error = string.Format("An error occurred whilst loading the file '{0}': {1}", path, ex.Message);
                return false;
            }
        }

        public static bool CheckForResultDirectory()
        {
            string resultDir = GetResultDirectoryPath();

            if (Directory.Exists(resultDir))
            {
                return true;
            }

            try
            {
                Directory.CreateDirectory(resultDir);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string GetResultDirectoryPath()
        {
            string exeDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return Path.Combine(exeDir, "results");
        }

        public ResultSet()
        {
            Results = new List<ResultItem>();
        }

        public ResultItem AddResult(int grayLevel)
        {
            ResultItem result = new ResultItem(grayLevel);
            Results.Add(result);

            return result;
        }

        public List<ResultItem> Results { get; set; }
    }
}
