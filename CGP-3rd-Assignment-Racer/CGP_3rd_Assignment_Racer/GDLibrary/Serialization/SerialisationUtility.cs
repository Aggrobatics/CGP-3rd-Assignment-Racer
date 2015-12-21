using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace GDLibrary
{
    //See https://msdn.microsoft.com/en-us/library/ms973893.aspx
    public class SerialisationUtility
    {
        public static void Save(object theObject, string path, string name, FileMode fileMode)
        {
           
            //open the file
            FileStream fStream = File.Open(path + name, fileMode);

            //create our own namespaces for the output
            XmlSerializerNamespaces nameSpace = new XmlSerializerNamespaces();
            //add an empty namespace and empty value
            nameSpace.Add("", "");
            //convert to XML
            XmlSerializer xmlSerial = new XmlSerializer(theObject.GetType());
            //write to stream
            xmlSerial.Serialize(fStream, theObject, nameSpace);
            //close the stream
            fStream.Close();
        }

        public static object Load(string path, string name, System.Type theType)
        {
            if (File.Exists(path + name))
            {
                //open for read
                FileStream fStream = File.Open(path + name, FileMode.OpenOrCreate, FileAccess.Read);

                //read the data from file
                XmlSerializer xmlSerial = new XmlSerializer(theType);
                object data = xmlSerial.Deserialize(fStream);

                //housekeeping
                fStream.Close();
                return data;
            }

            return null; //doesnt exist
        }
    }
}
