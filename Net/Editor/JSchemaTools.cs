//using UnityEditor;
//using System.IO;
//using Newtonsoft.Json.Schema;
//using System.Linq;
//using System.Reflection;
//using System.Collections.Generic;

//namespace ACO.Edt.Net
//{
//    public class JSchemaTools : Editor
//    {
//        public static System.Type entryType = typeof(Schema.TestClass);
//        [MenuItem(Edt.com.name + "/" + com.name + "/Update JSON schema")]
//        [UnityEditor.Callbacks.DidReloadScripts]
//        public static void UpdateSchemas()
//        {
//            JsonSchemaGenerator generator = new JsonSchemaGenerator();
//            string basePath = Path.Combine("..", "cas-server");
//            ClearDirectory(Path.Combine(basePath, entryType.Namespace));
//            Assembly assembly = Assembly.GetAssembly(entryType);

//            Dictionary<string, string> schemas = new Dictionary<string, string>();

//            var q = from t in assembly.GetTypes()
//                    where t.IsClass
//                    select t;

//            q.ToList().ForEach(t => {
//                if (t.Namespace == null)
//                {
//                    return;
//                }
//                string[] names = t.Namespace.Split('.');
//                if (names.Length > 0 && names[0] == entryType.Namespace)
//                {
//                    JsonSchema schema = generator.Generate(t);
//                    string path = basePath;
//                    foreach (var n in names)
//                    {
//                        path = Path.Combine(path, n);
//                    }
//                    Directory.CreateDirectory(path);
//                    File.WriteAllText(Path.Combine(path, t.Name + ".json"), schema.ToString());
//                    schemas[t.Namespace + "." + t.Name] = schema.ToString();
//                    //Debug.Log(t.Name + " in " + t.Namespace);
//                }
//            });


//            List<string> list = new List<string>();
//            DirSearch(ref list, Path.Combine(basePath, entryType.Namespace));
//            string currentPath = "";
//            string jsFile = "var JsonSchema = require('./../src/JsonSchema')\n";
//            //jsFile += "var "+entryType.Namespace+" = {}\n";
//            List<string> definedNamespaces = new List<string>();
//            foreach (var e in list)
//            {
//                if (currentPath != Path.GetDirectoryName(e))
//                {
//                    currentPath = Path.GetDirectoryName(e);
//                }
//                if (Path.GetExtension(e) != ".json")
//                {
//                    continue;
//                }
//                string[] currentPathSplitted = currentPath.Split(Path.DirectorySeparatorChar);
//                currentPathSplitted = currentPathSplitted.Skip(2).ToArray();

//                string nameSpace = System.String.Join(".", currentPathSplitted);
//                if (!definedNamespaces.Contains(nameSpace))
//                {
//                    jsFile += nameSpace + " = {}\n";
//                    definedNamespaces.Add(nameSpace);
//                }
//                string schemaName = nameSpace + "." + Path.GetFileNameWithoutExtension(e);
//                jsFile += System.String.Format("{0} = new JsonSchema.SchemaEntity({1})\n",
//                    schemaName, schemas[schemaName]);
//            }
//            jsFile += "module.exports = " + entryType.Namespace;
//            File.WriteAllText(Path.Combine(Path.Combine(basePath, entryType.Namespace), "schema.js"), jsFile);
//        }

//        static void DirSearch(ref List<string> list, string sDir)
//        {
//            try
//            {
//                foreach (string f in Directory.GetFiles(sDir))
//                {
//                    list.Add(f);
//                }
//                foreach (string d in Directory.GetDirectories(sDir))
//                {
//                    DirSearch(ref list, d);
//                }
//            }
//            catch (System.Exception excpt)
//            {
//                UnityEngine.Debug.Log(excpt.Message);
//            }
//        }
//        static void ClearDirectory(string path)
//        {
//            DirectoryInfo di = new DirectoryInfo(path);

//            foreach (FileInfo file in di.GetFiles())
//            {
//                file.Delete();
//            }
//            foreach (DirectoryInfo dir in di.GetDirectories())
//            {
//                dir.Delete(true);
//            }
//        }
//    }
//}