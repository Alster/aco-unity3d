using UnityEditor;
using System;
using System.Collections.Generic;
using System.IO;
using ProtoBuf;

namespace ACO.Edt.Net
{
    public partial class ProtoTools : Editor
    {
        [MenuItem(Edt.com.name + "/" + com.name + "/Update .proto")]
        static void UpdateProto()
        {
            /*person = new proto.ProfileData.ProfileData();
            ProtobufSave();
            person.diamonds = 999999;
            Debug.Log(person.diamonds);
            ProtobufLoad();
            Debug.Log(person.diamonds);*/

            ClearDirectory("Assets/proto");

            string cmdFile = "cd proto\n";
            string logOut = "Proto updated:\n";

            List<string> list = new List<string>();
            DirSearch(ref list, "proto");
            string currentPath = "";
            string cdUps = "";
            foreach (var e in list)
            {
                if (currentPath != Path.GetDirectoryName(e))
                {
                    //cmdFile += "cd ";
                    //foreach (var c in currentPath.Split(Path.DirectorySeparatorChar))
                    //{
                    //    cmdFile += "../";
                    //}
                    //cmdFile += "\n";
                    currentPath = Path.GetDirectoryName(e);
                    //cmdFile += "cd " + currentPath + "\n";
                }
                cdUps = "";
                //foreach (var c in currentPath.Split(Path.DirectorySeparatorChar))
                //{
                //    cdUps += "../";
                //}
                //UnityEngine.Debug.Log(currentPath);
                //UnityEngine.Debug.Log(e);
                if (Path.GetExtension(e) != ".proto")
                {
                    continue;
                }
                string nameSpace = Path.GetFileNameWithoutExtension(e);
                string[] currentPathSplitted = currentPath.Split(Path.DirectorySeparatorChar);
                if (currentPathSplitted.Length > 0)
                {
                    nameSpace = String.Join(".", currentPathSplitted) + "." + nameSpace;
                }
                nameSpace = "-ns:" + nameSpace;
                nameSpace = "";
                logOut += e + "\n";
                Directory.CreateDirectory(Path.Combine("Assets/", currentPath));
                cmdFile += String.Format("call protogen -i:{1} -o:{3}../Assets/{0}.cs {2}\n", e, CutPathRoot(e), nameSpace, cdUps);
            }
            cmdFile += "pause\ndel %0";

            list.Clear();

            string cmdFileName = "proto.build.bat";
            File.WriteAllText(cmdFileName, cmdFile);
            System.Diagnostics.Process.Start(cmdFileName);
            UnityEngine.Debug.Log(logOut);
        }

        static void DirSearch(ref List<string> list, string sDir)
        {
            try
            {
                foreach (string f in Directory.GetFiles(sDir))
                {
                    //list.Add(CutPathRoot(f));
                    list.Add(f);
                }
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    //UnityEngine.Debug.Log(d);
                    DirSearch(ref list, d);
                }
            }
            catch (System.Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }
        }
        static void ClearDirectory(string path)
        {
            System.IO.DirectoryInfo di = new DirectoryInfo(path);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }
        }
        static string CutPathRoot(string path)
        {
            string[] splitted = path.Split(Path.DirectorySeparatorChar);
            string result = "";
            for (int i = 0; i < splitted.Length; i++)
            {
                if (i == 0)
                {
                    continue;
                }
                else {
                    result += splitted[i];
                    if (i < splitted.Length - 1)
                    {
                        result += Path.DirectorySeparatorChar;
                    }
                }
            }
            return result;
        }

        //public proto.ProfileData.ProfileData person;
        //[ProtoContract]
        //[Serializable]
        //public class Person
        //{
        //    [ProtoMember(1)]
        //    public int id;
        //    [ProtoMember(2)]
        //    public string name;
        //    [ProtoMember(3)]
        //    public Address address;
        //    [ProtoMember(4)]
        //    public List<Address> addressList;
        //    [ProtoMember(5)]
        //    public Dictionary<string, Address> addressDict;
        //}
        //[ProtoContract]
        //[Serializable]
        //public class Address
        //{
        //    [ProtoMember(1)]
        //    public string line1;
        //    [ProtoMember(2)]
        //    public string line2;
        //}
        //public string protobufSavePlace = "";
        //public void ProtobufSave()
        //{
        //    using (var file = File.Create("person.bin"))
        //    {
        //        Serializer.Serialize<proto.ProfileData.ProfileData>(file, person);
        //    }
        //    using(var stream = new System.IO.MemoryStream()){
        // 	ProtoBuf.Serializer.Serialize<proto.ProfileData.ProfileData>(stream, person);
        // 	byte[] binary = stream.ToArray();
        // 	protobufSavePlace = System.Convert.ToBase64String(binary);
        // }
        //}
        //public void ProtobufLoad()
        //{
        //    using (var file = File.OpenRead("person.bin"))
        //    {
        //        person = Serializer.Deserialize<proto.ProfileData.ProfileData>(file);
        //    }
        //}
    }
}