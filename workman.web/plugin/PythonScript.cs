 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
namespace Workman.Plugin
{

    public class PythonInstance
    {
        private ScriptEngine Engine;
        private ScriptScope PyScope;
        private ScriptSource PySource;
        private CompiledCode PyCompiled;
        private object PythonClass;

        /// <summary>
        /// 实例化一个Python解释器
        /// </summary>
        /// <param name="code_file">脚本文件</param>

        public PythonInstance(string code_file, IList<string> otherLibPaths = null)
        {
            //creating engine and stuff
            Engine = Python.CreateEngine();
            PyScope = Engine.CreateScope();
            ICollection<string> Paths = Engine.GetSearchPaths();
            Paths.Add(AppDomain.CurrentDomain.BaseDirectory + "Plugins/Scripts/Lib");
            Paths.Add(AppDomain.CurrentDomain.BaseDirectory + "Plugins/Scripts/Lib/site-packages");
            if (otherLibPaths != null)
            {
                foreach (var p in otherLibPaths)
                {
                    Paths.Add(p);
                }
            }
            Engine.SetSearchPaths(Paths);
            PyScope.Engine.ImportModule("sys");
            PyScope.Engine.ImportModule("clr");
            //PyScope.Engine.ImportModule("controller");
            //loading and compiling code
            if (code_file.EndsWith(".py", StringComparison.CurrentCultureIgnoreCase))
            {

                PySource = Engine.CreateScriptSourceFromFile(code_file, System.Text.Encoding.UTF8, Microsoft.Scripting.SourceCodeKind.Statements);
            }
            else //直接代码
            {
                PySource = Engine.CreateScriptSourceFromString(System.IO.File.ReadAllText(code_file), Microsoft.Scripting.SourceCodeKind.Statements);
            }


            // PySource = Engine.CreateScriptSourceFromString(  System.IO.File.ReadAllText(code_file), code_file,Microsoft.Scripting.SourceCodeKind.Statements);
            try
            {
                PyCompiled = PySource.Compile();
                //System.IO.File.WriteAllBytes(code_file + ".pyc", PyCompiled);
            }
            catch (Microsoft.Scripting.SyntaxErrorException E)
            {
                throw new Exception("文件：" + E.SourcePath + "\r\n行：" + E.Line.ToString() + ",列：" + E.Column.ToString());

            }

        }
        /// <summary>
        /// 运行脚本
        /// </summary>
        /// <param name="className">要实例化的PYTHON类</param>
        /// <throws>抛出可能的无法加载模块错误</throws>
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
        /// <summary>
        /// 实例化一个PYTHON类，同时修改引擎默认操作类
        /// </summary>
        /// <param name="className"></param>
        /// <returns></returns>
        public object GetClassInstance(string className)
        {
            return PythonClass = Engine.Operations.Invoke(PyScope.GetVariable(className));
        }
        /// <summary>
        /// 设置一个变量
        /// </summary>
        /// <param name="variable">变量名</param>
        /// <param name="value">变量值</param>
        public void SetVariable(string variable, dynamic value)
        {
            PyScope.SetVariable(variable, value);
        }
        /// <summary>
        /// 获取一个变量值
        /// </summary>
        /// <param name="variable">变量名称</param>
        /// <returns>变量值</returns>
        public dynamic GetVariable(string variable)
        {
            return PyScope.GetVariable(variable);
        }
        /// <summary>
        /// 调用一个默认类的方法
        /// </summary>
        /// <param name="method">方法名</param>
        /// <param name="arguments">参数列表</param>
        public void CallMethod(string method, params dynamic[] arguments)
        {

            Engine.Operations.InvokeMember(PythonClass, method, arguments);
        }
        /// <summary>
        /// 调用一个默认类的函数
        /// </summary>
        /// <param name="method">函数名称</param>
        /// <param name="arguments">参数列表</param>
        /// <returns>函数返回值</returns>
        public dynamic CallFunction(string method, params dynamic[] arguments)
        {
            return Engine.Operations.InvokeMember(PythonClass, method, arguments);
        }

    }

}