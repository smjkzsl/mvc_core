 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
namespace workman.web.plugin
{
   
        public class PythonScript
        {
            private ScriptEngine Engine;
            private ScriptScope PyScope;
            private ScriptSource PySource;
            private CompiledCode PyCompiled;
            private object PythonClass;

            
            public void init(string code=""){
                //creating engine and stuff
                Engine = Python.CreateEngine();
                PyScope = Engine.CreateScope();
                ICollection<string> Paths = Engine.GetSearchPaths();
                Paths.Add(AppDomain.CurrentDomain.BaseDirectory + "Plugins/Scripts/Lib");
                Paths.Add(AppDomain.CurrentDomain.BaseDirectory + "Plugins/Scripts/Lib/site-packages");
                Engine.SetSearchPaths(Paths);
                PyScope.Engine.ImportModule("sys");
                PyScope.Engine.ImportModule("clr");
                if(!code.Equals("")){
                    //PySource = Engine.CreateScriptSourceFromFile(code_file, System.Text.Encoding.UTF8);
                    PySource = Engine.CreateScriptSourceFromString(  code   ,Microsoft.Scripting.SourceCodeKind.Statements);
                    try
                    {
                        PyCompiled = PySource.Compile();
                        //System.IO.File.WriteAllBytes(code_file + ".pyc", PyCompiled);
                    }
                    catch (Microsoft.Scripting.SyntaxErrorException E)
                    {
                        throw new Exception("文件：" + E.SourcePath + "\n行：" + E.Line.ToString() + ",列：" + E.Column.ToString());

                    }
                }
            }
            public PythonScript(){

            }
            /// <summary>
            /// 实例化一个Python解释器
            /// </summary>
            /// <param name="code_file">脚本文件</param>
            public PythonScript(string code_file)
            {
               this.init();
                //PyScope.Engine.ImportModule("controller");
                //loading and compiling code
                PySource = Engine.CreateScriptSourceFromFile(code_file, System.Text.Encoding.UTF8);
                // PySource = Engine.CreateScriptSourceFromString(  System.IO.File.ReadAllText(code_file), code_file,Microsoft.Scripting.SourceCodeKind.Statements);
                try
                {
                    PyCompiled = PySource.Compile();
                    //System.IO.File.WriteAllBytes(code_file + ".pyc", PyCompiled);
                }
                catch (Microsoft.Scripting.SyntaxErrorException E)
                {
                    throw new Exception("文件：" + E.SourcePath + "\n行：" + E.Line.ToString() + ",列：" + E.Column.ToString());

                }

            }
            
            public void run(string className = "")
            {
                try
                {
                    //now executing this code (the code should contain a class)
                    PyCompiled.Execute(PyScope);
                }
                catch (Exception ex)
                {

                    string strMessage = ex.Message + "\r\n\r\n--------IronPython 模块搜索路径:\r\n";
                    foreach (var v in Engine.GetSearchPaths())
                        strMessage += v + "\r\n";
                    strMessage += "--------\r\n";
                    strMessage += "若一些模块无法加载 确认此模块在搜索路劲下 若不再可以尝试一下方案\r\n";
                    strMessage += "    1.添加系统环境变量名[PYTHON_IMPORT_PATH] 并在其中添加模块路径\r\n";
                    strMessage += "    2.在脚本开头写入以下代码\r\n";
                    strMessage += "        import sys\r\n";
                    strMessage += "        sys.path.append('yourpath')";
                    throw new Exception(strMessage, ex);
                }


                //now creating an object that could be used to access the stuff inside a python script
                if (!string.IsNullOrEmpty(className))
                {
                    PythonClass = Engine.Operations.Invoke(PyScope.GetVariable(className));
                }
            }
            public object GetClassInstance(string className)
            {
                return PythonClass = Engine.Operations.Invoke(PyScope.GetVariable(className));
            }
            public void SetVariable(string variable, dynamic value)
            {
                PyScope.SetVariable(variable, value);
            }

            public dynamic GetVariable(string variable)
            {
                return PyScope.GetVariable(variable);
            }

            public void CallMethod(string method, params dynamic[] arguments)
            {

                Engine.Operations.InvokeMember(PythonClass, method, arguments);
            }

            public dynamic CallFunction(string method, params dynamic[] arguments)
            {
                return Engine.Operations.InvokeMember(PythonClass, method, arguments);
            }

        }
    
}