using System.Diagnostics;
using System.Reflection;

namespace ACO.Util
{
    public class ErrorRaiserUnity : UnityEngine.MonoBehaviour, ACO.Util.Base.IErrorRaiser
    {
        public void Raise(string evt)
        {
            //Get a StackTrace object for the exception
            StackTrace st = new StackTrace(true);
            //Get the first stack frame
            StackFrame frame = st.GetFrame(2);
            //Get the file name
            string fileName = frame.GetFileName();
            //Get the method name
            string methodName = frame.GetMethod().Name;
            //Get the line number from the stack frame
            int line = frame.GetFileLineNumber();
            //Get the column number
            int col = frame.GetFileColumnNumber();
            //Debug.LogError(evt);
            UnityEngine.Debug.LogError(System.String.Format("filename: {0}, methodName: {1}, line: {2}, col: {3}", fileName, methodName, line, col), this);

            //OpenFile();
        }
        private static void OpenFile()
        {
            //Assembly assembly = Assembly.GetAssembly(typeof(UnityEditor.SceneView));
            //System.Type type = assembly.GetType("UnityEditorInternal.InternalEditorUtility");
            //if (type == null)
            //{
            //    UnityEngine.Debug.Log("Failed to open source file");
            //    return;
            //}
            //string[] stackFrames = System.Environment.StackTrace.Split(new char[] { '\n' });
            //string callingFrame = stackFrames[4];
            //string[] splitLog = callingFrame.Split(':');
            //char drive = splitLog[0][splitLog[0].Length - 1];
            //string filePath = splitLog[1];
            //splitLog[2] = splitLog[2].TrimStartString(
            //    "line ");
            //int lineNumber = int.Parse(splitLog[2].GetBeforeNextChar(
            //    '\n'));
            //string fullDrive = drive + ":" + filePath;
            ////Debug.Log(@fullDrive + " line #" + lineNumber );
            //MethodInfo method = type.GetMethod("OpenFileAtLineExternal");
            //method.Invoke(method, new object[] { @fullDrive, lineNumber });
        }
    }
}